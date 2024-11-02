using System;
using UnityEngine;

public class FadedRemoveBehaviour : StateMachineBehaviour {
    [SerializeField] private Single fadeOutDuration = 0.25f;
    private Single timeSinceFadeOut = 0;
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private GameObject gameObjectToDestroy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.timeSinceFadeOut = 0;
        this.spriteRenderer = animator.GetComponent<SpriteRenderer>();
        this.startColor = this.spriteRenderer.color;
        this.gameObjectToDestroy = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Single newAlpha = this.startColor.a * (1 - this.timeSinceFadeOut / this.fadeOutDuration);
        this.timeSinceFadeOut += Time.deltaTime;
        this.spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        if (this.timeSinceFadeOut >= this.fadeOutDuration) {
            Destroy(this.gameObjectToDestroy);
        }
    }
}
