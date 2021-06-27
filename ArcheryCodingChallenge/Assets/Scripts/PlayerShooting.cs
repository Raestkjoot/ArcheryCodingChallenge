using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [SerializeField] private float arrowVelocity = 15f;
    public GameObject arrowPrefab;

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonUp("Fire1")) {
            Shoot();
        }
    }

    //Shoot an arrow from this object
    void Shoot() {
        //Spawn arrow and add velocity
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * arrowVelocity, ForceMode.VelocityChange);
    }
}
