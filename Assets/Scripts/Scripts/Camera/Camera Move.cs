using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // ȸ�� �ӵ�
    private float xRotation = 20f; // X�� ȸ����
    private float yRotation = 0f; // Y�� ȸ����

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
        // ���콺 �Է°� ��������
        float mouseX = lookLeftRightAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;
        float mouseY = lookUpDonwAction.ReadValue<float>() * rotationSpeed * Time.deltaTime;

        // X��(Yaw) �� Y��(Pitch) ������Ʈ
        yRotation += mouseX;            // Y�� ȸ�� (�¿�)
        xRotation -= mouseY;            // X�� ȸ�� (����)

        // X�� ȸ���� ���� (��: -90�� ~ 90��)
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        // Y�� ȸ���� ���� (��: -90�� ~ 90��)
        yRotation = Mathf.Clamp(yRotation, -30f, 95f);

        // Z���� �׻� 0���� ����
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
