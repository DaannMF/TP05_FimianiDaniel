using System;
using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] private Int16 maxHealth = 100;
    [SerializeField] private Int16 health = 100;
    [SerializeField] private Boolean isInvincible = false;
    [SerializeField] private Single invincibilityDuration = 0.25f;

    Animator animator;
    private Boolean _isAlive = true;
    private Single timeSinceLastHit = 0;

    public Int16 MaxHealth {
        get { return this.maxHealth; }
        set { this.maxHealth = value; }
    }

    public Int16 Health {
        get { return this.health; }
        set {
            this.health = value;
            if (this.health <= 0) {
                this.IsAlive = false;
            }
        }
    }

    public Boolean IsAlive {
        get { return this._isAlive; }
        private set {
            this._isAlive = value;
            this.animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    private void Awake() {
        this.animator = GetComponent<Animator>();
    }

    private void Update() {
        CheckLastHit();
    }

    private void CheckLastHit() {
        if (this.isInvincible) {
            if (this.timeSinceLastHit >= this.invincibilityDuration) {
                this.isInvincible = false;
                this.timeSinceLastHit = 0;
            }
            else {
                this.timeSinceLastHit += Time.deltaTime;
            }
        }
    }

    private void Hit(Int16 damage) {
        if (this.IsAlive && !isInvincible) {
            this.Health -= damage;
            this.isInvincible = true;
        }
    }
}
