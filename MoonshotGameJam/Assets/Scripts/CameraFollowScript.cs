using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform target;
    public BoxCollider2D[] mapBounds;
    public int currentIndex;

    private float xMin, xMax, yMin, yMax;
    private float camY,camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;
    public float smoothing = .5f;
    public Vector3 smoothPosition;
    public GameObject marker;
    public float followAhead;
    public GameObject player;
    public bool verticalFollow;
    void Start()
    {
        
        xMin = mapBounds[currentIndex].bounds.min.x ;
        xMax = mapBounds[currentIndex].bounds.max.x;
        yMin = mapBounds[currentIndex].bounds.min.y;
        yMax = mapBounds[currentIndex].bounds.max.y;

        mainCam = GetComponent<Camera>();
        
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = camOrthsize*mainCam.aspect;
        
    }
    void Update(){
        for(int i = 0; i < mapBounds.Length;i++){
            if(player.transform.position.x > mapBounds[i].bounds.min.x && player.transform.position.x < mapBounds[i].bounds.max.x){
                currentIndex = i;
                
            }
        }
        ChangeBounds();
    }
    void FixedUpdate()
    {
        if(verticalFollow){
            camY = Mathf.Clamp(target.position.y + 2.5f , yMin + camOrthsize, yMax - camOrthsize);
        } else{
            camY = Mathf.Clamp(target.position.y , yMin + camOrthsize, yMax - camOrthsize);
        }
        
        
        if(player.transform.localScale.x > 0){
            camX = Mathf.Clamp(target.position.x+ followAhead, xMin + cameraRatio, xMax - cameraRatio);
            smoothPosition = Vector3.Lerp(transform.position,new Vector3(camX ,camY,transform.position.z),smoothing*Time.deltaTime);
        } else{
            camX = Mathf.Clamp(target.position.x- followAhead, xMin + cameraRatio, xMax - cameraRatio);
            smoothPosition = Vector3.Lerp(transform.position,new Vector3(camX ,camY,transform.position.z),smoothing*Time.deltaTime);
        }
        
        transform.position = smoothPosition;
    }

    public void ChangeBounds(){
         xMin = mapBounds[currentIndex].bounds.min.x ;
        xMax = mapBounds[currentIndex].bounds.max.x;
        yMin = mapBounds[currentIndex].bounds.min.y;
        yMax = mapBounds[currentIndex].bounds.max.y;

        mainCam = GetComponent<Camera>();
        
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = camOrthsize*mainCam.aspect;
        
    }
}
