using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public GameObject HitParticles;
    public CameraShake cameraShake;
    public AudioClip hitAudio;
    public Color hurtColor;

    private float cameraShakeDuration = .1f;
    private float cameraShakeMagnitude = .35f;

    // Use this for initialization
    void Start() {
        cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            StartCoroutine(EnemyHit(collision.gameObject));
        } else if(collision.gameObject.tag == "Border") {
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    /// <summary> What happens when the arrow hits an enemy.
    /// <para> Arrow sticks in enemy, particles fly, camera shakes, audio plays, time slows.
    /// </summary>
    /// <param name="enemy" The enemy that was hit by this arrow </param>
    private IEnumerator EnemyHit(GameObject enemy) {
        // Arrow sticks to enemy
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.parent = enemy.transform;

        GameObject effect = Instantiate(HitParticles, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude));

        enemy.GetComponent<Enemy>().TakeDamage();

        // Slow time for a split second
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 1f;
    }
}