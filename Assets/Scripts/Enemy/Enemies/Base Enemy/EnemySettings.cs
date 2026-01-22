using System;
using UnityEngine;

[Serializable]
public abstract class EnemySettings<TConfig> : BaseEnemySettings where TConfig : BaseEnemyConfig, new()
{
    [SerializeField] protected TConfig _config = new();
    public TConfig Config => _config;
    public override BaseEnemyConfig BaseConfig => _config;
}