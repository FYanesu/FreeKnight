using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Globel.isKey == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if(Globel.isKey == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
