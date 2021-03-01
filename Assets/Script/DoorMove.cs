using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public int moveTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTime>0)
        moveTime--;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && moveTime==0)
        {
            moveTime = 5;
            if(this.gameObject.CompareTag("Door_U"))
            other.transform.gameObject.transform.Translate(0,9.3f,0);
            if(this.gameObject.CompareTag("Door_L"))
            other.transform.gameObject.transform.Translate(-15.9f,0,0);
            if(this.gameObject.CompareTag("Door_D"))
            other.transform.gameObject.transform.Translate(0,-9.3f,0);
            if(this.gameObject.CompareTag("Door_R"))
            other.transform.gameObject.transform.Translate(15.9f,0,0);
        }
    }
}
