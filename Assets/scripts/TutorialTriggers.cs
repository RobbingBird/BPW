using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTriggers : MonoBehaviour
{
    //variables 
    public Animator Intro1;
    public Animator Intro2;
    public Animator Intro3;
    public Animator Intro4;
    public Animator Intro5;
    public Animator Intro6;
    private bool Part1 = true;
    private bool Part2 = false;
    private bool Part3 = false;
    private bool Part4 = false;
    private bool Part5 = false;
    private bool Part6 = false;
    public Animator Player;
    public bool EnemyAttack = false;
    public GameObject Enemy;
    public string Scene;


    private void Update()
    {
        if (Part1 == true && Input.GetMouseButtonDown(0))
        {
            Part1 = false;
            Invoke("Part2True", 1f);
            Intro1.SetTrigger("Intro1Del");
        }

        if (Part2 == true && Input.GetMouseButtonDown(0))
        {
            Part2 = false;
            Intro2.SetTrigger("Intro1Del");
            Invoke("Part3True", 1f);
        }

        if (Part3 == true && Input.GetMouseButtonDown(0))
        {
            Part3 = false;
            Intro3.SetTrigger("Intro1Del");
            EnemyAttack = true;
            Invoke("Part4True", 1f);
        }

        if (Part4 == true && Enemy == null)
        {
            Part4 = false;
            Intro4.SetTrigger("Intro1Del");
            Invoke("Part5True", 1f);
        }

        if (Part5 == true && Input.GetMouseButtonDown(0))
        {
            Part5 = false;
            Intro5.SetTrigger("Intro1Del");
        }

        if (Part6 == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(Scene);
        }
    }

    public void Part2True()
    {
        Part2 = true;
        Intro2.SetTrigger("Intro1");
        GetComponent<PlayerMovement>().enabled = true;
    }

    public void Part3True()
    {
        Part3 = true;
        Intro3.SetTrigger("Intro1");
    }

    public void Part4True()
    {
        Part4 = true;
        Intro4.SetTrigger("Intro1");
        GetComponent<Shoot>().enabled = true;
    }

    public void Part5True()
    {
        Part5 = true;
        Intro5.SetTrigger("Intro1");
        GetComponent<PlayerMovement>().enabled = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger1"))
        {
            GetComponent<PlayerMovement>().enabled = false;
            Player.SetBool("isMoving", false);
            Destroy(collision.gameObject);

            if (Part2 == true)
            {
                Intro2.SetTrigger("Intro1Del");
                Part2 = false;
                Invoke("Part3True", 1f);
            }
        }

        if (collision.CompareTag("Campfire"))
        {
            Intro6.SetTrigger("Intro1");
            Intro5.SetTrigger("Intro1Del");
            Part6 = true; 
        }
    }
}
