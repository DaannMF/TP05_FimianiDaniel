using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour {

    [Header("Controls")]
    [SerializeField] private Button backButton;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;

    private void Awake() {
        this.backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void Start() {
        SetMusicVolume();
        SetSFXVolume();
    }

    private void OnDestroy() {
        this.backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    public void SetMusicVolume() {
        this.audioMixer.SetFloat("music", Mathf.Log10(this.musicSlider.value) * 20);
    }

    public void SetSFXVolume() {
        this.audioMixer.SetFloat("sfx", Mathf.Log10(this.sfxSlider.value) * 20);
    }

    private void OnBackButtonClicked() {
        this.mainMenuPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
