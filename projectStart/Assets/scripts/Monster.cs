using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int health = 100;
    public AudioClip[] noise;

    public Animator animator;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Weapon>() != null)
        {
            Weapon w = collider.gameObject.GetComponent<Weapon>();
            health -= w.damage();
            Debug.Log("health: " + health);
            if (w.damage() > 5)
            {
                HitReaction();
            }
            if (collider.gameObject.GetComponent<NinjaStar>() != null)
                {
                    collider.gameObject.GetComponent<NinjaStar>().destroyStar();
                }
            if (health <= 0)
            {
                Death();
            }
            else
            {
                PlayHitAudio();
            }
        }
    }

    public virtual void Death()
    {
        StartCoroutine("Die", 0f);
        //Destroy(gameObject);
    }

    public virtual void HitReaction()
    {
        float knockback = 2f;
        NavMeshAgent a = gameObject.GetComponent<NavMeshAgent>();
        a.Move(transform.forward * -0.5f * knockback);
        //animator.SetBool("Hit", true);
    }

    public IEnumerator Die()
    {
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public virtual void Attack(string attackType)
    {

    }

    public virtual bool isAttacking()
    {
        return false;
    }

    public virtual void PlayHitAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(noise[Random.Range(0, noise.Length)]);
    }
}
