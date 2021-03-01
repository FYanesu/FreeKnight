using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraP : MonoBehaviour
{
    
   public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Player.transform.position.x+3,
            this.transform.position.y,
            this.transform.position.z
        );
        if(this.transform.position.x < 3){
            transform.position = new Vector3(
            3,
            this.transform.position.y,
            this.transform.position.z
        );
        }
    }
}
