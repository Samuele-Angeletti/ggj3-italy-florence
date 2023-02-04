using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public Player Player => _player;
    List<FolderClickInteractable> _folderClickList;
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

        _folderClickList = FindObjectsOfType<FolderClickInteractable>().ToList();
    }

    private void Start()
    {

    }

    public void StartGame()
    {
        EnableMouseDragAndDrop(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SpawnPlayer()
    {

        _player.gameObject.SetActive(true);
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

    public void EnableMouseDragAndDrop(bool active)
    {
        Cursor.visible = active;

        if(active)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        _player.DragAndDropAvailable = true;
    }

    public void EnableMouseClickOnFolder(bool active)
    {
        Cursor.visible = active;

        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(_folderClickList != null)
        foreach (var x in _folderClickList)
        {
            if (x != null && x.Button != null)
            {
                x.Button.interactable = active;
            }
        }

        _player.ClickAvailable = true;
    }

    public void EnablePlayerKeyboard(bool active)
    {
        if (active)
            _inputSystem.PlayerKeyboard.Enable();
        else
            _inputSystem.PlayerKeyboard.Disable();

        _player.KeyboardTypeAvailable = active;
    }


    public void Exit()
    {
        Application.Quit();
    }
}
