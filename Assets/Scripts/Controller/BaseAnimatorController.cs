using UnityEngine;
using System.Collections;

public class BaseAnimatorController : MonoBehaviour {

    //动画控制器
    private Animator _animator;

    //控制站立，走，跑
    private int _idleWalkRunHashString = Animator.StringToHash("IdleWalkRun");
    //控制攻击
    private int _attackHashString = Animator.StringToHash("Attack");

	void Awake ()
    {
        _animator = this.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Idle()
    {
        _animator.SetInteger(_idleWalkRunHashString, (int)IdleWalkRun.Idle);
    }
    public void Walk()
    {
        _animator.SetInteger(_idleWalkRunHashString, (int)IdleWalkRun.Walk);
    }
    public void Run()
    {
        _animator.SetInteger(_idleWalkRunHashString, (int)IdleWalkRun.Run);
    }
}
