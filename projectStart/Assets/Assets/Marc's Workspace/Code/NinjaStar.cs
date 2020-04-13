using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour, Weapon
{

    public float damageMultiplier;
    public float durationBeforeVanishSeconds;
    public GameObject poof;

    private GameObject targetEnemy;
    private float aimAssistValue;
    private Vector3 velocity = Vector3.zero;

    private float throwTime = float.MaxValue;
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

        if(Time.time > throwTime + durationBeforeVanishSeconds)
        {

            Instantiate(poof, transform.position, transform.rotation);
            Destroy(this.gameObject);
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

    public void setThrowTime(float time)
    {
        this.throwTime = time;
    }
}