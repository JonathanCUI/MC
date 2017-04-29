using UnityEngine;
using System.Collections;

public class AvatarManager : MonoBehaviour {
    //data members
    private BodyManager _bodyManager;   //身体管理器
    private HeadManager _headManager;   //头脑管理器

    //HUD管理器
    

    private BaseAnimatorController _avatarAnimator;

    private Vector3 _terminalPosition = Vector3.zero;  //目的地的世界坐标，为了保证效率，在这里提出来
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
        _avatarAnimator = avatar.transform.GetComponent<BaseAnimatorController>();

        _headManager = new HeadManager(pHeroClass, this);
        _bodyManager = new BodyManager(pHeroClass);

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

    public void ReceiverMessage(BattleEvent pBattleEvent)
    {
        _headManager.ReceiverMessage(pBattleEvent);
    }

    //走向目的地
    public void WalkToPosition(Vector2 pTerminalPosition)
    {
        _terminalPosition.x = pTerminalPosition.x;
        _terminalPosition.z = pTerminalPosition.y;
        _avatarAnimator.Walk();
        this.transform.position += (_terminalPosition - this.transform.position).normalized * _bodyManager.WalkSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_terminalPosition - this.transform.position), Time.deltaTime * 10f);
    }
    //原地等待
    public void Idle()
    {
        _avatarAnimator.Idle();
    }

    //跑向目的地
    public void RunToPosition(Vector2 pTerminalPosition)
    {
        _terminalPosition.x = pTerminalPosition.x;
        _terminalPosition.z = pTerminalPosition.y;
        _avatarAnimator.Run();
        this.transform.position += (_terminalPosition - this.transform.position).normalized * _bodyManager.RunSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_terminalPosition - this.transform.position), Time.deltaTime * 10f);
    }
}
