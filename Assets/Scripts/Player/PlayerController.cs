using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] float walkSpeed = 5f;
    private Vector2 moveInput;
    public Boolean IsMoving { get; private set; }
    Rigidbody2D rigidBody2D;

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        this.rigidBody2D.velocity = new Vector2(this.moveInput.x * this.walkSpeed, this.rigidBody2D.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext ctx) {
        this.moveInput = ctx.ReadValue<Vector2>();
        IsMoving = this.moveInput != Vector2.zero;
    }
}
