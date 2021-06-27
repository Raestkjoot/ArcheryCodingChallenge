using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject mesh;
    public Color hurtColor;

    private Color normalColor;
    private Material myMaterial;

    // Use this for initialization
    void Start() {
       myMaterial = mesh.GetComponent<Renderer>().material;
       normalColor = myMaterial.color;
    }

    /// <summary> 
    /// This is what happens to the enemy when hurt.
    /// Currently they only flash red, but we could implement damage and such.
    /// </summary>
    public void TakeDamage() {
        StartCoroutine(HurtFlash());
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
