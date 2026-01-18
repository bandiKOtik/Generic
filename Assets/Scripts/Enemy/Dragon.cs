using System;
using UnityEngine;

public class Dragon : BaseEnemy
{
    public float FireBreath { get; private set; }

    public void Initialize(DragonSettings settings)
    {
        if (settings?.Config != null)
        {
            Health = settings.Config.Health;
            Damage = settings.Config.Damage;
            FireBreath = settings.Config.FireBreath;

            Debug.Log($"{gameObject.name}: HP: {Health}, DMG: {Damage}, STR: {FireBreath}.");
        }
    }
}

[Serializable]
public class DragonSettings : EnemySettings<DragonSettings.DragonConfig>
{
    [Serializable]
    public class DragonConfig : EnemyConfigBase
    {
        [field: SerializeField, Min(0)] public float FireBreath { get; set; }
    }
}