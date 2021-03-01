using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log(1);
        if(other.CompareTag("Player"))
        {
            CameraContoll.instance.ChangeTarget(transform);
        }    
    }
}
