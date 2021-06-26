using UnityEngine;

//Add a rigidbody to this object
//If you don't want your character to rotate, it's a good idea to freeze the rotation under constraints in the rigidbody component.
//The rigidbody gives the character gravity, if you don't want this, it is easily disabled in the rigidbody component.

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody myRigidbody;
    private Vector3 change;

    // Use this for initialization
    void Start() {
        //The rigidbody is automatically added at initialization.
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        //Get the input
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");
        //If we get input, update the character's position
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
    }
}