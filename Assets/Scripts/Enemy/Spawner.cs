using System;
using UnityEngine;

public class Spawner
{
    private Ork _orkPrefab;
    private Elf _elfPrefab;
    private Dragon _dragonPrefab;

    public Spawner(Ork orkPrefab, Elf elfPrefab, Dragon dragonPrefab)
    {
        _orkPrefab = orkPrefab;
        _elfPrefab = elfPrefab;
        _dragonPrefab = dragonPrefab;
    }

    public void Spawn(Vector3 coordinates, BaseEnemySettings enemyConfig)
    {
        switch (enemyConfig)
        {
            case OrkSettings settings:
                var ork = GameObject.Instantiate(_orkPrefab, coordinates, Quaternion.identity);
                ork.Initialize(settings);
                break;

            case ElfSettings settings:
                var elf = GameObject.Instantiate(_elfPrefab, coordinates, Quaternion.identity);
                elf.Initialize(settings);
                break;

            case DragonSettings settings:
                var dragon = GameObject.Instantiate(_dragonPrefab, coordinates, Quaternion.identity);
                dragon.Initialize(settings);
                break;

            default:
                throw new NotImplementedException("Such enemy class was not implemented in spawner.");
        }
    }

    public void SpawnRandom(Vector3 coordinates, BaseEnemySettings[] enemySettingsArray)
    {
        int randomIndex = UnityEngine.Random.Range(0, enemySettingsArray.Length);

        Spawn(coordinates, enemySettingsArray[randomIndex]);
    }
}