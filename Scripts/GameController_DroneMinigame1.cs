using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_DroneMinigame1 : MonoBehaviour
{
    public static GameController_DroneMinigame1 instance;
    public Camera mainCamera;
    public bool isWin, isLose;
    public Button btnCapture;
    public Image imgTakePhoto;
    public Image imgCaptureClone;
    public Vector2 startPosImgCapture;
    public bool isCapture;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isCapture = false;
    }

    private void Start()
    {
        SetSizeCamera();
        startPosImgCapture = imgCaptureClone.rectTransform.anchoredPosition;
        imgTakePhoto.gameObject.SetActive(false);
        btnCapture.onClick.AddListener(Capture);

    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Capture()
    {
        if (!isCapture)
        {
            isCapture = true;
            imgTakePhoto.gameObject.SetActive(true);
            imgTakePhoto.GetComponent<Image>().DOFade(0.9f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                imgCaptureClone.rectTransform.DOLocalMove(Vector3.zero, 2);
                imgCaptureClone.rectTransform.DOScale(1.2f, 2).OnComplete(() =>
                {
                    imgCaptureClone.rectTransform.DOLocalMove(new Vector2(imgCaptureClone.rectTransform.anchoredPosition.x - 1000, imgCaptureClone.rectTransform.anchoredPosition.y + 500), 1).OnComplete(() =>
                    {
                        imgCaptureClone.rectTransform.anchoredPosition = startPosImgCapture;
                        imgCaptureClone.rectTransform.localScale = Vector3.one;
                        imgTakePhoto.gameObject.SetActive(false);
                        isCapture = false;
                    });

                });
                imgTakePhoto.GetComponent<Image>().DOFade(0f, 1f);
            });
        }
        
    }
}
