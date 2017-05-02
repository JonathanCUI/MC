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
    }
    
	// Update is called once per frame
	void Update ()
	{
        //右键点击地面，强制移动
		if (Input.GetMouseButtonDown (1)) 
		{
			Ray clickRay = _mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

            if (Physics.Raycast (clickRay, out hitInfo, Mathf.Infinity)) 
			{
                //Debug.Log (hitInfo.point.ToString());//;transform.position.ToString ());
                //分发消息
                SendBattleEvent(BattleEventType.ForceMove, new Vector2(hitInfo.point.x, hitInfo.point.z));
            }
        }
	}

    public void SendBattleEvent(BattleEventType pType, Vector2 pLogicPosition)
    {
        BEI_ForceMove be = new BEI_ForceMove(pLogicPosition, HeroClass.None);
        BattleEvent bt = new BattleEvent(pType, be);
        for (int i = 0; i < _heroList.Count; i++)
        {
            _heroList[i].transform.GetComponent<AvatarManager>().ReceiverMessage(bt);
        }
    }

    public void SendNewRewardEvent()
    {

    }

	void OnDestroy()
	{
		DBManager.Close ();
	}
}
