using System;
using UnityEngine;

public class TouchDirection : MonoBehaviour {
    Animator animator;
    private ContactFilter2D castFilter;
    private float groundDistance = 0.05f;
    private float wallDistance = 0.2f;
    private float ceilingDistance = 0.05f;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private CapsuleCollider2D touchingCollider2D;
    private Boolean _isGrounded;
    private Boolean _isOnWall;
    private Boolean _isOnCeiling;
    private Vector2 wallCheckDirection;

    public bool IsGrounded {
        get {
            return this._isGrounded;
        }
        private set {
            this._isGrounded = value;
            this.animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    public bool IsOnWall {
        get {
            return this._isOnWall;
        }
        private set {
            this._isOnWall = value;
            this.animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    public bool IsOnCeiling {
        get {
            return this._isOnCeiling;
        }
        private set {
            this._isOnCeiling = value;
            this.animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake() {
        this.touchingCollider2D = GetComponent<CapsuleCollider2D>();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        CheckIsGrounded();
        CheckIsOnWall();
        CheckIsOnCeiling();
    }

    private void CheckIsGrounded() {
        this.IsGrounded = this.touchingCollider2D.Cast(Vector2.down, this.castFilter, this.groundHits, this.groundDistance) > 0;
    }

    private void CheckIsOnWall() {
        this.wallCheckDirection = gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        this.IsOnWall = this.touchingCollider2D.Cast(this.wallCheckDirection, this.castFilter, this.wallHits, this.wallDistance) > 0;
    }

    private void CheckIsOnCeiling() {
        this.IsOnCeiling = this.touchingCollider2D.Cast(Vector2.up, this.castFilter, this.ceilingHits, this.ceilingDistance) > 0;
    }
}
