using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SelectivePointSpawner : MonoBehaviour
{
    public static SelectivePointSpawner Instance { get; private set; }

    [SerializeField] private List<Vector3> spawnPositions = new List<Vector3>();
    [SerializeField] private List<GameObject> spawnPrefabs = new List<GameObject>();

    private int spawnNum = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RandomSpawn();
    }
    public void RandomSpawn()
    {
        int posCount = spawnPositions.Count;
        int prefabCount = spawnPrefabs.Count;

        int count = Mathf.Min(spawnNum, posCount, prefabCount);

        var posIndices = Enumerable.Range(0, posCount)
                                   .OrderBy(_ => Random.value)
                                   .Take(count)
                                   .ToList();

        var prefabIndices = Enumerable.Range(0, prefabCount)
                                      .OrderBy(_ => Random.value)
                                      .Take(count)
                                      .ToList();

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = spawnPositions[posIndices[i]];
            GameObject prefab = spawnPrefabs[prefabIndices[i]];

            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
