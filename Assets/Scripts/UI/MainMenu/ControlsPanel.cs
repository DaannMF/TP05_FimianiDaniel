using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPanel : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField] private Button backButton;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;

    private void Awake() {
        this.backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDestroy() {
        this.backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked() {
        this.mainMenuPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
