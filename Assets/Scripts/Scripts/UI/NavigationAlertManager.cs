using UnityEngine;
using UnityEngine.UI;

public class NavigationAlertManager : SingletonBehaviour<NavigationAlertManager>
{
    public int currentAlertType { get; private set; }
    public IngameUIController ingameUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;

        ingameUIController = FindAnyObjectByType<IngameUIController>();
        base.Init();
    }

    public void SetNavigationAlertZone(int NavigationAlertSignType)
    {
        currentAlertType = NavigationAlertSignType;
        ingameUIController.SetAlertSignsOn();
    }
}
