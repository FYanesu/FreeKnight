using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject PlayerUnit;
    private Transform  PlayerLocation;//玩家位置
    public GameObject fireball01;
    public GameObject fireball02;
    public GameObject fireball03;
    public GameObject explode01;
    public float Attack1TimeCD;
    private int Attack1Count;
    public int Attack1Wave;

    public float Attack2TimeCD;
    public float Attack2Angel;
    private int Attack2Count;
    public int Attack2Wave;

    public float Attack3scale;

    public float Attack4TimeCD;
    private int Attack4Count;
    public int Attack4Wave;

    public float Attack5TimeCD;
    private int Attack5Count;
    public int Attack5Wave;

    public float Attack6TimeCD;
    private int Attack6Count;
    public int Attack6Wave;

    private Vector3 randomAttack;
    public float Attack7TimeCD;
    private int Attack7Count;
    public int Attack7Wave;
    public float Attack7scale;

    public float Attack8TimeCD;
    public float Attack8Angel;
    private int Attack8Count;
    public int Attack8Wave;

    public float Attack9TimeCD;
    public float Attack9Angel;
    private int Attack9Count;
    public int Attack9Wave;

    public float Attack10TimeCD;
    public float Attack10scale;
    private int Attack10Count;
    public int Attack10Wave;

    public float Attack11scale;

    public float Attack12TimeCD;
    private int Attack12Count;
    public int Attack12Wave;
    //--------------------------------------------------------
    private int currentAttack;
    private float RandomN;
    private float lastAttackTime;
    public float OrderTime;
    private bool AttackReady;
    private bool inAttack;

    private int g_luckCheck;
    private bool g_luckHit;
    private int g_hittime = 0;//被击后的无敌时间
    public int g_maxhealth;//最大生命值-可修改
    public int g_health;//当前生命
    private int g_healthcover;
    private bool g_inDeath = false;
    public GameObject floatPoint;//伤害数值预制体-外露
    public GameObject floatPoint_O;//暴击伤害数值预制体-外露
    public GameObject floatPoint_G;//吸血回复数值预制体-外露
    private Animator g_animator;
    public Color S2;
    public Color S3;
    private float coverTime;

    public int dropCoinNumber;
    public GameObject dropCoin;
    private Vector3 randomCoin;
    // Start is called before the first frame update
    void Start()
    {
        PlayerUnit = GameObject.FindGameObjectWithTag("Player");
        PlayerLocation = PlayerUnit.transform.GetChild(6).transform;
        currentAttack = 1;
        lastAttackTime = Time.time;
        AttackReady = false;
        inAttack = false;
        g_maxhealth = (int)((float)g_maxhealth * (0.65f + (float)Globel.stage * 0.65f));
        g_health = g_maxhealth;
        g_healthcover = g_maxhealth;
        g_animator = GetComponent<Animator>();
        if(Globel.stage == 2)
            transform.GetComponent<SpriteRenderer>().color = S2;
        if(Globel.stage == 3)
            transform.GetComponent<SpriteRenderer>().color = S3;
        coverTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastAttackTime >= OrderTime && !inAttack)//攻击间隔
        {
            Debug.Log("AttackReady!");
            AttackReady = true;
        }
        if(g_hittime > 0)//被击的无敌时间
            g_hittime--;
        if(AttackReady && g_health > 0 && Globel.HP > 0)
        {
            AttackReady = false;
            inAttack = true;
            g_animator.SetTrigger("Attack");
        }
        //---
        coverTime += Time.deltaTime;
        if(g_healthcover > g_health && coverTime > 0.01f)
        {    
            g_healthcover -= 2;
            coverTime = 0f;
        }
        UIManager.instance.UpdateBossHpBar(g_health,g_healthcover,g_maxhealth);
    }
    public void beHited(int atk,int face)
    {
        if(Globel.Case30)//随机伤害 遗物效果
            atk = Random.Range(0,60);
        if(Globel.Case31)//根据护盾增加伤害 遗物效果
                atk +=(Globel.Arrmo * 10 / 400);
        if(Globel.Case34)//根据血量增加伤害 遗物效果
                atk =(int)((float)atk * ((1f + ((float)(Globel.MAXHP)-(float)(Globel.HP))/(float)(Globel.MAXHP)) * 0.9f + 0.1f));
        //暴击检测---------------------
        g_luckHit = false;
        g_luckCheck = Random.Range(0,101);
        if(Globel.Luck > g_luckCheck)
            g_luckHit = true;
        //-----------------------------
        if(g_hittime == 0 && g_health > 0)
        {
            SoundManager.PlayG_hurt();
            g_hittime = 5;
            if(Globel.Case8)//击中回血 遗物效果
            {
                Globel.HP+=2;
                GameObject gb = Instantiate(floatPoint_G,PlayerLocation.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = "2";
            }
            if(Globel.Case38 && g_luckHit)//暴击回血 遗物效果
            {
                Globel.HP+=15;
                GameObject gb = Instantiate(floatPoint_G,PlayerLocation.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = "15!";
            }
            if(g_luckHit == false)
            {
                GameObject gb = Instantiate(floatPoint,transform.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                g_health -= atk;
            }
            else
            {
                atk*=2;
                GameObject gb = Instantiate(floatPoint_O,transform.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                g_health -= atk;
            }
            if(g_health <= 0 && !g_inDeath)
            {
                g_animator.SetTrigger("Death");
                UIManager.instance.ExitBossHPBar();
                g_inDeath = true;
            }
        } 
    }
    void AE_Destroy()
    {
        if(Globel.stage < 3)
        {
            while(dropCoinNumber > 0)
            {
            dropCoinNumber--;
            randomCoin = new Vector3(Random.Range(-5.5f,5.5f),Random.Range(-2.7f,2f),0f);
            Instantiate(dropCoin, transform.position+randomCoin, Quaternion.identity);
            }
        }
        Globel.inBoss = false;
        Globel.inClear = true;
        Globel.killN += 1;
        SoundManager.PlayB_attack1();
        Destroy(gameObject);
    }
    void RandomAttack()
    {
        switch (Globel.stage)
        {
            case 1:
                RandomN = Random.Range(0f,16f);
                break;
            case 2:
                RandomN = Random.Range(0f,40f);
                break;
            case 3:
                RandomN = Random.Range(0f,100f);
                break;
        }
        if(currentAttack > 4)
        currentAttack = 1;
        switch (currentAttack)
        {
            case 1:
                if(RandomN <= 16f)
                {
                    Attack1Count = 0;
                    Attack1();
                }
                if(RandomN > 16f && RandomN <= 40f)
                    Attack3();
                if(RandomN > 40f)
                {
                    Attack4Count = 0;
                    Attack4();
                }
                break;
            case 2:
                if(RandomN <= 16f)
                {
                    Attack2Count = 0;
                    Attack2();
                }
                if(RandomN > 16f && RandomN <= 40f)
                {
                    Attack6Count = 0;
                    Attack6();
                }
                if(RandomN > 40f)
                    Attack11();
                break;
            case 3:
                if(RandomN <= 16f)
                {
                    Attack5Count = 0;
                    Attack5();
                }
                if(RandomN > 16f && RandomN <= 40f)
                {
                    Attack7Count = 0;
                    Attack7();
                }
                if(RandomN > 40f)
                {
                    Attack12Count = 0;
                    Attack12();
                }
                break;
            case 4:
                if(RandomN <= 16f)
                {
                    Attack8Count = 0;
                    Attack8();
                }
                if(RandomN > 16f && RandomN <= 40f)
                {
                    Attack9Count = 0;
                    Attack9();
                }
                if(RandomN > 40f)
                {
                    Attack10Count = 0;
                    Attack10();
                }
                break;
        }
        currentAttack++;
    }
    void AttackCord()
    {
        inAttack = false;
        lastAttackTime = Time.time;
    }
    void Attack1()//直线弹幕 
    {
        for (float i = 0f; i < 8.0; i++)
        {
            SoundManager.PlayB_attack1();
            var fireball1 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
            fireball1.transform.rotation=Quaternion.Euler(0.0f,0.0f,i * 45.0f);
        }
        Attack1Count++;
        if(Attack1Count < Attack1Wave)
            Invoke("Attack1",Attack1TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack2()//螺旋弹幕
    {
        SoundManager.PlayB_attack1();
        var fireball2 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
        fireball2.transform.rotation=Quaternion.Euler(0.0f,0.0f,Attack2Count * Attack2Angel);
        Attack2Count++;
        if(Attack2Count < Attack2Wave)
            Invoke("Attack2",Attack2TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack3()//分裂弹幕
    {
        for (float i = 0f; i < 4.0; i++)
        {
            SoundManager.PlayB_attack1();
            var fireball3 = Instantiate(fireball02,transform.position,Quaternion.identity) as GameObject;
            fireball3.transform.rotation=Quaternion.Euler(0.0f,0.0f,45.0f + i * 90.0f);
            fireball3.GetComponent<Arrow2>().scale = Attack3scale;
            fireball3.transform.localScale = new Vector3(Attack3scale,Attack3scale,Attack3scale);
        }
        Invoke("AttackCord" ,4.5f);
    }
    void Attack4()//跟踪直线弹幕
    {
        SoundManager.PlayB_attack1();
        var fireball4 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
        fireball4.transform.up = (PlayerLocation.position - transform.position).normalized;
        Attack4Count++;
        if(Attack4Count < Attack4Wave)
            Invoke("Attack4",Attack4TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack5()//自狙跟踪弹幕 -中命中
    {
        for (float i = 0f; i < 6.0; i++)
        {
            SoundManager.PlayB_attack1();
            var fireball5 = Instantiate(fireball03,transform.position,Quaternion.identity) as GameObject;
            fireball5.transform.rotation=Quaternion.Euler(0.0f,0.0f,i * 60.0f);
        }
        Attack5Count++;
        if(Attack5Count < Attack5Wave)
            Invoke("Attack5",Attack5TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack6()//跟踪爆破
    {
        Instantiate(explode01,PlayerLocation.position,Quaternion.identity);
        Attack6Count++;
        if(Attack6Count < Attack6Wave)
            Invoke("Attack6",Attack6TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack7()//大范围随机爆破 -中命中
    {
        randomAttack = new Vector3(Random.Range(-6f,6f),Random.Range(-3f,3f),0f);
        var fire7 = Instantiate(explode01,transform.position + randomAttack,Quaternion.identity) as GameObject;
        fire7.transform.localScale = new Vector3(Attack7scale,Attack7scale,Attack7scale);
        Attack7Count++;
        if(Attack7Count % 4 == 0)
            Instantiate(explode01,PlayerLocation.position,Quaternion.identity);
        if(Attack7Count < Attack7Wave)
            Invoke("Attack7",Attack7TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack8()//4螺旋+1快速螺旋 -高命中
    {
        SoundManager.PlayB_attack1();
        for (float i = 0; i < 4.0f; i++)
        {
            var fireball8 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
            fireball8.transform.rotation=Quaternion.Euler(0.0f,0.0f,i*90.0f + Attack8Count * Attack8Angel);
        }
        var fireball8_ = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
        fireball8_.transform.rotation=Quaternion.Euler(0.0f,0.0f,Attack8Count * Attack8Angel * 4);
        Attack8Count++;
        if(Attack8Count < Attack8Wave)
            Invoke("Attack8",Attack8TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack9()//2相反螺旋+跟踪爆破 -高命中
    {
        SoundManager.PlayB_attack1();
        var fireball9 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
        fireball9.transform.rotation=Quaternion.Euler(0.0f,0.0f,Attack9Count * Attack9Angel * -1f);
        var fireball9_ = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
        fireball9_.transform.rotation=Quaternion.Euler(0.0f,0.0f,Attack9Count * Attack9Angel * 0.7f);
        if(Attack9Count % 15 == 0)
            Instantiate(explode01,PlayerLocation.position,Quaternion.identity);
        Attack9Count++;
        if(Attack9Count < Attack9Wave)
            Invoke("Attack9",Attack9TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack10()//随机慢速爆破+自机狙 -高命中
    {
        randomAttack = new Vector3(Random.Range(-5f,5f),Random.Range(-2f,2f),0f);
        var fire10 = Instantiate(explode01,transform.position + randomAttack,Quaternion.identity) as GameObject;
        fire10.transform.localScale = new Vector3(Attack10scale,Attack10scale,Attack10scale);
        for(float i = 0f; i < 3.0f ;i++)
        {
            SoundManager.PlayB_attack1();
            var fire10_ = Instantiate(fireball03,transform.position + randomAttack,Quaternion.identity) as GameObject;
            fire10_.transform.rotation=Quaternion.Euler(0.0f,0.0f,i * 120f);
        }
        Attack10Count++;
        if(Attack10Count < Attack10Wave)
            Invoke("Attack10",Attack10TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
    void Attack11()//随机分裂弹幕
    {
        for (float i = 0f; i < 3.0; i++)
        {
            SoundManager.PlayB_attack1();
            var fireball11 = Instantiate(fireball02,transform.position,Quaternion.identity) as GameObject;
            fireball11.transform.rotation=Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360.0f) + i * 120.0f);
            fireball11.GetComponent<Arrow2>().scale = Attack11scale;
            fireball11.GetComponent<Arrow2>().type2 = true;
            fireball11.transform.localScale = new Vector3(Attack11scale,Attack11scale,Attack11scale);
            Invoke("AttackCord" ,5f);
        }
    }
    void Attack12()//3发跟踪射击 -中命中
    {
        for (float i = 0f; i < 3.0; i++)
        {
            SoundManager.PlayB_attack1();
            var fireball12 = Instantiate(fireball01,transform.position,Quaternion.identity) as GameObject;
            fireball12.transform.up = (PlayerLocation.position - transform.position).normalized;
            fireball12.transform.Rotate(0.0f,0.0f,-15f + i*15f);
        }
        Attack12Count++;
        if(Attack12Count < Attack12Wave)
            Invoke("Attack12",Attack12TimeCD);
        else
        {
            inAttack = false;
            lastAttackTime = Time.time;
        }
    }
}
