using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject StartSelect;
    void Start()
    {
        if(Globel.stage==1)
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        else
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        BGMManager.PlayUnbattle();
    }
    void Update()
    {
        if (Input.GetButton("Buy"))
        {
            Globel.isKey = false;
            Debug.Log("NS");
        }
        if(Input.GetKey(KeyCode.Return)||Input.GetMouseButton(0))
        {
            Globel.isKey = true;
            Debug.Log("Key");
        } 
    }
    public void PlayGame()
    {
        SoundManager.PlayP_select();
        
        Invoke("Iv_PlayGame",0.5f);
    }
    public void Iv_PlayGame()
    {
        //初始化
        Globel.stage = 1;//关卡
        Globel.killN = 0;//杀敌数
        Globel.Coin = -1;//金币
        Globel.HP = 1000 ;//生命值
        Globel.AbilityNum = 0;//初始化能力
        Globel.MAXHP = 1000;//最大血量
        Globel.MAXArrmo = 400;//最大护甲
        Globel.HP_Cover = 0;//血量自动恢复
        Globel.ATK = 30;//攻击
        Globel.DEF = 0;//防御
        Globel.Luck = 5;//暴击率
        Globel.Conter = 0;//减伤率
        Globel.Damege = 1.0f;//承伤率
        Globel.Speed = 3.0f;//奔跑速度
        Globel.Rollspeed = 5.0f;//翻滚速度
        Globel.Arrowspeed = 9.0f;//箭矢速度
        Globel.Skill1Time = 6.0f;//技能1持续
        Globel.Skill1CD = 12.0f;//技能1CD
        Globel.Skill2Time = 10.0f;//技能2持续 - 未开放
        Globel.Skill2CD = 20.0f;//技能1CD
        Globel.inClear = false;
        Globel.inBoss = false;

        Globel.Case8 = false;//攻击回复 遗物特效
        Globel.Case10 = false;//常驻剑技能 遗物特效
        Globel.Case15 = false;//强化剑技能 遗物特效
        Globel.Case21 = false;//根据血量坚守 遗物特效
        Globel.Case25 = false;//根据盾技能加伤 遗物特效
        Globel.Case30 = false;//随机伤害 遗物特效
        Globel.Case31 = false;//根据护盾浑身 遗物特效
        Globel.Case34 = false;//霸体 背水 遗物特效
        Globel.Case38 = false;//暴击回复 遗物特效
        if(Globel.Difficulty == 1)
        {
            Globel.MAXHP += 500;//最大血量
            Globel.MAXArrmo += 200;//最大护甲
            Globel.DEF += 5;//防御
            Globel.Coin += 501;//金币
            Globel.HP = Globel.MAXHP;

        }
        if(Globel.Difficulty == 2)
        {
            Globel.MAXHP += 1000;//最大血量
            Globel.MAXArrmo += 600;//最大护甲
            Globel.Coin += 1001;//金币
            Globel.DEF += 10;//防御
            Globel.Damege *= 0.75f;//承伤率
            Globel.HP = Globel.MAXHP;
            Globel.HP_Cover += 1;
        }
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SoundManager.PlayP_select();
        Invoke("Iv_QuitGame",0.5f);
    }
    public void Iv_QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SoundManager.PlayP_select();
        Invoke("Iv_ContinueGame",0.5f);
    }
    public void Iv_ContinueGame()
    {
        Globel.killN = 0;//杀敌数
        Globel.HP = Globel.HP_data;//血量
        Globel.Coin = Globel.Coin_data;//金币
        Globel.AbilityNum = Globel.AbilityNum_data;//能力数
        Globel.MAXHP = Globel.MAXHP_data;//最大血量
        Globel.MAXArrmo = Globel.MAXArrmo_data;//最大护甲
        Globel.HP_Cover = Globel.HP_Cover_data;//血量自动恢复
        Globel.ATK = Globel.ATK_data;//攻击
        Globel.DEF = Globel.DEF_data;//防御
        Globel.Luck = Globel.Luck_data;//暴击率
        Globel.Conter = Globel.Conter_data;//减伤率
        Globel.Damege = Globel.Damege_data;//承伤率
        Globel.Speed = Globel.Speed_data;//奔跑速度
        Globel.Rollspeed = Globel.Rollspeed_data;//翻滚速度
        Globel.Arrowspeed = Globel.Arrowspeed_data;//箭矢速度
        Globel.Skill1Time = Globel.Skill1Time_data;//技能1持续
        Globel.Skill1CD = Globel.Skill1CD_data;//技能1CD
        Globel.Skill2Time = Globel.Skill2Time_data;//技能2持续 - 未开放
        Globel.Skill2CD = Globel.Skill2CD_data;//技能1CD
        Globel.inClear = false;
        Globel.inBoss = false;

        Globel.Case8 = Globel.Case8_data;//攻击回复 遗物特效
        Globel.Case10 = Globel.Case10_data;//常驻剑技能 遗物特效
        Globel.Case15 = Globel.Case15_data;//强化剑技能 遗物特效
        Globel.Case21 = Globel.Case21_data;//根据血量坚守 遗物特效
        Globel.Case25 = Globel.Case25_data;//根据盾技能加伤 遗物特效
        Globel.Case30 = Globel.Case30_data;//随机伤害 遗物特效
        Globel.Case31 = Globel.Case31_data;//根据护盾浑身 遗物特效
        Globel.Case34 = Globel.Case34_data;//霸体 背水 遗物特效
        Globel.Case38 = Globel.Case38_data;//暴击回复 遗物特效
        SceneManager.LoadScene(1);
    }
    public void OpenSetting()
    {
        SoundManager.PlayP_select();
        transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
        transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
        transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = false;
        transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = false;
        transform.GetChild(1).gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(StartSelect);
    }
}
