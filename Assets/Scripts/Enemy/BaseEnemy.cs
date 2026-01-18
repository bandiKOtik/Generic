using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected float Health { get; set; }
    protected float Damage { get; set; }
}

[Serializable]
public abstract class EnemySettings
{
    public abstract EnemyConfigBase BaseConfig { get; }
}

[Serializable]
public abstract class EnemyConfigBase
{
    [field: SerializeField, Min(0)] public float Health { get; set; }
    [field: SerializeField, Min(0)] public float Damage { get; set; }
}

[Serializable]
public abstract class EnemySettings<T> : EnemySettings
    where T : EnemyConfigBase, new()
{
    [SerializeField] protected T _config = new();
    public T Config => _config;
    public override EnemyConfigBase BaseConfig => _config;
}