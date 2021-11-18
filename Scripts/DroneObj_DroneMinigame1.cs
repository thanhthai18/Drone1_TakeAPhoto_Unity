using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneObj_DroneMinigame1 : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody2D rb;
    public Vector2 direction;
    public Camera mainCamera;
    public float screenRatio;
    public float maxXPosCam;
    public GameObject BG;

    private void Start()
    {
        screenRatio = Screen.width * 1f / Screen.height;
        speed = 10;
        maxXPosCam = mainCamera.orthographicSize * screenRatio;
    }


    public void FixedUpdate()
    {
        direction = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical * 0.7f);
        rb.velocity = direction * speed;
        if (!GameController_DroneMinigame1.instance.isCapture)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -mainCamera.orthographicSize * screenRatio + 2, mainCamera.orthographicSize * screenRatio - 2), Mathf.Clamp(transform.position.y, -mainCamera.orthographicSize + 1, mainCamera.orthographicSize - 1));

            if (transform.position.x > 0 && transform.position.x >= maxXPosCam - 2)
            {
                BG.transform.Translate(Vector2.left * Time.deltaTime * 5);
                BG.transform.position = new Vector2(Mathf.Clamp(BG.transform.position.x, -29.1f, 28.1f), BG.transform.position.y);
            }
            else if (transform.position.x < 0 && transform.position.x <= -maxXPosCam + 2)
            {
                BG.transform.Translate(Vector2.right * Time.deltaTime * 5);
                BG.transform.position = new Vector2(Mathf.Clamp(BG.transform.position.x, -29.1f, 28.1f), BG.transform.position.y);
            }
        }
    }

}
