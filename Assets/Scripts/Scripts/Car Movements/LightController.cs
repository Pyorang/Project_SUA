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
                Debug.Log("¿ÞÂÊ ÄÑÁü");
                break;
            case LeftRIghtSignal.Middle:
                leftRightSignal = LeftRIghtSignal.Middle;
                Debug.Log("¹æÇâ Áö½Ãµî ²¨Áü");
                break;
            case LeftRIghtSignal.Right:
                leftRightSignal = LeftRIghtSignal.Right;
                Debug.Log("¿À¸¥ÂÊ ÄÑÁü");
                break;
            default:
                break;
        }
    }

    public void ChangeEmergencyLights()
    {
        Debug.Log("ºñ»óµî »óÅÂ ¹Ù²ñ");
    }
}
