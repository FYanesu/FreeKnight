using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Animator   DialogDisplay;
    private bool    inDisplay;
    private bool    inBack;
    private Vector3 fix = new Vector3(0f,0f,+10f);
    // Start is called before the first frame update
    void Start()
    {
        inDisplay = false;
        inBack =  false;
        DialogDisplay = transform.GetComponent<Animator>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 6;
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = transform.parent.position + fix;
        if(Globel.HP <= 0 && !inDisplay)
        {
            DialogDisplay.SetTrigger("display");
            inDisplay = true;
            Invoke("back" , 2f);
        }
        if((Input.GetKeyDown("b")||Input.GetButtonDown("Roll")) && inBack)
        {
            Globel.stage = 1;
            SceneManager.LoadScene(0);
        }
    }
    void back()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        inBack = true;
    }
}
