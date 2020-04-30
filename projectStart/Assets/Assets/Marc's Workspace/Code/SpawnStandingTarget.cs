﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStandingTarget : MonoBehaviour
{
    public Transform targetSpawnPoint;
    public Transform targetDestroyPoint;
    public GameObject targetObject;
    public float spawnPeriodSeconds;

    private  GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            SpawnTarget();
        } else if (targetSpawnPoint.position.y + (currentTarget.GetComponentInChildren<MeshFilter>().mesh.bounds.extents.y/2) < currentTarget.GetComponent<Transform>().position.y)
        {
            currentTarget.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    private void SpawnTarget()
    {
        currentTarget = Instantiate(targetObject, targetSpawnPoint.position, targetSpawnPoint.rotation);
        currentTarget.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 2, 0);
    }
}