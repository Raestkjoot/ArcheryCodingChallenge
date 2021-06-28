using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject mesh;
    public Color hurtColor;
    public AudioClip explosionAudio;

    [SerializeField] private int curHealth;
    [SerializeField] private int maxHealth = 100;

    public GameObject goopSplatterPrefab;
    public GameObject enemyDieParticlesPrefab;

    private Color normalColor;
    private Material myMaterial;

    // Use this for initialization
    void Start() {
       myMaterial = mesh.GetComponent<Renderer>().material;
       normalColor = myMaterial.color;
       curHealth = maxHealth;
    }

    /// <summary> 
    /// This is what happens to the enemy when hurt.
    /// Currently they only flash red, but we could implement damage and such.
    /// </summary>
    public void TakeDamage(int damage) {
        curHealth -= damage;
        if(curHealth <= 0) {
            Die();
        } else {
            StartCoroutine(HurtFlash());
        }
    }

    /// <summary> 
    /// Destroy this enemy in a big explosion of guts!
    /// </summary>
    public void Die() {
        Vector3 splatterPos = transform.position;
        splatterPos.y = 0.1f;
        Instantiate(goopSplatterPrefab, splatterPos, Quaternion.identity);
        Instantiate(enemyDieParticlesPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Changes this enemies color to hurtColor for a split second.
    /// <para> very juicy. </para>
    /// </summary>
    private IEnumerator HurtFlash() {
        myMaterial.color = hurtColor;
        yield return new WaitForSeconds(0.02f);
        myMaterial.color = normalColor;
    }
}
