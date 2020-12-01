using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoBackgroundTreesScrollScript : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        Vector3 newPos = transform.localPosition;
        newPos.x = 50 - (target.position.x/10);
        transform.localPosition = newPos;
    }
}
