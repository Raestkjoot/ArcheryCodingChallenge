using UnityEngine;

// Add a rigidbody to this object
// If you don't want your character to rotate, it's a good idea to freeze the rotation under constraints in the rigidbody component.
// The rigidbody gives the character gravity, if you don't want this, it is easily disabled in the rigidbody component.

public class PlayerMovement : MonoBehaviour
{
    private bool isChargingArrow = false;
    public bool IsChargingArrow
    {
        set { isChargingArrow = value; }
    }

    [SerializeField] private float normalSpeed = 10f;
    [SerializeField] private float chargingArrowSpeed = 5f;
    private Rigidbody myRigidbody;
    private Vector3 change;

    // Use this for initialization
    void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Get the input
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");
        // If we get input, update the character's position
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        if(isChargingArrow == false) {
            myRigidbody.MovePosition(transform.position + change.normalized * normalSpeed * Time.fixedDeltaTime);
        } else {
            myRigidbody.MovePosition(transform.position + change.normalized * chargingArrowSpeed * Time.fixedDeltaTime);
        }
    }
}