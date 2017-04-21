using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using BSM = BattleSceneManager;

public class InputController : MonoBehaviour {
    
    //UI const values
    private float heroPanelWidth   = 256f;
    private float rewardPanelWidth = 256f;

    public Camera MainCamera;

    //data members

    //connection with finger id and hero class
    private Dictionary<int, HeroClass>  _heroFingerClassSelectionDic = new Dictionary<int, HeroClass>();
    //connection with finger id and hero game object
    private Dictionary<int, GameObject> _heroFingerObjectSelectionDic = new Dictionary<int, GameObject>();
    //connection with finger id and reward type
    private Dictionary<int, RewardType> _rewardFingerClassSelectionDic = new Dictionary<int, RewardType>();  
    //connection with finger id and reward game object
    private Dictionary<int, GameObject> _rewardFingerObjectSelectionDic = new Dictionary<int, GameObject>();
    //connection with finger id and monster game object
    private Dictionary<int, GameObject> _monsterFingerObjectSelectionDic = new Dictionary<int, GameObject>();


	// Use this for initialization
	void Start () 
    {
        //initialize UI layout
        Transform heroPanel = GameObject.Find("UICanvas").transform.Find("HeroPanel");
        //11 in total, and 0 for none, so i starts from 1.
        for(int i = 1; i < HeroColor.ColorList.Length; i++)
        {
            GameObject heroButton = Instantiate(Resources.Load("Prefabs/UI/HeroButton") as GameObject);
            heroButton.transform.SetParent(heroPanel, false);
            heroButton.transform.GetComponent<Image>().color = HeroColor.ColorList[i];
            heroButton.transform.GetComponentInChildren<Text>().text = Translation.GetTrans("hero_class_name_" + i.ToString());
        }

        Transform rewardPanel = GameObject.Find("UICanvas").transform.Find("RewardPanel");
        for(int i = 1; i <= 5; i++)
        {
            GameObject rewardButton = Instantiate(Resources.Load("Prefabs/UI/RewardButton") as GameObject);
            rewardButton.transform.SetParent(rewardPanel, false);
            rewardButton.transform.GetComponent<Image>().color = RewardColor.ColorList[i];
            rewardButton.transform.GetComponentInChildren<Text>().text = Translation.GetTrans("reward_name_" + i.ToString());
        }

        float scale = Screen.width  / 2048f;
        heroPanelWidth   *= scale;
        rewardPanelWidth *= scale;
	}
	
