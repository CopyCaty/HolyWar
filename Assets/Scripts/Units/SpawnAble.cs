using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class SpawnAble : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 SpawnPosition;
    public GameObject TargetObject;
    public Vector3 TargetPosition;
    public float Speed;
    public float LifeTime;
    private float _timer;
    public virtual void Initialize(Vector3 spawnPosition, Vector3 targetPosition, float speed, float lifeTime)
    {
        SpawnPosition = spawnPosition;
        TargetPosition = targetPosition;
        Speed = speed;
        LifeTime = lifeTime;
        transform.position = SpawnPosition;
        _timer = 0f;
    }
    protected virtual void Update()
    {
        UpdateLifeTime();
    }

    protected virtual void UpdateLifeTime() {
        _timer += Time.deltaTime;
        if (_timer >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
