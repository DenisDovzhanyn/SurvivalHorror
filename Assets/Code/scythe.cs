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

            Health objHealth = other.gameObject.GetComponent<Health>();

            objHealth.TakeDamage(1);
            Debug.Log(objHealth.currentHealth);
        }
    }
}
