using UnityEngine;

enum WalkDirection {
    Right,
    Left
}

public class KnightController : MonoBehaviour {
    [SerializeField] private float walkSpeed = 3f;
    private Rigidbody2D rigidBody2D;
    private TouchDirection touchDirection;
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

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.touchDirection = GetComponent<TouchDirection>();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (this.touchDirection.IsOnWall && this.touchDirection.IsGrounded)
            FlipDirection();

        this.rigidBody2D.velocity = new Vector2(this.walkableDirection.x * this.walkSpeed, this.rigidBody2D.velocity.y);
    }

    private void FlipDirection() {
        if (this.WalkDirection == WalkDirection.Right)
            this.WalkDirection = WalkDirection.Left;
        else if (this.WalkDirection == WalkDirection.Left)
            this.WalkDirection = WalkDirection.Right;
        else
            Debug.LogError("Invalid WalkDirection");
    }
}
