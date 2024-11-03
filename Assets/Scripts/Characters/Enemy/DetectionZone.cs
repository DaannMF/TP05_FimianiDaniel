using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour {

    [SerializeField] private List<Collider2D> detectedColliders = new List<Collider2D>();
    [SerializeField] private UnityEvent NoCollidersDetected;

    Collider2D collider2Dl;
    private void Awake() {
        this.collider2Dl = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        this.detectedColliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        this.detectedColliders.Remove(other);
        if (!HasTarget()) {
            NoCollidersDetected.Invoke();
        }
    }

    public Boolean HasTarget() {
        return this.detectedColliders.Count > 0;
    }
}
