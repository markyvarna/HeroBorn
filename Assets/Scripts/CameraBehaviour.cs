using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    //camOffSet is the desired distance between the cam & player
    public Vector3 camOffSet = new Vector3(0f, 3.2f, -2.6f);
    private Transform target;

    void Start()
    {
        //Sets target to player transform data -> position,rotation,scale
        target = GameObject.Find("Player").transform;
    }

    //LateUpdate method will ensure that camera moves slightly after player does
    void LateUpdate()
    {
        this.transform.position = target.TransformPoint(camOffSet);
        //helps cam focus on targets transform every frame
        this.transform.LookAt(target);
    }
}
