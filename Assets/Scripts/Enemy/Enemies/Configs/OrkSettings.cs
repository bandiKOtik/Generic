using System;
using UnityEngine;

[Serializable]
public class OrkSettings : EnemySettings<OrkSettings.OrkConfig>
{
    [Serializable]
    public class OrkConfig : EnemyConfigBase
    {
        [field: SerializeField, Min(0)] public float Strength { get; set; }
    }
}