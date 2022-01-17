using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_DroneMinigame1 : MonoBehaviour
{
    public Transform droneTransform;
    public Vector3 newPos;
    public int indexCapture;
    public Transform currentPosCapture;
    public float distanceCheck;

    private void Start()
    {
        SetPosition();
        indexCapture = -1;
        distanceCheck = 1;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            indexCapture = 0;
            currentPosCapture = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("Tree"))
        {
            indexCapture = 1;
            currentPosCapture = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("People"))
        {
            indexCapture = 2;
            currentPosCapture = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("Balloon"))
        {
            indexCapture = 3;
            currentPosCapture = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("ColorHive"))
        {
            indexCapture = 4;
            currentPosCapture = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("BoundScreen"))
        {
            indexCapture = 5;
            currentPosCapture = collision.gameObject.transform;
        }
    }



    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Box") && indexCapture == 0 || collision.gameObject.CompareTag("Tree") && indexCapture == 1 || collision.gameObject.CompareTag("People") 
    //        && indexCapture == 2 || collision.gameObject.CompareTag("Balloon") && indexCapture == 3 || collision.gameObject.CompareTag("ColorHive")
    //        && indexCapture == 4 || collision.gameObject.CompareTag("BoundScreen") && indexCapture == 5)
    //    {
    //        indexCapture = -1;
    //        currentPosCapture = null;
    //    }
    //}
}
