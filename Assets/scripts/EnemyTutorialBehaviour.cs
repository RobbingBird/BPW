using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialBehaviour : MonoBehaviour
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
    public float MoveSpeed;
    public Transform target;
    public float AttackDistance;
    public PlayerHealth Health;
    public TutorialTriggers TutorialTriggers;

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

    private void FixedUpdate()
    {
        CurrentDistance = Vector2.Distance(Player.transform.position, transform.position);
        if (CurrentDistance > StartFollowDistance && MyState != EnemyState.Patrol)
        {
            UpdateBehaviour(EnemyState.Patrol);
        }
        else if (TutorialTriggers.EnemyAttack == true)
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
                GetComponent<EnemyTutorialBehaviour>().enabled = false;
            }
            else if (timer1 <= 0.0f)
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
