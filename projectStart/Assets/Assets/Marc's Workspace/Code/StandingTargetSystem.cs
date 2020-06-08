using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingTargetSystem : MonoBehaviour
{
    public Transform targetSpawnPoint;
    public Transform targetDestroyPoint;
    public GameObject targetObject;
    public float spawnPeriodSeconds;
    public AudioClip spawnAudio;


    public bool autoSpawn;

    private  GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        spawnAudio = Resources.Load<AudioClip>("Spawn_Target_Sounds/target1"); ;
        if (autoSpawn) 
            SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null && targetSpawnPoint.position.y + (currentTarget.GetComponentInChildren<MeshFilter>().mesh.bounds.extents.y/2) < currentTarget.GetComponent<Transform>().position.y)
        {
            currentTarget.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (autoSpawn && currentTarget == null)
        {
            SpawnTarget();
        }
    }

    public bool SpawnTarget()
    {
        if (currentTarget == null)
        {
            currentTarget = Instantiate(targetObject, targetSpawnPoint.position, targetSpawnPoint.rotation);
            currentTarget.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, 2, 0);
            PlaySpawnAudio();
            return true;
        }

        return false;
    }

    public bool HasTarget()
    {
        return currentTarget != null;
    }

    public virtual void PlaySpawnAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnAudio);
    }
}
