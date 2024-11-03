using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
    [SerializeField] private Int16 maxHealth = 100;
    [SerializeField] private Int16 health = 100;
    [SerializeField] private Boolean isInvincible = false;
    [SerializeField] private Single invincibilityDuration = 0.25f;
    [SerializeField] private UnityEvent<Int16, Vector2> damageEvent;
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

    public bool LockVelocity {
        get {
            return this.animator.GetBool(AnimationStrings.lockVelocity);
        }
        set {
            this.animator.SetBool(AnimationStrings.lockVelocity, value);
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

    public Boolean Hit(Int16 damage, Vector2 knockBack) {
        if (this.IsAlive && !isInvincible) {
            this.Health -= damage;
            this.isInvincible = true;

            // Invoke the damage event.
            this.animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            this.damageEvent.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(this.gameObject, damage);

            return true;
        }

        return false;
    }

    public Boolean Heal(Int16 healAmount) {
        if (this.IsAlive && this.Health < this.MaxHealth) {
            Int16 maxHeal = (Int16)Math.Max(this.MaxHealth - this.Health, 0);
            Int16 actualHealAmount = (Int16)Math.Min(maxHeal, healAmount);
            this.Health += actualHealAmount;
            CharacterEvents.characterHealed.Invoke(this.gameObject, actualHealAmount);

            return true;
        }

        return false;
    }
}
