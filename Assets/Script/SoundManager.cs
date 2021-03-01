using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioSource audioSrc;

    public static AudioClip p_attack;
    public static AudioClip p_hit;
    public static AudioClip p_defend;
    public static AudioClip p_hurt;
    public static AudioClip p_block;
    public static AudioClip p_skill;
    public static AudioClip p_roll;
    public static AudioClip g_attack;
    public static AudioClip g_hurt;
    public static AudioClip g_arrow;

    public static AudioClip b_attack1;
    public static AudioClip b_attack2;
    public static AudioClip p_sowrd;
    public static AudioClip p_coin;
    public static AudioClip p_shop;
    public static AudioClip p_select;
    public static AudioClip p_dialog;

    public static AudioClip d_open;
    public static AudioClip d_close;

    // public AudioClip[] audios;

    // public static AudioClip unbattle;
    // public static AudioClip battle;
    // public static AudioClip boss;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        
        p_attack = Resources.Load<AudioClip>("Sword5");
        p_hit = Resources.Load<AudioClip>("Slash3");
        p_defend = Resources.Load<AudioClip>("Equip1");
        p_hurt = Resources.Load<AudioClip>("Damage4");
        p_block = Resources.Load<AudioClip>("Parry");
        p_skill = Resources.Load<AudioClip>("Skill2");
        p_roll = Resources.Load<AudioClip>("Equip2");

        g_attack = Resources.Load<AudioClip>("Wind7");
        g_hurt = Resources.Load<AudioClip>("Damage5");
        g_arrow = Resources.Load<AudioClip>("Crossbow");
        b_attack1 = Resources.Load<AudioClip>("Fire2");
        b_attack2 = Resources.Load<AudioClip>("Fire3");

        p_sowrd = Resources.Load<AudioClip>("Slash2");
        p_coin = Resources.Load<AudioClip>("Coin");
        p_shop = Resources.Load<AudioClip>("Shop2");
        p_select = Resources.Load<AudioClip>("Save");
        p_dialog = Resources.Load<AudioClip>("Cursor1");

        d_open = Resources.Load<AudioClip>("Open3");
        d_close = Resources.Load<AudioClip>("Door3");

        
        // battle = Resources.Load<AudioClip>("battle");
        // boss = Resources.Load<AudioClip>("boss");
        // unbattle = Resources.Load<AudioClip>("unbattle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayP_attack()
    {
        audioSrc.PlayOneShot(p_attack);
    }
    public static void PlayP_hit()
    {
        audioSrc.PlayOneShot(p_hit);
    }
    public static void PlayP_defend()
    {
        audioSrc.PlayOneShot(p_defend);
    }
    public static void PlayP_hurt()
    {
        audioSrc.PlayOneShot(p_hurt);
    }
    public static void PlayP_block()
    {
        audioSrc.PlayOneShot(p_block);
    }
    public static void PlayP_skill()
    {
        audioSrc.PlayOneShot(p_skill);
    }
    public static void PlayP_roll()
    {
        audioSrc.PlayOneShot(p_roll);
    }
    public static void PlayG_attack()
    {
        audioSrc.PlayOneShot(g_attack);
    }
    public static void PlayG_hurt()
    {
        audioSrc.PlayOneShot(g_hurt);
    }
    public static void PlayG_arrow()
    {
        audioSrc.PlayOneShot(g_arrow);
    }
    public static void PlayB_attack1()
    {
        audioSrc.PlayOneShot(b_attack1);
    }
    public static void PlayB_attack2()
    {
        audioSrc.PlayOneShot(b_attack2);
    }
    public static void PlayP_sword()
    {
        audioSrc.PlayOneShot(p_sowrd);
    }
    public static void PlayP_coin()
    {
        audioSrc.PlayOneShot(p_coin);
    }
    public static void PlayP_shop()
    {
        audioSrc.PlayOneShot(p_shop);
    }
    public static void PlayP_select()
    {
        audioSrc.PlayOneShot(p_select);
    }
    public static void PlayP_dialog()
    {
        audioSrc.PlayOneShot(p_dialog);
    }
    public static void PlayD_open()
    {
        audioSrc.PlayOneShot(d_open);
    }
    public static void PlayD_close()
    {
        audioSrc.PlayOneShot(d_close);
    }

}
