using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private float rollSpeed = 80f;
    private float timer = 0f;
    

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    private void Scroll()
    {
        //字体滚动
        this.transform.Translate(Vector2.up * rollSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        //字体缩小
        this.GetComponent<TextMesh>().fontSize--;
        //字体渐变透明
        this.GetComponent<TextMesh>().color = new Color(1,1,1,1 - timer);
        
    }
}
