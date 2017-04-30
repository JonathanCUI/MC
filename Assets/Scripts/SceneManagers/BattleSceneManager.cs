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
    
	// Use this for initialization
	void Start () 
    {
        //{
        //    GameObject avatar = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
        //    //avatar.transform.position = new Vector3(0f, 0f, 0f);
        //    avatar.transform.GetComponent<AvatarManager>().SetData(HeroClass.Warrior, Camp.Ally);
        //    _heroList.Add(avatar);
        //}
    }
    
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray clickRay = _mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast (clickRay, out hitInfo, Mathf.Infinity)) 
			{
				//Debug.Log (hitInfo.point.ToString());//;transform.position.ToString ());
                //分发消息
                for (int i = 0; i < _heroList.Count; i++)
                {
                    BattleEvent bt = new BattleEvent();
                    bt.Position = new Vector2(hitInfo.point.x, hitInfo.point.z);
                    bt.Type = BattleEventType.ForceMove;
                    _heroList[i].transform.GetComponent<AvatarManager>().ReceiverMessage(bt);
                }
            }
        }
	}

	void OnDestroy()
	{
		DBManager.Close ();
	}
}
