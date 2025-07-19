using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    Vector2 moveVector;
    Rigidbody rb;
    Vector3 camForward;
    Vector3 camRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (moveVector != Vector2.zero)
        {
            rb.AddForce(camForward.normalized * moveVector.y + camRight.normalized * moveVector.x, ForceMode.VelocityChange);
            Vector3 linearVelocityNoY = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.rotation = Quaternion.LookRotation(linearVelocityNoY.normalized);
        }
    }

    public void OnMove(InputValue value)
    {
        //* why do we store cam forward and right? 
        //* because when a camera moves between positions, as long as the player keeps holding the
        //* same movement key/direction, we want to keep moving in that direction 
        //* making it feel better than instantly changing direction while holding the same key
        //* when switching between cam positions
        moveVector = value.Get<Vector2>();
        camForward = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        camRight = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
    }
}
