using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour {

    [SerializeField] private Single flySpeed = 2.5f;
    [SerializeField] private DetectionZone biteDetectionZone;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private Single distanceThreshold = 0.1f;

    private Boolean _hasTarget;
    private Animator animator;
    private Rigidbody2D rigidBody2D;
    private Boolean _isAlive = true;
    private Transform nextWayPoint;
    private Int16 wayPointNumber = 0;

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

    public Boolean IsAlive {
        get { return this._isAlive; }
        private set {
            this._isAlive = value;
            this.animator.SetBool(AnimationStrings.isAlive, value);
        }
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
        this.animator = GetComponent<Animator>();
        this.rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        this.nextWayPoint = this.wayPoints[this.wayPointNumber];
    }

    void Update() {
        DetectTarget();
    }

    void FixedUpdate() {
        Fly();
    }

    private void DetectTarget() {
        this.HasTarget = this.biteDetectionZone.HasTarget();
        this.AttackCoolDown -= Time.deltaTime;
    }

    private void Fly() {
        if (IsAlive) {
            if (CanMove) {
                Vector2 direction = (this.nextWayPoint.position - transform.position).normalized;
                Single distance = Vector2.Distance(transform.position, this.nextWayPoint.position);

                this.rigidBody2D.velocity = direction * this.flySpeed;

                UpdateDIrection();

                if (distance < this.distanceThreshold) {
                    wayPointNumber++;
                    if (wayPointNumber >= wayPoints.Count)
                        wayPointNumber = 0;

                    this.nextWayPoint = this.wayPoints[this.wayPointNumber];
                }
            }
            else
                this.rigidBody2D.velocity = Vector2.zero;
        }
        else {
            this.rigidBody2D.gravityScale = 2f;
        }
    }

    private void UpdateDIrection() {
        Vector3 localScale = transform.localScale;
        Boolean isMovingRight = this.rigidBody2D.velocity.x > 0;
        Boolean isMovingLeft = this.rigidBody2D.velocity.x < 0;
        Boolean isFacingRight = localScale.x > 0;
        Boolean isFacingLeft = localScale.x < 0;
        if ((isFacingRight && isMovingLeft) || (isFacingLeft && isMovingRight)) {
            this.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }
}
