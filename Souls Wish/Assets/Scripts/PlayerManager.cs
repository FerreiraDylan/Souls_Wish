using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Animation")]
    public Animator animator;

    [Header("Player Characteristics")]
    public float Speed = 6f;
/*
    public float MaxHealth = 100f;
    public float CurrentHealth = 0f;
    public float Heal = 5f;
    public float ShieldDuration = 2f;
    public float Damage = 50f;
    public float Level = 0f;
    public float CurrentExp = 0f;
    public float MaxExp = 5f;
    public float CurrentMoney = 0f;
    public float MoneyMultiplier = 1f;
*/
    private bool _shooting = false;
    private float _shootFrequency = 0.35f;
    //public int Ammunition = 15;

/*
    [Header("Private Values")]
    public bool Shield_isActive;
*/

    [Header("Player Setup")]
    public CharacterController controller;
/*
    public Transform objectif_position;
    public GameObject target;
    public GameObject bullet;
    public GameObject directionnalStick;
    public GameObject shootStick;
    */
    Vector3 velocity;
    public float gravity = -9.81f;
    public bool jump = false;
    public float jump_vel = 10.0f;
    public float JumpDuration = 1.0f;
    public float RollDuration = 2.0f;

    private float _speed = 0.0f;
    float HorizontalMove = 0f;

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
        float x;
        float z;

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if ((x != 0 || z != 0) || (x != 0 && z != 0))
            _speed = 1;
        else
            _speed = 0;
        
        controller.Move(move * Speed * Time.deltaTime);
        //CameraFollow.instance.getPosition_threeD(gameObject.transform.position);

        //GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public IEnumerator Jump_Duration()
    {
        yield return new WaitForSeconds(JumpDuration);
        animator.SetBool("Jump", false);
    }
    public IEnumerator Roll_Duration()
    {
        yield return new WaitForSeconds(RollDuration);
        animator.SetBool("Roll", false);
    }
    public void Player_Animation()
    {

        HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;

        Debug.Log(_speed);
        animator.SetFloat("Speed", _speed);
        
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            /*var tmp = gameObject.transform.localPosition;
            tmp.y += jump_vel * Time.deltaTime;
            gameObject.transform.localPosition = tmp;*/
            StartCoroutine(Jump_Duration());
        }
        
        if (Input.GetButtonDown("Roll"))
        {
            animator.SetBool("Roll", true);
            StartCoroutine(Roll_Duration());
        }
        
    }
}