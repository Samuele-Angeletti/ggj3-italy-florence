using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        _inputSystem.Player.EnterButton.performed += EnterButtonPerformed;

    }

    private void EnterButtonPerformed(InputAction.CallbackContext obj)
    {

    }

    private void ShiftCanceled(InputAction.CallbackContext obj)
    {

    }

    private void ShiftStarted(InputAction.CallbackContext obj)
    {
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }

    private void MovementCanceled(InputAction.CallbackContext obj)
    {
        _player.Move(Vector2.zero);
    }

    private void MovementPerformed(InputAction.CallbackContext obj)
    {
        _player.Move(obj.ReadValue<Vector2>());
    }


}
