using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float airSpeed = 3f;
    [SerializeField] float jumpImpulse = 10f;
    private TouchDirection touchDirection;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private Damageable damageable;
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
            if (this.CanMove)
                if (this.IsMoving && !this.touchDirection.IsOnWall)
                    if (this.touchDirection.IsGrounded)
                        return this.IsRunning ? this.runSpeed : this.walkSpeed;
                    else
                        return this.airSpeed;

            return 0;
        }
    }

    private Boolean IsFacingRight {
        get {
            return this._isFacingRight;
        }
        set {
            if (this._isFacingRight != value)
                gameObject.transform.localScale *= new Vector2(-1, 1);

            this._isFacingRight = value;
        }
    }

    private Boolean CanMove {
        get {
            return this.animator.GetBool(AnimationStrings.canMove);
        }
    }

    private Boolean IsAlive {
        get {
            return this.animator.GetBool(AnimationStrings.isAlive);
        }
    }

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.touchDirection = GetComponent<TouchDirection>();
        this.damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (!this.damageable.LockVelocity)
            this.rigidBody2D.velocity = new Vector2(this.moveInput.x * this.CurrentMoveSpeed, this.rigidBody2D.velocity.y);

        this.animator.SetFloat(AnimationStrings.yVelocity, this.rigidBody2D.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        this.moveInput = ctx.ReadValue<Vector2>();
        if (this.IsAlive) {
            this.IsMoving = this.moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else {
            this.IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput) {
        if (moveInput.x > 0 && !IsFacingRight)
            this.IsFacingRight = true;
        else if (moveInput.x < 0 && IsFacingRight)
            this.IsFacingRight = false;
    }

    public void OnRun(InputAction.CallbackContext ctx) {
        if (ctx.started)
            this.IsRunning = true;
        else if (ctx.canceled)
            this.IsRunning = false;
    }

    public void OnJump(InputAction.CallbackContext ctx) {
        if (ctx.started && this.touchDirection.IsGrounded && this.CanMove) {
            this.animator.SetTrigger(AnimationStrings.jumpTrigger);
            this.rigidBody2D.velocity = new Vector2(this.rigidBody2D.velocity.x, this.jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            this.animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangeAttack(InputAction.CallbackContext ctx) {
        if (ctx.started && this.touchDirection.IsGrounded) {
            this.animator.SetTrigger(AnimationStrings.rangeAttackTrigger);
        }
    }

    public void OnHit(Int16 damage, Vector2 knockBack) {
        this.rigidBody2D.velocity = new Vector2(knockBack.x, knockBack.y + this.rigidBody2D.velocity.y);
    }
}
