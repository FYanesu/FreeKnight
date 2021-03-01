using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Power_Dialog : MonoBehaviour,
IPointerEnterHandler,  IPointerExitHandler  
{
    public string mes1;
    public string mes2;
    public string mes3;
    private GameObject DialogFrame;
    private GameObject TextFrame1;
    private GameObject TextFrame2;
    private GameObject TextFrame3;
    public Sprite AbilitySprite;
    public bool inDisplay;
    // Start is called before the first frame update
    void Start()
    {
        DialogFrame = transform.GetChild(0).gameObject;
        TextFrame1 = transform.GetChild(1).gameObject;
        TextFrame2 = transform.GetChild(2).gameObject;
        TextFrame3 = transform.GetChild(3).gameObject;
        inDisplay = false;
        TextFrame1.GetComponent<Text>().text = mes1;
        TextFrame2.GetComponent<Text>().text = mes2;
        TextFrame3.GetComponent<Text>().text = mes3;
        gameObject.GetComponent<Image>().sprite = AbilitySprite;

    }

    // Update is called once per frame
    void Update()
    {
        DialogFrame.SetActive(inDisplay);
        TextFrame1.SetActive(inDisplay);
        TextFrame2.SetActive(inDisplay);
        TextFrame3.SetActive(inDisplay);
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        Debug.Log("In");
        inDisplay = true;
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        Debug.Log("out");
        inDisplay = false;
    }
}
