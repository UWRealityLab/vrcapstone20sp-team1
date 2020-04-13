using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Weapon
{

    public float damageMultiplier;
    public float fadeInDurationinS;

    private Color[] originalColors;
    private float initTime;

    private Vector3 prevHandPos;
    // Start is called before the first frame update
    void Start()
    {
        Material[] materials = gameObject.GetComponent<MeshRenderer>().materials;
        this.originalColors = new Color[materials.Length];
        this.initTime = Time.time;
        for(int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
            materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Material[] materials = gameObject.GetComponent<MeshRenderer>().materials;
        for(int i = 0; i < materials.Length; i++)
        {
            Debug.Log("time: " + Time.time + " initTime: " + initTime + " og: " + originalColors[i].a);
            Debug.Log(originalColors[i].a * ((Mathf.Min(1, Time.time - initTime) / fadeInDurationinS)));
            materials[i].color = new Color(materials[i].color.r, materials[i].color.g, materials[i].color.b, originalColors[i].a * ((Mathf.Min(1, Time.time - initTime) / fadeInDurationinS)));
        }

        prevHandPos = transform.parent.position;
    }
    public int damage()
    {
        Vector3 velocity = (prevHandPos - transform.parent.position) / Time.deltaTime;
        return Mathf.RoundToInt(Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z)) * damageMultiplier);
    }
}