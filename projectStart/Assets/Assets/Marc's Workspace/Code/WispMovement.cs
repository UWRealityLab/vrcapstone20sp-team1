using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WispMovement : MonoBehaviour
{

    public class TargetProps
    {
        public GameObject target;
        public Vector3 hoverOffset;
        public float hoverRadius;

        public TargetProps(GameObject target, Vector3 hoverOffset, float hoverRadius)
        {
            this.target = target;
            this.hoverOffset = hoverOffset;
            this.hoverRadius = hoverRadius;
        }
    }

    public Transform playerPosition;
    public Camera playerCamera;
    public float followSpeed = 0.1f;
    public float maxSpeed = 10;
    public WispAnimation wispAnimation;

    private Vector3 velocity = Vector3.zero;
    private MovementType movementType;


    private Stack<TargetProps> targets = new Stack<TargetProps>();
    public TargetProps currentTarget;

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
        if (targets.Count == 0 && (currentTarget == null || currentTarget.target == null))
        {
            Transform camera = playerCamera.GetComponent<Transform>();
            Vector3 loc = playerPosition.position + Vector3.Normalize(camera.forward + (camera.up * 0.65f) + (camera.right * 0.5f)) * 4;
            transform.position = Vector3.SmoothDamp(transform.position, loc, ref velocity, followSpeed, maxSpeed);
        }  
        else
        {
            while ((currentTarget == null || currentTarget.target == null) && targets.Count > 0)
            {
                currentTarget = targets.Pop();
            }

            if(currentTarget == null || currentTarget.target == null)
            {
                return;
            }

            Vector3 hoverPos = this.currentTarget.target.GetComponent<Transform>().position + this.currentTarget.hoverOffset;
            if ((hoverPos - this.transform.position).magnitude > this.currentTarget.hoverRadius)
            {
                transform.position = Vector3.SmoothDamp(transform.position, hoverPos, ref velocity, followSpeed, maxSpeed);
            } else
            {
                switch(movementType)
                {
                    case MovementType.STILL:
                        wispAnimation.floatStrength = 0.25f;
                        break;
                    case MovementType.ROTATE_AROUND:
                        transform.RotateAround(hoverPos, Vector3.up, 180 * Time.deltaTime);
                        break;
                    case MovementType.BOB_NEXT_TO:
                    default:
                        wispAnimation.floatStrength = 1f;
                        break;
                }
            }
        }

        
    }

    public void SetMovementType(MovementType type)
    {
        movementType = type;
    }

    public void AddTarget(GameObject target, Vector3 hoverOffset, float hoverRadius = 0.5f)
    {
        this.targets.Push(new TargetProps(target, hoverOffset, hoverRadius));
    }

    public void AddTargets(GameObject[] newTargets, Vector3 hoverOffset, float hoverRadius = 0.5f)
    {
        foreach(GameObject trgt in newTargets) {
            this.AddTarget(trgt, hoverOffset, hoverRadius);
        }
    }

    public void ClearTargets()
    {
        this.currentTarget = null;
        this.targets.Clear();
    }
}
