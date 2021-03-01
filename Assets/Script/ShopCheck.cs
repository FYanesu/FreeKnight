using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCheck : MonoBehaviour
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
            Debug.Log("商店区域");
            Globel.inShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Globel.inShop = false;
        }
    }
}
