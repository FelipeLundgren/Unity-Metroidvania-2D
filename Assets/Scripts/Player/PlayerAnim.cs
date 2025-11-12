using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private isGroundChecker isGroundChecker;
    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundChecker = GetComponent<isGroundChecker>();
        playerHealth = GetComponent<Health>();
        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDead += PlayerDeathAnim;

        GameManager.Instance.InputManager.onAttack += PlayAttackAnim;
    }

    


    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        bool isJumping = isGroundChecker.isGrounded();
        animator.SetBool("isJumping", !isJumping);
    }

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }
    
    private void PlayerDeathAnim()
    {
        animator.SetTrigger("death");
    }
    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }
    
}