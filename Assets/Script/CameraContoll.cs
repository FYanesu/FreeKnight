using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoll : MonoBehaviour
{
    public static CameraContoll instance;

    public float speed;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && transform.position!=target.position)
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,target.position.y + 0.34f,target.position.z),speed * Time.deltaTime);
        
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
