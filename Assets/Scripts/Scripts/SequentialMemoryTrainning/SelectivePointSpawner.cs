using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[System.Serializable]
public class SpawnObject
{
    public string name;
    public GameObject prefab;
    public Sprite sprite;
}

public class SelectivePointSpawner : MonoBehaviour
{
    public static SelectivePointSpawner Instance { get; private set; }

    [Header("������ ��ġ")]
    [SerializeField] private List<Vector3> spawnPositions = new();

    [Header("������ ��ü ���� ����Ʈ")]
    [SerializeField] private List<SpawnObject> spawnObjects = new();

    [SerializeField] private SequentialQuiz SequentialQuiz;
    private int spawnNum = 3;

    private List<SpawnObject> assignedObjects;
    public List<SpawnObject> spawnedInfos { get; private set; } = new();

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
        int objCount = spawnObjects.Count;
        int count = Mathf.Min(spawnNum, posCount, objCount);

        assignedObjects = Enumerable
            .Repeat<SpawnObject>(null, posCount)
            .ToList();

        // ��ġ �ε����� ������Ʈ �ε��� �������� ����
        var posIndices = Enumerable.Range(0, posCount)
                                   .OrderBy(_ => Random.value)
                                   .Take(count)
                                   .ToList();

        var objectIndices = Enumerable.Range(0, objCount)
                                      .OrderBy(_ => Random.value)
                                      .Take(count)
                                      .ToList();

        for (int i = 0; i < count; i++)
        {
            int posIdx = posIndices[i];
            SpawnObject so = spawnObjects[objectIndices[i]];

            assignedObjects[posIdx] = so;

            Instantiate(so.prefab, spawnPositions[posIdx], Quaternion.identity);
        }

        spawnedInfos.Clear();
        
        // ������Ʈ ������� ���
        for (int i = 0; i < assignedObjects.Count; i++)
        {
            if (assignedObjects[i] != null)
            {
                spawnedInfos.Add(assignedObjects[i]);
            }
        }

        for (int i = 0; i < spawnedInfos.Count; i++)
            Debug.Log($"Spawn order [{i}]: {spawnedInfos[i].name}");
    }
}
