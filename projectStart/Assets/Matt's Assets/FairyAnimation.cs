using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyAnimation : MonoBehaviour
{
    Animator animation;
    string animationName;
    float animationLength;
    float startPoint;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        animationName = animation.GetCurrentAnimatorStateInfo(0).ToString();
        animationLength = animation.GetCurrentAnimatorStateInfo(0).length;
        startPoint = Random.Range(0, animationLength);
        animation.Play(animationName, 0, startPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
