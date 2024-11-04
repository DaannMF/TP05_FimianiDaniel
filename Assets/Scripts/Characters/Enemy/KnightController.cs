using System;
using UnityEngine;

enum WalkDirection {
    Right,
    Left
}

public class KnightController : MonoBehaviour {
    [SerializeField] private float walkAcceleration = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float walkStopRate = 0.05f;
    [SerializeField] private DetectionZone attackZone;
    [SerializeField] private DetectionZone cliffZone;
    Animator animator;
    private Rigidbody2D rigidBody2D;
    private TouchDirection touchDirection;
    private Damageable damageable;
    private Vector2 walkableDirection = Vector2.right;
    private WalkDirection walkDirection;
    private WalkDirection WalkDirection {
        get {
            return this.walkDirection;
        }
        set {
            if (this.walkDirection != value)
                gameObject.transform.localScale *= new Vector2(-1, 1);

            if (value == WalkDirection.Right)
                this.walkableDirection = Vector2.right;
            else if (value == WalkDirection.Left)
                this.walkableDirection = Vector2.left;

            this.walkDirection = value;
        }
    }
    private Boolean _hasTarget;

    private bool HasTarget {
        get { return this._hasTarget; }
        set {
            this._hasTarget = value;
            this.animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private bool CanMove {
        get { return this.animator.GetBool(AnimationStrings.canMove); }
    }

    private Single AttackCoolDown {
        get {
            return this.animator.GetFloat(AnimationStrings.attackCoolDown);
        }
        set {
            this.animator.SetFloat(AnimationStrings.attackCoolDown, Mathf.Max(value, 0));
        }
    }

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.touchDirection = GetComponent<TouchDirection>();
        this.animator = GetComponent<Animator>();
        this.damageable = GetComponent<Damageable>();
    }

    private void Update() {
        DetectTarget();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (this.touchDirection.IsOnWall && this.touchDirection.IsGrounded || !this.cliffZone.HasTarget())
            FlipDirection();

        if (!this.damageable.LockVelocity)
            if (this.CanMove && this.touchDirection.IsGrounded) {
                Single xVelocity = Mathf.Clamp(
                    this.rigidBody2D.velocity.x + (this.walkableDirection.x * this.walkAcceleration * Time.fixedDeltaTime),
                    -this.maxSpeed, this.maxSpeed
                );
                this.rigidBody2D.velocity = new Vector2(xVelocity, this.rigidBody2D.velocity.y);
            }
            else
                this.rigidBody2D.velocity = new Vector2(Mathf.Lerp(this.rigidBody2D.velocity.x, 0, this.walkStopRate), this.rigidBody2D.velocity.y);
    }

    private void FlipDirection() {
        if (this.WalkDirection == WalkDirection.Right)
            this.WalkDirection = WalkDirection.Left;
        else if (this.WalkDirection == WalkDirection.Left)
            this.WalkDirection = WalkDirection.Right;
        else
            Debug.LogError("Invalid WalkDirection");
    }

    private void DetectTarget() {
        this.HasTarget = this.attackZone.HasTarget();
        this.AttackCoolDown -= Time.deltaTime;
    }

    public void OnHit(Int16 damage, Vector2 knockBack) {
        this.rigidBody2D.velocity = new Vector2(knockBack.x, knockBack.y + this.rigidBody2D.velocity.y);
    }

    public void OnCliffDetected() {
        if (this.touchDirection.IsGrounded)
            FlipDirection();
    }
}
