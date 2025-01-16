using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // 새 도로를 붙이기 위한 변수
    float roadPosition;
    float roadLength;
    float roadLotation;

    // 도로 패턴를 만들기 위한 변수
    int roadTypeCount;
    int roadCount = 20;
    int[] roadPattern;

    // 도로의 종류 정리
    [System.Serializable]
    public class RoadType
    {
        public string roadName;
        public GameObject roadPrefab;
    }

    [Header("Road Types")]
    [SerializeField] private RoadType[] roadTypes;

    void Start()
    {
        roadTypeCount = roadTypes.Length;
        Debug.Log($"도로의 종류: {roadTypeCount}개");
        GenerateRandomPattern();
    }

    void Update()
    {
        
    }

    // 랜덤 패턴 생성하기
    void GenerateRandomPattern()
    {
        roadPattern = new int[roadCount];
        for (int i = 0; i < roadCount; i++)
        {
            roadPattern[i] = Random.Range(0, roadTypeCount);
        }

        string patternStr = "도로 패턴: ";
        foreach (var type in roadPattern)
        {
            patternStr += type + " ";
        }
        Debug.Log(patternStr);

        AttachRoads();
    }

    // 새 도로의 정보 가져오기
    void GetNewRoadInfo()
    {
        /*
        for (int i = 0; i < roadCount; i++)
        {
            roadTypes[roadPattern[i]].roadPrefab.transform.position = 
        }
        */
    }

    // 도로를 붙여서 도로 패턴을 구현하기
    void AttachRoads()
    {
        Vector3 currentRoadPosition = new Vector3(0, 0, 0);
        // Vector3 offset = new 
        for (int i = 0; i < roadCount; i++)
        {
            Quaternion currentRoadRotation = roadTypes[roadPattern[i]].roadPrefab.transform.rotation;
            
            Instantiate(roadTypes[roadPattern[i]].roadPrefab, currentRoadPosition , currentRoadRotation);
        }
    }
}
