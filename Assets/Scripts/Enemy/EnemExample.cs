using System;
using UnityEngine;

namespace Minor
{
    public abstract class Enemy : MonoBehaviour
    {
        protected float _health;
        protected float _damage;
    }

    public class Ork : Enemy
    {
        private float _strength;

        public void Initialize(float health, float damage, float strength)
        {
            _health = health;
            _damage = damage;
            _strength = strength;
        }
    }

    [Serializable]
    public class OrkConfig : EnemyConfig
    {
        [field: SerializeField] float Strength { get; set; }
    }

    public class EnemyConfig
    {
        [field: SerializeField] float Health { get; set; }
        [field: SerializeField] float Damage { get; set; }
    }

    public class Example
    {
        [SerializeField] private OrkConfig[] configs;
    }

    public class Spawner
    {
        public Enemy SpawnEnemy(EnemyConfig config, Vector3 position)
        {
            switch (config)
            {
                case OrkConfig:
                    // spawn
                    // return
                    break;
                case null: // default
                    break;
            }

            return new Ork();
        }
    }
}