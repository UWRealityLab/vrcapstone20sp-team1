using UnityEngine;

public class ThrowNinjaStar : MonoBehaviour
{

    public GameObject starObject;
    public GameObject starSpawn;

    private bool holding = false;
    private GameObject star;
    private Vector3 throwStart;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !holding)
        {
            SpawnStar();
            holding = true;
            
        } else if(!Input.GetMouseButton(0) && holding)
        {
            Throw();
            holding = false;
        }
    }

    void Throw()
    { 
        Vector3 throwEnd = Input.mousePosition;
        float deltaX = throwEnd.x - throwStart.x;
        float deltaY = throwEnd.y - throwStart.y;

        Rigidbody rb = star.GetComponent<Rigidbody>();
        rb.AddForce(deltaX, deltaY, Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY)));
        rb.angularVelocity = new Vector3(100000, 0, 0);
        rb.useGravity = true;

        star = null;
    }

    void SpawnStar()
    {
        star = Instantiate(starObject, starSpawn.transform.position, starSpawn.transform.rotation) as GameObject;
        throwStart = Input.mousePosition;
    }
}
