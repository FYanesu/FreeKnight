﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodText : MonoBehaviour
{
    private float destoryTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
