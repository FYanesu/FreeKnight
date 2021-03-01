using UnityEngine;
using System.Collections;
using System.Collections.Generic;     

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_rollForce = 3.0f;//翻滚速度
    [SerializeField] bool       m_noBlood = false;//0血判定
    [SerializeField] float      m_attackRange = 1.0f;//攻击范围 -可能用不到
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private GameObject          AttackBox;
    public GameObject          floatPoint_R;//血量伤害
    public GameObject          floatPoint_B;//护盾伤害
    private Vector2             m_hitMove;//受击后退
    public bool                m_rolling = false;//是否在翻滚
    private bool                m_inblock = false;
    private bool                m_indeath = false;
    private bool                m_injured = false;
    //技能 护盾 相关的声明
    public bool                m_inSkill1 = false;
    public bool                m_canSkill1 = true;
    public GameObject           m_skill1Ring;
    public Color                m_skillRingCan;
    public Color                m_skillRingNot;
    public GameObject          m_skill1Shield;//持续护盾
    private float               m_skill1LastTime;
    private float               m_useTime;
    //技能 剑 相关的声明
    public bool                m_inSkill2 = false;
    public bool                m_canSkill2 = true;
    public GameObject           m_skill2Ring;
    public Color                m_skillRingCan2;
    public Color                m_skillRingNot2;
    public GameObject          m_skill2Shield;//持续护盾
    private float               m_skill2LastTime;
    private float               m_useTime2;
    public GameObject          m_skill2Sword;
    //
    public int                  m_arrmo;//当前护甲
    private int                 m_healthcover;
    private int                 m_arrmocover;

    public int                  m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_lastHurtTime;
    private float               m_arrmoCoverTime = 4.0f;
    private int                 m_hittime = 5;
    private enum m_State
    {
        IDLE,       //待机
        MOVE,       //移动
        ROLL,       //翻滚
        ATK,        //攻击
        DEF,        //防御
        BLOCK,      //格挡
        HIT,        //受伤
        DEATH       //挂了
    }
    private m_State m_currentState = m_State.IDLE;//当前状态
    
    public int                  m_facingDirection = 1;
    private float                 m_coverTime;
    private float               m_case21 = 0f;
    private float               m_Counter= 0f;
    private bool                m_case10 = false;
    private bool                m_defAudio = true;
    private bool                m_case25 = false;

    private string currentButton;//当前按下的按键  
    // 使用它进行初始化
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        AttackBox = transform.GetChild(1).gameObject;
        Globel.Arrmo = Globel.MAXArrmo;
        m_healthcover = Globel.MAXHP;
        m_arrmocover = Globel.MAXArrmo;
        
        m_skill1LastTime = -Globel.Skill1CD;
        m_skill1Ring = transform.GetChild(4).gameObject;
        m_skill1Shield = transform.GetChild(5).gameObject;
        m_skill1Ring.GetComponent<SpriteRenderer>().color = m_skillRingCan;

        m_skill2LastTime = -Globel.Skill2CD;
        m_skill2Ring = transform.GetChild(7).gameObject;
        m_skill2Shield = transform.GetChild(8).gameObject;
        m_skill2Ring.GetComponent<SpriteRenderer>().color = m_skillRingCan2;
        
        BGMManager.PlayBattle();
        
    }

    // 每帧调用一次Update
    void Update ()
    {
        
        //禁止举盾
        if(Globel.Arrmo <= 0)
        {
            m_animator.SetBool("Block", false);
        }
        //防止连续攻击
        if(m_hittime > 0)
            m_hittime--;
        
        //防止护甲溢出
        if(Globel.Arrmo > Globel.MAXArrmo)
            Globel.Arrmo = Globel.MAXArrmo;
        //连续攻击检测的时间
        m_timeSinceAttack += Time.deltaTime;
        //能力——血量自动恢复
        if(Globel.HP <= Globel.MAXHP && m_coverTime >= 1f && Globel.HP > 0)
        {
            Globel.HP += Globel.HP_Cover;
            m_coverTime = 0f;
        }
        m_coverTime += Time.deltaTime;
        //防止血量溢出
        if(Globel.HP > Globel.MAXHP)
            Globel.HP = Globel.MAXHP;    
        // -- 处理输入和移动 --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // 左右反向
        if (inputX > 0 && (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE) && !Globel.inMenu)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0 && (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE) && !Globel.inMenu)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
        
        // 移动
        if (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE)
        {
            m_currentState = m_State.MOVE;
            m_body2d.velocity = new Vector2(inputX * Globel.Speed, inputY * Globel.Speed / 2);

        }
            
        //受伤判定
        if(m_injured)
        {
            
            if(Globel.Arrmo == 0)
                Globel.HP -= 200;
            else if(Globel.Arrmo < 200 && Globel.Arrmo != 0)
                Globel.Arrmo = 0;
            else if(Globel.Arrmo >= 200)
                Globel.Arrmo -= 200;
            m_injured = false;
        }
        //死亡判定
        if(m_currentState != m_State.DEATH && Globel.HP <= 0)
        {
            m_currentState = m_State.DEATH;
            m_indeath = true;
        }
        if(m_indeath)
        {
            m_animator.SetTrigger("Death");
            m_indeath = false;
        }
        //护甲恢复
        if(Time.time - m_lastHurtTime > m_arrmoCoverTime && Globel.HP > 0)
        {
            Globel.Arrmo += 2;
            if(Globel.Arrmo > Globel.MAXArrmo)
                Globel.Arrmo = Globel.MAXArrmo;
        }
        // -- 控制动画 ---------------------------------------------------------------------------------
        
        
        //技能1------------------------------------
        if (Input.GetButtonDown("Skill1") && m_canSkill1 && !Globel.inMenu && Globel.HP > 0)
        {
            SoundManager.PlayP_skill();
            m_skill1LastTime = Time.time;
            m_inSkill1 = true;
            m_skill1Shield.SetActive(true);
            m_canSkill1 = false;
            m_skill1Ring.GetComponent<SpriteRenderer>().color = m_skillRingNot;
            if(Globel.Case25)
            {
                Globel.ATK += 20;
                m_case25 = true;
            }
            Invoke("outSkill",Globel.Skill1Time);
            Invoke("canSkill",Globel.Skill1CD);
        }
        //-------------------------------------------------
        //技能2------------------------------------
        if (Input.GetButtonDown("Skill2") && m_canSkill2 && !Globel.Case10 && !Globel.inMenu && Globel.HP > 0)
        {
            SoundManager.PlayP_skill();
            m_skill2LastTime = Time.time;
            m_inSkill2 = true;
            m_skill2Shield.SetActive(true);
            m_canSkill2 = false;
            m_skill2Ring.GetComponent<SpriteRenderer>().color = m_skillRingNot2;
            Invoke("outSkill2",Globel.Skill2Time);
            Invoke("canSkill2",Globel.Skill2CD);
        }
        if(Globel.Case10 && !m_case10)//剑技能常驻 遗物特效
        {
            m_case10 = true;
            m_inSkill2 = true;
            m_skill2Shield.SetActive(true);
            m_canSkill2 = false;
        }
        //-------------------------------------------------
            

        //攻击
        else if(Input.GetButtonDown("Attack") && m_timeSinceAttack > 0.25f && (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE) && !Globel.inMenu)
        {
            SoundManager.PlayP_sword();
            m_hitMove = new Vector2 (m_facingDirection * Globel.Speed * Time.deltaTime/3,0);
            //transform.Translate(m_facingDirection * Globel.Speed * Time.deltaTime/3,0,0);
            m_currentState = m_State.ATK;
            m_currentAttack++;
            if(m_inSkill2 && !Globel.Case15)//技能2使用中----------------------------------
            {
                SoundManager.PlayP_attack();
                var sk2 = Instantiate(m_skill2Sword,transform.GetChild(6).transform.position,Quaternion.identity) as GameObject;
                if(m_facingDirection == -1)
                sk2.transform.rotation=Quaternion.Euler(0.0f,180.0f,0.0f);
            }
            if(m_inSkill2 && Globel.Case15)//强化剑技能 遗物特效
            {
                for(float i = 0f;i < 3f;i++)
                {
                    SoundManager.PlayP_attack();
                    var sk2_ = Instantiate(m_skill2Sword,transform.GetChild(6).transform.position,Quaternion.identity) as GameObject;
                    if(m_facingDirection == -1)
                        sk2_.transform.rotation=Quaternion.Euler(0.0f,180.0f,-15.0f + i * 15.0f);
                    else
                        sk2_.transform.rotation=Quaternion.Euler(0.0f,0f,-15.0f + i * 15.0f);
                }
            }
            // 第三次攻击后循环回来
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // 攻击间隔过长
            if (m_timeSinceAttack > 1.0f)
            {
                m_currentAttack = 1;
            }
                

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            
            // 重置时间
            m_timeSinceAttack = 0.0f;
        }

        // 防御

        else if (Input.GetButton("Defend") && (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE) && Globel.Arrmo > 0 && Globel.HP > 1 && !Globel.inMenu)
        {
            if(m_defAudio)
            {
                SoundManager.PlayP_defend();
                m_defAudio = false;
            }
            m_currentState = m_State.DEF;
            m_animator.SetBool("Block", true);
            m_inblock = true;
            
        }
        else if (!(Input.GetButton("Defend")) && m_currentState == m_State.DEF)
        {
            m_defAudio = true;
            m_currentState = m_State.IDLE;
            m_animator.SetBool("Block", false);
            m_inblock = false;
        }
           

        // 翻滚
        else if (Input.GetButtonDown("Roll") && (m_currentState == m_State.IDLE || m_currentState == m_State.MOVE) && !Globel.inMenu)
        {
            SoundManager.PlayP_roll();
            m_currentState = m_State.ROLL;
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            
        }
        else if(m_currentState == m_State.ROLL)
        {
            if((Mathf.Abs(inputX) + Mathf.Abs(inputY)) < Mathf.Epsilon)
            m_body2d.velocity = new Vector2(m_facingDirection * Globel.Rollspeed , inputY * Globel.Rollspeed / 2);
            else
            m_body2d.velocity = new Vector2(inputX * Globel.Rollspeed , inputY * Globel.Rollspeed / 2);
        }

        // 跑
        else if ((Mathf.Abs(inputX) + Mathf.Abs(inputY)) > Mathf.Epsilon)
        {
            
            // 重置时间
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        
        //待机
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
        //血条UI显示
        if(m_healthcover > Globel.HP)
            m_healthcover -= 3;
        if(m_healthcover < Globel.HP)
            m_healthcover = Globel.HP;
        if(m_arrmocover > Globel.Arrmo)
            m_arrmocover -= 3;
        if(m_arrmocover < Globel.Arrmo)
            m_arrmocover = Globel.Arrmo;
        UIManager.instance.UpdateHpBar(Globel.HP,m_healthcover,Globel.MAXHP);
        UIManager.instance.UpdateArrmoBar(m_arrmo,m_arrmocover,Globel.MAXArrmo);

        //技能1UI显示 
        m_useTime = Time.time - m_skill1LastTime;
        UIManager.instance.UpdateSkillRing(Globel.Skill1Time, Globel.Skill1CD, m_useTime);
        //技能2UI显示 
        m_useTime2 = Time.time - m_skill2LastTime;
        UIManager.instance.UpdateSkillRing2(Globel.Skill2Time, Globel.Skill2CD, m_useTime2);
    }

    // 动画事件
    // 在滚动结束后调用
    void AE_ResetIdle()
    {
        m_defAudio = true;
        m_currentState = m_State.IDLE;
        m_rolling = false;
        
    }
    void AE_ResetBlock()
    {
        m_defAudio = true;
        if(Globel.Arrmo > 0)
            m_animator.SetBool("Block", true);
        else
            m_animator.SetBool("Block", false);
        m_currentState = m_State.DEF;
        
    }
    public void AE_Attack()
    {
        AttackBox.GetComponent<Player_Attack>().Abox1();
    }
    public void AE_ResetAttack()
    {
        m_defAudio = true;
        AttackBox.GetComponent<Player_Attack>().resetAbox1();
    }
    //延迟执行
    public void outSkill()
    {
        Debug.Log("技能持续时间结束");
        if(Globel.Case25 && m_case25)
        {
            Globel.ATK -= 20;
            m_case25 = false;
        }
        m_inSkill1 = false;
        m_skill1Shield.SetActive(false);
    }
    public void canSkill()
    {
        Debug.Log("能使用技能");
        m_canSkill1 = true;
        m_skill1Ring.GetComponent<SpriteRenderer>().color = m_skillRingCan;
    }
    public void outSkill2()
    {
        Debug.Log("技能持续时间结束");
        m_inSkill2 = false;
        m_skill2Shield.SetActive(false);
    }
    public void canSkill2()
    {
        Debug.Log("能使用技能");
        m_canSkill2 = true;
        m_skill2Ring.GetComponent<SpriteRenderer>().color = m_skillRingCan2;
    }
    //------------
    public void beHited(int atk,int face)
    {
        //Debug.Log("被打了");
        if(Globel.Case21)//根据血量坚守 遗物特效
            m_case21 = ((float)(Globel.MAXHP - Globel.HP)/(float)(Globel.MAXHP)) * 42 / 100;//血量越少 减伤越高
        m_Counter = m_case21 + Globel.Conter;//减伤率计算
        atk = (int)((atk - Globel.DEF) * (1f - m_Counter));//防御 与 减伤计算
        if(m_Counter >= 1f)//防止减伤溢出
            atk = 1;
        atk = (int)((float)atk * Globel.Damege);//承伤比计算
        if(m_currentState != m_State.DEATH && m_hittime == 0 && m_currentState != m_State.ROLL)
        {
            if(face==0)face = -m_facingDirection;//被弓箭击中
            m_hittime = 5;
            m_lastHurtTime = Time.time;//刷新护甲回复
            if(m_inSkill1)
            {  
                SoundManager.PlayP_block();
                if(Globel.Arrmo <= 0)
                {
                    GameObject gb = Instantiate(floatPoint_R,transform.GetChild(0).transform.position,Quaternion.identity) as GameObject;
                    gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                    Globel.HP -= atk;
                }
                if(Globel.Arrmo > 0)
                {
                    GameObject gb = Instantiate(floatPoint_B,transform.GetChild(0).transform.position,Quaternion.identity) as GameObject;
                    gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                    Globel.Arrmo -= atk;
                    if(Globel.Arrmo < 0)
                        Globel.Arrmo = 0;
                }  
            }
            else if((m_currentState == m_State.DEF || m_currentState == m_State.BLOCK )&& Globel.Arrmo > 0)
            {
                SoundManager.PlayP_block();
                if(!Globel.Case34)
                {
                    m_currentState = m_State.BLOCK;  
                    m_animator.SetTrigger("BlockHurt");
                    m_animator.SetBool("Block", false);
                    Invoke("AE_ResetBlock",0.4f);
                    m_hitMove = new Vector2 (face / 8, 0);
                    m_body2d.velocity = m_hitMove;
                } 
                GameObject gb = Instantiate(floatPoint_B,transform.GetChild(0).transform.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                Globel.Arrmo -= atk;
                if(Globel.Arrmo < 0)
                    Globel.Arrmo = 0;
                
            }
            else
            {   
                SoundManager.PlayP_hurt();
                m_animator.SetBool("Block", false);
                if(!Globel.Case34)
                {
                    m_currentState = m_State.HIT;
                    m_animator.SetTrigger("Hurt");
                    m_hitMove = new Vector2 (face / 8, 0);
                    m_body2d.velocity = m_hitMove;
                    //transform.Translate(m_hitMove / 8 );
                }
                GameObject gb = Instantiate(floatPoint_R,transform.GetChild(0).transform.position,Quaternion.identity) as GameObject;
                gb.transform.GetChild(0).GetComponent<TextMesh>().text = atk.ToString();
                Globel.HP -= atk;
                Invoke("AE_ResetIdle",0.4f);
            }
            

        } 
    }

}
