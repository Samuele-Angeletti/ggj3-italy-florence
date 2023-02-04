using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject graphics;
    [Header("Horizontal Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float runSpeed;
    [Header("Jumping")]
    [SerializeField] float verticalSpeed;
    [SerializeField] float verticalMaxDestination;
    [SerializeField] float verticalSpeedDecelerator;
    [SerializeField] Transform groundCheckPivot;
    [SerializeField] float groundCheckDistance = 2;
    [SerializeField] LayerMask groundCheckLayerMask;
    [Header("Falling")]
    [SerializeField] float fallingSpeed;
    [SerializeField] float fallingSpeedAccelerator;
    [SerializeField] float maxFallingSpeed;
    [Header("Interactable Settings")]
    [SerializeField] float radius;
    [Header("VFX")]
    [SerializeField] ParticleSystem dematerializePrefab;
    [SerializeField] ParticleSystem materializePrefab;
    [SerializeField] float timeBetweenRespawn;

    Checkpoint currentCheckpoint;
    Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    Vector2 _direction;
    [HideInInspector] public Vector2 JumpDestination;
    [HideInInspector] public Vector2 LastTransform;
    float _currentFallingSpeed;
    public bool IsJumping;
    public bool IsFalling;
    bool _isGrounded;
    bool _isDying;
    float _currentVerticalSpeed;
    float _currentRunSpeed;
    int _passwordCount;
    public Animator Animator => _animator;
    public Checkpoint Checkpoint => currentCheckpoint;
    private Vector3 _firstPosition;

    bool _wasDematerialize;
    public bool IsRunning => _currentRunSpeed > 0;
    public bool Landed;
    public bool Interacting;
    public bool Dying => _isDying;
    public Vector2 Direction => _direction;
    public GenericStateMachine<EPlayerState> StateMachine;
    public int PasswordCount
    {
        get
        {
            return _passwordCount;
        }
        set
        {
            _passwordCount = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _firstPosition = transform.position;

        StateMachine = new GenericStateMachine<EPlayerState>();

        StateMachine.RegisterState(EPlayerState.Idle, new IdleCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Walking, new WalkingCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Running, new RunningPlayerState(this));
        StateMachine.RegisterState(EPlayerState.Jumping, new JumpingCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Falling, new FallingCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Interacting, new InteractingCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Landing, new LandedCharacterState(this));
        StateMachine.RegisterState(EPlayerState.Dying, new DyingPlayerState(this));

        StateMachine.SetState(EPlayerState.Idle);
    }

    private void Start()
    {
        IsFalling = !GroundCheck();
    }

    private void Update()
    {
        StateMachine.OnUpdate();

        if (_rigidbody.velocity.x > 0)
            _spriteRenderer.flipX = false;
        else if (_rigidbody.velocity.x < 0)
            _spriteRenderer.flipX = true;

    }

    public bool GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.BoxCastNonAlloc(groundCheckPivot.position, new Vector2(0.5f, groundCheckDistance), 0, Vector2.down, hits, 0, groundCheckLayerMask);

        foreach (var hit in hits.Where(x => x.collider != null))
        {
            if (hit.collider.bounds.max.y < groundCheckPivot.position.y)
            {
                _isGrounded = true;

                if (IsFalling)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                }

                IsFalling = !_isGrounded && !IsJumping;
                return true;
            }
        }
        _isGrounded = false;

        IsFalling = !IsJumping;

        return _isGrounded;
    }

    private void FixedUpdate()
    {
        StateMachine.OnFixedUpdate();
    }

    public void VerticalMove()
    {
        if (IsJumping)
        {
            _direction = new Vector2(_direction.x, 1);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_currentVerticalSpeed * Time.fixedDeltaTime * _direction).y);
            _currentVerticalSpeed -= verticalSpeedDecelerator;
        }
        if (IsFalling)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_currentFallingSpeed * Time.fixedDeltaTime * _direction).y);
            _currentFallingSpeed = Mathf.Clamp(_currentFallingSpeed + fallingSpeedAccelerator, 0, maxFallingSpeed);
        }
    }

    public void HorizontalMove()
    {
        _rigidbody.velocity = (movementSpeed + _currentRunSpeed) * Time.fixedDeltaTime * _direction.normalized;
    }

    public void MoveHorizontal(Vector2 newDirection)
    {
        _direction = new Vector2(newDirection.x, _direction.y);
    }
    public void MoveVertical(Vector2 newDirection)
    {
        _direction = new Vector2(_direction.x, newDirection.y);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        _direction = Vector2.zero;
    }

    public void Jump()
    {
        if (IsJumping || !_isGrounded) return;

        _isGrounded = false;
        _currentFallingSpeed = fallingSpeed;
        LastTransform = transform.position;
        _currentVerticalSpeed = verticalSpeed;
        IsJumping = true;
        JumpDestination = transform.position + Vector3.up * verticalMaxDestination;

        StateMachine.SetState(EPlayerState.Jumping);
    }

    public void Run(bool active)
    {
        _currentRunSpeed = active ? runSpeed : 0;
    }

    public void TryGoDown()
    {
        if (!_isGrounded) return;

        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.BoxCastNonAlloc(groundCheckPivot.position, Vector2.one, 0, Vector2.down, hits, groundCheckDistance, groundCheckLayerMask);

        foreach (var hit in hits.Where(x => x.collider != null))
        {
            if (hit.collider.TryGetComponent<PlatformHandler>(out var handler))
            {
                handler.FastDisable();
            }
        }
    }

    public void TryInteract()
    {
        if (!_isGrounded || Interacting) return;

        StateMachine.SetState(EPlayerState.Interacting);
        Interacting = true;

    }

    public bool Interact()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.CircleCastNonAlloc(transform.position + new Vector3(0, 0.5f, 0), radius, Vector2.zero, hits, groundCheckDistance, groundCheckLayerMask);

        foreach (var hit in hits.Where(x => x.collider != null))
        {
            if (hit.collider.TryGetComponent<Interactable>(out var interactable))
            {

                interactable.Interact(this);
                return true;
            }
        }
        return false;
    }

    public void Dematerialize(Checkpoint newCheckPoint)
    {
        var particle = Instantiate(dematerializePrefab, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity);
        Destroy(particle.gameObject, 2f);

        _wasDematerialize = true;

        graphics.SetActive(false);

        SetCheckpoint(newCheckPoint);
        GameManager.Instance.EnablePlayerKeyboard(false);
        Invoke(nameof(ResetPosition), timeBetweenRespawn);
        Invoke(nameof(InteractComplete), timeBetweenRespawn);
    }

    public void ResetPosition()
    {
        transform.position = currentCheckpoint != null ? currentCheckpoint.transform.position : _firstPosition;
        GameManager.Instance.EnablePlayerKeyboard(true);
        _isDying = false;

        if (_wasDematerialize)
        {
            _wasDematerialize = false;

            Materialize();
        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void Kill()
    {
        GameManager.Instance.EnablePlayerKeyboard(false);
        _rigidbody.velocity = Vector2.zero;

        StateMachine.SetState(EPlayerState.Dying);

        _isDying = true;
    }

    public void InteractComplete()
    {
        Interacting = false;

    }

    public void LandingComplete()
    {
        Landed = true;

    }

    public void Materialize()
    {
        var particle = Instantiate(materializePrefab, new Vector3(transform.position.x, transform.position.y, -0.1f), Quaternion.identity);
        Destroy(particle.gameObject, 2f);
        graphics.SetActive(true);

    }
}
