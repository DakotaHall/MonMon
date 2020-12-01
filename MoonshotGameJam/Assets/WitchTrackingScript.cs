using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTrackingScript : MonoBehaviour
{
    public Transform witch;
    public GameObject witchHead;
    public float cameraRatio;
    public float camOrthoSize;
    public Camera mainCam;
    public Transform witchArrow;

    void Start()
    {
        mainCam = Camera.main;
        camOrthoSize = Camera.main.orthographicSize;
        cameraRatio = camOrthoSize * Camera.main.aspect;
    }

    void Update()
    {
        Vector3 facePos = witch.position;
        bool onScreen = true;
        float camLeft = mainCam.transform.position.x - cameraRatio;
        float camRight = mainCam.transform.position.x + cameraRatio;
        float camUp = mainCam.transform.position.y + camOrthoSize;
        float camDown = mainCam.transform.position.y - camOrthoSize;
        if (witch.position.x < camLeft || witch.position.x > camRight || witch.position.y < camDown || witch.position.y > camUp)
        {
            if (witch.position.x < camLeft)
            {
                onScreen = false;
                facePos.x = camLeft + 7.5f;
            }
            else if (witch.position.x > camRight)
            {
                onScreen = false;
                facePos.x = camRight - 7.5f;
            }
            else
            {
                if (witch.position.x < transform.position.x)
                {
                    facePos.x = witch.position.x + 7.5f;
                }
                else
                {
                    facePos.x = witch.position.x - 7.5f;
                }
            }
            if (witch.position.y < camDown)
            {
                onScreen = false;
                facePos.y = camDown + 7.5f;
            }
            else if (witch.position.y > camUp)
            {
                onScreen = false;
                facePos.y = camUp - 7.5f;
            } else
            {
                if (witch.position.y < transform.position.y)
                {
                    facePos.y = witch.position.y + 7.5f;
                }
                else
                {
                    facePos.y = witch.position.y - 7.5f;
                }
            }

        }


        if (!onScreen)
        {
            witchHead.transform.position = facePos;
            if (!witchHead.activeSelf)
            {
                witchHead.SetActive(true);
                witchArrow.gameObject.SetActive(true);
            }

            Vector3 vectorDif = witchHead.transform.position - witch.transform.position;
            witchArrow.position = witchHead.transform.position - (vectorDif.normalized * 5f);
            witchArrow.up = -vectorDif;
        }
        else
        {
            if (witchHead.activeSelf)
            {
                witchHead.SetActive(false);
                witchArrow.gameObject.SetActive(false);
            }
        }
    }
}
