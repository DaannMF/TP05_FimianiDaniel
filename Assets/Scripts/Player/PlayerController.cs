using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Vector2 moveInput;
    private Boolean _isMoving = false;
    private Boolean _isRunning = false;
    private Boolean _isFacingRight = true;
    public Boolean IsMoving {
        get {
            return _isMoving;
        }
        private set {
            _isMoving = value;
            this.animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    public Boolean IsRunning {
        get {
            return _isRunning;
        }
        private set {
            _isRunning = value;
            this.animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    private float CurrentMoveSpeed {
        get {
            if (this.IsMoving) {
                return this.IsRunning ? runSpeed : walkSpeed;
            }
            return 0;
        }
    }

    private Boolean IsFacingRight {
        get {
            return _isFacingRight;
        }
        set {
            if (_isFacingRight != value)
                gameObject.transform.localScale *= new Vector2(-1, 1);

            _isFacingRight = value;
        }
    }

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        this.rigidBody2D.velocity = new Vector2(this.moveInput.x * this.CurrentMoveSpeed, this.rigidBody2D.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        this.moveInput = ctx.ReadValue<Vector2>();
        IsMoving = this.moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !IsFacingRight)
            IsFacingRight = true;
        else if (moveInput.x < 0 && IsFacingRight)
            IsFacingRight = false;
    }

    public void OnRun(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            IsRunning = true;
        }
        else if (ctx.canceled) {
            IsRunning = false;
        }
    }
}
