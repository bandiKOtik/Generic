using System;
using UnityEngine;

[Serializable]
public abstract class BaseEnemyConfig
{
    [field: SerializeField, Min(0)] public float Health { get; set; }
    [field: SerializeField, Min(0)] public float Damage { get; set; }
}
