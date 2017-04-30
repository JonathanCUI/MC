using UnityEngine;
using System.Collections;

public class HeadManager {

    //data members
    private HeroAIStatus _currentAIStatus;
    private AvatarManager _avatarManager;

    //巡逻相关参数
    private float   _maxPatrolRadius = 3f;        //最大巡逻半径
    private Vector2 _patrolCentrePosition;         //巡逻中心点
    private Vector2 _patrolTerminalPosition;       //巡逻目标点
    private float   _minPatrolIdleTime = 1f;       //到达位置后，最小空闲时间
    private float   _maxPatrolIdleTime = 2f;       //到达位置后，最大空闲时间
    private float   _patrolIdleTime;               //到达位置后，随机一个在最大值和最小值之间个一个时间
    private float   _idleTimeAccmulate;            //到达位置后，已经空闲的时间
    private bool    _patrolReachTerminal;          //是否到达位置
    //可以说，红轴现在是机械键盘当中，压感最小的机械键盘，打字有一种蜻蜓点水的感觉，非常的快速
    //跑向目标地点相关参数
    private Vector2 _runStartPosition;         //跑状态起始位置
    private Vector2 _runTerminalPosition;      //跑向目标地点
    private float   _runDistanceSqr;           //需要跑动的距离的平方

    public HeadManager(HeroClass pHeroClass, AvatarManager pAvatarManager)
    {
        _avatarManager = pAvatarManager;
        _patrolCentrePosition = new Vector2(pAvatarManager.transform.position.x, pAvatarManager.transform.position.z);
    }

	// Use this for initialization
	public void Start ()
    {
        StartNewPatrol(_patrolCentrePosition);
        //Debug.Log(_patrolCentrePosition.ToString());
    }
	
	// Update is called once per frame
	public void UpdateLogic (Vector2 pLogicPosition, float pDeltaTime)
    {
        switch (_currentAIStatus)
        {
            case HeroAIStatus.Patrol:
                UpdatePatrolLogic(pLogicPosition, pDeltaTime);
                break;
            case HeroAIStatus.Attack:

                break;
            case HeroAIStatus.Run:
                UpdateRunLogic(pLogicPosition, pDeltaTime);
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
        //子状态1 已经到达目标位置，空闲状态
        if (_patrolReachTerminal)
        {
            if (_idleTimeAccmulate < _patrolIdleTime)
            {
                _idleTimeAccmulate += pDeltaTime;
                //休息了足够的时间，需要开始下一段路程
                if (_idleTimeAccmulate >= _patrolIdleTime)
                {
                    _patrolReachTerminal = false;
                
                    //指定下一个位置
                    _patrolTerminalPosition = _patrolCentrePosition + Random.insideUnitCircle * _maxPatrolRadius;            
                }
            }
        }
        //子状态2 没有到达目标位置，前往状态
        else 
        {
            if (Vector2.SqrMagnitude(pLogicPosition - _patrolTerminalPosition) > 0.1f)
            {
                //朝向avatar发出指令
                _avatarManager.WalkToPosition(_patrolTerminalPosition);
            }
            else
            {
                _patrolReachTerminal = true;
                _idleTimeAccmulate = 0f;
                _patrolIdleTime = Mathf.Lerp(_minPatrolIdleTime, _maxPatrolIdleTime, Random.Range(0f, 1f));
                _avatarManager.Idle();
            }
        }
    }

    private void StartNewPatrol(Vector2 pCentrePosition)
    {
        _currentAIStatus = HeroAIStatus.Patrol;
        _patrolCentrePosition = pCentrePosition;
        _patrolReachTerminal = true;
        _idleTimeAccmulate = 0f;
        _patrolIdleTime = Mathf.Lerp(_minPatrolIdleTime, _maxPatrolIdleTime, Random.Range(0f, 1f));
        _avatarManager.Idle();
    }

    private void UpdateRunLogic(Vector2 pLogicPosition, float pDeltaTime)
    {
        //检查是否已经跑到位置，为了避免移动速度过快而产生的偏差，这里用跑动距离的方式来表示是否到达或者超过目标点
        if (Vector2.SqrMagnitude(pLogicPosition - _runStartPosition) < _runDistanceSqr)
        {
            //继续跑
            _avatarManager.RunToPosition(_runTerminalPosition);
        }
        else
        {
            //进入巡逻状态
            StartNewPatrol(pLogicPosition);
        }
    }

    //接收消息
    public void ReceiverMessage(BattleEvent pBattleEvent, Vector2 pCurrentPosition)
    {
        //根据消息的不同类型来决定不同行为
        switch (pBattleEvent.Type)
        {
            case BattleEventType.ForceMove:
                this.RunToPosition(pBattleEvent.Position, pCurrentPosition);
                break;

            default:
                break;
        }
    }

    //跑向目标地点
    private void RunToPosition(Vector2 pTerminalPosition, Vector2 pCurrentPosition)
    {
        _runTerminalPosition = pTerminalPosition;
        _runStartPosition    = pCurrentPosition;
        _currentAIStatus     = HeroAIStatus.Run;
        _runDistanceSqr = Vector2.SqrMagnitude(pTerminalPosition - pCurrentPosition);
    }

    //发送指令
    public void SendCommand()
    {
    }
}
