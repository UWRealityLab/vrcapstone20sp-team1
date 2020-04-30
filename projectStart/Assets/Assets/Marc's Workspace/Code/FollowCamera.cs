using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCamera : MonoBehaviour
{
    public Transform playerPosition;
    public Camera playerCamera;

    public GameObject sampleObject;

    public float followSpeed = 0.1f;
    public float maxSpeed = 10;


    private Vector3 velocity = Vector3.zero;
    private Vector3 hoverPos;
    private float hoverRadius;
    void Start()
    {
        hoverPos = sampleObject.GetComponent<Transform>().position + new Vector3(0, sampleObject.GetComponent<Renderer>().bounds.size.y / 2 + 0.75f, 0);
        hoverRadius = (sampleObject.GetComponent<Renderer>().bounds.size.x / 2) + 0.25f;
    }

    // Update is called once per frame
    void Update()
    {

        Transform camera = playerCamera.GetComponent<Transform>();
        Vector3 loc;

        if(!Input.GetKey(KeyCode.Space))
        {
            loc = playerPosition.position + Vector3.Normalize(camera.forward + (camera.up * 0.65f) + (camera.right * 0.5f)) * 4;
            transform.position = Vector3.SmoothDamp(transform.position,
                                                loc,
                                                ref velocity,
                                                followSpeed,
                                                maxSpeed);
        } else
        {
            if((hoverPos - this.transform.position).magnitude > hoverRadius)
            {
                transform.position = Vector3.SmoothDamp(transform.position,
                                                hoverPos,
                                                ref velocity,
                                                followSpeed,
                                                maxSpeed);
            } else
            {
                transform.RotateAround(hoverPos, Vector3.up, 360 * Time.deltaTime);
            }
        }

        
    }
}
