using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class GetPose : MonoBehaviour
{
    List<Vector3> pose = new List<Vector3>();
    public string poseName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            GetRotations(transform);
            SavePose();
        }

    }

    void GetRotations(Transform parent)
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                GetRotations(child);
            }
        }
        pose.Add(parent.localEulerAngles);
    }
    void SavePose()
    {
        PlayerPrefs.SetInt(poseName, pose.Count);
        for (int counter = 0; counter < pose.Count; counter++)
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetFloat(poseName + counter + i, pose[counter][i]);
            }
        }
        Debug.Log("Pose Saved: " + poseName);
    }
}
