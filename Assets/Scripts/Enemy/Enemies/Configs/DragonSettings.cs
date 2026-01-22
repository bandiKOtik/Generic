using System;
using UnityEngine;

[Serializable]
public class DragonSettings : EnemySettings<DragonSettings.DragonConfig>
{
    [Serializable]
    public class DragonConfig : BaseEnemyConfig
    {
        [field: SerializeField, Min(0)] public float FireBreath { get; set; }
    }
}