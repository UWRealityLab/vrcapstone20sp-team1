using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation anim;
    void Start()
    {
        anim.Play("birth");
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.isPlaying)
        {
            anim.Play("attack2");
        } 
    }
}
