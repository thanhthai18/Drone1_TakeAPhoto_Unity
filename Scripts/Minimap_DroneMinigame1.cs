using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_DroneMinigame1 : MonoBehaviour
{
    public Transform droneTransform;
    public Vector3 newPos;

    private void Start()
    {
        SetPosition();
    }

    private void LateUpdate()
    {
        if (droneTransform != null)
        {
            SetPosition();
        }
    }

    void SetPosition()
    {
        newPos = droneTransform.position;
        newPos.y = transform.position.y;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
