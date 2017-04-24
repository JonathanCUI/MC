using UnityEngine;
using System.Collections;

public class AvatarManager : MonoBehaviour {
    //data members
    private BodyManager _bodyManager;   //身体管理器
    private HeadManager _headManager;   //头脑管理器

    private Animator _avatarAnimator;
    //functions
    private Vector2 _logicPosition; //逻辑坐标系里面的实际坐标，只有2d平面

    //根据角色类型和阵营来创建角色
    public void SetData(HeroClass pHeroClass, Camp pCamp)
    {
        GameObject avatar = null;
        switch (pHeroClass)
        {
            case HeroClass.Warrior:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Warrior") as GameObject);
                break;
            case HeroClass.Hunter:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Hunter") as GameObject);
                break;
            case HeroClass.Mage:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Mage") as GameObject);
                break;
            case HeroClass.Priest:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Priest") as GameObject);
                break;
            case HeroClass.Rogue:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Warrior") as GameObject);
                break;
            default:
                break;
        }
        avatar.transform.SetParent(this.transform, false);
        _avatarAnimator = this.transform.GetComponentInChildren<Animator>();
        this.transform.GetComponent<HeroManager>().SetData(pHeroClass);
    }


	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        _logicPosition.x = this.transform.position.x;
        _logicPosition.y = this.transform.position.z;
        //访问头脑，获得当前命令
        _headManager.UpdateLogic(_logicPosition, Time.deltaTime);

	}
}
