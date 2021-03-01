using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPointBase : MonoBehaviour
{
    public float Destroytime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Destroytime); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
