using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도
    private float xRotation = 20f; // X축 회전값
    private float yRotation = 0f; // Y축 회전값

    KeyBoardInputActions action;
    InputAction lookUpDonwAction;
    InputAction lookLeftRightAction;

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
        // 마우스 입력값 가져오기
        float mouseX = lookLeftRightAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;
        float mouseY = lookUpDonwAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;

        // X축(Yaw) 및 Y축(Pitch) 업데이트
        yRotation += mouseX;            // Y축 회전 (좌우)
        xRotation -= mouseY;            // X축 회전 (상하)

        // X축 회전을 제한 (예: -90도 ~ 90도)
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        // Y축 회전을 제한 (예: -90도 ~ 90도)
        yRotation = Mathf.Clamp(yRotation, -30f, 95f);

        // Z축은 항상 0으로 고정
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
