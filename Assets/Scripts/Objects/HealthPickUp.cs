using System;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    [SerializeField] private HealthPickupStats healthPickupStats;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);
    private AudioSource audioSource;

    private void Awake() {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        transform.eulerAngles += this.rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null) {
            if (damageable.Heal(this.healthPickupStats.healthAmount)) {
                if (this.audioSource != null) {
                    AudioSource.PlayClipAtPoint(this.audioSource.clip, transform.position, this.audioSource.volume);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
