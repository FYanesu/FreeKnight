using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomTips : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] tip;
    public int MaxRandomNum;
    private int RandomNum;
    public Text tipsText;
    void Start()
    {
        RandomNum = Random.Range(0, MaxRandomNum);
        tipsText.text = tip[RandomNum];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
