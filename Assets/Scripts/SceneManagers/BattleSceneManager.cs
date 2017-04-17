using UnityEngine;
using UnityEngine.UI;
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

    public void AddHero(GameObject pHeroObject, HeroClass pHeroClass)
    {
        pHeroObject.GetComponent<HeroManager>().SetData(pHeroClass);
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
        {
            GameObject avatar = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            //avatar.transform.position = new Vector3(0f, 0f, 0f);
            avatar.transform.GetComponent<AvatarController>().SetData(HeroClass.Warrior);
            _heroList.Add(avatar);
        }

        //{
        //    GameObject avatar = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
        //    avatar.transform.position = new Vector3(5, 0, -5);
        //    avatar.transform.GetComponent<AvatarController>().SetData(HeroClass.Mage);
        //    _heroList.Add(avatar);
        //}
        //{
        //    GameObject avatar = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
        //    avatar.transform.position = new Vector3(-5, 0, -5);
        //    avatar.transform.GetComponent<AvatarController>().SetData(HeroClass.Priest);
        //    _heroList.Add(avatar);
        //}
        //{
        //    GameObject avatar = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
        //    avatar.transform.position = new Vector3(-5, 0, 5);
        //    avatar.transform.GetComponent<AvatarController>().SetData(HeroClass.Hunter);
        //    _heroList.Add(avatar);
        //}
    }


    void OnMouseDown()
	{
		Debug.Log (Input.mousePosition);

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
//			Debug.Log (Input.mousePosition);
			Ray clickRay = _mainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;

			if (Physics.Raycast (clickRay, out hitInfo, Mathf.Infinity)) 
			{
				Debug.Log (hitInfo.point.ToString());//;transform.position.ToString ());
                //分发消息
                for(int i = 0; i < _heroList.Count; i++)
                {
                    _heroList[i].transform.GetComponent<AvatarController>().RunToPosition(new Vector2(hitInfo.point.x, hitInfo.point.z));
                }
            }
		}
	}

	void OnDestroy()
	{
		DBManager.Close ();
	}
}
