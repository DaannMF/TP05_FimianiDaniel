using System;
using UnityEngine;

public class Attack : MonoBehaviour {
    [SerializeField] private Int16 damage = 10;
    [SerializeField] private Vector2 knockBack = Vector2.zero;

    public Int16 Damage {
        get { return this.damage; }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Damageable>(out var damageable)) {
            Boolean isFacingRight = transform.parent.localScale.x > 0;
            Vector2 knockBackDirection = isFacingRight ? this.knockBack : new Vector2(-this.knockBack.x, this.knockBack.y);
            if (damageable.Hit(this.damage, knockBackDirection))
                Debug.Log("Player hit enemy for " + this.damage + " damage");
        }
    }
}