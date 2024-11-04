using System;
using UnityEngine;

public class PlayOneShootBehaviour : StateMachineBehaviour {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Boolean playOnEnter = true, playOnExit = false, playAfterDelay = false;
    [SerializeField] private Single delay = 0.25f;
    private AudioSource audioSource;

    private Single timeSinceEnter = 0;
    private Boolean hasDelayedSoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.audioSource = animator.gameObject.GetComponent<AudioSource>();

        if (this.playOnEnter)
            AudioSource.PlayClipAtPoint(this.audioClip, animator.gameObject.transform.position, this.audioSource.volume);

        this.timeSinceEnter = 0;
        this.hasDelayedSoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.audioSource = animator.gameObject.GetComponent<AudioSource>();

        if (this.playAfterDelay && !this.hasDelayedSoundPlayed) {
            this.timeSinceEnter += Time.deltaTime;
            if (this.timeSinceEnter >= this.delay) {
                AudioSource.PlayClipAtPoint(this.audioClip, animator.gameObject.transform.position, this.audioSource.volume);
                this.hasDelayedSoundPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.audioSource = animator.gameObject.GetComponent<AudioSource>();

        if (this.playOnExit)
            AudioSource.PlayClipAtPoint(this.audioClip, animator.gameObject.transform.position, this.audioSource.volume);
    }
}
