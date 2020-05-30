using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    bool hit = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            hit = true;
            StartCoroutine(HitDisable());
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (hit)
        {
            if (collision.gameObject.GetComponent<Fracture>())
            {
                collision.gameObject.GetComponent<Fracture>().Execute();
            }
            hit = false;
        }
    }


    IEnumerator HitDisable()
    {
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }
}
