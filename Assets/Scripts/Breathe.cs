﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Breathe : MonoBehaviour
{

    Vector3 startPos;
    public float amplitude = 0.1f;
    public float period = 1f;

    protected void Start()
    { startPos = transform.position; }

    

    protected void Update()
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.up * distance;
    }

    
}