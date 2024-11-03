using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private GameObject healthTextPrefab;
    [SerializeField] private Canvas gameCanvas;

    private void Awake() {
        this.gameCanvas = FindObjectOfType<Canvas>();
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDestroy() {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, Int16 damage) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //TODO : Add a pool for the text objects
        TMP_Text tMP_Text = Instantiate(this.damageTextPrefab, spawnPosition, Quaternion.identity, this.gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tMP_Text.text = damage.ToString();
    }

    public void CharacterHealed(GameObject character, Int16 heal) {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //TODO : Add a pool for the text objects
        TMP_Text tMP_Text = Instantiate(this.healthTextPrefab, spawnPosition, Quaternion.identity, this.gameCanvas.transform)
            .GetComponent<TMP_Text>();
        tMP_Text.text = heal.ToString();
    }
}