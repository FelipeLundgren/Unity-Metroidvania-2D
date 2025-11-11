
using System;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();
    public event Action onJump;
    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();


        playerControls.Gameplay.Jump.performed += onJumpPerformed;
    }

    private void onJumpPerformed(InputAction.CallbackContext context)
    {
        onJump?.Invoke();
    }
}
