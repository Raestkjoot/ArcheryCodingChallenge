using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public GameObject HitParticles;
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            StartCoroutine(EnemyHit(collision.gameObject.transform));
        } else if(collision.gameObject.tag == "Border") {
            Destroy(gameObject);
        }
    }

    //When this arrow hits an enemy, it gets stuck to them.
    IEnumerator EnemyHit(Transform enemy) {
        //Arrow sticks to enemy
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.parent = enemy;

        //Particle effects
        Instantiate(HitParticles, transform.position, Quaternion.identity);

        //Slow time for a split second
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.015f);
        Time.timeScale = 1f;
    }
}
