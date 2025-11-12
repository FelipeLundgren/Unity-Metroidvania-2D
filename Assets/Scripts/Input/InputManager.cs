using System;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();
    public event Action onJump;
    public event Action onAttack;

    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();


        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        onJump?.Invoke();
    }

    private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        onAttack?.Invoke();
    }

    public void DisablePlayerInput() => playerControls.Gameplay.Disable();
}