using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerPosition;
    public Camera playerCamera;


    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Transform camera = playerCamera.GetComponent<Transform>();

        Debug.Log(camera.forward);

        Vector3 loc = playerPosition.position + Vector3.Normalize(camera.forward + (camera.up * 0.8f) + (camera.right * 0.5f)) * 4;

        transform.position = Vector3.SmoothDamp(transform.position,
                                                loc,
                                                ref velocity,
                                                1f,
                                                2);
    }
}
