using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private OrkSettings[] orkSettings;
    [SerializeField] private ElfSettings[] elfSettings;
    [SerializeField] private DragonSettings[] dragonSettings;

    private void Start()
    {
        if (orkSettings.Length > 0)
            _spawner.SpawnRandom(new Vector3(-3, 1, 0), orkSettings);

        if (elfSettings.Length > 0)
            _spawner.SpawnRandom(new Vector3(0, 1, 0), elfSettings);

        if (dragonSettings.Length > 0)
            _spawner.SpawnRandom(new Vector3(3, 1, 0), dragonSettings);
    }
}