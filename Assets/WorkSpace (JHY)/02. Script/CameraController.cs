using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 5.0f;
    [SerializeField] GameObject player;

    Vector3 offset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / cameraSpeed);
    }
}
