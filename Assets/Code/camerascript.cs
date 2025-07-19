using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    Rigidbody player;
    Camera cam;
    readonly int CAMERA_TRANSITION_LAYER = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("player").GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        cam.transform.LookAt(player.transform.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != CAMERA_TRANSITION_LAYER) return;

        var cameraPositions = other.gameObject.GetComponent<cameraTransitionVolumeData>();
        Vector3 direction = player.transform.position - other.gameObject.transform.position;
        float dot = Vector3.Dot(other.gameObject.transform.forward, direction);

        // position one should always be opposite of the transition volumes forward
        // meaning the volume should always be facing in the direction that progresses deeper into the map
        if (dot < 0) cam.transform.position = cameraPositions.cameraPositionOne.transform.position;
        else cam.transform.position = cameraPositions.cameraPositionTwo.transform.position;
    }
}
