using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {
    private enum AIStatus
    {
        None,           //idle, nothing to do
        //巡逻相关 //walk and stop in a ralative small range
        PatrolIdle,     //巡逻中的空闲状态
        PatrolWalk,     //巡逻中的走动状态
    

        RunToPosition,  //run to specific position
        RunToTarget,    //run to a specific target, the target might move around or destroy
        Attack,         //attack an enemy
        Defense,        //fear, heal or others
    }

    private Animator _AIFSM;
    private AIStatus _currentAIStatus;

    private AvatarManager _avatarManager;

    //巡逻相关参数
    private float _maxPatrolRadius = 3f;         //最大巡逻半径
    private float _minPatrolIdleTime = 1f;       //到达位置后，最小空闲时间
    private float _maxPatrolIdleTime = 2f;       //到达位置后，最大空闲时间
    private float _patrolPerceptionRadius;       //巡逻时间感知半径

    //巡逻辅助变量
    private Vector2 _patrolCentrePosition;         //巡逻中心点
    //巡逻空闲相关
    private float   _patrolIdleTime;               //到达位置后，随机一个在最大值和最小值之间个一个时间
    private float   _patrolIdleTimeAccmulate;      //到达位置后，已经空闲的时间
    //巡逻走动相关
    private float   _patrolDistanceSqrLimit;       //本次巡逻移动应该走的最长距离判定，用于判断是否已经走到指定位置
    private Vector2 _patrolStartPosition;          //巡逻走动开始位置
    
    //    private bool _patrolReachTerminal;           //是否到达位置


    //定义所有可能出现的变量
    //状态
    private int _statePatrolIdle = Animator.StringToHash("Base Layer.Patrol.PatrolIdle");
    private int _statePatrolWalk = Animator.StringToHash("Base Layer.Patrol.PatrolWalk");

    //触发器
    private int _triggerPatrolIdleToWalk =  Animator.StringToHash("PatrolIdleToWalkTrigger");
    private int _triggerPatrolWalkToIdle = Animator.StringToHash("PatrolWalkToIdleTrigger");

    // Use this for initialization
    void Start () {
        _AIFSM = this.transform.GetComponent<Animator>();
        _avatarManager = this.transform.parent.GetComponent<AvatarManager>();
        _patrolCentrePosition = _avatarManager.LogicPosition;
        this.StartNewPatrol(_patrolCentrePosition);
    }

    private void StartNewPatrol(Vector2 pCentrePosition)
    {
//        _currentAIStatus = AIStatus.PatrolIdle;
        _patrolCentrePosition = pCentrePosition;
//        _patrolReachTerminal = true;
//        _idleTimeAccmulate = 0f;
//        _patrolIdleTime = Mathf.Lerp(_minPatrolIdleTime, _maxPatrolIdleTime, Random.Range(0f, 1f));
//        _avatarManager.Idle();
    }

    // Update is called once per frame
    void Update ()
    {
        //将当前状态打印出来
        //Debug.Log(_AIFSM.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString());
//        AIStateBehaviour sb = _AIFSM.GetBehaviour<AIStateBehaviour>();
	}

    public void UpdateLogic()
    {
        if (_currentAIStatus == AIStatus.PatrolIdle)
        {
            _patrolIdleTimeAccmulate += Const.UpdateGapTime;
            //判断是否休息了足够的时间
            if (_patrolIdleTimeAccmulate >= _patrolIdleTime)
            {
                //休息了足够的时间，需要开始下一段路程
                _AIFSM.SetTrigger(_triggerPatrolIdleToWalk);
            }
            return;
        }

        if (_currentAIStatus == AIStatus.PatrolWalk)
        {
            //判断是否走了足够的距离
            if (Vector2.SqrMagnitude(_avatarManager.LogicPosition - _patrolStartPosition) >= _patrolDistanceSqrLimit)
            {
                //如果走了足够的距离，则转入空闲状态
                _AIFSM.SetTrigger(_triggerPatrolWalkToIdle);
            }
            return;
        }
    }

    public void OnStateEnter(int pStateHash)
    {
        if (pStateHash == _statePatrolIdle)
        {
            _currentAIStatus = AIStatus.PatrolIdle;
            _patrolIdleTimeAccmulate = 0f;
            _patrolIdleTime = Mathf.Lerp(_minPatrolIdleTime, _maxPatrolIdleTime, Random.Range(0f, 1f));
            _avatarManager.Idle();
            return;
        }

        if (pStateHash == _statePatrolWalk)
        {
            _currentAIStatus = AIStatus.PatrolWalk;
            Vector2 _patrolTerminalPosition = _patrolCentrePosition + Random.insideUnitCircle * _maxPatrolRadius;
            _avatarManager.WalkToPosition(_patrolTerminalPosition);
            _patrolStartPosition = _avatarManager.LogicPosition;
            _patrolDistanceSqrLimit = Vector2.SqrMagnitude(_patrolTerminalPosition - _patrolStartPosition);
            return;
        }
    }

    public void OnStateExit(int pStateHash)
    {
        if (pStateHash == _statePatrolIdle)
        {
            return;
        }
        if (pStateHash == _statePatrolWalk)
        {
            return;
        }
    }

}
