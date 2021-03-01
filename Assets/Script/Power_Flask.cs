using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Flask : MonoBehaviour
{
    public string mes1;
    public string mes2;
    public string mes3;
    //public string mes4;
    public string mes5;
    public string sourceName;
    public int effect;
    private GameObject DialogFrame;
    private GameObject TextFrame1;
    private GameObject TextFrame2;
    private GameObject TextFrame3;
    private GameObject TextFrame4;
    private GameObject TextFrame5;
    private Animator   DialogDisplay;
    private MeshRenderer meshRenderer;
    public GameObject CostNumPrefab;
    private GameObject abilityData;
    public Sprite PowerSprite;
    public int CostNum;
    public bool inShop;
    // Start is called before the first frame update
    void Start()
    {
        //mes = "按下'空格键'购买'生命药瓶'";
        DialogFrame = transform.GetChild(0).gameObject;
        TextFrame1 = transform.GetChild(1).gameObject;
        TextFrame2 = transform.GetChild(2).gameObject;
        TextFrame3 = transform.GetChild(3).gameObject;
        TextFrame4 = transform.GetChild(4).gameObject;
        TextFrame5 = transform.GetChild(5).gameObject;
        LayerFix(TextFrame1);
        LayerFix(TextFrame2);
        LayerFix(TextFrame3);
        LayerFix(TextFrame4);
        LayerFix(TextFrame5);
        DialogDisplay = DialogFrame.GetComponent<Animator>();
        inShop = false;
        abilityData = GameObject.FindGameObjectWithTag("Data");
        do{
            effect = Random.Range(1,39);
        }while(abilityData.GetComponent<AbilityData>().temp_abilityCheck[effect]==1 || abilityData.GetComponent<AbilityData>().abilityCheck[effect]==1);
        abilityData.GetComponent<AbilityData>().temp_abilityCheck[effect] = 1;
        sourceName = "power (" + effect.ToString() + ")";
        PowerSprite = Resources.Load<Sprite>(sourceName);
        gameObject.GetComponent<SpriteRenderer>().sprite = PowerSprite;
        effectMes();
        TextFrame4.GetComponent<TextMesh>().text = CostNum.ToString() + "G";
        if(Globel.isKey == false)
            mes1 = "按A键购买";
        //effectMes();


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
        if(Input.GetButtonDown("Buy")&&inShop)
        {
            if(Globel.Coin - CostNum >= 0)
            {
                Debug.Log("G!");
                SoundManager.PlayP_shop();
                GameObject gb2 = Instantiate(CostNumPrefab,transform.position,Quaternion.identity) as GameObject;
                gb2.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + CostNum.ToString();
                Globel.Coin -= CostNum;
                UIManager.instance.UpdateAbility(PowerSprite,mes2,mes3,mes5);
                abilityData.GetComponent<AbilityData>().temp_abilityCheck[effect] = 1;
                Effect();   
                inShop = false;
                Destroy(gameObject);
            }
            else
            {
                GameObject gb3 = Instantiate(CostNumPrefab,transform.position,Quaternion.identity) as GameObject;
                gb3.transform.GetChild(0).GetComponent<TextMesh>().text = "No Money!";
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("检测触发器靠近");
        if(other.gameObject.CompareTag("Player") && !inShop)
        {
            Debug.Log("检测玩家靠近");
            DialogDisplay.SetTrigger("Display");
            Invoke("Iv_display" ,0.125f);
            inShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && inShop)
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
        TextFrame3.GetComponent<TextMesh>().text = mes3;
        TextFrame5.GetComponent<TextMesh>().text = mes5;
    }
    void Iv_undisplay()
    {
        TextFrame1.GetComponent<TextMesh>().text = "";
        TextFrame2.GetComponent<TextMesh>().text = "";
        TextFrame3.GetComponent<TextMesh>().text = "";
        TextFrame5.GetComponent<TextMesh>().text = "";
    }
    void Effect()
    {
        switch (effect)
        {
            case 1:
                // mes2 = "愤怒";
                // mes3 = "增加攻击";
                Globel.ATK += 10;
                break;
            case 2:
                // mes2 = "平静";
                // mes3 = "微略增加生命恢复";
                Globel.HP_Cover += 2;
                break;
            case 3:
                // mes2 = "轻松";
                // mes3 = "增加奔跑速度";
                // mes5 = "微略增加最大护盾";
                Globel.Speed += 1.5f;
                Globel.MAXArrmo += 200;
                break;
            case 4:
                // mes2 = "繁硕";
                // mes3 = "微略增加最大生命和护盾";
                // mes5 = "盾技能持续up";
                Globel.MAXHP += 125;
                Globel.MAXArrmo += 125;
                Globel.Skill1Time += 3f;
                break;
            case 5:
                // mes2 = "猛烈";
                // mes3 = "加深攻击量";
                Globel.ATK = Globel.ATK * 125 / 100;
                break;
            case 6:
                // mes2 = "尘烟";
                // mes3 = "增加翻滚距离";
                // mes5 = "微略增加最大生命";
                Globel.Rollspeed += 1.5f;
                Globel.MAXArrmo += 200;
                break;
            case 7:
                // mes2 = "喷气式";
                // mes3 = "加深奔跑速度量和翻滚距离量";
                // mes5 = "微略增加最大HP和最大护盾"
                Globel.Speed *= 1.25f;
                Globel.Rollspeed *= 1.25f;
                Globel.MAXArrmo = Globel.MAXArrmo * 125 / 100;
                Globel.MAXHP = Globel.MAXHP * 125 / 100;
                break;
            case 8:
                // mes2 = "韧性";
                // mes3 = "攻击时会回复血量";
                Globel.Case8 = true;
                break;
            case 9:
                // mes2 = "牛奶";
                // mes3 = "增加防御";
                //mes3 = "微略增加护盾";
                Globel.DEF += 15;
                Globel.MAXArrmo +=125;
                break;
            case 10:
                // mes2 = "皇家剑术";
                // mes3 = "常驻剑技能效果";
                Globel.Case10 = true;
                break;
            case 11:
                // mes2 = "禁忌之果";
                // mes3 = "加深受伤，加深攻击";
                Globel.Damege *= 1.2f;
                Globel.ATK = Globel.ATK * 145 / 100;
                break;
            case 12:
                // mes2 = "光滑表面";
                // mes3 = "增加减伤率";
                Globel.Conter += 0.2f;
                break;
            case 13:
                // mes2 = "羽毛";
                // mes3 = "奔跑速度翻倍 降低翻滚距离";
                // mes5 = "微略增加最大护盾";
                Globel.Speed *= 2f;
                Globel.Rollspeed *= 0.75f;
                Globel.MAXArrmo += 125;
                break;
            case 14:
                // mes2 = "血红刚石";
                // mes3 = "大量减少最大生命";
                // mes5 = "最大护盾翻倍";
                Globel.MAXHP -= 400;
                Globel.MAXArrmo *= 2;
                break;
            case 15:
                // mes2 = "偏折钻石";
                // mes3 = "强化剑能力";
                Globel.Case15 = true;
                break;
            case 16:
                // mes2 = "翻滚泥沙";
                // mes3 = "翻滚距离翻倍";
                // mes5 = "微略增加减伤";
                Globel.Rollspeed *= 2;
                Globel.Conter += 0.1f;
                break;
            case 17:
                // mes2 = "闪亮粉尘";
                // mes3 = "微略加深最大护盾";
                // mes5 = "微略增加减伤";
                Globel.MAXArrmo = Globel.MAXArrmo * 115 / 100;
                Globel.Conter += 0.1f;
                break;
            case 18:
                // mes2 = "沉重金石";
                // mes3 = "增加暴击 加深攻击";
                // mes5 = "降低奔跑速度和翻滚距离";
                Globel.ATK = Globel.ATK * 135 / 100;
                Globel.Luck += 20;
                Globel.Speed *= 0.8f;
                Globel.Rollspeed *= 0.8f;
                break;
            case 19:
                // mes2 = "沉重银石";
                // mes3 = "增加暴击和减伤";
                // mes5 = "降低奔跑速度和翻滚距离";
                Globel.Luck += 20;
                Globel.Conter += 0.15f;
                Globel.Speed *= 0.8f;
                Globel.Rollspeed *= 0.8f;
                break;
            case 20:
                // mes2 = "沉重铁石";
                // mes3 = "加深最大护盾 增加减伤";
                // mes5 = "降低奔跑速度和翻滚距离";
                Globel.Conter += 0.15f;
                Globel.MAXArrmo = Globel.MAXArrmo * 125 / 100;
                Globel.Speed *= 0.8f;
                Globel.Rollspeed *= 0.8f;
                break;
            case 21:
                // mes2 = "坚守骨灰";
                // mes3 = "效果受生命值影响";
                // mes5 = "随生命增加减伤";
                Globel.Case21 = true;
                break;
            case 22:
                // mes2 = "人参";
                // mes3 = "加深更多最大生命值";
                // mes5 = "并恢复其增加的一半";
                Globel.MAXHP = Globel.MAXHP * 160 / 100;
                Globel.HP += Globel.MAXHP / 8;
                break;
            case 23:
                // mes2 = "疯狂";
                // mes3 = "微略减少生命恢复";
                // mes5 = "加深更多攻击和暴击";    
                Globel.HP_Cover -= 1;
                Globel.ATK = Globel.ATK * 125 / 100;
                Globel.Luck += 35;
                break;
            case 24:
                // mes2 = "蜂蜜";
                // mes3 = "微略增加防御";
                // mes5 = "微略增加暴击";
                Globel.Luck += 10;
                Globel.DEF += 10;
                break;
            case 25:
                // mes2 = "冷静香草";
                // mes3 = "减少技能冷却";
                Globel.Skill1CD *= 0.85f;
                Globel.Skill2CD *= 0.85f;
                Globel.Case25 = true;
                break;
            case 26:
                // mes2 = "意志";
                // mes3 = "大幅度减少暴击";
                // mes5 = "增加更多减伤";
                Globel.Luck -= 50;
                Globel.Conter += 0.25f;
                break;
            case 27:
                // mes2 = "弱点基因";
                // mes3 = "降低攻击";
                // mes5 = "大幅度增加暴击";
                Globel.Luck += 50;
                Globel.ATK = Globel.ATK * 80 / 100;
                break;
            case 28:
                // mes2 = "魔法粉尘";
                // mes3 = "大幅度增加盾技能持续时间";
                // mes5 = "增加剑技能冷却时间";
                Globel.Skill1Time += 5f;
                Globel.Skill2CD += 5f;
                break;
            case 29:
                // mes2 = "缠绕";
                // mes3 = "降低更多弹道速度";
                // mes5 = "微略增加减伤";    
                Globel.Arrowspeed -= 4;
                Globel.Conter += 0.1f;
                break;
            case 30:
                // mes2 = "命运硬币10";
                // mes3 = "造成的伤害随机！";
                Globel.Case30 = true;
                break;
            case 31:
                // mes2 = "黑珍珠";
                // mes3 = "效果受护盾值影响";
                // mes5 = "随护盾增加伤害";
                Globel.Case31 = true;
                break;
            case 32:
                // mes2 = "紫水晶";
                // mes3 = "微略加深攻击";
                // mes5 = "微略增加暴击";
                Globel.Luck += 10;
                Globel.ATK = Globel.ATK * 115 / 100;
                break;
            case 33:
                // mes2 = "诅咒力量";
                // mes3 = "双倍伤害！";
                // mes5 = "双倍承伤！";
                Globel.Damege *= 2f;
                Globel.ATK = Globel.ATK * 2;
                break;
            case 34:
                // mes2 = "底力蘑菇";
                // mes3 = "不再僵直";
                // mes5 = "血量越低伤害越高";
                Globel.Case34 = true;
                break;
            case 35:
                // mes2 = "幸运三叶草";
                // mes3 = "增加暴击";
                Globel.Luck += 25;
                break;
            case 36:
                // mes2 = "过于幸运四叶草";
                // mes3 = "减少最大生命";
                // mes5 = "大幅度增加暴击";
                Globel.Luck += 50;
                Globel.MAXHP -= 250;
                break;
            case 37:
                // mes2 = "磐石";
                // mes3 = "降低更多奔跑速度和翻滚距离";
                // mes5 = "大幅度增加减伤";
                Globel.Conter += 0.35f;
                Globel.Speed *= 0.85f;
                Globel.Rollspeed *= 0.85f;
                break;
            case 38:
                // mes2 = "澎湃";
                // mes3 = "暴击时回复大量血量";
                Globel.Case38 = true;
                break;
            default:
            break;
        }
    }
    void effectMes()
    {
        switch (effect)
        {
            case 1:
                mes2 = "愤怒";
                mes3 = "微略增加攻击";
                break;
            case 2:
                mes2 = "平静";
                mes3 = "微略增加生命恢复";
                break;
            case 3:
                mes2 = "轻松";
                mes3 = "增加奔跑速度";
                mes5 = "增加最大护盾";
                CostNum -= 75;
                break;
            case 4:
                mes2 = "繁硕";
                mes3 = "微略增加最大生命和护盾";
                mes5 = "增加盾技能持续时间";
                CostNum += 25;
                break;
            case 5:
                mes2 = "猛烈";
                mes3 = "加深攻击量";
                break;
            case 6:
                mes2 = "尘烟";
                mes3 = "增加翻滚距离";
                mes5 = "增加最大护盾";  
                CostNum -= 75;                  
                break;
            case 7:
                mes2 = "喷气式";
                mes3 = "加深奔跑速度量和翻滚距离量";
                mes5 = "微略加深最大生命和护盾量";
                CostNum -= 75;
                break;
            case 8:
                mes2 = "韧性";
                mes3 = "攻击时会回复血量";
                CostNum += 25;
                break;
            case 9:
                mes2 = "牛奶";
                mes3 = "增加防御";
                mes5 = "微略增加最大护盾";
                break;
            case 10:
                mes2 = "皇家剑术";
                mes3 = "常驻剑技能效果";
                CostNum += 25;
                break;
            case 11:
                mes2 = "禁忌之果";
                mes3 = "加深受伤，加深攻击";
                break;
            case 12:
                mes2 = "光滑表面";
                mes3 = "增加减伤率";
                break;
            case 13:
                mes2 = "羽毛";
                mes3 = "奔跑速度翻倍 翻滚距离减半";
                mes5 = "微略增加最大护盾";
                CostNum -= 75;
                break;
            case 14:
                mes2 = "血红刚石";
                mes3 = "大量减少最大生命";
                mes5 = "最大护盾翻倍";
                CostNum -= 25;
                break;
            case 15:
                mes2 = "偏折钻石";
                mes3 = "强化剑能力";
                CostNum += 25;
                break;
            case 16:
                mes2 = "翻滚泥沙";
                mes3 = "翻滚距离翻倍";
                mes5 = "微略增加减伤";
                break;
            case 17:
                mes2 = "闪亮粉尘";
                mes3 = "微略加深最大护盾";
                mes5 = "微略增加减伤";
                break;
            case 18:
                mes2 = "沉重金石";
                mes3 = "增加暴击 加深攻击";
                mes5 = "降低奔跑速度和翻滚距离";
                CostNum += 50;
                break;
            case 19:
                mes2 = "沉重银石";
                mes3 = "增加暴击和减伤";
                mes5 = "降低奔跑速度和翻滚距离";
                CostNum += 25;
                break;
            case 20:
                mes2 = "沉重铁石";
                mes3 = "加深最大护盾 增加减伤";
                mes5 = "降低奔跑速度和翻滚距离";
                break;
            case 21:
                mes2 = "坚守骨灰";
                mes3 = "效果受生命值影响";
                mes5 = "生命越少 越增加减伤";
                CostNum += 25;
                break;
            case 22:
                mes2 = "人参";
                mes3 = "加深更多最大生命值";
                mes5 = "恢复其增加的一半";
                break;
            case 23:
                mes2 = "疯狂";
                mes3 = "减少生命恢复";
                mes5 = "加深更多攻击和暴击";    
                break;
            case 24:
                mes2 = "蜂蜜";
                mes3 = "微略增加防御";
                mes5 = "微略增加暴击";
                break;
            case 25:
                mes2 = "冷静香草";
                mes3 = "减少技能冷却";
                mes5 = "盾技能状态下获得大幅度增伤";
                CostNum += 25;
                break;
            case 26:
                mes2 = "意志";
                mes3 = "大幅度减少暴击";
                mes5 = "增加更多减伤";
                break;
            case 27:
                mes2 = "弱点基因";
                mes3 = "降低攻击";
                mes5 = "大幅度增加暴击";
                break;
            case 28:
                mes2 = "魔法粉尘";
                mes3 = "大幅度增加盾技能持续时间";
                mes5 = "增加剑技能冷却时间";
                CostNum += 44;
                break;
            case 29:
                mes2 = "缠绕";
                mes3 = "降低更多弹道速度";
                mes5 = "微略增加减伤";    
                break;
            case 30:
                mes2 = "命运硬币10";
                mes3 = "你造成的伤害随机！";
                CostNum -= 25;
                break;
            case 31:
                mes2 = "黑珍珠";
                mes3 = "效果受护盾值影响";
                mes5 = "护盾越高 越增加攻击";
                CostNum += 25;
                break;
            case 32:
                mes2 = "紫水晶";
                mes3 = "微略加深攻击";
                mes5 = "微略增加暴击";
                break;
            case 33:
                mes2 = "诅咒力量";
                mes3 = "双倍伤害！";
                mes5 = "双倍承伤！";
                CostNum += 50;
                break;
            case 34:
                mes2 = "怪异蘑菇";
                mes3 = "不再僵直";
                mes5 = "血量越低 越增加攻击";
                break;
            case 35:
                mes2 = "幸运三叶草";
                mes3 = "增加暴击";
                break;
            case 36:
                mes2 = "过于幸运四叶草";
                mes3 = "减少最大生命";
                mes5 = "大幅度增加暴击";
                CostNum -= 25;
                break;
            case 37:
                mes2 = "磐石";
                mes3 = "降低更多奔跑速度和翻滚距离";
                mes5 = "大幅度增加减伤";
                break;
            case 38:
                mes2 = "澎湃";
                mes3 = "暴击时回复大量血量";
                break;
            default:
            break;
        }        
    }

    
}
