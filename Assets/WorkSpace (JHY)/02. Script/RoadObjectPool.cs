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

    // 인스펙터에서 설정할 Pool 정보
    public RoadPool[] pools;

    // 생성된 게임오브젝트들을 관리할 Dictionary
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    // 맵 크기 설정
    public int mapSizeX = 10;
    public int mapSizeZ = 10;

    // 각 프리팹의 공간 크기
    public float blockSpacingX = 30f;
    public float blockSpacingZ = 30f;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 각 Pool별로 Queue를 만들어서 오브젝트를 미리 생성
        foreach (RoadPool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                // 오브젝트 풀에 들어가는 초기 상태
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolName, objectPool);
        }
    }

    private void Start()
    {
        //랜덤하게 도로 생성
        GenerateRoads();
    }

    public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"풀에 '{poolName}'이(가) 없습니다. 풀 이름을 확인하세요.");
            return null;
        }

        // 큐에서 꺼내서 세팅 및 활성화
        GameObject objectToSpawn = poolDictionary[poolName].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // 재활용을 위해 다시 큐에 삽입
        poolDictionary[poolName].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private void GenerateRoads()
    {
        // poolDictionary에 있는 풀들의 이름을 리스트로 추출
        List<string> poolNames = new List<string>(poolDictionary.Keys);

        // (0,0)부터 (n,n)까지 한 칸씩 랜덤 도로 생성
        for (int z = 0; z < mapSizeZ; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                // 사용할 도로 풀을 랜덤으로 선택
                int randomIndex = Random.Range(0, poolNames.Count);
                string randomPoolName = poolNames[randomIndex];

                // **좌표 설정**: 프리팹 스케일이 (m, ?, m)이므로 배치 간격도 m만큼씩
                Vector3 spawnPosition = new Vector3(
                    x * blockSpacingX,
                    1f,
                    z * blockSpacingZ
                );

                // 회전은 일단 고정 (필요 시 Random.rotation 등으로 랜덤화 가능)
                Quaternion spawnRotation = Quaternion.identity;

                // 오브젝트 풀에서 꺼내어 배치
                SpawnFromPool(randomPoolName, spawnPosition, spawnRotation);
            }
        }
    }
}
