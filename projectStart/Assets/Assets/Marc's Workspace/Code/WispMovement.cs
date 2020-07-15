using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WispMovement : MonoBehaviour
{
    public Transform playerPosition;
    public Camera playerCamera;
    public float followSpeed = 0.1f;
    public float maxSpeed = 10;
    public WispAnimation animation;

    private Vector3 velocity = Vector3.zero;
    private Vector3 hoverOffset;
    private float hoverRadius;
    private GameObject target;
    private MovementType movementType;

    public enum MovementType
    {
        ROTATE_AROUND,
        BOB_NEXT_TO,
        STILL
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Transform camera = playerCamera.GetComponent<Transform>();
            Vector3 loc = playerPosition.position + Vector3.Normalize(camera.forward + (camera.up * 0.65f) + (camera.right * 0.5f)) * 4;
            transform.position = Vector3.SmoothDamp(transform.position, loc, ref velocity, followSpeed, maxSpeed);
        } else
        {

            Vector3 hoverPos = this.target.GetComponent<Transform>().position + hoverOffset;
            if ((hoverPos - this.transform.position).magnitude > hoverRadius)
            {
                transform.position = Vector3.SmoothDamp(transform.position, hoverPos, ref velocity, followSpeed, maxSpeed);
            } else
            {
                if (movementType == MovementType.ROTATE_AROUND)
                {
                    transform.RotateAround(hoverPos, Vector3.up, 180 * Time.deltaTime);
                } else if(movementType == MovementType.STILL)
                {
                    animation.floatStrength = 0;
                }
            }
        }

        
    }

    public void SetMovementType(MovementType type)
    {
        Debug.Log("SetMovementType called with type: " + type);
        movementType = type;
    }

    public void SetTarget(GameObject target, Vector3 hoverOffset, float hoverRadius = 0.5f)
    {
        this.target = target;
        this.hoverOffset = hoverOffset;
        this.hoverRadius = hoverRadius;
    }

    public void UnsetTarget()
    {
        this.target = null;
        this.hoverOffset = Vector3.zero;
        this.hoverRadius = 0;
    }
}
