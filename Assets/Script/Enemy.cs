using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
        
    [SerializeField] int            g_maxhealth;//最大生命值-可修改
    [SerializeField] float          g_speed;//速度-可修改
    [SerializeField] float          g_WarningRange;//警戒范围-可修改
    [SerializeField] float          g_AttackRange;//攻击范围-可修改
    [SerializeField] float          g_orderCD;//行动切换的CD-可修改
    [SerializeField] float          g_AttackCD;//攻击间隔-可修改
    [SerializeField] float          g_AttackRange_Y;//y轴攻击范围-可修改
    private Animator                g_animator;//动画组件
    private Rigidbody2D             g_body2d;//刚体组件
    private GameObject              PlayerUnit;
    private GameObject              AttackBox;
    public GameObject               floatPoint;//伤害数值预制体-外露
    public GameObject               floatPoint_O;//暴击伤害数值预制体-外露
    public GameObject               floatPoint_G;//吸血回复数值预制体-外露
    public GameObject               dropCoin;//掉落金币预制体-外露
    public GameObject               ArrowPrefab;//箭矢预制体-外露
    public GameObject               FirePrefab;//火球预制体-外露
    public GameObject               FirePrefab2;//火球预制体2-外露
    public GameObject               ExpolePrefab;//爆破预制体-外露
    private Transform               PlayerLocation;//玩家脚底位置
    private Transform               thisLocation;//怪物脚底位置
    private Vector3                 randomCoin;//金币随机散落的位置
    private Vector2                 g_hitMove;//被击退的方向
    private Vector2                 g_randomMove;//随机行动的方向
    private Vector2                 g_chaseMove;//追赶方向
    public int                     g_health;//当前生命
    public int                      g_attack;//攻击-可修改
    public int                      g_faceDirection = 1;//朝向-外露
    private float                   g_distanceToPlayer;//和玩家的距离
    private float                   g_distanceToPlayer_Y;//和玩家Y轴差
    private float                   g_chaseMove_Y;//向Y轴移动
    private float                   g_lastOrderTime;//上次指令的时间
    private float                   g_lastAttackTime;//上次攻击的时间
    private int                     g_hittime = 0;//被击后的无敌时间
    public int                      dropCoinNumber;//掉落硬币的数量-可修改
    public float[]                  g_actionWeight = { 2000 ,5000 };//待机和移动行为权重-可修改
    private float                   g_Weightnumber;//行为数量
    private bool                    g_death = false;
    private bool                    g_hurt = false;
    private bool                    g_chase = false;
    private bool                    g_inAttack = false;
    private bool                    g_inDeath = false;
    public string                   g_name;//怪物名-外露
    public bool                     g_hard = false;//硬直-可修改
    public bool                     g_notArcher = true;//是否是远程兵 - 可修改
    private int                     g_luckCheck;
    private bool                    g_luckHit;
    private enum g_State
    {
        IDLE,       //待机
        WALK,       //移动
        CHASE,      //追击玩家
        ATTACK,     //攻击
        HIT,
        DEATH
    }
    private g_State g_currentState = g_State.IDLE;

    public void Start()
    {
        //获得玩家和怪物的脚底坐标
        PlayerUnit = GameObject.FindGameObjectWithTag("Player");
        PlayerLocation = PlayerUnit.transform.GetChild(0).transform;
        thisLocation = this.gameObject.transform.GetChild(0).transform;
        g_animator = GetComponent<Animator>();
        g_maxhealth = (int)((float)g_maxhealth * (0.65f + (float)Globel.stage * 0.35f));
        g_health = g_maxhealth;
        g_body2d = GetComponent<Rigidbody2D>();
        if(g_notArcher)AttackBox = transform.GetChild(1).gameObject;
        if(!g_notArcher)PlayerLocation = PlayerUnit.transform.GetChild(6).transform;
        RandomOrder();
    }

    public void EnemydistanceCheck()//距离检测
    { 
        //更新和玩家的位置
        g_distanceToPlayer = Vector2.Distance(PlayerLocation.position, thisLocation.position);
        g_distanceToPlayer_Y = Mathf.Abs(PlayerLocation.position.y - thisLocation.position.y);
        //判断是否进入攻击状态
        if(g_distanceToPlayer <= g_AttackRange && g_distanceToPlayer_Y <= g_AttackRange_Y && g_health > 0)
        {
            //Debug.Log("attack");
            if(PlayerLocation.position.x - thisLocation.position.x > 0 && !g_inAttack)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                g_faceDirection = 1;
            }
            else if(PlayerLocation.position.x - thisLocation.position.x <= 0 && !g_inAttack)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                g_faceDirection = -1;
            }
            g_lastAttackTime = Time.time;
            g_animator.SetInteger(g_name+"_State",0);
            g_animator.SetTrigger(g_name+"_Attack");
            g_currentState = g_State.ATTACK;
            g_chase = false;
            g_inAttack = true;
        }
        //判断是否进入追逐状态
        else if(g_health > 0 && (g_distanceToPlayer <= g_WarningRange &&g_distanceToPlayer > g_AttackRange)||(g_distanceToPlayer <= g_AttackRange && g_distanceToPlayer_Y > g_AttackRange_Y))
        {
            g_animator.SetInteger(g_name+"_State",1);
            g_currentState = g_State.CHASE;
            g_chase = true;
            g_inAttack = false;
        }
        //判断是否远离警戒范围
        else if(g_health > 0 && g_distanceToPlayer > g_WarningRange && (g_currentState == g_State.CHASE||g_currentState == g_State.ATTACK))
        {
            g_animator.SetInteger(g_name+"_State",0);
            g_currentState = g_State.IDLE;
            g_chase = false;
            g_inAttack = false;
        }

    }

    public void RandomOrder()//随机行动
    {
        g_lastOrderTime = Time.time;//更新最后一次行动时间设置
        g_Weightnumber = Random.Range(0,g_actionWeight[0] + g_actionWeight[1]);//行动权重
        //行为1：此处为待机
        if(g_Weightnumber < g_actionWeight[0] && g_health > 0)
        {
            g_currentState = g_State.IDLE;
            g_animator.SetInteger(g_name+"_State",0);
        }
        //行为2：此处为漫步
        else if(g_Weightnumber < g_actionWeight[1] && g_Weightnumber > g_actionWeight[0] && g_health > 0)
        {
            g_currentState = g_State.WALK;
            g_animator.SetInteger(g_name+"_State",1);
            g_randomMove = new Vector2(Random.Range(-2f,2f),Random.Range(-2f,2f));
            if(g_randomMove.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                g_faceDirection = 1;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                g_faceDirection = -1;
            }
            g_randomMove.Normalize();
        }
    }
    // Update is called once per frame
    public void Update()
    {
        if(g_hittime > 0)
            g_hittime--;
        switch (g_currentState)
        {
            case g_State.IDLE:
                
                if(Time.time - g_lastOrderTime > g_orderCD)
                    RandomOrder();
                EnemydistanceCheck();
                break;
            case g_State.WALK:
                g_body2d.velocity = g_randomMove * g_speed;
                //transform.Translate(g_randomMove * Time.deltaTime * g_speed / 4); 
                if(Time.time - g_lastOrderTime > g_orderCD)
                    RandomOrder();
                EnemydistanceCheck();
                break;
            case g_State.CHASE:
                g_distanceToPlayer_Y = Mathf.Abs(PlayerLocation.position.y - thisLocation.position.y);
                g_chaseMove = PlayerLocation.position - thisLocation.position;
                if(PlayerLocation.position.y > thisLocation.position.y)
                    g_chaseMove_Y = 1;
                else
                    g_chaseMove_Y = -1;
                if(g_chaseMove.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    g_faceDirection = 1;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    g_faceDirection = -1;
                }
                g_chaseMove.Normalize();
                g_body2d.velocity = g_chaseMove * g_speed;
                //transform.Translate(g_chaseMove * Time.deltaTime * g_speed / 8) ;
                g_animator.SetInteger(g_name+"_State",1);
                EnemydistanceCheck();
                break;
            case g_State.ATTACK:
                if(g_inAttack != true && Time.time - g_lastAttackTime > g_AttackCD && g_health > 0)
                EnemydistanceCheck();
                break;
            case g_State.DEATH:
                if(g_inDeath)
                {
                    g_inDeath = false;
                    g_animator.SetTrigger(g_name+"_Death");  
                }
                break;     
        }
    }

    //动画事件
    void AE_ResetHit()
    {
        g_currentState = g_State.IDLE;
    }
    void AE_ResetAttack()
    {
        g_inAttack = false;
    }
    public void AE_Attack()
    {
        SoundManager.PlayG_attack();
        AttackBox.GetComponent<Monster_Attack>().Abox1();
    }
    public void AE_Attack_Archer()
    {
        if(g_name == "Mushroom")
        {
            SoundManager.PlayB_attack1();
            var Fire = Instantiate(FirePrefab, thisLocation.position, Quaternion.identity);
            var directionTo = (PlayerLocation.position - thisLocation.position).normalized;//朝向跟踪
            Fire.GetComponent<Arrow2>().scale = 2f;
            Fire.transform.localScale = new Vector3(2f,2f,2f);
            Fire.transform.up = directionTo;
        }
        else
        {
            SoundManager.PlayG_arrow();
            var Arrow = Instantiate(ArrowPrefab, thisLocation.position, Quaternion.identity);
            var directionTo2 = (PlayerLocation.position - thisLocation.position).normalized;
            Arrow.transform.up = directionTo2;
        }
    }

    void AE_Destroy() {
        if(g_name == "Skeleton")
        {
            var fix = new Vector3(0f,1f,0f);
            var Fire2 = Instantiate(ExpolePrefab, transform.position + fix, Quaternion.identity);
            Fire2.transform.localScale = new Vector3(3f,3f,3f);
        }
        if(g_name == "Bat" && !g_notArcher)
        {
            for(float i = 0f;i < 3f;i++)
            {
                SoundManager.PlayB_attack1();
                var Fire2 = Instantiate(FirePrefab2, thisLocation.position, Quaternion.identity);
                var directionTo = (thisLocation.position - PlayerLocation.position).normalized;
                Fire2.transform.up = directionTo;
                Fire2.transform.Rotate(0.0f,0.0f,-15f + i*15f);
                Fire2.transform.localScale = new Vector3(1f,1f,1f); 
            }
        }
        while(dropCoinNumber > 0)
        {
            dropCoinNumber--;
            randomCoin = new Vector3(Random.Range(-1f,1f),Random.Range(-0.5f,0.5f),0f);
            Instantiate(dropCoin, transform.position+randomCoin, Quaternion.identity);
        }
        Globel.killN++;
        Debug.Log(g_name+" kill "+Globel.killN);
        Destroy(gameObject);
    }
    //受击判定
    public void beHited(int atk,int face)
    {
        //Debug.Log(g_name+"被打了");
        
        if(g_currentState != g_State.DEATH && g_hittime == 0 && g_health > 0)
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
            SoundManager.PlayG_hurt();
            g_hittime = 5;
            if(face==0)face = -g_faceDirection;//被弓箭击中
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
            if(g_hard == false && g_health > 0)//怪物是否霸体
            {
                g_animator.SetTrigger(g_name+"_Hit");
                g_currentState = g_State.HIT;
            }
            g_hitMove = new Vector2 (face / 10, 0);
            g_body2d.velocity = g_hitMove;
            //transform.Translate(g_hitMove  );
            if(g_health <= 0)
            {
                g_currentState = g_State.DEATH;
                g_inDeath = true;
            }
        } 
    }
}
