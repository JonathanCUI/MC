﻿using UnityEngine;
using BSM = BattleSceneManager;
using System.Collections;
//用来通过键盘和鼠标来快速的添加想要测试的英雄和奖励
public class TestMenu : MonoBehaviour {

    private Camera _mainCamera;

	// Use this for initialization
	void Awake () {
        _mainCamera = Camera.main;
	}

    private Vector3 GetMousePosition()
    {
        Ray clickRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(clickRay, out hitInfo, Mathf.Infinity))
        {            
            return hitInfo.point;
        }
        return Vector3.zero;
    }

	// Update is called once per frame
	void Update ()
    {
        //如果按下键盘的1-5，则对应在一定的中心圆范围内创建5种不同类型的英雄
        //改为在鼠标所在位置创建
        if (Input.GetKeyDown(KeyCode.Alpha1))   //添加战士
        {
            BSM.Instance.AddNewHero(AvatarClass.Warrior, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))   //添加法师
        {
            BSM.Instance.AddNewHero(AvatarClass.Mage, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))   //添加牧师
        {
            BSM.Instance.AddNewHero(AvatarClass.Priest, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))   //添加猎人
        {
            BSM.Instance.AddNewHero(AvatarClass.Hunter, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))   //添加盗贼
        {
            BSM.Instance.AddNewHero(AvatarClass.Rogue, GetMousePosition());
        }
        //如果按下小键盘1 - 5，表示创造不同的奖赏点，荣耀，金钱，知识，信仰，力量
        if (Input.GetKeyDown(KeyCode.Keypad1)) //添加荣耀
        {
            BSM.Instance.AddNewReward(RewardType.Glory, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) //添加金钱
        {
            BSM.Instance.AddNewReward(RewardType.Gold, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) //添加知识
        {
            BSM.Instance.AddNewReward(RewardType.Knowledge, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) //添加信仰
        {
            BSM.Instance.AddNewReward(RewardType.Belief, GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.Keypad5)) //添加力量
        {
            BSM.Instance.AddNewReward(RewardType.Power, GetMousePosition());
        }
        //if (Input.GetKeyDown(KeyCode.Keypad2)) //添加金钱
        //{
        //    GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Reward") as GameObject);
        //    go.transform.GetComponent<Renderer>().material.color = RewardColor.ColorList[2];
        //    go.transform.position = GetMousePosition();
        //    go.transform.GetComponent<RewardManager>().SetData(RewardType.Gold);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad3)) //添加知识
        //{
        //    GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Reward") as GameObject);
        //    go.transform.GetComponent<Renderer>().material.color = RewardColor.ColorList[3];
        //    go.transform.position = GetMousePosition();
        //    go.transform.GetComponent<RewardManager>().SetData(RewardType.Knowledge);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad4)) //添加信仰
        //{
        //    GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Reward") as GameObject);
        //    go.transform.GetComponent<Renderer>().material.color = RewardColor.ColorList[4];
        //    go.transform.position = GetMousePosition();
        //    go.transform.GetComponent<RewardManager>().SetData(RewardType.Belief);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad5)) //添加力量
        //{
        //    GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Reward") as GameObject);
        //    go.transform.GetComponent<Renderer>().material.color = RewardColor.ColorList[5];
        //    go.transform.position = GetMousePosition();
        //    go.transform.GetComponent<RewardManager>().SetData(RewardType.Power);
        //}
    }
}
