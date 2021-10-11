using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Animation")]
    public Animator animator;
    public Transform cam;
    public ParticleSystem Health_restored;

    [Header("Player Characteristics")]
    public float Speed = 6f;
    public float MaxHealth = 100f;
    public float CurrentHealth = 0f;
    public float Heal = 30f;
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
    public GameObject ActualCampFire;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float HorizontalMove = 0f;
    /*
        public Transform objectif_position;
        public GameObject target;
        public GameObject directionnalStick;
    */

    [Header("Player UI")]
    public PlayerUIManager PlayerUI_Manager;
    public GameObject UI_Interaction;

    [Header("Gravity Jump")]
    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool jump = false;
    public float jumpHeight = 3f;

    [Header("Private Values")]
    [SerializeField] private bool Shield_isActive;
    [SerializeField] private float JumpDuration = .8f;
    [SerializeField] private float RollDuration = 2f;
    [SerializeField] private float ShieldDuration = 2f;
    [SerializeField] private bool isGrounded;
    private float _speed = 0.0f;
    public bool _insideCampFire;

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
        StarterPack_Payer();
    }

    public void StarterPack_Payer()
    {
        PlayerUI_Manager.SetPlayerUI_Health(MaxHealth, CurrentHealth);
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        Player_Animation();
        Player_UI();
        //CheckPlatform();
        TakeHeal();
    }

    //PLAYER
    public void Player_Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }

        if ((x != 0 || z != 0) || (x != 0 && z != 0))
            _speed = 1;
        else
            _speed = 0;

        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void Player_UI()
    {
        PlayerUI_Manager.UpdatePlayerUI_Health(CurrentHealth);
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
        controller.center = new Vector3(0, 1.1f, 0);
        controller.radius = .5f;
        controller.height = 2f;
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
            controller.center = new Vector3(0, .5f, 0);
            controller.radius = .35f;
            controller.height = .6f;
            StartCoroutine(Roll_Duration());
        }

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetBool("Attack", true);
        }

        if (Input.GetButtonDown("Shield"))
        {
            animator.SetBool("ShieldBlock", true);
        }

    }

    public void TakeHeal()
    {
        if (Input.GetButtonDown("Use Item"))
        {
            CurrentHealth += Heal;
            Health_restored.gameObject.SetActive(true);
            Health_restored.Play();
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage * Time.deltaTime;

        //StartCoroutine(CameraFollow.instance.CameraShaking.Shake(.15f, .4f));
    }

    public void TakeSlow(bool slow)
    {
        Speed = (slow ? 3 : 6);
    }
}