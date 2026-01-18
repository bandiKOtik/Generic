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
public abstract class EnemySettings<TConfig> : EnemySettings where TConfig : EnemyConfigBase, new()
{
    [SerializeField] protected TConfig _config = new();
    public TConfig Config => _config;
    public override EnemyConfigBase BaseConfig => _config;
}