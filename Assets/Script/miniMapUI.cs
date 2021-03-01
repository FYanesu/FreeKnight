using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniMapUI : MonoBehaviour
{
    
    public RawImage minimap;
    public Vector3 fix;
    // Start is called before the first frame update
    void Start()
    {
        fix = transform.parent.gameObject.transform.position;
        minimap = GetComponent<RawImage>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetComponent<RectTransform>().anchoredPosition = player.transform.localPosition;
        //transform.position = fix - player.transform.position * 4f;
        if (Input.GetButtonDown("Map") && !Globel.inMenu && Globel.HP > 0)
        {
            //Debug.Log("q");
            
            if(minimap.enabled)
            {
                minimap.enabled = false;
            }
            else
            {
                minimap.enabled = true;
            }
            
            
        }
    }
}
