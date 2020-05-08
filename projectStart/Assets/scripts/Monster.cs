using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int health = 100;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Weapon>() != null)
        {
            Weapon w = collider.gameObject.GetComponent<Weapon>();
            health -= w.damage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
