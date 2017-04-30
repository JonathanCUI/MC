using UnityEngine;
using System.Collections;
//奖励品管理器
public class RewardManager : MonoBehaviour {

	// Use this for initialization
    //最开始的时候，全局广播战场消息，有一个新的奖励被投放到战场
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //判断是否对应的奖励被对应的英雄所拾取，如果是，则将该奖励加到对应英雄身上
        //并清除自身，同时全局广播战场效果，一个奖励已经被拾取，使得其他以此奖励为目标的英雄停止前进
        //并进入自身的巡逻状态
	}

    //
}
