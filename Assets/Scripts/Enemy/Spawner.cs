using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Ork _orkPrefab;
    [SerializeField] private Elf _elfPrefab;
    [SerializeField] private Dragon _dragonPrefab;

    public void Spawn(Vector3 coordinates, EnemySettings enemyConfig)
    {
        switch (enemyConfig)
        {
            case OrkSettings settings:
                var ork = Instantiate(_orkPrefab, coordinates, Quaternion.identity);
                ork.Initialize(settings);
                break;

            case ElfSettings settings:
                var elf = Instantiate(_elfPrefab, coordinates, Quaternion.identity);
                elf.Initialize(settings);
                break;

            case DragonSettings settings:
                var dragon = Instantiate(_dragonPrefab, coordinates, Quaternion.identity);
                dragon.Initialize(settings);
                break;

            default:
                throw new NotImplementedException("Such enemy class was not implemented in spawner.");
        }
    }

    public void SpawnRandom(Vector3 coordinates, EnemySettings[] enemySettingsArray)
    {
        int randomIndex = UnityEngine.Random.Range(0, enemySettingsArray.Length);

        Spawn(coordinates, enemySettingsArray[randomIndex]);
    }
}