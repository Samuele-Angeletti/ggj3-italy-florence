using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance != null)
                    return instance;

                GameObject go = new GameObject("GameManager");
                return go.AddComponent<GameManager>();
            }
            else
                return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    InputSystem _inputSystem;
    [SerializeField] Player _player;
    private void Awake()
    {
        _inputSystem = new InputSystem();

        _inputSystem.Player.Enable();

        _inputSystem.Player.WASDMovement.performed += MovementPerformed;
        _inputSystem.Player.ArrowsMovement.performed += MovementPerformed;

        _inputSystem.Player.WASDMovement.canceled += MovementCanceled;
        _inputSystem.Player.ArrowsMovement.canceled += MovementCanceled;

        _inputSystem.Player.Jump.performed += JumpPerformed;

        _inputSystem.Player.Shift.started += ShiftStarted;
        _inputSystem.Player.Shift.canceled += ShiftCanceled;

        _inputSystem.Player.InteractButton.performed += InteractButtonPerformed;

        _inputSystem.Player.SInteract.performed += DownPerformed;
        _inputSystem.Player.DownInteract.performed += DownPerformed;
        
    }

    private void DownPerformed(InputAction.CallbackContext obj)
    {
        _player.TryGoDown();
    }

    private void InteractButtonPerformed(InputAction.CallbackContext obj)
    {
        _player.TryInteract();
    }

    private void ShiftCanceled(InputAction.CallbackContext obj)
    {
        _player.Run(false);
    }

    private void ShiftStarted(InputAction.CallbackContext obj)
    {
        _player.Run(true);
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }

    private void MovementCanceled(InputAction.CallbackContext obj)
    {
        _player.MoveHorizontal(Vector2.zero);
    }

    private void MovementPerformed(InputAction.CallbackContext obj)
    {
        _player.MoveHorizontal(obj.ReadValue<Vector2>());
    }

    public void EnablePlayerMovement(bool active)
    {
        if (active)
            _inputSystem.Player.Enable();
        else
            _inputSystem.Player.Disable();
    }

    public void EnablePlayerMouse(bool active)
    {
        if (active)
            _inputSystem.PlayerMouse.Enable();
        else
            _inputSystem.PlayerMouse.Disable();
    }

    public void EnablePlayerKeyboard(bool active)
    {
        if (active)
            _inputSystem.PlayerKeyboard.Enable();
        else
            _inputSystem.PlayerKeyboard.Disable();
    }

}