	// Update is called once per frame
	void Update () {
        Touch[] touches = Input.touches;
        for(int i = 0; i < touches.Length; i++)
        {
            int fingerID = touches[i].fingerId;
            if(touches[i].phase == TouchPhase.Began)
            {
                //hero selection
                if(touches[i].position.x <= heroPanelWidth)
                {
                    int heroIndex = Common.HeroClassCount - (int)(touches[i].position.y * Common.HeroClassCount / Screen.height);
                    _heroFingerClassSelectionDic.Add(fingerID, (HeroClass)heroIndex);
                    _heroFingerObjectSelectionDic.Add(fingerID, null);
                }
                //reward selection
                else if(touches[i].position.x >= Screen.width - rewardPanelWidth)
                {
                    int rewardIndex = Common.RewardCount - (int)(touches[i].position.y * Common.RewardCount / Screen.height);
                    _rewardFingerClassSelectionDic.Add(fingerID, (RewardType)rewardIndex);
                    _rewardFingerObjectSelectionDic.Add(fingerID, null);
                }
                else //put down monsters
                {
                    GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Hero") as GameObject);
                    _monsterFingerObjectSelectionDic.Add(fingerID, go);
//                    _monsterFingerObjectSelectionDic[fingerID] = go;
                    RaycastHit objHit;
                    if(Physics.Raycast(MainCamera.ScreenPointToRay(touches[i].position), out objHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Floor")))
                    {
                        go.transform.position = objHit.point;
                        go.GetComponent<Renderer>().material.color = Color.black;
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
            }
            else if((touches[i].phase == TouchPhase.Moved || touches[i].phase == TouchPhase.Stationary))
            {               
                if(_heroFingerClassSelectionDic.ContainsKey(fingerID))
                {
                    RaycastHit objHit;
                    if(Physics.Raycast(MainCamera.ScreenPointToRay(touches[i].position), out objHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Floor")))
                    {
                        if(_heroFingerObjectSelectionDic[fingerID] == null)
                        {
                            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Hero") as GameObject);
                            _heroFingerObjectSelectionDic[fingerID] = go;
                            go.transform.GetComponent<Renderer>().material.color = HeroColor.ColorList[(int)_heroFingerClassSelectionDic[fingerID]];
                        }
                        _heroFingerObjectSelectionDic[fingerID].transform.position = objHit.point;


                        if((touches[i].position.x > heroPanelWidth && touches[i].position.x < Screen.width - rewardPanelWidth))
                        {
                            _heroFingerObjectSelectionDic[fingerID].SetActive(true);
                        }
                        else
                        {
                            _heroFingerObjectSelectionDic[fingerID].SetActive(false);
                        }

                    }
                }
                else if(_rewardFingerClassSelectionDic.ContainsKey(fingerID))
                {
                    RaycastHit objHit;
                    if(Physics.Raycast(MainCamera.ScreenPointToRay(touches[i].position), out objHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Floor")))
                    {
                        if(_rewardFingerObjectSelectionDic[fingerID] == null)
                        {
                            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Reward") as GameObject);
                            _rewardFingerObjectSelectionDic[fingerID] = go;
                            go.transform.GetComponent<Renderer>().material.color = RewardColor.ColorList[(int)_rewardFingerClassSelectionDic[fingerID]];
                        }
                        _rewardFingerObjectSelectionDic[fingerID].transform.position = objHit.point;


                        if((touches[i].position.x > heroPanelWidth && touches[i].position.x < Screen.width - rewardPanelWidth))
                        {
                            _rewardFingerObjectSelectionDic[fingerID].SetActive(true);
                        }
                        else
                        {
                            _rewardFingerObjectSelectionDic[fingerID].SetActive(false);
                        }
                    }
                }
                else if(_monsterFingerObjectSelectionDic.ContainsKey(fingerID))
                {
                    RaycastHit objHit;

                    if(Physics.Raycast(MainCamera.ScreenPointToRay(touches[i].position), out objHit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Floor")))
                    {
                        _monsterFingerObjectSelectionDic[fingerID].transform.position = objHit.point;
                        _monsterFingerObjectSelectionDic[fingerID].SetActive(true);
                    }
                    else
                    {
                        _monsterFingerObjectSelectionDic[fingerID].SetActive(false);                    
                    }
                }

            }
            else if(touches[i].phase == TouchPhase.Ended)
            {

                if(_heroFingerObjectSelectionDic.ContainsKey(fingerID) && _heroFingerObjectSelectionDic[fingerID] != null)
                {
                    if(touches[i].position.x > heroPanelWidth && touches[i].position.x < Screen.width - rewardPanelWidth)
                    {
                        //BSM.Instance.AddHero(_heroFingerObjectSelectionDic[fingerID], _heroFingerClassSelectionDic[fingerID]);
                    }
                    else
                    {
                        Destroy(_heroFingerObjectSelectionDic[fingerID]);
                    }
                }
                _heroFingerClassSelectionDic.Remove(fingerID);
                _heroFingerObjectSelectionDic.Remove(fingerID);


                _rewardFingerClassSelectionDic.Remove(fingerID);
                if(_rewardFingerObjectSelectionDic.ContainsKey(fingerID) && _rewardFingerObjectSelectionDic[fingerID] != null)
                {
                    if(touches[i].position.x > heroPanelWidth && touches[i].position.x < Screen.width - rewardPanelWidth)
                    {
                        BSM.Instance.AddReward(_rewardFingerObjectSelectionDic[fingerID]);
                    }
                    else
                    {
                        Destroy(_rewardFingerObjectSelectionDic[fingerID]);
                    }
                }
                _rewardFingerObjectSelectionDic.Remove(fingerID);
                if (_monsterFingerObjectSelectionDic.ContainsKey(fingerID) && _monsterFingerObjectSelectionDic[fingerID] != null)
                {
                    if(touches[i].position.x > heroPanelWidth && touches[i].position.x < Screen.width - rewardPanelWidth)
                    {
                        BSM.Instance.AddMonster(_monsterFingerObjectSelectionDic[fingerID]);
                    }
                    else
                    {
                        Destroy(_monsterFingerObjectSelectionDic[fingerID]);
                    }
                    _monsterFingerObjectSelectionDic.Remove(fingerID);
                }
            }
            else if(touches[i].phase == TouchPhase.Canceled)
            {
                _heroFingerClassSelectionDic.Remove(fingerID);
                if(_heroFingerObjectSelectionDic.ContainsKey(fingerID) && _heroFingerObjectSelectionDic[fingerID] != null)
                {
                    Destroy(_heroFingerObjectSelectionDic[fingerID]);
                }
                _heroFingerObjectSelectionDic.Remove(fingerID);


                _rewardFingerClassSelectionDic.Remove(fingerID);
                if(_rewardFingerObjectSelectionDic.ContainsKey(fingerID) && _rewardFingerObjectSelectionDic[fingerID] != null)
                {
                    Destroy(_rewardFingerObjectSelectionDic[fingerID]);
                }
                _rewardFingerObjectSelectionDic.Remove(fingerID);


                if(_monsterFingerObjectSelectionDic.ContainsKey(fingerID) && _monsterFingerObjectSelectionDic[fingerID] != null)
                {
                    Destroy(_monsterFingerObjectSelectionDic[fingerID]);
                }
                _monsterFingerObjectSelectionDic.Remove(fingerID);
            }
        }
	}
}
