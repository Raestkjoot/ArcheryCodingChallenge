using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        if(Input.GetKeyUp(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
    }
}
