using UnityEngine;

public class camerascript : MonoBehaviour
{
    GameObject one;
    GameObject two;
    Rigidbody player;
    Camera cur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Rigidbody>();
        one = GameObject.Find("Main Camera");
        two = GameObject.Find("camtwo");
        cur = one.GetComponent<Camera>();
        two.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        cur.transform.LookAt(player.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
       
    }
}
