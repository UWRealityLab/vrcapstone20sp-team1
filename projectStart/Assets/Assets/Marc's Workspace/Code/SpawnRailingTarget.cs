using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRailingTarget : MonoBehaviour
{

    public Transform targetSpawnPoint;
    public Transform targetDestroyPoint;
    public Vector3 start, end;
    public RailingTarget target;
    public float spawnPeriodSeconds;

    private int activeTargets;

    // Start is called before the first frame update
    void Start()
    {
        start = targetSpawnPoint.position;
        end = targetDestroyPoint.position;
    }
    void Update()
    {
    }
    public IEnumerator Spawn3Targets()
    {
        activeTargets = 3;
        SpawnTarget(1, 3);
        yield return new WaitForSeconds(1);
        SpawnTarget(2, 3);
        yield return new WaitForSeconds(1);
        SpawnTarget(3, 3);
    }

    private void SpawnTarget(int thisNumber, int total)
    {
        int spacing = total + 1;
        RailingTarget t = Instantiate(target, targetSpawnPoint.position, targetSpawnPoint.rotation);
        t.velocityFunc = () => Mathf.Abs((t.GetAnchor().GetComponent<Transform>().position - ((end - start) * (spacing - thisNumber) / (spacing) + start)).magnitude) > 0.1f ? Vector3.Normalize(targetDestroyPoint.position - targetSpawnPoint.position) * 3 : Vector3.zero;
        t.targetSystem = this;
    }

    public void DecrementActiveTargets()
    {
        activeTargets--;
        Debug.Log("targets left: " + activeTargets);
    }

    public int ActiveTargets()
    {
        return activeTargets;
    }
}
