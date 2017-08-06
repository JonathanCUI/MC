using UnityEngine;
using System.Collections;

public class AvatarManager : UpdateableEntity
{
    //data members
    private BodyManager _bodyManager;   //身体管理器
    private HeadManager _headManager;   //头脑管理器
    public  AvatarHUDController _hudController; //HUD管理器
    private BaseAnimatorController _avatarAnimator; //角色模型动画管理器

    private Vector3 _terminalPosition = Vector3.zero;  //目的地的世界坐标，为了保证效率，在这里提出来
    //functions
    //private Vector2 _logicPosition; //逻辑坐标系里面的实际坐标，只有2d平面

    //根据角色类型和阵营来创建角色
    public void SetData(AvatarClass pAvatarClass, Camp pCamp, Vector3 pBornPosition)
    {
        //创建角色模型
        GameObject avatar = null;
        switch (pAvatarClass)
        {
            case AvatarClass.Warrior:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Warrior") as GameObject);
                break;
            case AvatarClass.Hunter:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Hunter") as GameObject);
                break;
            case AvatarClass.Mage:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Mage") as GameObject);
                break;
            case AvatarClass.Priest:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Priest") as GameObject);
                break;
            case AvatarClass.Rogue:
                avatar = Instantiate(Resources.Load("Prefabs/Avatars/Warrior") as GameObject);
                break;
            default:
                break;
        }
        avatar.transform.SetParent(this.transform, false);
        //设置位置
        this.LogicPosition = Const.RenderToLogicVector(pBornPosition);
        this.transform.position = Const.LogicToRenderVector(this.LogicPosition);
        //设置速度
        this.LogicSpeed = Vector2.zero;

        _hudController.SetData(pAvatarClass, pCamp);
        //获取所有钩子
        _avatarAnimator = avatar.transform.GetComponent<BaseAnimatorController>();
        _headManager = new HeadManager(pAvatarClass, this);
        _bodyManager = new BodyManager(pAvatarClass);
    }

	// Use this for initialization
	void Start ()
    {
        _headManager.Start();
	}
	
	// Update is called once per frame
	//void Update ()
 //   {
 //       _logicPosition.x = this.transform.position.x;
 //       _logicPosition.y = this.transform.position.z;
 //       //访问头脑，获得当前命令
 //       _headManager.UpdateLogic(_logicPosition, Time.deltaTime);
	//}



    public void ReceiverMessage(BattleEvent pBattleEvent)
    {
        _logicPosition.x = this.transform.position.x;
        _logicPosition.y = this.transform.position.z;
        _headManager.ReceiverMessage(pBattleEvent, _logicPosition);
    }

    //走向目的地
    public void WalkToPosition(Vector2 pTerminalPosition)
    {
        //_terminalPosition.x = pTerminalPosition.x;
        //_terminalPosition.z = pTerminalPosition.y;
        _avatarAnimator.Walk();
        this.LogicSpeed = (pTerminalPosition - LogicPosition).normalized * _bodyManager.WalkSpeed;
        //this.transform.position += (_terminalPosition - this.transform.position).normalized * _bodyManager.WalkSpeed * Time.deltaTime;
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_terminalPosition - this.transform.position), Time.deltaTime * 10f);
    }
    //原地等待
    public void Idle()
    {
        _avatarAnimator.Idle();
        this.LogicSpeed = Vector2.zero;
    }

    //跑向目的地
    public void RunToPosition(Vector2 pTerminalPosition)
    {
//        _terminalPosition.x = pTerminalPosition.x;
//        _terminalPosition.z = pTerminalPosition.y;
        _avatarAnimator.Run();
        this.LogicSpeed = (pTerminalPosition - LogicPosition).normalized * _bodyManager.RunSpeed;
        //this.transform.position += (_terminalPosition - this.transform.position).normalized * _bodyManager.RunSpeed * Time.deltaTime;
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_terminalPosition - this.transform.position), Time.deltaTime * 10f);
    }

    //供battle scene manager调用逻辑和更新
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _headManager.UpdateLogic(LogicPosition, 1 / 30f);
    }

    public override void UpdatePosition(float pDeltaTime)
    {
        base.UpdatePosition(pDeltaTime);
    }


}
