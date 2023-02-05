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
    [SerializeField] AlphabetManager _alphabetManager;

    public AudioSource AudioSourceGlobal;

    public AudioClip ProtectedFolder;

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


        _inputSystem.PlayerKeyboard.Enable();
        _inputSystem.PlayerKeyboard.A.performed += A_performed;
        _inputSystem.PlayerKeyboard.B.performed += B_performed;
        _inputSystem.PlayerKeyboard.C.performed += C_performed;
        _inputSystem.PlayerKeyboard.D.performed += D_performed;
        _inputSystem.PlayerKeyboard.E.performed += E_performed;
        _inputSystem.PlayerKeyboard.F.performed += F_performed;
        _inputSystem.PlayerKeyboard.G.performed += G_performed;
        _inputSystem.PlayerKeyboard.H.performed += H_performed;
        _inputSystem.PlayerKeyboard.I.performed += I_performed;
        _inputSystem.PlayerKeyboard.J.performed += J_performed;
        _inputSystem.PlayerKeyboard.K.performed += K_performed;
        _inputSystem.PlayerKeyboard.L.performed += L_performed;
        _inputSystem.PlayerKeyboard.M.performed += M_performed;
        _inputSystem.PlayerKeyboard.N.performed += N_performed;
        _inputSystem.PlayerKeyboard.O.performed += O_performed;
        _inputSystem.PlayerKeyboard.P.performed += P_performed;
        _inputSystem.PlayerKeyboard.Q.performed += Q_performed;
        _inputSystem.PlayerKeyboard.R.performed += R_performed;
        _inputSystem.PlayerKeyboard.S.performed += S_performed;
        _inputSystem.PlayerKeyboard.T.performed += T_performed;
        _inputSystem.PlayerKeyboard.U.performed += U_performed;
        _inputSystem.PlayerKeyboard.V.performed += V_performed;
        _inputSystem.PlayerKeyboard.W.performed += W_performed;
        _inputSystem.PlayerKeyboard.X.performed += X_performed;
        _inputSystem.PlayerKeyboard.Y.performed += Y_performed;
        _inputSystem.PlayerKeyboard.Z.performed += Z_performed;
        _inputSystem.PlayerKeyboard.Backspace.performed += Backspace_performed; ;

        EnablePlayerKeyboard(false);
    }

    private void Backspace_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.Backspace();
    }

    private void A_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("A");
    }
    private void B_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("B");
    }
    private void C_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("C");
    }
    private void D_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("D");
    }
    private void E_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("E");
    }
    private void F_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("F");
    }
    private void G_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("G");
    }
    private void H_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("H");
    }
    private void I_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("I");
    }
    private void J_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("J");
    }
    private void K_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("K");
    }
    private void L_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("L");
    }
    private void M_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("M");
    }
    private void N_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("N");
    }
    private void O_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("O");
    }
    private void P_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("P");
    }
    private void Q_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Q");
    }
    private void R_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("R");
    }
    private void S_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("S");
    }
    private void T_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("T");
    }
    private void U_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("U");
    }
    private void V_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("V");
    }
    private void W_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("W");
    }
    private void X_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("X");
    }
    private void Y_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Y");
    }
    private void Z_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Z");
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

    public void EnablePlayerKeyboard(bool active, AlphabetManager alphabetManager = null)
    {
        if (active)
            _inputSystem.PlayerKeyboard.Enable();
        else
            _inputSystem.PlayerKeyboard.Disable();

        _player.KeyboardTypeAvailable = active;

        if (alphabetManager != null)
            _alphabetManager = alphabetManager;
    }


    public void Exit()
    {
        Application.Quit();
    }
}
