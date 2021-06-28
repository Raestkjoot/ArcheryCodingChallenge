using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public Renderer rend;
    public Material hoverMaterial;
    public Material downMaterial;
    private Material normalMaterial;

    void Start() {
       normalMaterial = rend.material;
    }
    void OnMouseEnter() {
        rend.material = hoverMaterial;
    }
    void OnMouseExit() {
        rend.material = normalMaterial;
    }
    void OnMouseOver() {
        if(Input.GetButtonDown("Fire1")){
            rend.material = downMaterial;
        }
        if(Input.GetButtonUp("Fire1")){
            rend.material = hoverMaterial;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}
