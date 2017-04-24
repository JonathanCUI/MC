using UnityEngine;
using System.Collections;

public class HeadManager {

    //data members
    HeroAIStatus _currentAIStatus;

	// Use this for initialization
	void Start ()
    {
        _currentAIStatus = HeroAIStatus.Patrol;
	}
	
	// Update is called once per frame
	public void UpdateLogic (Vector2 pLogicPosition, float pDeltaTime)
    {
        switch (_currentAIStatus)
        {
            case HeroAIStatus.Patrol:
                
                break;
            case HeroAIStatus.Attack:

                break;
            case HeroAIStatus.Move:

                break;
            case HeroAIStatus.Defense:

                break;
            default:

                break;
        }
    }

    //巡逻逻辑，首先指定巡逻的中心点坐标，然后在指定的最大巡逻范围内随机指定一个点，走过去。
    //到达位置之后，随机休息一段时间，然后重新指定一个最大范围内的随机的一个点，
    //重复这两步，直到额外的指令到达
    private void UpdatePatrolLogic(Vector2 pLogicPosition, float pDeltaTime)
    {

    }

    //接收消息
    //到达目的地
    public void ReceiverMessage()
    {

    }
    //发送指令
    public void SendCommand()
    {
    }
}
