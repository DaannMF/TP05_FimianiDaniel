using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    [Header("Panels")]
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Awake() {
        this.playButton.onClick.AddListener(OnPlayButtonClicked);
        this.controlsButton.onClick.AddListener(OnControlsButtonClicked);
        this.settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        this.creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        this.exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDestroy() {
        this.playButton.onClick.RemoveListener(OnPlayButtonClicked);
        this.controlsButton.onClick.RemoveListener(OnControlsButtonClicked);
        this.settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        this.creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        this.exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnPlayButtonClicked() {
        if (SceneManager.GetActiveScene().name != "GamePlayScene")
            SceneManager.LoadScene("GamePlayScene");
        else
            this.panelMainMenu.SetActive(false);

        Time.timeScale = 1;
    }

    private void OnControlsButtonClicked() {
        this.controlsPanel.SetActive(true);
    }

    private void OnSettingsButtonClicked() {
        this.settingsPanel.gameObject.SetActive(true);
    }

    private void OnCreditsButtonClicked() {
        this.creditsPanel.gameObject.SetActive(true);
    }

    public void OnExitButtonClicked() {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        Debug.Log("Exiting game");
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            Application.Quit();
#endif
    }
}
