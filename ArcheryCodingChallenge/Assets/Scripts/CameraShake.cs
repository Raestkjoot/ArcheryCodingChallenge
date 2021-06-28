using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;

    // Use this for initialization
    void Start() {
        originalPos = transform.localPosition;
    }

    /// <summary> Shakes the camera. </summary>
    /// <param name="duration"> The duration of the shaking of the camera. </param>
    /// <param name="magnitude"> Magnitudes of the shaking. A higher value will move the camera further per "shake". </param>
    public IEnumerator Shake (float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPos.y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }
    
        transform.localPosition = originalPos;
    }
}