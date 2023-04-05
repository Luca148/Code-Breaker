using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] [Range(0, 10)]float mouseSensitivity = 3.5f; 
    [SerializeField] public float walkSpeed = 4.0f; 
    [SerializeField] public float resetwalkSpeed = 4.0f; 
    [SerializeField] public float sprintSpeed = 8.0f; 
    [SerializeField] public float crouchSpeed = 3.0f;
    [SerializeField] float crouchHeight = 1.0f; 
    [SerializeField] float standingHeight = 2.0f;
    [SerializeField] float gravity = -13.0f; 
    [SerializeField][Range(0.0f, 0.1f)] float moveSmoothTime = 0.05f;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] public float jumpMultiplier = 5f;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    private bool isJumping;
    [SerializeField] private bool canCrouch = false;

    public bool disableInput = false;
    public bool lockCursor = true;
    public bool isCrouching = false; 
    public bool isSprinting = false;

    Vector2 targetDir;
    [SerializeField] LayerMask lmask; //ground

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        JumpInput();

        //lock cursor in middle of screen
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //this part needs to be at the bottom of the update void
        //when player is not grounded or not moving return
        if (!controller.isGrounded) return;
        if (targetDir == Vector2.zero) return;
    }

    void UpdateMouseLook() //move camera with mouse
    {
        if(!disableInput)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //mouse movement aus vector2

            cameraPitch -= mouseDelta.y * mouseSensitivity; //calculate camera pitch with vector2.y

            //camerapitch is clamped between -90 and 90
            cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
            
            //local rotation of camera on y
            //Die lokalen Rotationswerte der Kamera werden mit dem Vector3.right (1,0,0) mal den Kamerawinkel genommen, um die Kamera zu bewegen 
            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            //rotate player on x
            transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
        }
    }

    void UpdateMovement() //move player
    {
        //if player press lshift then increase speed then if release reset speed
        if (Input.GetKey(KeyCode.LeftShift) && !disableInput)
        {
            walkSpeed = sprintSpeed; 
            isSprinting = true; 
        }
        else
        {
            walkSpeed = resetwalkSpeed; 
            isSprinting = false; 
        }

        //if player press lstrg then decrease height then if release reset height
        if (Input.GetKey(KeyCode.LeftControl) && !disableInput)
        {
            if (canCrouch)
            {
                controller.height = crouchHeight;
                walkSpeed = crouchSpeed;
                isCrouching = true;
            }
        }
        else
        {
            controller.height = standingHeight;
            isCrouching = false;
        }

        if(!disableInput)
        {
            //walk direction
            // W = Vertical +
            // S = Verical -
            // D = Horizontal +
            // A = Horizontal -
            targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //normalize value
            // W = Vertical +1
            // S = Verical -1
            // D = Horizontal +1
            // A = Horizontal -1
            targetDir.Normalize();


            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

            if (controller.isGrounded)
            {
                velocityY = 0.0f; 
            }

            velocityY += gravity * Time.deltaTime; //gravity calculation

            //velocity of player + gravity
            Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

            controller.Move(velocity * Time.deltaTime); //move player
        }
    }

    //input of jump
    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping && !disableInput)
        {
                isJumping = true; //start jump
                StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f; //reset timeInAir

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir); //calculate jumpforce with falloff curve
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime); //move player
            timeInAir += Time.deltaTime; //add timeInAir
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above); //check if player can jump

        isJumping = false; //end jump
    }
}
