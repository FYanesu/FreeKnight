using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int startCoinNumber;
    public Text Coin_Number;

    // Start is called before the first frame update
    void Start()
    {
        if(Globel.Coin == -1)
        Globel.Coin = startCoinNumber;
    }

    // Update is called once per frame
    void Update()
    {
        Coin_Number.text = Globel.Coin.ToString();
    }
}
