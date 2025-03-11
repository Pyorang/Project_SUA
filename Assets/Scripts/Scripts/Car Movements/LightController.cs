using UnityEngine;

public class LightController : MonoBehaviour
{
    public static readonly int LightIntensity = 1000;

    [Header("Lights Transform")]

    [SerializeField] private Light FrontLeftLamp;
    [SerializeField] private Light FrontRightLamp;
    [SerializeField] private Light RearLeftLamp;
    [SerializeField] private Light RearRightLamp;

    private bool currentLigthOn = false;

    public void Awake()
    {
        RearLeftLamp.intensity = LightIntensity;
        RearRightLamp.intensity = LightIntensity;
    }

    public void ChangeFrontLightState()
    {
        if (currentLigthOn)
        {
            //ChangeFrontLightIntensity(LightIntensitys);
        }
        else
            ChangeFrontLightIntensity(0);

        currentLigthOn = !currentLigthOn;
    }

    public void ChangeFrontLightIntensity(int Intensity)
    {
        FrontLeftLamp.intensity = Intensity;
        FrontLeftLamp.intensity = Intensity;
    }
}
