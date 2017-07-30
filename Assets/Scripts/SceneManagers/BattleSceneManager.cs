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
    private float _defaultUpdateGap;        //标准更新时间间隔
    private float _updateTimeAccumulate;    //更新逻辑累加
    private float _battleStartTime;         //战斗开始真实时间
    private float _previousUpdateLogicTime; //上一次更新逻辑帧的真实时间
    private float _previousUpdateTime;      //上一次更新游戏的时间

    private float _updateTimeGap;   //更新时间间隔

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
        _updateTimeGap = Time.realtimeSinceStartup - _previousUpdateTime;



        _previousUpdateLogicTime = Time.realtimeSinceStartup;

        //        Time.realtimeSinceStartup
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


        UpdateRender(_updateTimeGap);
        
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
