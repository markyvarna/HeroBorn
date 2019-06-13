﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float onscreenDelay = 3f;
    private Rigidbody _bulletRb;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, onscreenDelay);
    }

   
}
