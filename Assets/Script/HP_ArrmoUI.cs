using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_ArrmoUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HP_Number;
    public Text Arrmo_Number;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HP_Number.text = Globel.HP.ToString() + "/" + Globel.MAXHP.ToString();
        Arrmo_Number.text = Globel.Arrmo.ToString() + "/" + Globel.MAXArrmo.ToString();
        if(Globel.HP <= 0)
            HP_Number.text = "0" + "/" + Globel.MAXHP.ToString();
    }
}
