using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Essentials")]
    public Transform cam;
    [SerializeField] private CinemachineFreeLook freeCam;
    [SerializeField] [Range(.1f, 10)]private float sensitivity = 1;
    [SerializeField] private float sensitivityScale = 100;
    CharacterController controller;
    float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    Vector2 movement;

    [Header("Jumping")]
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    bool isJumping;
    bool isFalling;
    [SerializeField]Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        freeCam = FindObjectOfType<CinemachineFreeLook>();
        anim = GetComponentInChildren<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    void Update()
    {
        freeCam.m_XAxis.m_MaxSpeed = 2 * sensitivity * sensitivityScale;
        freeCam.m_YAxis.m_MaxSpeed = sensitivity / 33 * sensitivityScale;

        isGrounded = Physics.CheckSphere(transform.position, .2f, 1);
        anim.SetBool("IsGrounded", isGrounded);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        if (isGrounded)
        {
            isJumping = false;
            isFalling = false;
        }


        anim.transform.localPosition = Vector3.zero;
        anim.transform.localEulerAngles = Vector3.zero;
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        }

        if(velocity.y >= 1)
        {
            if (!isJumping)
            {
                isJumping = true;
                anim.SetTrigger("IsJumping");
            }
        }

        if(velocity.y <= -8)
        {
            if (!isFalling)
            {
                isFalling = true;
                anim.SetTrigger("IsFalling");
            }
        }

        if (velocity.y > -20)
        {
            velocity.y += (gravity * 10) * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
