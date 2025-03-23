using UnityEngine;

public class MinimapPathRenderer : MonoBehaviour
{
    public GameObject[] waypoints; // 경로를 구성할 오브젝트 배열
    public LineRenderer lineRenderer; // 경로를 그릴 LineRenderer

    void Start()
    {
        // 초기화
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints가 설정되지 않았습니다!");
            lineRenderer.positionCount = 0;
            return;
        }

        UpdatePath(); // 처음 시작할 때 경로 그리기
    }

    void Update()
    {
        // 실시간으로 오브젝트 위치가 변경될 경우 경로를 갱신
        UpdatePath();
    }

    void UpdatePath()
    {
        // 웨이포인트 위치를 가져와 LineRenderer에 설정
        lineRenderer.positionCount = waypoints.Length;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Vector3 position = waypoints[i].transform.position;
                position = AdjustToMinimap(position); // 미니맵에 맞게 조정
                lineRenderer.SetPosition(i, position);
            }
            else
            {
                Debug.LogWarning($"Waypoint {i}가 null입니다!");
            }
        }
    }

    Vector3 AdjustToMinimap(Vector3 worldPosition)
    {
        Vector3 adjustedPos = worldPosition;
        adjustedPos.y = 25f;
        return adjustedPos;
    }
}