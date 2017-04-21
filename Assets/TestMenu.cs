using UnityEngine;
using System.Collections;
//用来通过键盘和鼠标来快速的添加想要测试的英雄和奖励
public class TestMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //如果按下键盘的1-5，则对应在一定的中心圆范围内创建5种不同类型的英雄
        if (Input.GetKeyDown(KeyCode.Alpha1))   //添加战士
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            Vector2 randomPosition = Random.insideUnitCircle * 5f;
            go.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
            go.transform.GetComponent<AvatarController>().SetData(HeroClass.Warrior);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))   //添加法师
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            Vector2 randomPosition = Random.insideUnitCircle * 5f;
            go.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
            go.transform.GetComponent<AvatarController>().SetData(HeroClass.Mage);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))   //添加牧师
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            Vector2 randomPosition = Random.insideUnitCircle * 5f;
            go.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
            go.transform.GetComponent<AvatarController>().SetData(HeroClass.Priest);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))   //添加猎人
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            Vector2 randomPosition = Random.insideUnitCircle * 5f;
            go.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
            go.transform.GetComponent<AvatarController>().SetData(HeroClass.Hunter);
            BattleSceneManager.Instance.AddHero(go);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))   //添加盗贼
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/Avatars/Avatar") as GameObject);
            Vector2 randomPosition = Random.insideUnitCircle * 5f;
            go.transform.position = new Vector3(randomPosition.x, 0, randomPosition.y);
            go.transform.GetComponent<AvatarController>().SetData(HeroClass.Rogue);
            BattleSceneManager.Instance.AddHero(go);
        }



    }
}
