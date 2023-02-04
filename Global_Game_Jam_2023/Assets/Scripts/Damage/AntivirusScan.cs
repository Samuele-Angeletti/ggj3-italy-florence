using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntivirusScan : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float xDestination;
    [SerializeField] float timeCoolDown;
    float timePassed;
    Rigidbody2D _rigidbody;
    Vector3 startPosition;
    bool canMove = true;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if(canMove)
            _rigidbody.velocity = speed * Time.fixedDeltaTime * Vector2.right;
    }

    private void Update()
    {
        if(transform.position.x > xDestination && canMove)
        {
            transform.position = startPosition;
            _rigidbody.velocity = Vector2.zero;
            canMove = false;
        }
        if(!canMove)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeCoolDown)
            {
                timePassed = 0;
                canMove = true;
            }
        }
    }
}
