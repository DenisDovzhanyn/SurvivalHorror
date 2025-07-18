using UnityEngine;

public class scythe : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Attack") && other.gameObject.layer == 6)
        {
            Debug.Log("SURELY WE ATTACKING HERE");
            // from here i want to send an event to the enemy or maybe I dont even need to do that?
            // i will need to manage my weapons durability and i want it to go down when i hit an enemy
            // but from this event happening i also need to let the enemy know to take damange
            // so how can i do that? do this same exact check on the enemy or should i just 
            // save the time and send an event?
        }
    }
}
