using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearText : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Animator   DialogDisplay;
    // Start is called before the first frame update
    void Start()
    {
        DialogDisplay = transform.GetComponent<Animator>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("AMD Yes");
            DialogDisplay.SetTrigger("display");
        }
    }
}
