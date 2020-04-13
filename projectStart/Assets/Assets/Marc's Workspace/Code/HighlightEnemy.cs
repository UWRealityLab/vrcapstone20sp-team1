using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightEnemy : MonoBehaviour
{

    public Camera cam;
    GameObject highlighted = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject nearestObj = null;
        float nearestDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            // If both the x and y coordinate of the returned point is between 0 and 1 (and the z coordinate is positive), then the point is seen by the camera.
            
            Vector3 pos = cam.WorldToViewportPoint(enemy.transform.position);

            if (pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1 && pos.z > 0)
            {
                float dist = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(pos.x) - 0.5f, 2) + Mathf.Pow(Mathf.Abs(pos.y) - 0.5f, 2));

                if (nearestDist.CompareTo(dist) > 0)
                {
                    nearestDist = dist;
                    nearestObj = enemy;
                }
            }
        }

        if (highlighted != nearestObj)
        {
            if (highlighted != null)
                highlighted.GetComponentInChildren<ParticleSystem>().Stop();
            if (nearestObj != null)
                nearestObj.GetComponentInChildren<ParticleSystem>().Play();

            highlighted = nearestObj;
        }

    }

    public GameObject getHighlightedEnemy()
    {
        return highlighted;
    }
}
