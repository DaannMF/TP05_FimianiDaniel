using System;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour {
    [SerializeField] private String floatName;
    [SerializeField] private Boolean updateOnSateEnter, updateOnStateExit;
    [SerializeField] private Boolean updateOnStateMachineEnter, updateOnStateMachineExit;
    [SerializeField] private Single valueOnEnter, valueOnExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.updateOnSateEnter)
            animator.SetFloat(floatName, valueOnEnter);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (updateOnStateExit)
            animator.SetFloat(floatName, valueOnExit);
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
        if (this.updateOnStateMachineEnter)
            animator.SetFloat(floatName, valueOnEnter);

    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
        if (this.updateOnStateMachineExit)
            animator.SetFloat(floatName, valueOnExit);
    }
}
