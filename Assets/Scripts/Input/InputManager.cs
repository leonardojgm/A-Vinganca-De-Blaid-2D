using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager
{
    private readonly PlayerControls playerControls;
    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnMenuOpenClose;

    public InputManager()
    {
        playerControls = new PlayerControls();

        EnablePlayerInput();
        EnableUIInput();

        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
        playerControls.UI.OpenCloseMenu.performed += OpenClosePauseMenuPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj) => OnJump?.Invoke();

    private void OnAttackPerformed(InputAction.CallbackContext obj) => OnAttack?.Invoke();

    private void OpenClosePauseMenuPerformed(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay") return;

        OnMenuOpenClose?.Invoke();
    }

    public void DisablePlayerInput() => playerControls.Gameplay.Disable();

    public void EnablePlayerInput() => playerControls.Gameplay.Enable();

    public void EnableUIInput() => playerControls.UI.Enable();
}
