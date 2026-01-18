using System;
using UnityEngine;

public class Elf : BaseEnemy
{
    public float Mana { get; private set; }

    public void Initialize(ElfSettings settings)
    {
        if (settings?.Config != null)
        {
            Health = settings.Config.Health;
            Damage = settings.Config.Damage;
            Mana = settings.Config.Mana;

            Debug.Log($"{gameObject.name}: HP: {Health}, DMG: {Damage}, STR: {Mana}.");
        }
    }
}

[Serializable]
public class ElfSettings : EnemySettings<ElfSettings.ElfConfig>
{
    [Serializable]
    public class ElfConfig : EnemyConfigBase
    {
        [field: SerializeField, Min(0)] public float Mana { get; set; }
    }
}