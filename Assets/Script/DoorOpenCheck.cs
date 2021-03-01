using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenCheck : MonoBehaviour
{
    
    private GameObject roomParent;
    public bool enemyIn,doorUnlock = true;
    public bool bossIn;
    public bool isSpecial = false;
    public bool isEndroom = false;
    // Start is called before the first frame update
    void Start()
    {
        roomParent = transform.parent.gameObject;
        bossIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Globel.killN >= 6 && !doorUnlock)
        {
            //Debug.Log("check");
            roomParent.GetComponent<DoorCreat>().clear = true;
            roomParent.GetComponent<DoorCreat>().openDoor();
            Globel.killN=0;
            enemyIn = false;
            doorUnlock = true;
        }
        if(Globel.killN >= 1 && !doorUnlock && bossIn)
        {
            //Debug.Log("check");
            BGMManager.PlayUnbattle();
            roomParent.GetComponent<DoorCreat>().clear = true;
            roomParent.GetComponent<DoorCreat>().openDoor();
            Globel.killN=0;
            enemyIn = false;
            doorUnlock = true;
        }

    }

    private void OnTriggerStay2D(Collider2D other) {
        //Debug.Log("1");
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyIn = true;
        }
        if(other.gameObject.CompareTag("Boss") && !bossIn && doorUnlock && !Globel.inClear)
        {
            bossIn = true;
            doorUnlock = false;
            roomParent.GetComponent<DoorCreat>().closeDoor();
            //Debug.Log("BossIn!");
        }
    }
        
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetChild(7).gameObject.SetActive(true);
            if(isEndroom)
                transform.parent.GetChild(8).gameObject.SetActive(true);
            if(isSpecial)
                transform.parent.GetChild(9).gameObject.SetActive(true);
        }
        if(other.gameObject.CompareTag("Player") && enemyIn == true && doorUnlock && !roomParent.GetComponent<DoorCreat>().clear)
        {
            //Globel.killN = 0;
            Debug.Log("close");
            doorUnlock = false;
            roomParent.GetComponent<DoorCreat>().closeDoor();
        }

    }
}
