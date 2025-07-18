using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    Vector2 moveVector;
    Rigidbody rb;
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
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        Vector3 sideWays = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);

        rb.AddForce(forward.normalized * moveVector.y + sideWays.normalized * moveVector.x, ForceMode.VelocityChange);

        if (moveVector != Vector2.zero)
        {
            rb.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized);
        }
    }

    public void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
    }
}
