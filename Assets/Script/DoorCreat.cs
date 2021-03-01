using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCreat : MonoBehaviour
{
    private GameObject DoorLeft,DoorRight,DoorUp,DoorDown;
    public GameObject GateLeft,GateRight,GateUp,GateDown;
    private Animator  Animator_GL,Animator_GU,Animator_GD,Animator_GR;
    private BoxCollider2D BoxCol_GL,BoxCol_GR,BoxCol_GU,BoxCol_GD;
    public bool RoomLeft,RoomRight,RoomUp,RoomDown;
    public bool clear;

    // Start is called before the first frame update
    void Start()
    {
        DoorLeft = transform.GetChild(0).gameObject;
        DoorUp = transform.GetChild(1).gameObject;
        DoorRight = transform.GetChild(2).gameObject;
        DoorDown = transform.GetChild(3).gameObject;

        GateLeft = DoorLeft.transform.GetChild(2).gameObject;
        GateUp = DoorUp.transform.GetChild(2).gameObject;
        GateRight = DoorRight.transform.GetChild(2).gameObject;
        GateDown = DoorDown.transform.GetChild(0).gameObject;

        DoorLeft.SetActive(RoomLeft);
        DoorRight.SetActive(RoomRight);
        DoorUp.SetActive(RoomUp);
        DoorDown.SetActive(RoomDown);

        Animator_GL = GateLeft.GetComponent<Animator>();
        Animator_GU = GateUp.GetComponent<Animator>();
        Animator_GD = GateDown.GetComponent<Animator>();
        Animator_GR = GateRight.GetComponent<Animator>();

        BoxCol_GL = GateLeft.GetComponent<BoxCollider2D>();
        BoxCol_GU = GateUp.GetComponent<BoxCollider2D>();
        BoxCol_GD = GateDown.GetComponent<BoxCollider2D>();
        BoxCol_GR = GateRight.GetComponent<BoxCollider2D>();

        clear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openDoor()
    {
        SoundManager.PlayD_open();
        Debug.Log("open");
        if(RoomLeft)
        {
            Animator_GL.SetTrigger("Open");
            BoxCol_GL.enabled = false;
        }
        if(RoomRight)
        {
            Animator_GR.SetTrigger("Open");
            BoxCol_GR.enabled = false;
        }
        if(RoomUp)
        {
            Animator_GU.SetTrigger("Open");
            BoxCol_GU.enabled = false;
        }
        if(RoomDown)
        {
            Animator_GD.SetTrigger("Open");
            BoxCol_GD.enabled = false;
        }
    }
    public void closeDoor()
    {
        if(!clear)
        {
            Debug.Log("close");
            if(RoomLeft)
            BoxCol_GL.enabled = true;
            if(RoomRight)
                BoxCol_GR.enabled = true;
            if(RoomUp)
                BoxCol_GU.enabled = true;
            if(RoomDown)
                BoxCol_GD.enabled = true;
            Invoke("AE_closeDoor",0.2f);
        }
    }
    public void AE_closeDoor()
    {
        SoundManager.PlayD_close();
        if(RoomLeft)
            Animator_GL.SetTrigger("Close");
        if(RoomRight)
            Animator_GR.SetTrigger("Close");
        if(RoomUp)
            Animator_GU.SetTrigger("Close");
        if(RoomDown)
            Animator_GD.SetTrigger("Close");
    }
}
