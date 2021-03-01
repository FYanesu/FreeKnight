using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_Flask : MonoBehaviour
{
    public string mes1;
    public string mes2;
    public string mes3;
    public string mes4;
    public string mes5;
    public string mes6;
    private int randomDia;
    private int randomDia_temp;
    private GameObject DialogFrame;
    private GameObject TextFrame1;
    private GameObject TextFrame2;
    private GameObject TextFrame3;
    private GameObject TextFrame4;
    private Animator   DialogDisplay;
    private MeshRenderer meshRenderer;
    public bool inShop;
    // Start is called before the first frame update
    void Start()
    {
        //mes = "按下'空格键'购买'生命药瓶'";
        DialogFrame = transform.GetChild(0).gameObject;
        TextFrame1 = transform.GetChild(1).gameObject;
        TextFrame2 = transform.GetChild(2).gameObject;
        TextFrame3 = transform.GetChild(3).gameObject;
        LayerFix(TextFrame1);
        LayerFix(TextFrame2);
        LayerFix(TextFrame3);
        DialogDisplay = DialogFrame.GetComponent<Animator>();
        inShop = false;
        if(Globel.isKey == false)
            mes1 = "按A键对话";
    }
    void LayerFix(GameObject TextFrame)
    {
        meshRenderer = TextFrame.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 4;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Buy")&&inShop)
        {
            SoundManager.PlayP_dialog();
            do{
                randomDia_temp = Random.Range(1,4);
            }while(randomDia_temp == randomDia);
            randomDia = randomDia_temp;
            switch (randomDia)
            {
                case 1:
                    TextFrame3.GetComponent<TextMesh>().text = mes4;
                    break;
                case 2:
                    TextFrame3.GetComponent<TextMesh>().text = mes5;
                    break;
                case 3:
                    TextFrame3.GetComponent<TextMesh>().text = mes6;
                    break;   
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("检测触发器靠近");
        if(other.gameObject.CompareTag("Player") && !inShop)
        {
            Debug.Log("检测玩家靠近");
            DialogDisplay.SetTrigger("Display");
            Invoke("Iv_display" ,0.125f);
            inShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && inShop)
        {
            DialogDisplay.SetTrigger("Undisplay");
            Invoke("Iv_undisplay" ,0.125f);
            inShop = false;
        }
    }
    void Iv_display()
    {
        TextFrame1.GetComponent<TextMesh>().text = mes1;
        TextFrame2.GetComponent<TextMesh>().text = mes2;
        TextFrame3.GetComponent<TextMesh>().text = mes3;
    }
    void Iv_undisplay()
    {
        TextFrame1.GetComponent<TextMesh>().text = "";
        TextFrame2.GetComponent<TextMesh>().text = "";
        TextFrame3.GetComponent<TextMesh>().text = "";
    }
}

    