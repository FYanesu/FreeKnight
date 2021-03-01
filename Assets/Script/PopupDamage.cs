using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDamage : MonoBehaviour
{
    private Vector3 mTarget;
    private Vector3 mScreen;  
    public int damageValue;  
    public float ContentWidth = 100;  
    public float ContentHeight = 50;  
    private Vector2 mPoint;  
    public float FreeTime = 1.5F;  
    // Start is called before the first frame update
    void Start()
    {
        //获取目标位置    
        mTarget = transform.position;  
        //获取屏幕坐标    
        mScreen = Camera.main.WorldToScreenPoint(mTarget);  
        //将屏幕坐标转化为GUI坐标    
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);  
        //开启自动销毁线程    
        StartCoroutine("Free"); 
    }

    // Update is called once per frame
    void Update()
    {
        //使文本在垂直方向山产生一个偏移    
        transform.Translate(Vector3.up * 1.5F * Time.deltaTime);  
        //重新计算坐标    
        mTarget = transform.position;  
        //获取屏幕坐标    
        mScreen = Camera.main.WorldToScreenPoint(mTarget);  
        //将屏幕坐标转化为GUI坐标    
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);  
    }

    void OnGUI()  
    {
        //内部使用GUI坐标进行绘制    
        GUIStyle style = new GUIStyle();  
        style.fontSize = 30;  
        style.normal.textColor = Color.white;  
        GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight),damageValue.ToString(),style);  
    }
    IEnumerator Free()  
    {  
        yield return new WaitForSeconds(FreeTime);  
        Destroy(this.gameObject);  
    }
}
