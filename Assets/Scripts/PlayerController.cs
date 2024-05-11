using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public Animator cameraAnimator;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;


    public bool canMove = true;
    private bool isRunning;
    private bool RunInvoked;
    private bool WalkInvoked;


    public AudioClip[] soundVariations;
    public AudioSource audioSource;


    void Start()
    {

        characterController = GetComponent<CharacterController>();
        cameraAnimator.enabled = false;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void PlayRandomFootstep()
    {
        // Check if there are footstep sounds available
        if (soundVariations.Length > 0)
        {

            AudioClip randomFootstep = soundVariations[Random.Range(0, soundVariations.Length)];
            audioSource.PlayOneShot(randomFootstep);

        }
    }

    void PlayRandomFootstepRun()
    {
        // Check if there are footstep sounds available
        if (soundVariations.Length > 0)
        {

            AudioClip randomFootstep = soundVariations[Random.Range(0, soundVariations.Length)];
            audioSource.PlayOneShot(randomFootstep);

        }
    }


    void Update()
    {
        if (cameraAnimator.enabled)
        {
            audioSource.volume = 0.25f;
        }
        else if (!cameraAnimator.enabled)
        {
            audioSource.volume = 0;
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            cameraAnimator.enabled = true;
        }
        else
        {
            cameraAnimator.enabled = false;
        }

        if (isRunning)
        {
            cameraAnimator.SetBool("Running", true);
            if(!RunInvoked)
            {
                InvokeRepeating("PlayRandomFootstepRun", 0f, 0.3f);
                CancelInvoke("PlayRandomFootstep");
                RunInvoked = true;
                WalkInvoked = false;
            }
        }
        else
        {
            cameraAnimator.SetBool("Running", false);
            if (!WalkInvoked)
            {
                InvokeRepeating("PlayRandomFootstep", 0f, 0.55f);
                CancelInvoke("PlayRandomFootstepRun");
                WalkInvoked = true;
                RunInvoked = false;
            }
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}