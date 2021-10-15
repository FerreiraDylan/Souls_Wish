using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Animation")]
    public PlayerAnimManger PlayerAnim_Manager;
    public Transform cam;
    public ParticleSystem Health_restored;

    [Header("Player Characteristics")]
    public float Speed = 6f;
    public float CurrentSpeed = 6f;
    public float MaxHealth = 100f;
    public float CurrentHealth = 0f;
    public float MaxShield = 90f;
    public float CurrentShield = 0f;
    public int HealPotions = 3;
    public float Heal = 30f;
    public float Damage = 35f;
    public float Level = 0f;
    public float CurrentExp = 0f;
    public float MaxExp = 5f;
    public int CurrentMoney = 0;
    public float MoneyMultiplier = 1f;

    [Header("Player Setup")]
    public CharacterController controller;
    public GameObject ActualCampFire;
    public PlayerAudioManager PlayerAudio_Manager;
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
    public Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool jump = false;
    public float jumpHeight = 3f;

    [Header("Private Values")]
    [SerializeField] public bool Shield_isActive;
    [SerializeField] public bool Attack_isActive;
    [SerializeField] private bool isGrounded;
    public float _speed = 0.0f;

    public float downTime, upTime, pressTime = 0;
    public float countDown = 2.0f;
    public bool ready = false;

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
        Speed = CurrentSpeed;
        CurrentHealth = MaxHealth;
        PlayerUI_Manager.SetPlayerUI_Health(MaxHealth, CurrentHealth);
        CurrentShield = MaxShield;
        PlayerUI_Manager.SetPlayerUI_Shield(MaxShield, CurrentShield);
        PlayerAnim_Manager.SetSpeedActive();
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        Player_Animation();
        Player_UI();
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
        if (CurrentShield < MaxShield)
            CurrentShield += 3 * Time.deltaTime;
        PlayerUI_Manager.UpdatePlayerUI_Health(CurrentHealth);
        PlayerUI_Manager.UpdatePlayerUI_Shield(CurrentShield);
        PlayerUI_Manager.UpdatePlayerUI_HealPotions(HealPotions);
        PlayerUI_Manager.UpdatePlayerUI_Coins(CurrentMoney);
    }

    public void Player_Animation()
    {
        if (CurrentHealth > 0)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;

            PlayerAnim_Manager.Anim_Speed();

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                PlayerAudio_Manager.Audio_JumpRoll();
                PlayerAnim_Manager.Anim_Jump();
            }

            if (Input.GetButtonDown("Roll"))
            {
                PlayerAudio_Manager.Audio_JumpRoll();
                PlayerAnim_Manager.Anim_Roll();
            }

            if (Input.GetButtonDown("Attack"))
            {
                PlayerAudio_Manager.Audio_Attack();
                PlayerAnim_Manager.Anim_Attack();
            }

            if (CurrentShield > 30)
            {
                if (Input.GetButtonDown("Shield") && ready == false)
                {
                    downTime = Time.time;
                    pressTime = downTime + countDown;
                    ready = true;
                    Shield_isActive = true;
                    CurrentShield -= 30;
                    PlayerAudio_Manager.Audio_Shield();
                    PlayerAnim_Manager.Anim_Shield();
                }
                if (Input.GetButtonUp("Shield"))
                {
                    ready = false;
                    Shield_isActive = false;
                }
                if (Time.time >= pressTime && ready == true)
                {
                    ready = false;
                    Shield_isActive = false;
                }
            }
        }
    }

    public void Player_Death()
    {
        PlayerAudio_Manager.Audio_Death();
        PlayerAnim_Manager.Anim_Death();
    }
    public IEnumerator Death_Duration()
    {
        yield return new WaitForSeconds(5f);
    }

    public void TakeHeal()
    {
        if (Input.GetButtonDown("Use Item") && HealPotions > 0)
        {
            if (CurrentHealth == MaxHealth)
                return;
            if ((CurrentHealth + Heal) > MaxHealth)
            {
                CurrentHealth = MaxHealth;
                Health_restored.gameObject.SetActive(true);
                Health_restored.Play();
                HealPotions -= 1;
            } else
            {
                CurrentHealth += Heal;
                Health_restored.gameObject.SetActive(true);
                Health_restored.Play();
                HealPotions -= 1;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage * Time.deltaTime;

        //StartCoroutine(CameraFollow.instance.CameraShaking.Shake(.15f, .4f));
    }

    public void TakeSlow(bool slow)
    {
        Speed = (slow ? CurrentSpeed/2 : CurrentSpeed);
    }
}