using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public GameObject HitParticles;
    public CameraShake cameraShake;
    public AudioClip hitAudio;

    // Use this for initialization
    void Start() {
        cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
    }

    //OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            StartCoroutine(EnemyHit(collision.gameObject.transform));
        } else if(collision.gameObject.tag == "Border") {
            Destroy(gameObject);
        }
    }

    ///<summary> What happens when the arrow hits an enemy.
    ///<para> arrow sticks in enemy, particles fly, camera shakes, audio plays, time slows.
    ///</summary>
    ///<param name="enemy" The enemy that was hit by this arrow </param>
    IEnumerator EnemyHit(Transform enemy) {
        //Arrow sticks to enemy
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.parent = enemy;

        //Particle effects
        GameObject effect = Instantiate(HitParticles, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        //Camera shake
        StartCoroutine(cameraShake.Shake(.1f, .14f));

        //Slow time for a split second
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1f;
    }
}