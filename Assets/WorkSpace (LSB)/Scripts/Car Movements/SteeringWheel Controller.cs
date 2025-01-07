using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public float maxTurnAngle = 60f;
    private float currentTurnAngle = 0f;

    [SerializeField] Transform steeringWheel;

    public void UpdateSteeringWheel(float Input)
    {
        Vector3 rotation = steeringWheel.localEulerAngles;

        currentTurnAngle = maxTurnAngle * Input;
        rotation.z = currentTurnAngle;

        steeringWheel.localRotation = Quaternion.Euler(rotation);
    }
}
