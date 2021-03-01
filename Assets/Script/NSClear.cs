using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NSClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Globel.isKey == false)
            transform.GetComponent<Text>().text = "按B键跳过";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
