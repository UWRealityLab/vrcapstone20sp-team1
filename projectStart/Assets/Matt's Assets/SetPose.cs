using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPose : MonoBehaviour
{
    public string[] poseNames;
    List<Vector3> pose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetList(poseNames[0]);
        }
    }
    void GetList(string poseName)
    {
        pose = new List<Vector3>();
        if (PlayerPrefs.HasKey(poseName))
        {
            Debug.Log("Setting Pose: " + poseName);
            Vector3 newV = new Vector3(0, 0, 0);
            for (int counter = 0; counter < PlayerPrefs.GetInt(poseName); counter++)
            {
                //newV = new Vector3(0, 0, 0);
                for (int i = 0; i < 3; i++)
                {
                    newV[i] = PlayerPrefs.GetFloat(poseName + counter + i);
                }
                pose.Add(newV);
            }
            SetRotation(transform);
        }
        else
        {
            Debug.Log("No Pose Exists for: " + poseName);
        }
    }
    void SetRotation(Transform parent)
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                SetRotation(child);
            }
        }
        parent.localEulerAngles =pose[0];
        pose.RemoveAt(0);

    }
}
