using System;
using UnityEngine;

public class SetBooleanBehaviour : StateMachineBehaviour {
    [SerializeField] private String boolName;
    [SerializeField] private Boolean updateOnState;
    [SerializeField] private Boolean updateOnStateMachine;
    [SerializeField] private Boolean valueOnEnter, valueOnExit;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.updateOnState)
            animator.SetBool(boolName, valueOnEnter);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.updateOnState)
            animator.SetBool(boolName, valueOnExit);
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
        if (this.updateOnStateMachine)
            animator.SetBool(boolName, valueOnEnter);
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
        if (this.updateOnStateMachine)
            animator.SetBool(boolName, valueOnExit);
    }
}
