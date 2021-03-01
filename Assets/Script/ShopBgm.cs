using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBgm : MonoBehaviour
{
    public AudioSource ShopBgmSrc;
    public AudioSource BgmSrc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Globel.inClear)
        {
            if(Globel.inShop)
            {
                if(ShopBgmSrc.volume <= Globel.BGMVolume)
                {
                    ShopBgmSrc.volume += 0.01f;
                }
                if(BgmSrc.volume >= Globel.BGMVolume * 0.15f)
                {
                    BgmSrc.volume -= 0.01f;
                }
            }
            if(!Globel.inShop)
            {
                if(ShopBgmSrc.volume >= 0.005f)
                {
                    ShopBgmSrc.volume -= 0.005f;
                }
                if(BgmSrc.volume <= Globel.BGMVolume)
                {
                    BgmSrc.volume += 0.005f;
                }
            }
        }
        if(!Globel.inClear && !Globel.inBoss && Globel.inEndRoom)
        {
            if(BgmSrc.volume >= Globel.BGMVolume * 0.15f)
            {
                BgmSrc.volume -= 0.01f;
            }
        }
        if(Globel.inBoss)
        {
            BgmSrc.volume = Globel.BGMVolume;
        }
    }
}
