using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [Header("Camera Sensitivity Settings")]
    [Space]
    [SerializeField] private float rotationSpeed = 100f;

    [Header("Camera  Max Rotation Settings")]
    [Space]
    [SerializeField] private float MaxLeftRotation = -30f;
    [SerializeField] private float MaxRightRotation = 30f;
    [SerializeField] private float MaxUpRotation = 95f;
    [SerializeField] private float MaxDownRotation = -30f;

    private float xRotation = 20f;
    private float yRotation = 0f;

    #region InputSystems
    KeyBoardInputActions action;
    InputAction lookUpDonwAction;
    InputAction lookLeftRightAction;
    #endregion

    private void Awake()
    {
        AllocateCameraActions();
        CursorLock();
    }

    private void OnEnable()
    {
        lookUpDonwAction.Enable();
        lookLeftRightAction.Enable();
    }

    private void OnDisable()
    {
        lookUpDonwAction.Disable();
        lookLeftRightAction.Disable();
    }

    void Update()
    {
        UpdateCameraMove();
    }

    private void UpdateCameraMove()
    {
        float xMove = lookLeftRightAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;
        float yMove = lookUpDonwAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;

        yRotation += xMove;
        xRotation -= yMove;

        xRotation = Mathf.Clamp(xRotation, MaxLeftRotation, MaxRightRotation);

        yRotation = Mathf.Clamp(yRotation, MaxDownRotation, MaxUpRotation);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void AllocateCameraActions()
    {
        action = new KeyBoardInputActions();
        lookUpDonwAction = action.Car.LookUpDown;
        lookLeftRightAction = action.Car.LookLeftRight;
    }
}
