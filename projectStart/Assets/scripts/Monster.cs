using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int health = 100;
    public AudioClip[] noise;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Weapon>() != null)
        {
            Weapon w = collider.gameObject.GetComponent<Weapon>();
            health -= w.damage();
            if (w.damage() > 5)
            {
                float knockback = 2f;
                NavMeshAgent a = gameObject.GetComponent<NavMeshAgent>();
                a.Move(transform.forward * -1 * knockback);
                
            }
            if (collider.gameObject.GetComponent<NinjaStar>() != null)
                {
                    collider.gameObject.GetComponent<NinjaStar>().destroyStar();
                }
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(noise[Random.Range(0, noise.Length)]);
            }
        }
    }
}
