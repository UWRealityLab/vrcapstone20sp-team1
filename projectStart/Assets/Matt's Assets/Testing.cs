using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour, Weapon
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bump");
    }
    public int damage()
    {
        return 5;
    }

}
