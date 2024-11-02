using System;
using UnityEngine;

enum WalkDirection {
    Right,
    Left
}

public class KnightController : MonoBehaviour {
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float walkStopRate = 0.05f;
    [SerializeField] private DetectionZone attackZone;
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

    public bool HasTarget {
        get { return this._hasTarget; }
        private set {
            this._hasTarget = value;
            this.animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove {
        get { return this.animator.GetBool(AnimationStrings.canMove); }
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
        if (this.touchDirection.IsOnWall && this.touchDirection.IsGrounded)
            FlipDirection();

        if (!this.damageable.LockVelocity)
            if (this.CanMove)
                this.rigidBody2D.velocity = new Vector2(this.walkableDirection.x * this.walkSpeed, this.rigidBody2D.velocity.y);
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
        HasTarget = this.attackZone.HasTarget();
    }

    public void OnHit(Int16 damage, Vector2 knockBack) {
        this.rigidBody2D.velocity = new Vector2(knockBack.x, knockBack.y + this.rigidBody2D.velocity.y);
    }
}
