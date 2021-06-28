using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [SerializeField] private float baseArrowVelocity = 15f;
    [SerializeField] private float maxArrowVelocity = 15f;
    [SerializeField] private float maxChargedExtraMass = 0.8f;
    private float arrowVelocity;
    [SerializeField] private float chargeSpeed = 2f;
    [SerializeField] private float bowMinChargePosition = .3f;
    [SerializeField] private float bowMaxChargePosition = .6f;
    public GameObject arrowPrefab;

    public GameObject maxChargedParticlesPrefab;
    public GameObject loadingArrowMesh;
    public Transform bowMesh;
    private Vector3 bowIdlePosition;
    private Vector3 bowMinChargePosVec3;
    private Vector3 bowMaxChargePosVec3;

    public AudioClip shootAudio;
    public AudioClip chargeUpAudio;
    private AudioSource  audioSource;
    private ParticleSystem arrowReleaseParticles;

    private bool isCharging = false;
    private bool canPlayMaxChargeAudio = true;
    public PlayerMovement player;

    // Use this for initialization
    void Start() {
        // Setting the different bow positions when chargin a shot
        bowIdlePosition = bowMinChargePosVec3 = bowMaxChargePosVec3 
                = bowMesh.localPosition;
        bowMinChargePosVec3.z += bowMinChargePosition;
        bowMaxChargePosVec3.z += bowMaxChargePosition;

        audioSource = GetComponent<AudioSource>();
        arrowReleaseParticles = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButton("Fire1")) {
            ChargeShot();
        }
        if(Input.GetButtonUp("Fire1")) {
            Shoot();
        }
    }

    /// <summary> Charges an arrow shot </summary>
    private void ChargeShot() {
        if(isCharging == false) {
            isCharging = true;
            loadingArrowMesh.SetActive(true);
            bowMesh.localPosition = bowMinChargePosVec3;
            player.IsChargingArrow = true;
        }

        bowMesh.localPosition = Vector3.MoveTowards(bowMesh.localPosition, 
                bowMaxChargePosVec3, chargeSpeed * Time.deltaTime);

        // Gradually increase the arrow's velocity towards max
        float charge = RangePercentage(bowMesh.localPosition.z,
                bowMinChargePosVec3.z, bowMaxChargePosVec3.z);
        arrowVelocity = baseArrowVelocity + (maxArrowVelocity * charge);

        if(canPlayMaxChargeAudio && charge > 0.8f) {
            canPlayMaxChargeAudio = false;
            audioSource.clip = chargeUpAudio;
            audioSource.Play();
            GameObject particles = Instantiate(maxChargedParticlesPrefab, 
                    transform.position, Quaternion.identity);
            Destroy(particles, 2f);
        }
    }

    /// <summary> Shoot an arrow.
    /// <para> Instantiate arrow, add velocity and play shoot audioclip </para>
    /// </summary>
    private void Shoot() {
        loadingArrowMesh.SetActive(false);
        isCharging = false;
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * arrowVelocity, ForceMode.VelocityChange);

        canPlayMaxChargeAudio = true;
        player.IsChargingArrow = false;

        bowMesh.localPosition = bowIdlePosition;

        audioSource.clip = shootAudio;
        audioSource.Play();

        //MAX POWER RELEASED!!!
        if(arrowVelocity == (baseArrowVelocity + maxArrowVelocity)) {
            arrowReleaseParticles.Play();
            rb.mass += maxChargedExtraMass;
            arrow.GetComponent<Arrow>().IsMaxCharged = true;
        }
    }

    /// <summary> Calculates the current value's percentage between two values.
    /// <param name="cur"> The current value. </param>
    /// <param name="min"> The minimum value that cur can have (= 0%). </param>
    /// <param name="cur"> The maximum value that cur can have (= 100%). </param>
    /// <returns> A float that is the percentage (where 100% = 1.0f) 
    /// that the current value has in the range min(0%) to max(100%). </returns>
    private float RangePercentage (float cur, float min, float max) {
        float increase = cur - min;
        float percentageIncrease = increase / (max - min);
        return percentageIncrease;
    }
}
