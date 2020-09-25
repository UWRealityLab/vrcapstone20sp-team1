using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    // Start is called before the first frame update
    public Animation anim;
    public AudioClip roar;
    public AudioClip death;

    private bool isDying = false;
    void Start()
    {
        anim.Play("birth");
        this.GetComponent<AudioSource>().PlayOneShot(roar);

        /*foreach (GameObject env in GameObject.FindGameObjectsWithTag("DestructEnv"))
        {
            Rigidbody rb = env.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;*/
       // }
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.isPlaying)
        {
            if (isDying)
            {
                Destroy(this.gameObject);
            }
            anim.Play("attack2");
            GetComponent<AudioSource>().PlayOneShot(roar);
        }
    }

    public override void Death()
    {
        anim.Play("die");
        GetComponent<AudioSource>().PlayOneShot(death);
        isDying = true;
    }
    public override void HitReaction()
    {
        Debug.Log("hit");
    }

    public override void PlayHitAudio()
    {
        //GetComponent<AudioSource>().PlayOneShot(roar);
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestructEnv"))
        {
            Rigidbody rb = collision.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

            /*
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            Debug.Log("Collision");
            
        }
    }
    */
}
