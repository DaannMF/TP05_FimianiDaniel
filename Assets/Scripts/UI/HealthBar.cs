using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthBar;

    GameObject player;
    Damageable damageable;

    private void OnEnable() {
        this.damageable.healthEvent.AddListener(UpdateHealthBar);
    }

    private void Awake() {
        this.player = GameObject.FindGameObjectWithTag("Player");
        if (this.player == null) Debug.LogError("Player not found");
        this.damageable = this.player.GetComponent<Damageable>();
    }

    private void OnDestroy() {
        this.damageable.healthEvent.RemoveListener(UpdateHealthBar);
    }

    private Single CalculateHealth(Single currentHealth, Single maxHealth) {
        return currentHealth / maxHealth;
    }

    private void UpdateHealthBar(Int16 currentHealth, Int16 maxHealth) {
        this.healthBar.value = CalculateHealth(currentHealth, maxHealth);
        this.healthText.text = $"HP :  {currentHealth}/{maxHealth}";
    }
}
