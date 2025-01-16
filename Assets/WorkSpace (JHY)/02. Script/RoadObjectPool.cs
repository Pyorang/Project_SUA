using System.Collections.Generic;
using UnityEngine;

public class RoadObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class RoadPool
    {
        public string poolName; 
        public GameObject prefab; 
        public int poolSize; 
    }

    // �ν����Ϳ��� ������ Pool ����
    public RoadPool[] pools;

    // ������ ���ӿ�����Ʈ���� ������ Dictionary
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    // �� ũ�� ����
    public int mapSizeX = 10;
    public int mapSizeZ = 10;

    // �� �������� ���� ũ��
    public float blockSpacingX = 30f;
    public float blockSpacingZ = 30f;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // �� Pool���� Queue�� ���� ������Ʈ�� �̸� ����
        foreach (RoadPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                // ������Ʈ Ǯ�� ���� �ʱ� ����
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolName, objectPool);
        }
    }

    private void Start()
    {
        //�����ϰ� ���� ����
        GenerateRoads();
    }

    public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"Ǯ�� '{poolName}'��(��) �����ϴ�. Ǯ �̸��� Ȯ���ϼ���.");
            return null;
        }

        // ť���� ������ ���� �� Ȱ��ȭ
        GameObject objectToSpawn = poolDictionary[poolName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // ��Ȱ���� ���� �ٽ� ť�� ����
        poolDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private void GenerateRoads()
    {
        // poolDictionary�� �ִ� Ǯ���� �̸��� ����Ʈ�� ����
        List<string> poolNames = new List<string>(poolDictionary.Keys);

        // (0,0)���� (n,n)���� �� ĭ�� ���� ���� ����
        for (int z = 0; z < mapSizeZ; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                // ����� ���� Ǯ�� �������� ����
                int randomIndex = Random.Range(0, poolNames.Count);
                string randomPoolName = poolNames[randomIndex];

                // **��ǥ ����**: ������ �������� (m, ?, m)�̹Ƿ� ��ġ ���ݵ� m��ŭ��
                Vector3 spawnPosition = new Vector3(
                    x * blockSpacingX,
                    1f,
                    z * blockSpacingZ
                );

                // ȸ���� �ϴ� ���� (�ʿ� �� Random.rotation ������ ����ȭ ����)
                Quaternion spawnRotation = Quaternion.identity;

                // ������Ʈ Ǯ���� ������ ��ġ
                SpawnFromPool(randomPoolName, spawnPosition, spawnRotation);
            }
        }
    }
}
