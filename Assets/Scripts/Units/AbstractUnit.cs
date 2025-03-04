using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microlight.MicroBar;
using Cinemachine;
using Unity.Netcode;

public abstract class AbstractUnit : NetworkBehaviour, DamageAble
{
    public enum UnitTypeEnum
    {
        Servant,
        Building,
        Minion
    }
    public UnitTypeEnum UnitType;
    public NetworkVariable<int> Team;
    public float MaxHP;
    public float HP;
    public float Armor;
    public float Shield;
    public bool IsDead;
    public bool IsVisible;
    public bool IsImortal;

    public GameObject HealthBarPrefab;
    public GameObject HealthBarInstance;

    public void TakeDamage(Damage damage)
    {
        HP -= damage.DamageAmount;
        if (HP <= 0) IsDead = true;
        Debug.Log(HP);
    }

    override public void OnNetworkSpawn()
    {
        InitHealthBar();
    }

    private void LateUpdate()
    {
        UpdateHealthBar();
    }

    private void OnMouseEnter()
    {
        if (IsDead) return;
        if(GlobalManager.Instance.playerController.playerTeam == Team.Value)
        {
            GetComponentInChildren<Outline>().OutlineColor = Color.green;
        }else
        {
            GetComponent<Outline>().OutlineColor = Color.red;
        }
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void UpdateHealthBar()
    {
        HealthBarInstance.transform.LookAt(
            HealthBarInstance.transform.position + Camera.main.transform.rotation * Vector3.forward
            , Camera.main.transform.rotation * Vector3.up);
        HealthBarInstance.GetComponentInChildren<MicroBar>().UpdateBar(this.HP);
        return;
    }

    public void InitHealthBar()
    {
        HealthBarInstance = Instantiate(HealthBarPrefab, transform);
        Debug.Log(HealthBarInstance.GetComponentInChildren<MicroBar>());
        HealthBarInstance.GetComponentInChildren<MicroBar>().Initialize(this.MaxHP);
        HealthBarInstance.GetComponentInChildren<MicroBar>().UpdateBar(this.HP);

    }
}
