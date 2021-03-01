﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoomCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("最终房间区域");
            Globel.inEndRoom = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Globel.inEndRoom = false;
        }
    }
}
