using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    //variables
    public EnemyState MyState;
    public GameObject Player;
    public Coroutine CurrentBehaviour;
    public float CurrentDistance;
    public float StartFollowDistance;
    public Animator animator;
    private float timer = 0.0f;
    private float timer1 = 0.0f;
    public Rigidbody2D rb;
    private bool PatrolMove = true;
    private Vector2 Movement;
    public float MoveSpeed;
    public Transform target;
    public float AttackDistance;
    public PlayerHealth Health; 

    //finite state machine
    private void Awake()
    {
        UpdateBehaviour(EnemyState.Patrol); 
    }

    public enum EnemyState
    {
        Patrol,
        Follow,
    }

    private void FixedUpdate ()
    {
        CurrentDistance = Vector2.Distance(Player.transform.position, transform.position);
        if (CurrentDistance > StartFollowDistance && MyState != EnemyState.Patrol)
        {
            UpdateBehaviour(EnemyState.Patrol);
        }
        else if (CurrentDistance <= StartFollowDistance && MyState != EnemyState.Follow)
        { 
            UpdateBehaviour(EnemyState.Follow);
        }
    }

    private void UpdateBehaviour(EnemyState state)
    {
        MyState = state;
        if (CurrentBehaviour != null)
        {
            StopCoroutine(CurrentBehaviour); 
        }
        switch (MyState)
        {
            case EnemyState.Patrol:
                CurrentBehaviour = StartCoroutine(Patrol());
                break;
            case EnemyState.Follow:
                CurrentBehaviour = StartCoroutine(Follow());
                break;
        }
    }

    //follow state & attack
    public IEnumerator Follow()
    {
        while (true)
        {
            if (CurrentDistance <= AttackDistance && timer1 <= 0.0f)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("attack", true);
                timer1 = 1.0f;
                Health.health -= 2;
            }
            else if(timer1 <= 0.0f)
            {
                animator.SetBool("isMoving", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        
    }

    //patrol state
    public IEnumerator Patrol()
    {
        while (true)
        {
            rb.MovePosition(rb.position + Movement * Time.fixedDeltaTime);
            if (timer <= 0.0f && PatrolMove == true)
            {
                Movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                timer = Random.Range(5, 10);
                PatrolMove = false;
                animator.SetBool("isMoving", true);
            }
            else if (timer <= 0.0f && PatrolMove == false)
            {
                timer = Random.Range(5, 20);
                Movement = new Vector2(0, 0);
                PatrolMove = true;
                animator.SetBool("isMoving", false);
            }
            yield return null;
        }
    }

    
    private void Update()
    {
        //timer
        timer -= Time.deltaTime;
        timer1 -= Time.deltaTime;


        //sprite rotation
        if (Player.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1); 
        }
    }
}
