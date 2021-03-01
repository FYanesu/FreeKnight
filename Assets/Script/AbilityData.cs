using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : MonoBehaviour
{
    public string[] Datam1 = new string[100];
    public string[] Datam2 = new string[100];
    public string[] Datam3 = new string[100];
    public Sprite[] DataSprite = new Sprite[100];
    public string[] temp_Datam1 = new string[100];
    public string[] temp_Datam2 = new string[100];
    public string[] temp_Datam3 = new string[100];
    public Sprite[] temp_DataSprite = new Sprite[100];
    public int[] abilityCheck = new int[100];
    public int[] temp_abilityCheck = new int[100];
    // Start is called before the first frame update
    void Start()
    {
        if(Globel.stage == 1)
        {        
            abilityCheck = new int[100];
            temp_abilityCheck = new int[100];
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
