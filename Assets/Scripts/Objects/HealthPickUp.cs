using System;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    [SerializeField] private Int16 healAmount = 25;
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 180, 0);
    [SerializeField] private AudioSource audioSource;

    private void Awake() {
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        transform.eulerAngles += this.rotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null) {
            if (damageable.Heal(this.healAmount)) {
                if (this.audioSource != null) {
                    AudioSource.PlayClipAtPoint(this.audioSource.clip, transform.position, this.audioSource.volume);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
