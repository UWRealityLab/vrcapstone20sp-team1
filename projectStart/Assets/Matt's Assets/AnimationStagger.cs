using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStagger : MonoBehaviour
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
        //animationName = animation.name;
        Debug.Log("Animation Name: " + animationName);
        animationLength = animation.GetCurrentAnimatorStateInfo(0).length;
        startPoint = Random.Range(0, animationLength);
        //Debug.Log("StartPoint: " + startPoint);
        //animation.Play(animationName, 0, startPoint/animationLength);
        animation.Play("Fairy Animation", 0, startPoint / animationLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
