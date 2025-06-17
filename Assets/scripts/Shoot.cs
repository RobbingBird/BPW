using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //variables
    public Transform player;
    public GameObject bullet;
    public Transform firePoint;
    public Animator animator;
    public PlayerHealth Health;
    public Transform Staff;
    public float BulletVel = 5; 

    Vector2 lookDirection;
    float lookAngle;

    // Update is called once per frame
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = Staff.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle - 90);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * 5;

            animator.SetBool("attack", true);
            Health.health -= 1; 
        }
    }
}
