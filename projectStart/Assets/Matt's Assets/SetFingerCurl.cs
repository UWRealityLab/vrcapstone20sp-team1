using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SetFingerCurl : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    SteamVR_Behaviour_Skeleton leftSkeleton;
    SteamVR_Behaviour_Skeleton rightSkeleton;
    SteamVR_Behaviour_Skeleton activeSkeleton;
    float[] defaultCurls;
    [Range(0, 1)]
    public float thumbCurl;
    [Range(0, 1)]
    public float indexCurl;
    [Range(0, 1)]
    public float middleCurl;
    [Range(0, 1)]
    public float ringCurl;
    [Range(0, 1)]
    public float pinkyCurl;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(slightDelay());
        //leftSkeleton = GetSkeleton(leftHand);
        //rightSkeleton = GetSkeleton(rightHand);
        //activeSkeleton = leftSkeleton;
    }
    IEnumerator slightDelay()
    {
        yield return new WaitForSeconds(.1f);
        leftSkeleton = GetSkeleton(leftHand);
        rightSkeleton = GetSkeleton(rightHand);
        activeSkeleton = leftSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (activeSkeleton != null)
        {
            //activeSkeleton.updatePose = false;
            //activeSkeleton.skeletonBlend = .25f;
        }
        //activeSkeleton.thumbCurl = thumbCurl;
    }
    SteamVR_Behaviour_Skeleton GetSkeleton(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach(Transform t in parent)
        {
            children.Add(t);
        }
        while(children.Count > 0)
        {
            if(children[0].GetComponent<SteamVR_Behaviour_Skeleton>() != null)
            {
                return children[0].GetComponent<SteamVR_Behaviour_Skeleton>();
            }
            else
            {
                foreach (Transform t in children[0])
                {
                    children.Add(t);
                }
                children.RemoveAt(0);
            }
        }
        return null;
    }

}
