using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRailingTarget : MonoBehaviour
{

    public Transform targetSpawnPoint;
    public Transform targetDestroyPoint;
    public RailingTarget target;
    public float spawnPeriodSeconds;

    private float lastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawn = Time.time;
        RailingTarget t = Instantiate(target, targetSpawnPoint.position, targetSpawnPoint.rotation);
        t.velocityFunc = () => Vector3.Normalize(targetDestroyPoint.position - targetSpawnPoint.position);
        float destroyTime = Time.time + 10;
        t.destroyConditionFunc = () =>  Time.time > destroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawn >= spawnPeriodSeconds)
        {
            lastSpawn = Time.time;
            RailingTarget t = Instantiate(target, targetSpawnPoint.position, targetSpawnPoint.rotation);
            t.velocityFunc = () => Vector3.Normalize(targetDestroyPoint.position - targetSpawnPoint.position);
            float destroyTime = Time.time + 10;
            t.destroyConditionFunc = () => Time.time > destroyTime;
        }
    }
}
