using System;
using UnityEngine;

public class Ork : BaseEnemy
{
    public float Strength { get; private set; }

    public void Initialize(OrkSettings settings)
    {
        if (settings?.Config != null)
        {
            Health = settings.Config.Health;
            Damage = settings.Config.Damage;
            Strength = settings.Config.Strength;

            Debug.Log($"{gameObject.name}: HP: {Health}, DMG: {Damage}, STR: {Strength}.");
        }
    }
}

[Serializable]
public class OrkSettings : EnemySettings<OrkSettings.OrkConfig>
{
    [Serializable]
    public class OrkConfig : EnemyConfigBase
    {
        [field: SerializeField, Min(0)] public float Strength { get; set; }
    }
}