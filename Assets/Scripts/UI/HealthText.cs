using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour {
    [SerializeField] Vector3 moveSpeed = new Vector3(0, 75f, 0);
    [SerializeField] private Single timeToFade = 2.5f;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private Single elapsedTime = 0f;
    private Color startColor;

    private void Awake() {
        this.textTransform = GetComponent<RectTransform>();
        this.textMeshPro = GetComponent<TextMeshProUGUI>();
        this.startColor = this.textMeshPro.color;
    }

    private void Update() {
        FadeOutText();
    }

    void FadeOutText() {
        this.textTransform.position += this.moveSpeed * Time.deltaTime;
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime < this.timeToFade) {
            Single fadeAlpha = this.startColor.a * (1 - this.elapsedTime / this.timeToFade);
            this.textMeshPro.color = new Color(this.startColor.r, this.startColor.g, this.startColor.b, fadeAlpha);
        }
        else {
            Destroy(gameObject);
        }
    }
}
