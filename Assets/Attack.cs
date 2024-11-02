using UnityEngine;

public class Attack : MonoBehaviour {
    Collider2D collider2D;

    private void Awake() {
        this.collider2D = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {

    }
}
