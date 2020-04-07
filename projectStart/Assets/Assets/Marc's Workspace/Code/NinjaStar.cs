using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public float damageMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int damage()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Collider>().isTrigger = true; 
    }
}