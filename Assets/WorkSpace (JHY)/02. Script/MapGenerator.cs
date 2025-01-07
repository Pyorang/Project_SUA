using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // �� ���θ� ���̱� ���� ����
    float roadPosition;
    float roadLength;
    float roadLotation;

    // ���� ���ϸ� ����� ���� ����
    int roadTypeCount;
    int roadCount = 20;
    int[] roadPattern;

    // ������ ���� ����
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
        Debug.Log($"������ ����: {roadTypeCount}��");
        GenerateRandomPattern();
    }

    void Update()
    {
        
    }

    // ���� ���� �����ϱ�
    void GenerateRandomPattern()
    {
        roadPattern = new int[roadCount];
        for (int i = 0; i < roadCount; i++)
        {
            roadPattern[i] = Random.Range(0, roadTypeCount);
        }

        string patternStr = "���� ����: ";
        foreach (var type in roadPattern)
        {
            patternStr += type + " ";
        }
        Debug.Log(patternStr);

        AttachRoads();
    }

    // �� ������ ���� ��������
    void GetNewRoadInfo()
    {
        /*
        for (int i = 0; i < roadCount; i++)
        {
            roadTypes[roadPattern[i]].roadPrefab.transform.position = 
        }
        */
    }

    // ���θ� �ٿ��� ���� ������ �����ϱ�
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
