using UnityEngine;

public enum LeftRIghtSignal
{
    Left = -1,
    Middle = 0,
    Right = 1
}

public class LightController : MonoBehaviour
{
    private LeftRIghtSignal leftRightSignal = LeftRIghtSignal.Middle;

    public void ChangeLeftRightSignal(LeftRIghtSignal Input)
    {
        switch(Input)
        {
            case LeftRIghtSignal.Left:
                leftRightSignal = LeftRIghtSignal.Left;
                Debug.Log("���� ����");
                break;
            case LeftRIghtSignal.Middle:
                leftRightSignal = LeftRIghtSignal.Middle;
                Debug.Log("���� ���õ� ����");
                break;
            case LeftRIghtSignal.Right:
                leftRightSignal = LeftRIghtSignal.Right;
                Debug.Log("������ ����");
                break;
            default:
                break;
        }
    }

    public void ChangeEmergencyLights()
    {
        Debug.Log("���� ���� �ٲ�");
    }
}
