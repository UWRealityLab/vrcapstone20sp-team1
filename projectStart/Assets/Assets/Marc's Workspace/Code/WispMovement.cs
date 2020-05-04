﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WispMovement : MonoBehaviour
{
    public Transform playerPosition;
    public Camera playerCamera;
    public float followSpeed = 0.1f;
    public float maxSpeed = 10;

    private Vector3 velocity = Vector3.zero;
    private Vector3 hoverPos;
    private float hoverRadius;
    private GameObject target;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Transform camera = playerCamera.GetComponent<Transform>();
            Vector3 loc = playerPosition.position + Vector3.Normalize(camera.forward + (camera.up * 0.65f) + (camera.right * 0.5f)) * 4;
            transform.position = Vector3.SmoothDamp(transform.position, loc, ref velocity, followSpeed, maxSpeed);
        } else
        {
            if ((hoverPos - this.transform.position).magnitude > hoverRadius)
            {
                transform.position = Vector3.SmoothDamp(transform.position, hoverPos, ref velocity, followSpeed, maxSpeed);
            } else
            {
                transform.RotateAround(hoverPos, Vector3.up, 360 * Time.deltaTime);
            }
        }

        
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
        this.hoverPos = this.target.GetComponent<Transform>().position + new Vector3(0, this.target.GetComponent<Renderer>().bounds.size.y / 2 + 0.75f, 0);
        this.hoverRadius = (this.target.GetComponent<Renderer>().bounds.size.x / 2) + 0.25f;
    }

    public void unsetTarget()
    {
        this.target = null;
        this.hoverPos = Vector3.zero;
        this.hoverRadius = 0;
    }
}
