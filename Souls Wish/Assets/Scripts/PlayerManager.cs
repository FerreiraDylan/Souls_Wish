using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Animation")]
    public Animator animator;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float HorizontalMove = 0f;

    [Header("Player Characteristics")]
    public float Speed = 6f;
    public float MaxHealth = 100f;
    public float CurrentHealth = 0f;
    public float Heal = 5f;
    public float Damage = 50f;
    public float Level = 0f;
    public float CurrentExp = 0f;
    public float MaxExp = 5f;
    public float CurrentMoney = 0f;
    public float MoneyMultiplier = 1f;
    private bool _shooting = false;
    private float _shootFrequency = 0.35f;

    [Header("Player Setup")]
    public CharacterController controller;
    /*
        public Transform objectif_position;
        public GameObject target;
        public GameObject bullet;
        public GameObject directionnalStick;
        public GameObject shootStick;
    */

    [Header("Gravity & Jump")]
    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool jump = false;
    public float jumpHeight = 3f;

    [Header("Private Values")]
    [SerializeField] private bool Shield_isActive;
    [SerializeField] private float JumpDuration = 1f;
    [SerializeField] private float RollDuration = 2f;
    [SerializeField] private float ShieldDuration = 2f;
    [SerializeField] private bool isGrounded;
    private float _speed = 0.0f;

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        Player_Animation();
        //CheckPlatform();
    }

    //PLAYER
    public void Player_Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x;
        float z;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }

        //Vector3 move = transform.right * x + transform.forward * z;
        if ((x != 0 || z != 0) || (x != 0 && z != 0))
            _speed = 1;
        else
            _speed = 0;
        
        //controller.Move(move * Speed * Time.deltaTime);
        //CameraFollow.instance.getPosition_threeD(gameObject.transform.position);

        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public IEnumerator Jump_Duration()
    {
        yield return new WaitForSeconds(JumpDuration);
        animator.SetBool("Jump", false);
        jump = false;
    }
    public IEnumerator Roll_Duration()
    {
        yield return new WaitForSeconds(RollDuration);
        animator.SetBool("Roll", false);
    }
    public void Player_Animation()
    {

        HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;

        animator.SetFloat("Speed", _speed);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("Jump", true);
            jump = true;
            StartCoroutine(Jump_Duration());
        }
        
        if (Input.GetButtonDown("Roll"))
        {
            animator.SetBool("Roll", true);
            StartCoroutine(Roll_Duration());
        }
        
    }
}