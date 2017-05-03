using UnityEngine;
using BSM = BattleSceneManager;
using System.Collections;
//奖励品管理器
public class RewardManager : MonoBehaviour {

    //data members
    private RewardType _rewardType;

	// Use this for initialization
    //最开始的时候，全局广播战场消息，有一个新的奖励被投放到战场
	void Start ()
    {
        BEI_NewReward bei = new BEI_NewReward(_rewardType, this.transform, 100f);
        BSM.Instance.SendBattleEvent(new BattleEvent(BattleEventType.NewReward, bei));
    }
	
	// Update is called once per frame
	void Update ()
    {
        //判断是否对应的奖励被对应的英雄所拾取，如果是，则将该奖励加到对应英雄身上
        //并清除自身，同时全局广播战场效果，一个奖励已经被拾取，使得其他以此奖励为目标的英雄停止前进
        //并进入自身的巡逻状态
//        if ()
        {

        }


	}

    public void SetData(RewardType pRewardType)
    {
        _rewardType = pRewardType;
    }

    //
}
