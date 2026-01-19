using System;
using UnityEngine;

[Serializable]
public class ElfSettings : EnemySettings<ElfSettings.ElfConfig>
{
    [Serializable]
    public class ElfConfig : EnemyConfigBase
    {
        [field: SerializeField, Min(0)] public float Mana { get; set; }
    }
}