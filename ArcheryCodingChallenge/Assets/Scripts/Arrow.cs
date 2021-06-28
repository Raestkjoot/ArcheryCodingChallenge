using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private bool isMaxCharged = false;
    public bool IsMaxCharged
    {
        set { isMaxCharged = value; }
    }

    public GameObject hitParticles;
    public GameObject criticalHitParticles;
    public AudioSource audioSource;
    public AudioClip hitAudio;
    public AudioClip criticalHitAudio;
    public AudioClip hitOtherAudio;
    public Color hurtColor;

    [SerializeField] private int damage = 20;
    [SerializeField] private int critDamage = 50;

    private CameraShake cameraShake;
    [SerializeField] private float cameraShakeDuration = .1f;
    [SerializeField] private float normalCameraShakeMagnitude = .15f;
    [SerializeField] private float maxChargedCameraShakeMagnitude = .5f;

    // Use this for initialization
    void Start() {
        cameraShake = Camera.main.gameObject.GetComponent<CameraShake>();
        audioSource = GetComponent<AudioSource>();
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            EnemyHit(collision.gameObject);
        } else if(collision.gameObject.tag == "Border") {
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody>());
            audioSource.PlayOneShot(hitOtherAudio);
        }
    }

    /// <summary> What happens when the arrow hits an enemy.
    /// <para> Arrow sticks in enemy, particles fly, camera shakes, audio plays, time slows.
    /// </summary>
    /// <param name="enemy" The enemy that was hit by this arrow </param>
    private void EnemyHit(GameObject enemy) {
        // Arrow sticks to enemy
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.parent = enemy.transform;

        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
        GameObject effect = Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        if(isMaxCharged == false) {
            cameraShake.Shake(cameraShakeDuration, normalCameraShakeMagnitude);
            //StartCoroutine(cameraShake.Shake(cameraShakeDuration, normalCameraShakeMagnitude));
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        } else {
            cameraShake.Shake(cameraShakeDuration, maxChargedCameraShakeMagnitude);
            //StartCoroutine(cameraShake.Shake(cameraShakeDuration, maxChargedCameraShakeMagnitude));
            enemy.GetComponent<Enemy>().TakeDamage(critDamage);

            AudioSource.PlayClipAtPoint(criticalHitAudio, transform.position);
            GameObject criticalEffect = Instantiate(criticalHitParticles, transform.position, Quaternion.identity);
            Destroy(criticalEffect, 2f);
        }
    }
}