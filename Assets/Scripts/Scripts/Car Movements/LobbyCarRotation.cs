using UnityEngine;

public class LobbyCarRotation : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward, 10.0f * Time.deltaTime);
    }
}
