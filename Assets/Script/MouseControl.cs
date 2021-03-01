using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MouseControl : MonoBehaviour
{
    float inputX;
    float inputY;
    float inputX_M;
    float inputY_M;
    int SetX;
    int SetY;
    
    [DllImport("user32.dll")] //强制设置鼠标坐标
    public static extern int SetCursorPos(int x, int y);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT pt);
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    POINT pt;
    // Start is called before the first frame update
    void Start()
    {
        
        GetCursorPos(out pt);
        SetX = pt.X;
        SetY = pt.Y;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("MouseX_C");
        inputY = Input.GetAxis("MouseY_C");
        if((Mathf.Abs(inputX) + Mathf.Abs(inputY)) > Mathf.Epsilon)
        {
            GetCursorPos(out pt);
            SetX = pt.X;
            SetY = pt.Y;
            SetX += (int)(inputX * 30);
            SetY += (int)(inputY * 30); 
            Debug.Log("inputX:" + inputX + ",inputY:" + inputY + "SetX:" + SetX + ",SetY:" + SetY );
            SetCursorPos(SetX, SetY);
        }
    }
}
