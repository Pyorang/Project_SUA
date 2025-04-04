using System;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private bool x, y, z;
    [SerializeField] private bool copyYRotation;
    [SerializeField] private Transform target;

    private void Update()
    {
        if (!target) return;

        Vector3 newPosition = transform.position;
        bool positionChanged = false;

        if (x && newPosition.x != target.position.x)
        {
            newPosition.x = target.position.x;
            positionChanged = true;
        }
        if (y && newPosition.y != target.position.y)
        {
            newPosition.y = target.position.y;
            positionChanged = true;
        }
        if (z && newPosition.z != target.position.z)
        {
            newPosition.z = target.position.z;
            positionChanged = true;
        }

        if (positionChanged)
        {
            transform.position = newPosition;
        }
        
        if (copyYRotation && transform.rotation.eulerAngles.y != target.rotation.eulerAngles.y)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                target.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
        }
    }
}