using UnityEngine;
using System.Collections;

enum IdleWalkRun
{
    None,
    Idle,
    Walk,
    Run
}

public class AvatarController : MonoBehaviour {
        
    private Animator _avatarAnimator;

    private int _moveStateHashString = Animator.StringToHash("IdleWalkRun");
    private Vector3 _terminalPosition;
    private float _runSpeed = 4f;
    void Start()
    {
        //Animator.StringToHash()
        //_terminalPosition = this.transform.position;
    }

    public void SetData(HeroClass pHeroClass)
    {
        GameObject avatar = null;
        switch(pHeroClass)
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
        
	// Update is called once per frame
	void Update () 
    {
        if(Vector3.SqrMagnitude(this.transform.position - _terminalPosition) > 0.1f)
        {
            this.transform.position += (_terminalPosition - this.transform.position).normalized * _runSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(_terminalPosition - this.transform.position), Time.deltaTime * 10f);
        }
        else
        {
            Idle();          
        }
	}

    public void WalkToPosition(Vector2 pTerminalPosition)
    {
        _terminalPosition = new Vector3(pTerminalPosition.x, 0, pTerminalPosition.y);
        //_moveState = IdleWalkRun.Run;
        _avatarAnimator.SetInteger(_moveStateHashString, (int)IdleWalkRun.Run);
    }


    public void RunToPosition(Vector2 pTerminalPosition)
    {
        //按照一定速度跑过去，跑到之后进入状态
        _terminalPosition = new Vector3(pTerminalPosition.x, 0, pTerminalPosition.y);
        //_moveState = IdleWalkRun.Run;
        Run(); //run, walk just for test
    }

    public void Idle()
    {
        _avatarAnimator.SetInteger(_moveStateHashString, (int)IdleWalkRun.Idle);
    }

    public void Walk()
    {
        _avatarAnimator.SetInteger(_moveStateHashString, (int)IdleWalkRun.Walk);
    }

    public void Run()
    {
        _avatarAnimator.SetInteger(_moveStateHashString, (int)IdleWalkRun.Run);
    }
}
