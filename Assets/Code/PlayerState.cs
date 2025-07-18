using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : MonoBehaviour
{
    Animator animator;
    public GameObject scythe;
    public GameObject hand;
    Vector2 moveVector;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue input)
    {
        moveVector = input.Get<Vector2>();
    }

    public void OnAttack()
    {
        if (!animator.GetBool("weaponEquipped")) animator.SetTrigger("isEquipping");
        else animator.SetTrigger("Attacking");
    }

    public void ParentScytheToHand()
    {
        animator.SetBool("weaponEquipped", true);
        scythe.transform.parent = hand.transform;
        scythe.transform.localPosition = new Vector3(0.0011f, 0.00105f, 0.00105f);
        scythe.transform.localEulerAngles = new Vector3(-167.434f, -95.931f, 74.69f);
    }
    // Update is called once per frame
    void Update()
    {
        if (moveVector != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
