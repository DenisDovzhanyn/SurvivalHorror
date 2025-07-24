using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody player;
    Rigidbody parent;
    Animator anim;
    float lastAttackTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Rigidbody>();
        parent = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, parent.position);

        lastAttackTime += Time.deltaTime;
        if (distance <= 1.5 && lastAttackTime > 0.5)
        {
            Debug.Log("Attacking");
            anim.SetTrigger("Attacking");
            anim.SetBool("isMoving", false);
            return;
        }

        anim.SetBool("isMoving", true);
        Vector3 movementDirection = (player.position - parent.position).normalized;
        parent.AddForce(movementDirection * 0.5f, ForceMode.VelocityChange);
        parent.rotation = Quaternion.LookRotation(movementDirection);
    }

    void OnTriggerStay(Collider collider)
    {
        if (lastAttackTime < 0.5) return;
        lastAttackTime = 0;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") && collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Health>().TakeDamage(1);
        }
    }
}
