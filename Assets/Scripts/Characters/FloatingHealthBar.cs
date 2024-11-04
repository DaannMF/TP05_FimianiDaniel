using System;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void Update() {
        FaceToTheCamera();
    }

    public void UpdateHealthBar(Single health, Single maxHealth) {
        slider.value = (Single)health / maxHealth;
    }

    private void FaceToTheCamera() {
        Boolean isParentFacingLeft = target.localScale.x > 0;
        transform.localScale = new Vector3(isParentFacingLeft ? 1 : -1, transform.localScale.y, transform.localScale.z);
        transform.SetPositionAndRotation(target.position + offset, cameraTransform.rotation);
    }
}
