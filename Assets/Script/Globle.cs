using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globel
{
    public static bool inClear = false;
    public static bool inBoss = false;
    public static bool inShop = false;
    public static bool inEndRoom = false;
    public static int killN = 0;//杀敌数 用于检测门的开启
    public static int stage = 1;//关卡数
    public static int Coin = -1;//金币数
    public static int Coin_data;
    public static int AbilityNum = 0;//能力数量
    public static int AbilityNum_data;
    public static int HP = 1000;//血量
    public static int HP_data;
    public static int Arrmo = 500;
    public static int MAXHP = 1000;//最大血量
    public static int MAXHP_data;
    public static int MAXArrmo = 400;//最大护甲
    public static int MAXArrmo_data;
    public static int HP_Cover = 0;//血量自动恢复
    public static int HP_Cover_data;
    public static int ATK = 30;//攻击
    public static int ATK_data;
    public static int DEF = 0;//防御
    public static int DEF_data;
    public static int Luck = 5;//暴击率
    public static int Luck_data;
    public static float Conter = 0;//减伤率
    public static float Conter_data;
    public static float Damege = 1.0f;//承伤率
    public static float Damege_data;
    public static float Speed = 3.0f;//奔跑速度
    public static float Speed_data;
    public static float Rollspeed = 5.0f;//翻滚速度
    public static float Rollspeed_data;
    public static float Arrowspeed = 8.0f;//箭矢速度
    public static float Arrowspeed_data;
    public static float Skill1Time = 6.0f;//技能1持续
    public static float Skill1Time_data;
    public static float Skill1CD = 12.0f;//技能1CD
    public static float Skill1CD_data;
    public static float Skill2Time = 10.0f;//技能2持续 - 未开放
    public static float Skill2Time_data;
    public static float Skill2CD = 20.0f;//技能2CD - 未开放
    public static float Skill2CD_data;
    
    public static bool Case8 = false;//攻击回复 遗物特效 Y
    public static bool Case10 = false;//常驻剑技能 遗物特效 Y
    public static bool Case15 = false;//强化剑技能 遗物特效 Y
    public static bool Case21 = false;//血量越低 减伤率越增加 遗物特效 Y
    public static bool Case25 = false;//盾技能下加伤 遗物特效 
    public static bool Case30 = false;//随机伤害 遗物特效 Y
    public static bool Case31 = false;//护盾值越高 攻击越增加 遗物特效 Y
    public static bool Case34 = false;//霸体 血量越低攻击越加深 遗物特效 Y
    public static bool Case38 = false;//暴击回复 遗物特效 Y
    public static bool Case8_data;
    public static bool Case10_data;
    public static bool Case15_data;
    public static bool Case21_data;
    public static bool Case25_data;
    public static bool Case30_data;
    public static bool Case31_data;
    public static bool Case34_data;
    public static bool Case38_data;

    public static bool inMenu;
    public static float BGMVolume = 0.75f;
    public static float SEVolume = 0.75f;

    public static int Difficulty = 0;
    public static int ScreenSetting = 0;

    public static bool isKey = false;
    
}
