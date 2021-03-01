using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Dialog : MonoBehaviour
{
    public string mes1;
    public string mes2;
    public string mes3;
    public string mes5;
    private GameObject DialogFrame;
    private GameObject TextFrame1;
    private GameObject TextFrame2;
    private GameObject TextFrame3;
    private GameObject abilityData;
    private Animator   DialogDisplay;
    private MeshRenderer meshRenderer;
    private GameObject PlayerGb;
    public GameObject Boss_bornGb;
    public bool inShop;
    private Vector3 fix;
    // Start is called before the first frame update
    void Start()
    {
        //mes = "按下'空格键'购买'生命药瓶'";
        DialogFrame = transform.GetChild(0).gameObject;
        TextFrame1 = transform.GetChild(1).gameObject;
        TextFrame2 = transform.GetChild(2).gameObject;
        TextFrame3 = transform.GetChild(3).gameObject;
        LayerFix(TextFrame1);
        LayerFix(TextFrame2);
        LayerFix(TextFrame3);
        DialogDisplay = DialogFrame.GetComponent<Animator>();
        inShop = false;
        PlayerGb = GameObject.FindGameObjectWithTag("Player");
        Globel.inBoss = false;
        Globel.inClear = false;
        if(Globel.isKey == false)
            mes1 = "按A键进行传送";
    }
    void LayerFix(GameObject TextFrame)
    {
        meshRenderer = TextFrame.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = 4;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Buy")&&inShop&&Globel.inClear)//通过boss
        {
            Debug.Log("G!");
            //储存临时数据----------------------------------------------------------------------------------------------------------
            
            Globel.HP_data = Globel.HP;
            Globel.Coin_data = Globel.Coin;
            Globel.AbilityNum_data = Globel.AbilityNum;
            Globel.MAXHP_data = Globel.MAXHP;//最大血量
            Globel.MAXArrmo_data = Globel.MAXArrmo;//最大护甲
            Globel.HP_Cover_data = Globel.HP_Cover;//血量自动恢复
            Globel.ATK_data = Globel.ATK;//攻击
            Globel.DEF_data = Globel.DEF;//防御
            Globel.Luck_data = Globel.Luck;//暴击率
            Globel.Conter_data = Globel.Conter;//减伤率
            Globel.Damege_data = Globel.Damege;//承伤率
            Globel.Speed_data = Globel.Speed;//奔跑速度
            Globel.Rollspeed_data = Globel.Rollspeed;//翻滚速度
            Globel.Arrowspeed_data = Globel.Arrowspeed;//箭矢速度
            Globel.Skill1Time_data = Globel.Skill1Time;//技能1持续
            Globel.Skill1CD_data = Globel.Skill1CD;//技能1CD
            Globel.Skill2Time_data = Globel.Skill2Time;//技能2持续
            Globel.Skill2CD_data = Globel.Skill2CD;//技能2CD
            Globel.Case8_data = Globel.Case8;//攻击回复 遗物特效
            Globel.Case10_data = Globel.Case10;//常驻剑技能 遗物特效
            Globel.Case15_data = Globel.Case15;//强化剑技能 遗物特效
            Globel.Case21_data = Globel.Case21;//根据血量坚守 遗物特效
            Globel.Case25_data = Globel.Case25;//根据盾技能加伤 遗物特效
            Globel.Case30_data = Globel.Case30;//随机伤害 遗物特效
            Globel.Case31_data = Globel.Case31;//根据护盾浑身 遗物特效
            Globel.Case34_data = Globel.Case34;//霸体 背水 遗物特效
            Globel.Case38_data = Globel.Case38;//暴击回复 遗物特效

            abilityData = GameObject.FindGameObjectWithTag("Data");
            for(int i = 0;i < Globel.AbilityNum;i++)
            {
                abilityData.GetComponent<AbilityData>().Datam1[i] = abilityData.GetComponent<AbilityData>().temp_Datam1[i];
                abilityData.GetComponent<AbilityData>().Datam2[i] = abilityData.GetComponent<AbilityData>().temp_Datam2[i];
                abilityData.GetComponent<AbilityData>().Datam3[i] = abilityData.GetComponent<AbilityData>().temp_Datam3[i];
                abilityData.GetComponent<AbilityData>().DataSprite[i] = abilityData.GetComponent<AbilityData>().temp_DataSprite[i];
            }
            for(int i = 0;i<=40 ;i++)
            {
                abilityData.GetComponent<AbilityData>().abilityCheck[i] = abilityData.GetComponent<AbilityData>().temp_abilityCheck[i];
            }
            //----------------------------------------------------------------------------------------------------------------------
            
            Globel.stage++;
            Globel.inEndRoom = false;
            SceneManager.LoadScene(1);
            //Destroy(gameObject);
        }
        if(Input.GetButtonDown("Buy")&&inShop&&Globel.inClear&&Globel.stage > 3)
        {
            Globel.stage = 1;
            Globel.inEndRoom = false;
            SceneManager.LoadScene(3);
        }
        if(Input.GetButtonDown("Buy")&&inShop&&!Globel.inClear&&!Globel.inBoss)
        {
            Debug.Log(1);
            BGMManager.PlayBoss();
            fix = new Vector3(0f,1.32f,0f);
            Instantiate(Boss_bornGb,transform.position+fix,Quaternion.identity);
            Globel.inBoss = true;
            DialogDisplay.SetTrigger("Undisplay");
            Invoke("Iv_undisplay" ,0.125f);
            inShop = false;
            
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("检测触发器靠近 inBoss"+Globel.inBoss);
        if(other.gameObject.CompareTag("Player") && !inShop &&!Globel.inBoss)
        {
            Debug.Log("检测玩家靠近");
            DialogDisplay.SetTrigger("Display");
            Invoke("Iv_display" ,0.125f);
            inShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && inShop &&!Globel.inBoss)
        {
            DialogDisplay.SetTrigger("Undisplay");
            Invoke("Iv_undisplay" ,0.125f);
            inShop = false;
        }
    }
    void Iv_display()
    {
        TextFrame1.GetComponent<TextMesh>().text = mes1;
        TextFrame2.GetComponent<TextMesh>().text = mes2;
        TextFrame3.GetComponent<TextMesh>().text = mes5;
        if(Globel.inClear)
        TextFrame3.GetComponent<TextMesh>().text = mes3;
        if(Globel.inClear && Globel.stage == 3)
        TextFrame3.GetComponent<TextMesh>().text = "清理完毕";
    }
    void Iv_undisplay()
    {
        TextFrame1.GetComponent<TextMesh>().text = "";
        TextFrame2.GetComponent<TextMesh>().text = "";
        TextFrame3.GetComponent<TextMesh>().text = "";
    }



    
}
