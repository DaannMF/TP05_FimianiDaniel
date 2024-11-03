using System;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    [SerializeField] private Int16 healAmount = 25;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);

    private void Update() {
        transform.eulerAngles += this.rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null) {
            if (damageable.Heal(this.healAmount))
                Destroy(this.gameObject);
        }
    }
}
