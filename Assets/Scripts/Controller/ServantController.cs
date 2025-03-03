using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class ServantController : NetworkBehaviour
{
    public int id;
    private GameObject selectedObject;
    private NavMeshAgent agent;
    private AbstractServantUnit servant;
    
    private GameObject Target;
    public int Team { get => servant.Team.Value; set => servant.Team.Value = value; }

    float rotateVelocity;
    float rotateSpeedMovement = .1f;
    public Animator anim;
    float motionSmoothTime = .1f;

    public enum State
    {
        IDLE,//¾²Ö¹
        MOVING,//ÒÆ¶¯
        ATTACKING,//¹¥»÷
    }
    private State _servantState;
    public State ServantState
    {
        set
        {
            _servantState = value;
            anim.SetBool("IsAttacking", _servantState == State.ATTACKING);
        }
        get
        {
            return _servantState;
        }
    }

    override public void OnNetworkSpawn()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        servant = gameObject.GetComponent<AbstractServantUnit>();
        ServantState = State.IDLE;
    }
    void Update()
    {
        if (servant.IsDead) return;
        Animation();
        MoveAndAttack();
    }
    private void Animation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        ServantState = (speed == 0 && ServantState != State.ATTACKING) ? State.IDLE : State.MOVING;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
    private void MoveAndAttack()
    {
        if(Target != null)
        {
            AttactUnit(Target);
        }
    }

    [ClientRpc]
    public void SetTargetClientRpc(ulong targetNetworkObjectId)
    {
        if(NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(targetNetworkObjectId, out NetworkObject target))
        {
            this.Target = target.gameObject;
        }
    }

    IEnumerator BasicAttack()
    {
        ServantState = State.ATTACKING;
        yield return null;
    }
    [ClientRpc]
    public void MoveToDesClientRpc(Vector3 Des, float Distance = 0)
    {
        if(Distance == 0) this.Target = null;
        agent.SetDestination(Des);
        agent.stoppingDistance = Distance;
        Quaternion rotationToLookAt = Quaternion.LookRotation(Des - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
            ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
    private void AttactUnit(GameObject Unit)
    {
        if(Unit.GetComponent<AbstractUnit>().IsDead)
        {
            Target = null;
            return;
        }
        if(Vector3.Distance(transform.position, Unit.transform.position) > servant.ServantData.AttackRange)
        {
            MoveToDesClientRpc(Unit.transform.position, servant.ServantData.AttackRange);
            return;
        }
        if(ServantState != State.ATTACKING)
        {
            Quaternion rotationToLookAt = Quaternion.LookRotation(Unit.transform.position - transform.position);
            transform.eulerAngles = new Vector3(0, rotationToLookAt.eulerAngles.y, 0);
            agent.velocity = Vector3.zero;
            StartCoroutine(nameof(BasicAttack));
        }
    }

    public void DealBasicAttack()
    {
        if (Target == null) return;
        Damage damage = new Damage();
        damage.DamageAmount = servant.ServantData.WeaponDamage + servant.WeaponBouns;
        damage.DamageType = Damage.DamageTypeEnum.PhysicalDamage;
        damage.DamageDealtType = 
            servant.ServantData.AttactType == ServantScriptable.AttactTypeEnum.Melee ? 
            Damage.DamageDealtTypeEnum.Melee : 
            Damage.DamageDealtTypeEnum.Projectile;
        damage.DamageSender = servant;
        damage.IsDogeAble = true;
        damage.DamageTaker = Target.GetComponent<AbstractUnit>();
        EventManager.Instance.damageEvent.DealDamageServerRpc(damage);
    }
}
