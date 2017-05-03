using UnityEngine;
using System.Collections;

public abstract class BattleEventInformation
{

}


//强制移动信息
public class BEI_ForceMove : BattleEventInformation
{
    //data members
    private HeroClass _heroClass;
    private Vector2 _position;

    public BEI_ForceMove(Vector2 pLogicPosition, HeroClass pHeroClass)
    {
        _position = pLogicPosition;
        _heroClass = pHeroClass;
    }

    public Vector2 LogicPosition
    {
        get { return _position; }
    }
    public HeroClass SpecificHeroClass
    {
        get { return _heroClass; }
    }
}

//新奖励信息
public class BEI_NewReward : BattleEventInformation
{
    //data members
    private RewardType _rewardType;
    private Transform  _transform;  //由于奖励可能会移动，这里用transform代替position
    private float       _amount;  //奖励的多少，TBD

    public BEI_NewReward(RewardType pRewardType, Transform pTransform, float pAmount)
    {
        _rewardType = pRewardType;
        _transform = pTransform;
        _amount = pAmount;
    }
    
    public RewardType RewardType
    {
        get { return _rewardType; }
    }

    public Transform RewardTransform
    {
        get { return _transform; }
    }

    public float Amount
    {
        get { return _amount; }
    }
    
}
