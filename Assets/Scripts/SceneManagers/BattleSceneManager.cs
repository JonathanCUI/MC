using UnityEngine;
using System.Collections.Generic;

public class BattleSceneManager : MonoBehaviour {

    //instance
    private static BattleSceneManager _instance;

    //data members
    private List<GameObject> _heroList = new List<GameObject>();
    private List<GameObject> _rewardList = new List<GameObject>();
    private List<GameObject> _monsterList = new List<GameObject>();

	//scene members
	private Camera _mainCamera;

    //游戏时间管理
    private float _defaultUpdateGap;        //标准更新逻辑时间间隔，标准一般为1/30秒
    private float _updateTimeAccumulate;    //更新逻辑累加
    private float _battleStartTime;         //战斗开始真实时间
    private float _previousUpdateLogicTime; //上一次更新逻辑帧的真实时间
    private float _previousUpdateTime;      //上一次更新游戏的时间
    private float _currentTime;             //当前游戏时间
    private float _updateTimeGap;           //更新时间间隔
    private bool _updateLogicThisFrame;     //是否在本次更新更新了逻辑信息
//    private float 
//    private float _update



    public static BattleSceneManager Instance{
        get{
            return _instance;
        }
    }

    public void AddHero(GameObject pHeroObject)
    {
        _heroList.Add(pHeroObject);
    }

    public void AddReward(GameObject pRewardObject)
    {
        _rewardList.Add(pRewardObject);
    }

    public void AddMonster(GameObject pMonsterObject)
    {
        _monsterList.Add(pMonsterObject);
    }

    void Awake()
    {
        //initialize all necessary components
        DBManager.Initialize();
        Translation.Initialize();
        _instance = this;
		_mainCamera = Camera.main;

        //time relative
        _battleStartTime = Time.realtimeSinceStartup;
        _previousUpdateLogicTime = Time.realtimeSinceStartup;
        _updateTimeAccumulate = 0f;
        _defaultUpdateGap = 1f / 30f;
    }

    // Update is called once per frame
    //主更新函数，是游戏内时间驱动的唯一入口，为了保证效率和正确率，所有子类的更新函数都由这里统一调用
    void Update ()
	{
        _currentTime = Time.realtimeSinceStartup;
        _updateLogicThisFrame = false;

        _updateTimeGap = _currentTime - _previousUpdateTime;
        _updateTimeAccumulate += _updateTimeGap;
        //当累计到1/30秒之后，更新逻辑
        while (_updateTimeAccumulate >= _defaultUpdateGap)
        {
            _updateLogicThisFrame = true;
            UpdateLogic();
            _updateTimeAccumulate -= _defaultUpdateGap;
        }
        //如果本次更新同时更新了逻辑帧(至少一次)，那么就不必更新位置信息

        //为了保证显示的平滑，每次update都会更新位置信息position

        //更新渲染信息，比如粒子特效等等，其实也可以直接调用update来实现，这里存疑
        //UpdateRender(_updateTimeGap);
        if (!_updateLogicThisFrame)
        {
            UpdatePosition(_updateTimeGap);
        }

        _previousUpdateLogicTime = _currentTime;

        //右键点击地面，强制移动
        /*
		if (Input.GetMouseButtonDown (1)) 
		{
			Ray clickRay = _mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

            if (Physics.Raycast (clickRay, out hitInfo, Mathf.Infinity)) 
			{
                //Debug.Log (hitInfo.point.ToString());//;transform.position.ToString ());
                //分发消息
                Vector2 logicPosition = new Vector2(hitInfo.point.x, hitInfo.point.z);
                BEI_ForceMove bei = new BEI_ForceMove(logicPosition, HeroClass.None);
                BattleEvent be = new BattleEvent(BattleEventType.ForceMove, bei);
                SendBattleEvent(be);
            }
        }
        */
	}

    //每隔固定时间更新逻辑
    private void UpdateLogic()
    {

    }
    //每次非更新逻辑帧的更新逻辑会调用，用来平滑显示
    private void UpdateRender(float pDeltaTime)
    {

    }

    //
    private void UpdatePosition(float pDelatTime)
    {
        //根据刷新时间间隔来更新位置信息，使得移动更加平滑
    }

    public void SendBattleEvent(BattleEvent pBattleEvent)
    {
        for (int i = 0; i < _heroList.Count; i++)
        {
            _heroList[i].transform.GetComponent<AvatarManager>().ReceiverMessage(pBattleEvent);
        }
    }

    public void RemoveReward(Transform pRewardTransform)
    {
        if (pRewardTransform != null)
        {
            Destroy(pRewardTransform.gameObject);
        }
    }

	void OnDestroy()
	{
		DBManager.Close ();
	}
}
