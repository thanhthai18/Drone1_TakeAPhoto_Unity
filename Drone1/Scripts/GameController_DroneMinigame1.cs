using DG.Tweening;
using Spine;
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
    public Minimap_DroneMinigame1 droneFlyCam;
    public bool isExactCapture;
    public Button btnTarget;
    public GameObject panelTargetClone;
    public bool isOnClickBtnTarget;
    public GameObject trueIcon, falseIcon;
    public int countWin;
    public DroneObj_DroneMinigame1 droneObj;
    public GameObject UIScreen;
    public GameObject tutorial;
    public int indexTutorial;
    public bool isTutorial1, isTutorial2, isTutorial3;
    public Transform allPosCaptureCollider;
    public List<Transform> posSpawnCar = new List<Transform>();
    public List<GameObject> carPrefabs = new List<GameObject>();
    public int random;


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
        isExactCapture = false;
        isOnClickBtnTarget = false;
        isTutorial1 = true;
        isTutorial2 = false;
        isTutorial3 = false;
        panelTargetClone.SetActive(false);
        trueIcon.SetActive(false);
        falseIcon.SetActive(false);
        countWin = 0;
        indexTutorial = 0;
        panelTargetClone.transform.localScale = Vector3.zero;
        tutorial.transform.position = new Vector2(7.79f, 2.72f);
        tutorial.transform.localScale = Vector3.one;
    }

    private void Start()
    {
        SetSizeCamera();
        startPosImgCapture = imgCaptureClone.rectTransform.anchoredPosition;
        imgTakePhoto.gameObject.SetActive(false);
        btnCapture.onClick.AddListener(Capture);
        btnTarget.onClick.AddListener(CheckTarget);
        btnCapture.enabled = false;
        tutorial.SetActive(true);
        Tutorial1();

    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Tutorial1()
    {
        tutorial.transform.DOMove(new Vector2(8.42f, 4.21f), 1).SetUpdate(true).OnComplete(() =>
        {
            tutorial.transform.DOMove(new Vector2(7.79f, 2.72f), 1).SetUpdate(true).OnComplete(() =>
            {
                if (tutorial.activeSelf && isTutorial1)
                {
                    Tutorial1();
                }
            });
        });
    }

    void Tutorial2()
    {
        tutorial.transform.DOMove(new Vector2(-5.45f, -2.08f), 1).SetUpdate(true).OnComplete(() =>
        {
            tutorial.transform.DOMove(new Vector2(-3.78f, -0.41f), 1).SetUpdate(true).OnComplete(() =>
            {
                if (tutorial.activeSelf && isTutorial2)
                {
                    Tutorial2();
                }
            });
        });
    }

    public void Tutorial3()
    {
        tutorial.transform.DOMoveY(-3.75f, 1).SetUpdate(true).OnComplete(() =>
        {
            tutorial.transform.DOMoveY(-1.15f, 1).SetUpdate(true).OnComplete(() =>
            {
                if (tutorial.activeSelf && isTutorial3)
                {
                    Tutorial3();
                }
            });
        });
    }

    IEnumerator SpawnCar()
    {
        while (true)
        {
            random = Random.Range(0, 2);
            var tmpCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)], posSpawnCar[random].transform.position, Quaternion.identity);
            if(random == 0)
            {              
                tmpCar.transform.DOMoveX(posSpawnCar[1].transform.position.x, 10).OnComplete(() =>
                {
                    Destroy(tmpCar);
                });
            }
            else if(random == 1)
            {
                tmpCar.transform.eulerAngles = new Vector3(0, 180, 0);
                tmpCar.transform.DOMoveX(posSpawnCar[0].transform.position.x, 10).OnComplete(() =>
                {
                    Destroy(tmpCar);
                });
            }
            yield return new WaitForSeconds(3);
        }
    }

    void CompleteCapture()
    {
        btnTarget.transform.GetChild(droneFlyCam.indexCapture).GetChild(1).gameObject.SetActive(true);
        panelTargetClone.transform.GetChild(droneFlyCam.indexCapture).GetChild(1).gameObject.SetActive(true);
        countWin++;
    }

    void CheckCapture()
    {
        if (droneFlyCam.currentPosCapture != null)
        {
            if (Vector2.Distance(droneFlyCam.transform.position, droneFlyCam.currentPosCapture.transform.position) <= droneFlyCam.distanceCheck)
            {
                isExactCapture = true;
                if (droneFlyCam.indexCapture == 4)
                {
                    for (int i = 4; i <= 6; i++)
                    {
                        allPosCaptureCollider.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;

                    }
                }
                droneFlyCam.currentPosCapture.GetComponent<BoxCollider2D>().enabled = false;

                Invoke(nameof(CompleteCapture), 1.5f);
            }
            else
            {
                isExactCapture = false;
            }
        }
        else
        {
            isExactCapture = false;
        }
    }

    void CheckTarget()
    {
        if (tutorial.activeSelf)
        {
            if (indexTutorial == 0)
            {
                indexTutorial++;

            }
            else if (indexTutorial == 1)
            {
                indexTutorial++;
                isTutorial1 = false;
                isTutorial2 = true;
                tutorial.transform.DOKill();
                tutorial.transform.position = new Vector2(-3.78f, -0.41f);
                tutorial.transform.localScale = new Vector3(-1, -1, 1);
                Tutorial2();
            }
        }

        if (!isOnClickBtnTarget)
        {
            if (panelTargetClone.activeSelf)
            {
                isOnClickBtnTarget = true;
                Time.timeScale = 1;
                panelTargetClone.transform.DOScale(0, 0.5f).OnComplete(() =>
                {
                    panelTargetClone.SetActive(false);
                    isOnClickBtnTarget = false;
                });
            }
            else
            {
                isOnClickBtnTarget = true;
                panelTargetClone.SetActive(true);
                panelTargetClone.transform.DOScale(1, 0.5f).OnComplete(() =>
                {
                    Time.timeScale = 0;
                    isOnClickBtnTarget = false;
                });
            }
        }
    }

    void ShowResult()
    {
        if (isExactCapture)
        {
            trueIcon.SetActive(true);
            trueIcon.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).OnComplete(() =>
            {
                trueIcon.transform.DOScale(0.5f, 1f).OnComplete(() =>
                {
                    trueIcon.SetActive(false);
                });
            });
        }
        else
        {
            falseIcon.SetActive(true);
            falseIcon.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).OnComplete(() =>
            {
                falseIcon.transform.DOScale(0.5f, 1f).OnComplete(() =>
                {
                    falseIcon.SetActive(false);
                });
            });
        }
    }

    void Capture()
    {
        if (tutorial.activeSelf)
        {
            tutorial.SetActive(false);
            tutorial.transform.DOKill();
            StartCoroutine(SpawnCar());

        }
        if (!isCapture && !panelTargetClone.activeSelf)
        {
            isCapture = true;
            CheckCapture();
            imgTakePhoto.gameObject.SetActive(true);
            imgTakePhoto.GetComponent<Image>().DOFade(0.9f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(ShowResult), 1.2f);
                imgCaptureClone.rectTransform.DOLocalMove(Vector3.zero, 2);
                imgCaptureClone.rectTransform.DOScale(1.2f, 2).OnComplete(() =>
                {
                    if (isExactCapture)
                    {
                        imgCaptureClone.rectTransform.DOLocalMove(new Vector2(imgCaptureClone.rectTransform.anchoredPosition.x + 1000, imgCaptureClone.rectTransform.anchoredPosition.y + 500), 1).OnComplete(() =>
                        {
                            imgCaptureClone.rectTransform.anchoredPosition = startPosImgCapture;
                            imgCaptureClone.rectTransform.localScale = Vector3.one;
                            imgTakePhoto.gameObject.SetActive(false);
                            isCapture = false;
                            if (countWin == 6)
                            {
                                Invoke(nameof(Win), 1);
                                btnCapture.enabled = false;
                                btnTarget.enabled = false;
                            }
                        });
                    }
                    else
                    {
                        imgCaptureClone.rectTransform.DOLocalMove(new Vector2(imgCaptureClone.rectTransform.anchoredPosition.x, imgCaptureClone.rectTransform.anchoredPosition.y - 500), 1).OnComplete(() =>
                         {
                             imgCaptureClone.rectTransform.anchoredPosition = startPosImgCapture;
                             imgCaptureClone.rectTransform.localScale = Vector3.one;
                             imgTakePhoto.gameObject.SetActive(false);
                             isCapture = false;
                         });
                    }


                });
                imgTakePhoto.GetComponent<Image>().DOFade(0f, 1f);
            });
        }
    }

    void Win()
    {
        isWin = true;
        Debug.Log("Win");
        StopAllCoroutines();
        btnCapture.enabled = false;
        btnTarget.enabled = false;
        UIScreen.SetActive(false);
        imgCaptureClone.gameObject.SetActive(false);
        droneObj.transform.DOMoveX(droneObj.transform.position.x + 30, 1);
    }
    void Lose()
    {
        isLose = true;
        Debug.Log("Thua");
        StopAllCoroutines();
        btnCapture.enabled = false;
        btnTarget.enabled = false;
    }
}
