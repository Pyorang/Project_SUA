using UnityEngine;

public enum NavigationAlertType
{
    GoLeft = 0,
    GoMiddle = 1,
    GoRight = 2,
}

public class NavigationAlertZone : MonoBehaviour
{
    [SerializeField] private NavigationAlertType navigationAlertType;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<VehicleController>() != null)
        {
            NavigationAlertManager.Instance.SetNavigationAlertZone((int)navigationAlertType);
        }
    }
}
