using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public float damageMultiplier;

    private GameObject targetEnemy;
    private float aimAssistValue;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, 
                                                    targetEnemy.transform.position, 
                                                    ref velocity, aimAssistValue,
                                                    gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
    public int damage()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
    }

    public void setAimAssist(GameObject target, float aimAssistValue)
    {
        targetEnemy = target;
        this.aimAssistValue = aimAssistValue;
    }
}