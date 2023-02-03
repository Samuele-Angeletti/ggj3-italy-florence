using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    Rigidbody2D _rigidbody;
    Vector2 _direction;
    Vector2 _jumpDestination;
    Vector2 _lastTransform;
    float _currentFallingSpeed;
    bool _isJumping;
    bool _isFalling;
    bool _isGrounded;
    float _currentVerticalSpeed;
    float _currentRunSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _isFalling = !GroundCheck();
    }

    private void Update()
    {
        if(_isJumping)
        {
            if(transform.position.y > _jumpDestination.y || transform.position.y < _lastTransform.y)
            {
                _isJumping = false;
                _isFalling = true;
                GroundCheck();
            }
            _lastTransform = transform.position;

        }
        else if(_isFalling)
        {
            _direction = new Vector2(_direction.x, -1);
        }

        GroundCheck();
    }

    private bool GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[10];
        Physics2D.RaycastNonAlloc(groundCheckPivot.position, Vector2.down, hits, groundCheckDistance, groundCheckLayerMask);
        _isGrounded = hits.Count(x => x.collider != null) > 0;

        _isFalling = !_isGrounded && !_isJumping;

        return _isGrounded;
    }

    private void FixedUpdate()
    {
        HorizontalMove();
        VerticalMove();
    }

    private void VerticalMove()
    {
        if(_isJumping)
        {
            _direction = new Vector2(_direction.x, 1);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_currentVerticalSpeed * Time.fixedDeltaTime * _direction).y);
            _currentVerticalSpeed -= verticalSpeedDecelerator;
        }
        if(_isFalling)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_currentFallingSpeed * Time.fixedDeltaTime * _direction).y);
            _currentFallingSpeed = Mathf.Clamp(_currentFallingSpeed + fallingSpeedAccelerator, 0, maxFallingSpeed);
        }
    }

    private void HorizontalMove()
    {
        _rigidbody.velocity = (movementSpeed + _currentRunSpeed) * Time.fixedDeltaTime * _direction.normalized;
    }

    public void Move(Vector2 newDirection)
    {
        _direction = new Vector2(newDirection.x, _direction.y);
    }

    public void Jump()
    {
        if (_isJumping || !_isGrounded) return;

        _currentFallingSpeed = fallingSpeed;
        _lastTransform = transform.position;
        _currentVerticalSpeed = verticalSpeed;
        _isJumping = true;
        _jumpDestination = transform.position + Vector3.up * verticalMaxDestination;
    }

    public void Run(bool active)
    {
        _currentRunSpeed = active ? runSpeed : 0;
    }

}
