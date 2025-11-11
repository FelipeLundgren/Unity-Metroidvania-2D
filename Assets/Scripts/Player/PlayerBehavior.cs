using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float moveSpeed = 5;
    private Rigidbody2D rigidbodyPlayer;
    private isGroundChecker isGroundChecker;

    private void Awake()
    {
        rigidbodyPlayer  = GetComponent<Rigidbody2D>();
        isGroundChecker = GetComponent<isGroundChecker>();
    }

    private void Start()
    {
        GameManager.Instance.InputManager.onJump += HandleJump;
    }
    
    private void Update()
    {
        float moveDirection =  GameManager.Instance.InputManager.Movement* Time.deltaTime * moveSpeed;
        //float direction = Input.GetAxis("Horizontal");
        transform.Translate(moveDirection, 0,0);
        //Vector2 vectorMoveDirection = new Vector2(moveDirection, transform.position.y);
        //rigidbodyPlayer.linearVelocity = vectorMoveDirection * moveSpeed;
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
        rigidbodyPlayer.linearVelocity += Vector2.up *  jumpForce;
    }
}
