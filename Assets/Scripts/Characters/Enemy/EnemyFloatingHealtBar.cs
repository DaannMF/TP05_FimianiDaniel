using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyFloatingHealthBar : MonoBehaviour {
    [SerializeField] private Vector3 offset;
    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update() {
        FaceToTheCamera();
    }

    public void UpdateHealthBar(Single health, Single maxHealth) {
        slider.value = (Single)health / maxHealth;
    }

    private void FaceToTheCamera() {
        Boolean isParentFacingLeft = transform.parent.parent.localScale.x > 0;
        transform.localScale = new Vector3(isParentFacingLeft ? 1 : -1, transform.localScale.y, transform.localScale.z);
        transform.SetPositionAndRotation(transform.parent.parent.position + offset, Camera.main.transform.rotation);
    }
}
