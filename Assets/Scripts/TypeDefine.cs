using UnityEngine;
using System.Collections;

public class TypeDefine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public static class Common{
    public static int HeroClassCount = 10;
    public static int RewardCount    = 5;

}

public enum HeroClass
{
    None,           //0默认值
    Warrior,        //1战士
    Mage,           //2法师
    Priest,         //3牧师
    Hunter,         //4猎人
    Rogue,          //5盗贼
    Shaman,         //6萨满
    Paladin,        //7圣骑士
    Druid,          //8德鲁伊
    Warlock,        //9术士
    DeathKnight,    //10死亡骑士
}

public enum Camp
{
    None,   //默认值
    Ally,   //友军
    Enemy,  //敌人
}

public enum AttackType
{
    None,   //0默认值
    Melee,  //1近战物理攻击
    Range,  //2远程物理攻击
    Magic,  //3法术攻击
}

public enum DefenseType
{
    None,   //0默认值
    Melee,  //1近战物理防御
    Range,  //2远程物理防御
    Magic,  //3法术防御
}

public enum HeroRace
{
    None,   //默认值
    Human,  //人类
    Elf,    //精灵
    Drawf,  //矮人
    Orc,    //兽族
    Undead, //不死族
}

public static class HeroColor{

    public static Color None        = Color.black;
    public static Color Warrior     = new Color(0.7804f, 0.6118f, 0.4312f);
    public static Color Mage        = new Color(0.4118f, 0.8000f, 0.9412f);
    public static Color Priest      = new Color(1.0000f, 1.0000f, 1.0000f);
    public static Color Hunter      = new Color(0.6706f, 0.8312f, 0.4510f);
    public static Color Rogue       = new Color(1.0000f, 0.9607f, 0.4118f);
    public static Color Shaman      = new Color(0.0000f, 0.4392f, 0.8706f);
    public static Color Paladin     = new Color(0.9608f, 0.5490f, 0.7294f);
    public static Color Druid       = new Color(1.0000f, 0.4902f, 0.0392f);
    public static Color Warlock     = new Color(0.5804f, 0.5098f, 0.7882f);
    public static Color DeathKnight = new Color(0.7686f, 0.1176f, 0.2314f);
    public static Color[] ColorList = new Color[]{
        None,
        Warrior,
        Mage,
        Priest,
        Hunter,
        Rogue,
        Shaman,
        Paladin,
        Druid,
        Warlock,
        DeathKnight};
}

public enum RewardType
{
    None = -1,  //默认值
    Glory,      //荣耀
    Gold,       //金钱
    Knowledge,  //知识
    Belief,     //信仰
    Power,      //力量
}


public static class RewardColor
{
    public static Color None        = Color.black;
    public static Color Glory       = new Color(0.7804f, 0.6118f, 0.4312f);
    public static Color Gold        = new Color(1.0000f, 0.9607f, 0.4118f);
    public static Color Knowledge   = new Color(0.4118f, 0.8000f, 0.9412f);
    public static Color Belief      = new Color(1.0000f, 1.0000f, 1.0000f);
    public static Color Power       = new Color(0.7686f, 0.1176f, 0.2314f);
    public static Color[] ColorList = new Color[]{
        None,
        Glory,
        Gold,
        Knowledge,
        Belief,
        Power};
}

public enum BattleEventType
{
    None,       //标准事件
    ForceMove,  //强制移动，有一个友军移动到目标位置则该命令失效
    NewReward,  //新的奖励，吸引现在战场上的英雄过来分享
}

//游戏战斗事件
public class BattleEvent
{
    public BattleEvent(BattleEventType pType, BattleEventInformation pBattleEventObject)
    {
        _type = pType;
        _eventObject = pBattleEventObject;
    }

    private BattleEventType _type;
    private BattleEventInformation _eventObject;

    public BattleEventType Type
    {
        get { return _type; }
    }

    public BattleEventInformation BattleEventObject
    {
        get { return _eventObject; }
    }
}
