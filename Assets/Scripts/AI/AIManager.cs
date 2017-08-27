using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

    private Animator _AIFSM;

	// Use this for initialization
	void Start () {
        _AIFSM = this.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //将当前状态打印出来
        //Debug.Log(_AIFSM.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString());
	
	}
}
