using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource audioSrc;
    public static AudioClip unbattle;
    public static AudioClip battle;
    public static AudioClip boss;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        battle = Resources.Load<AudioClip>("battle");
        boss = Resources.Load<AudioClip>("boss");
        unbattle = Resources.Load<AudioClip>("unbattle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayBattle()
    {
        //audioSrc.Stop();
        
        audioSrc.clip = battle;
        audioSrc.Play();
    }
    public static void PlayUnbattle()
    {
        //audioSrc.Stop();
        //audioSrc.clip = audios[0];
        //audioSrc.Play();
        //transform.GetComponent<AudioSource>().clip = audios[0];   
        //transform.GetComponent<AudioSource>().Play();
        audioSrc.clip = unbattle;
        audioSrc.Play();
    }
    public static void PlayBoss()
    {
        //audioSrc.Stop();
        audioSrc.clip = boss;
        audioSrc.Play();
    }
}
