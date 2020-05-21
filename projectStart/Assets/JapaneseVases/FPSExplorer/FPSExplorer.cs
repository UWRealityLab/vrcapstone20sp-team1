﻿using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FPSExplorer : MonoBehaviour
{
    public Animator animator;
    public AudioClip[] sound;

    public float stepSpeed = 10F;
    public float runSpeed = 20F;
    public float sensitivity = 5F;
    public bool run = true;
    public bool lockCursor = true;
    private bool m_cursorIsLocked = true;

    //jump 
    public float powerJump = 10.0F;
    private Vector3 gravity = -Physics.gravity;
    private Vector3 moveDirection = Vector3.zero;

    private float minimumY = -70F;
    private float maximumY = 90F;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private float speed = 0F;

    private Transform myCamera;
    private CharacterController myCharacter;
    private Vector3 movement;


    // Use this for initialization
    void Start()
    {
        if (GetComponentInChildren<Camera>())
            myCamera = GetComponentInChildren<Camera>().transform;

        // Make the rigid body not change rotation
        myCharacter = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myCamera) return;

        //Look
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        myCamera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);

        UpdateCursorLock();
    }

    void FixedUpdate()
    {
        //
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        //Move
        float right = Input.GetAxisRaw("Horizontal");
        float forward = Input.GetAxisRaw("Vertical");

        movement.Set(right, 0f, forward);
        movement = transform.TransformDirection(movement);
        movement *= speed;
        myCharacter.Move(movement * Time.deltaTime);

        //Run
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            speed = run ? stepSpeed : runSpeed;
        else
            speed = run ? runSpeed : stepSpeed;

        //Jump/fly
        if (Input.GetButton("Jump"))// || Input.GetMouseButtonDown(1))
            moveDirection.y = powerJump;

        moveDirection.y -= gravity.y * Time.deltaTime;
        myCharacter.Move(moveDirection * Time.deltaTime);
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void Attack()
    {
        if(animator)
        {
            int n = Random.Range(0, 2);

            if (n == 0) Action("Attack1");
            if (n == 1) Action("Attack2");
        }
    }

    private void Action(string action_name)
    {
        animator.SetTrigger(action_name);

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().pitch = 1 + Random.value * 0.2f;
            GetComponent<AudioSource>().clip = sound[Random.Range(0, sound.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
