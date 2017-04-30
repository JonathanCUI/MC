﻿using UnityEngine;
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
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            go.transform.position = GetMousePosition();
            go.transform.GetComponent<AvatarManager>().SetData(HeroClass.Warrior, Camp.Ally);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))   //添加法师
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            go.transform.position = GetMousePosition();
            go.transform.GetComponent<AvatarManager>().SetData(HeroClass.Mage, Camp.Ally);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))   //添加牧师
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            go.transform.position = GetMousePosition();
            go.transform.GetComponent<AvatarManager>().SetData(HeroClass.Priest, Camp.Ally);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))   //添加猎人
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            go.transform.position = GetMousePosition();
            go.transform.GetComponent<AvatarManager>().SetData(HeroClass.Hunter, Camp.Ally);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))   //添加盗贼
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            go.transform.position = GetMousePosition();
            go.transform.GetComponent<AvatarManager>().SetData(HeroClass.Rogue, Camp.Ally);
            BattleSceneManager.Instance.AddHero(go);
        }
        //如果按下小键盘1-5，表示创造不同的奖赏点，荣耀，金钱，知识，信仰，力量



    }
}
