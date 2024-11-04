using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private Int16 damage = 10;
    [SerializeField] private Vector2 speed = new Vector2(3, 0);
    [SerializeField] private Vector2 knockBack = new Vector2(0, 0);

    private Rigidbody2D rigidBody2D;

    private void Awake() {
        this.rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Start() {
        Shoot();
    }

    private void Shoot() {
        this.rigidBody2D.velocity = new Vector2(this.speed.x * transform.localScale.x, this.speed.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Damageable>(out var damageable)) {
            Boolean isFacingRight = transform.localScale.x > 0;
            Vector2 knockBackDirection = isFacingRight ? this.knockBack : new Vector2(-this.knockBack.x, this.knockBack.y);
            if (damageable.Hit(this.damage, knockBackDirection)) {
                Debug.Log("Player hit enemy for " + this.damage + " damage");
                Destroy(this.gameObject);
            }
        }
    }
}
