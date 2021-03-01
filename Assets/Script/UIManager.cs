using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get;private set;}

    public Image HpBar;
    public Image HpBar_Cover;
    public Image ArrmoBar;
    public Image ArrmoBar_Cover;

    public Image skillTimeRing;
    public Image skillCDRing;

    public Image skillTimeRing2;
    public Image skillCDRing2;

    public GameObject AbilityPosition;
    public GameObject AbilityIcon;
    public GameObject abilityData;
    public Vector3 fixPosition;

    public GameObject BossHPFrame;
    public Image BossHPBar;
    public Image BossHPBar_Cover;
    void Start()
    {
        instance = this;
        abilityData = GameObject.FindGameObjectWithTag("Data");
        for(int i = 0;i < Globel.AbilityNum;i++)
        {
            AbilityPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(270+22*i,-11);
            var init = Instantiate(AbilityIcon, AbilityPosition.transform.position, Quaternion.identity,transform.parent.transform);
            init.GetComponent<Power_Dialog>().mes1 = abilityData.GetComponent<AbilityData>().Datam1[i];
            init.GetComponent<Power_Dialog>().mes2 = abilityData.GetComponent<AbilityData>().Datam2[i];
            init.GetComponent<Power_Dialog>().mes3 = abilityData.GetComponent<AbilityData>().Datam3[i];
            init.GetComponent<Power_Dialog>().AbilitySprite = abilityData.GetComponent<AbilityData>().DataSprite[i];
        }

    }
    public void UpdateHpBar(int curAmount,int curAmount_Cover,int maxAmount)
    {
        HpBar.fillAmount = (float)curAmount / (float)maxAmount;
        HpBar_Cover.fillAmount = (float)curAmount_Cover / (float)maxAmount;
    }
    public void UpdateArrmoBar(int curAmount,int curAmount_Cover,int maxAmount)
    {
        ArrmoBar.fillAmount = (float)curAmount / (float)maxAmount;
        ArrmoBar_Cover.fillAmount = (float)curAmount_Cover / (float)maxAmount;
    }
    public void UpdateSkillRing(float skillTime, float skillCD, float useTime)
    {
        if(useTime >= skillCD)
            skillTimeRing.fillAmount = 1f;
        else if(useTime >= skillTime)
            skillTimeRing.fillAmount = 0f;
        else
            skillTimeRing.fillAmount = (skillTime - useTime)/skillTime;

        if(useTime >= skillCD)
            skillCDRing.fillAmount = 1f;
        else
            skillCDRing.fillAmount = useTime/skillCD;
    }
    public void UpdateSkillRing2(float skillTime, float skillCD, float useTime)
    {
        if(useTime >= skillCD)
            skillTimeRing2.fillAmount = 1f;
        else if(useTime >= skillTime)
            skillTimeRing2.fillAmount = 0f;
        else
            skillTimeRing2.fillAmount = (skillTime - useTime)/skillTime;

        if(useTime >= skillCD)
            skillCDRing2.fillAmount = 1f;
        else
            skillCDRing2.fillAmount = useTime/skillCD;
    }
    public void UpdateAbility(Sprite abilitySprite,string m1,string m2,string m3)
    {
        //fixPosition = new Vector3(22 * AbilityNum,0f,0f);
        //AbilityPosition.transform.Translate(22 * AbilityNum,0,0);
        AbilityPosition.GetComponent<RectTransform>().anchoredPosition = new Vector2(270+22*Globel.AbilityNum,-11);
        var ab = Instantiate(AbilityIcon, AbilityPosition.transform.position, Quaternion.identity,transform.parent.transform);
        ab.GetComponent<Power_Dialog>().mes1 = m1;
        ab.GetComponent<Power_Dialog>().mes2 = m2;
        ab.GetComponent<Power_Dialog>().mes3 = m3;
        ab.GetComponent<Power_Dialog>().AbilitySprite = abilitySprite;
        abilityData.GetComponent<AbilityData>().temp_Datam1[Globel.AbilityNum] = m1;
        abilityData.GetComponent<AbilityData>().temp_Datam2[Globel.AbilityNum] = m2;
        abilityData.GetComponent<AbilityData>().temp_Datam3[Globel.AbilityNum] = m3;
        abilityData.GetComponent<AbilityData>().temp_DataSprite[Globel.AbilityNum] = abilitySprite;
        //DontDestroyOnLoad(ab);
        Globel.AbilityNum++;
    }
    public void CreatBossHPBar()
    {
        BossHPFrame.SetActive(true);
        BossHPFrame.GetComponent<Animator>().SetTrigger("Enter");
    }
    public void UpdateBossHpBar(int curAmount,int curAmount_Cover,int maxAmount)
    {
        BossHPBar.fillAmount = (float)curAmount / (float)maxAmount;
        BossHPBar_Cover.fillAmount = (float)curAmount_Cover / (float)maxAmount;
    }
    public void ExitBossHPBar()
    {
        BossHPFrame.GetComponent<Animator>().SetTrigger("Exit");
    }
}
