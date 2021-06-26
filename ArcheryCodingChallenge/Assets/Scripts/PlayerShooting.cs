using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    //public Transform shootPoint;
    public GameObject arrowPrefab;

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonUp("Fire1")) {
            Shoot();
        }
    }

    //Spawns an arrow at this transformation
    void Shoot() {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }
}
