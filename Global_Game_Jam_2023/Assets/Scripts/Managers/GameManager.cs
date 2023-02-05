using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

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
    public AudioClip PickableTaken;
    public AudioClip MouseClick;
    public List<AudioClip> KeyboardSounds;

    public void PlayPotectedFolder()
    {
        AudioSourceGlobal.clip = ProtectedFolder;
        AudioSourceGlobal.Play();
    }
    public void PlayPickableTaken()
    {
        AudioSourceGlobal.clip = PickableTaken;
        AudioSourceGlobal.Play();
    }
    public void PlayMouseClick()
    {
        AudioSourceGlobal.clip = MouseClick;
        AudioSourceGlobal.Play();
    }
    public void PlayRandomKeyboard()
    {
        AudioSourceGlobal.clip = KeyboardSounds[UnityEngine.Random.Range(0, KeyboardSounds.Count)];
        AudioSourceGlobal.Play();
    }

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

        _inputSystem.PlayerMouse.LeftMouse.performed += LeftMouse_performed;

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

        _inputSystem.PlayerEndMap1.Enter.performed += Enter_performed;
        _inputSystem.PlayerEndMap1.Escer.performed += Escer_performed;
        _inputSystem.PlayerEndMap1.Spenter.performed += Spenter_performed;

        EnablePlayerKeyboard(false);
    }

    private void Spenter_performed(InputAction.CallbackContext obj)
    {
        UIManager.Instance.ShowCredits();
        _inputSystem.PlayerEndMap1.Disable();
    }

    private void Escer_performed(InputAction.CallbackContext obj)
    {
        Exit();
    }

    private void Enter_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _inputSystem.PlayerEndMap1.Disable();
    }

    private void LeftMouse_performed(InputAction.CallbackContext obj)
    {
        PlayMouseClick();
    }

    private void Backspace_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.Backspace();
        PlayRandomKeyboard();
    }

    private void A_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("A");
        PlayRandomKeyboard();
    }
    private void B_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("B");
        PlayRandomKeyboard();
    }
    private void C_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("C");
        PlayRandomKeyboard();
    }
    private void D_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("D");
        PlayRandomKeyboard();
    }
    private void E_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("E");
        PlayRandomKeyboard();
    }
    private void F_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("F");
        PlayRandomKeyboard();
    }
    private void G_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("G");
        PlayRandomKeyboard();
    }
    private void H_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("H");
        PlayRandomKeyboard();
    }
    private void I_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("I");
        PlayRandomKeyboard();
    }
    private void J_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("J");
        PlayRandomKeyboard();
    }
    private void K_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("K");
        PlayRandomKeyboard();
    }
    private void L_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("L");
        PlayRandomKeyboard();
    }
    private void M_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("M");
        PlayRandomKeyboard();
    }
    private void N_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("N");
        PlayRandomKeyboard();
    }
    private void O_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("O");
        PlayRandomKeyboard();
    }
    private void P_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("P");
        PlayRandomKeyboard();
    }
    private void Q_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Q");
        PlayRandomKeyboard();
    }
    private void R_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("R");
        PlayRandomKeyboard();
    }
    private void S_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("S");
        PlayRandomKeyboard();
    }
    private void T_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("T");
        PlayRandomKeyboard();
    }
    private void U_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("U");
        PlayRandomKeyboard();
    }
    private void V_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("V");
        PlayRandomKeyboard();
    }
    private void W_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("W");
        PlayRandomKeyboard();
    }
    private void X_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("X");
        PlayRandomKeyboard();
    }
    private void Y_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Y");
        PlayRandomKeyboard();
    }
    private void Z_performed(InputAction.CallbackContext obj)
    {
        _alphabetManager.SpawnLetter("Z");
        PlayRandomKeyboard();
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

    }

    public void EnableMouseClickOnFolder(bool active)
    {
        Cursor.visible = active;

        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
            _inputSystem.PlayerMouse.Enable();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystem.PlayerMouse.Disable();
        }

        StartCoroutine(SearchClickButtonCoroutine(active));
    }

    private IEnumerator SearchClickButtonCoroutine(bool active)
    {
        yield return new WaitForSeconds(0.1f);
        SearchClickButton(active);
    }

    private void SearchClickButton(bool active)
    {
        _folderClickList = FindObjectsOfType<FolderClickInteractable>().ToList();
        if (_folderClickList != null)
            foreach (var x in _folderClickList)
            {
                if (x != null && x.Button != null)
                {
                    x.Button.interactable = active;
                }
            }
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

    public void GameOver()
    {
        UIManager.Instance.ShowGameOver();
        EnableMouseClickOnFolder(false);
        EnableMouseDragAndDrop(false);
        EnablePlayerKeyboard(false);
        EnablePlayerMovement(false);
        _inputSystem.PlayerEndMap1.Enable();
        _player.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
