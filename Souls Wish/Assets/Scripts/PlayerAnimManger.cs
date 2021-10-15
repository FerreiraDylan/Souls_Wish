using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManger : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float JumpAnimDuration = .25f;
    [SerializeField] private float RollAnimDuration = 1.04f;
    [SerializeField] private float AttackAnimDuration = 1.14f;
    [SerializeField] private float ShieldAnimDuration = .15f;
    [SerializeField] private float DeathAnimDuration = 5f;

    [SerializeField] private float ShieldDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ANIMATIONS //
    public void Anim_Speed()
    {
        animator.SetFloat("Speed", PlayerManager.instance._speed);
    }
    public void Anim_Jump()
    {
        PlayerManager.instance.velocity.y = Mathf.Sqrt(PlayerManager.instance.jumpHeight * -2f * PlayerManager.instance.gravity);
        animator.SetBool("Jump", true);
        PlayerManager.instance.jump = true;
        StartCoroutine(JumpAnim_Duration());
    }

    public void Anim_Roll()
    {
        animator.SetBool("Roll", true);
        PlayerManager.instance.controller.center = new Vector3(0, .5f, 0);
        PlayerManager.instance.controller.radius = .35f;
        PlayerManager.instance.controller.height = .6f;
        StartCoroutine(RollAnim_Duration());
    }

    public void Anim_Death()
    {
        animator.SetBool("IsDead", true);
        animator.SetBool("SpeedActive", false);
        StartCoroutine(DeathAnim_Duration());
    }
    public void SetSpeedActive()
    {
        animator.SetBool("SpeedActive", true);
    }

    public void Anim_Attack()
    {
        animator.SetBool("Attack", true);
        StartCoroutine(AttackAnim_Duration());
        PlayerManager.instance.Attack_isActive = true;
    }

    public void Anim_Shield()
    {
        animator.SetBool("ShieldBlock", true);
        StartCoroutine(ShieldCD_Duration());
        StartCoroutine(ShieldAnim_Duration());
    }

    // ANIMATION CD //

    public IEnumerator JumpAnim_Duration()
    {
        yield return new WaitForSeconds(JumpAnimDuration);
        animator.SetBool("Jump", false);
        PlayerManager.instance.jump = false;
    }
    public IEnumerator DeathAnim_Duration()
    {
        yield return new WaitForSeconds(DeathAnimDuration);
        animator.SetBool("IsDead", false);
        animator.SetBool("SpeedActive", true);
    }

    public IEnumerator RollAnim_Duration()
    {
        yield return new WaitForSeconds(RollAnimDuration);
        PlayerManager.instance.controller.center = new Vector3(0, 1.1f, 0);
        PlayerManager.instance.controller.radius = .5f;
        PlayerManager.instance.controller.height = 2f;
        animator.SetBool("Roll", false);
    }
    public IEnumerator AttackAnim_Duration()
    {
        yield return new WaitForSeconds(AttackAnimDuration);
        animator.SetBool("Attack", false);
        PlayerManager.instance.Attack_isActive = false;
    }
    public IEnumerator ShieldAnim_Duration()
    {
        yield return new WaitForSeconds(ShieldAnimDuration);
        animator.SetBool("ShieldBlock", false);
        //PlayerManager.instance.Shield_isActive = false;
    }
    public IEnumerator ShieldCD_Duration()
    {
        yield return new WaitForSeconds(ShieldDuration);
    }
}
