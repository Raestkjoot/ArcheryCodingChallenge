using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    [SerializeField] private float arrowVelocity = 10;
    public Rigidbody rb;
    private float destroyTime = 6f;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = transform.forward * arrowVelocity;
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("I hit a " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
