using UnityEngine;
using System.Collections;

public class AIStateBehaviour : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //定义所有可能出现的变量
    //状态
    private int _statePatrolIdle = Animator.StringToHash("Base Layer.Patrol.PatrolIdle");
    private int _statePatrolWalk = Animator.StringToHash("Base Layer.Patrol.PatrolWalk");

    //触发器
    private int _triggerPatrolIdleToWalk = Animator.StringToHash("PatrolIdleToWalkTrigger");
    private int _triggerPatrolWalkToIdle = Animator.StringToHash("PatrolWalkToIdleTrigger");
    
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(Animator.StringToHash("Base Layer.Patrol.PatrolIdle"));
        Debug.Log("On State Enter:" + stateInfo.fullPathHash);

    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
