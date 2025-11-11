using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private isGroundChecker isGroundChecker;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundChecker = GetComponent<isGroundChecker>();
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        bool isJumping = isGroundChecker.isGrounded();
        animator.SetBool("isJumping", !isJumping);
    }
}