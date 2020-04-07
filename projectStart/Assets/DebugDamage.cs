using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugDamage : MonoBehaviour
{
    // Start is called before the first frame update


    public Text damageText;
    void Start()
    {
        damageText.text = "Has Not Been Hit Yet";
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Weapon weapon = obj.GetComponent<Weapon>();

        damageText.text = "Hit by a weapon \nDamage: " + weapon.damage();
    }
}
