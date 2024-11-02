using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour {
    [SerializeField] private List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D collider2Dl;
    private void Awake() {
        this.collider2Dl = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        this.detectedColliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        this.detectedColliders.Remove(other);
    }

    public bool HasTarget() {
        return this.detectedColliders.Count > 0;
    }
}
