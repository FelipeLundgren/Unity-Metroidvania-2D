using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float moveSpeed = 5;

    [Header("Propriedades de ataque")] [SerializeField]
    private float attackRange = 1f;

    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private Rigidbody2D rigidbodyPlayer;
    private isGroundChecker isGroundChecker;
    private float moveDirection;

    private void Awake()
    {
        GetComponent<Health>().OnDead += HandlePlayerDeath;
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        isGroundChecker = GetComponent<isGroundChecker>();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.onJump += HandleJump;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveDirection = GameManager.Instance.InputManager.Movement * Time.deltaTime * moveSpeed;

        transform.Translate(moveDirection, 0, 0);
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    private void HandleJump()
    {
        if (isGroundChecker.isGrounded() == false)
            return;
        rigidbodyPlayer.linearVelocity += Vector2.up * jumpForce;
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.InputManager.DisablePlayerInput();
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);
        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Checking enemy");
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting damage");
                enemyHealth.TakeDamage();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
