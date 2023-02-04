using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float timeCoolDown;
    [SerializeField] Transform shootPivot;

    Animator animator;

    float timePassed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= timeCoolDown)
        {
            timePassed = 0;
            animator.SetTrigger("Shoot");
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, shootPivot.position, transform.rotation);

    }

}
