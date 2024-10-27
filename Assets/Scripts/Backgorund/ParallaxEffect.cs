using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
    [SerializeField] Camera cam;
    [SerializeField] Transform followTarget;

    private Vector2 startingPosition;
    private float startingZ;
    private Vector2 camMoveSinceStart;
    private float parallaxEffect;
    private float zDistanceFromTarget;
    private float clippingPane;

    void Start() {
        this.startingPosition = gameObject.transform.position;
        this.startingZ = gameObject.transform.position.z;
    }

    void Update() {
        Parallax();
    }

    void Parallax() {
        this.clippingPane = this.cam.transform.position.z + (this.zDistanceFromTarget > 0 ? this.cam.farClipPlane : this.cam.nearClipPlane);
        this.zDistanceFromTarget = gameObject.transform.position.z - this.followTarget.transform.position.z;
        this.parallaxEffect = Mathf.Abs(this.zDistanceFromTarget) / this.clippingPane;
        this.camMoveSinceStart = (Vector2)cam.transform.position - this.startingPosition;
        Vector2 newPosition = this.startingPosition + this.camMoveSinceStart * this.parallaxEffect;
        gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, this.startingZ);
    }
}
