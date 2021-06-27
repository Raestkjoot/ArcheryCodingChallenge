using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 10f;
    private float meshRotSpeedUp = 0.5f;

    // The gameobject containing the player's meshes (character, weapons, etc.)
    public Transform meshTransform;
    private Quaternion meshRotation;

    void Start() {
        meshRotation = meshTransform.rotation;
    }

    // Update is called once per frame
    void Update() {
        //Rotate this object to face the mouse position
        float angle = AngleTowardsMouse(transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

        //Rotate the character a little bit behind
        meshRotation = SlowlyRotateTowards(meshRotation);
        meshTransform.rotation = meshRotation;
    }

    //Returns direct angle towards the mouse
    //Parameter: the position of the object that should point towards the mouse
    float AngleTowardsMouse(Vector3 pos){
        Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
        Vector3 vectorToMouse = Input.mousePosition - objectPos;
        
        float angle = Mathf.Atan2(vectorToMouse.x, vectorToMouse.y) * Mathf.Rad2Deg;

        return angle;
    }

    //Returns angle slowly moving towards the mouse
    //Parameter: the position of the object that should point towards the mouse
    Quaternion SlowlyRotateTowards(Quaternion rot){
        float angleDist = Mathf.Abs(Mathf.Abs(meshRotation.y) - Mathf.Abs(transform.rotation.y));
        rot = Quaternion.Slerp(rot, transform.rotation, Time.deltaTime * rotationSpeed);
        if (angleDist > meshRotSpeedUp) {
            rot = Quaternion.Slerp(rot, transform.rotation, Time.deltaTime * rotationSpeed * 2f);
        }
        return Quaternion.Euler(0f, rot.eulerAngles.y, 0f);
    }
}
