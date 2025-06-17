using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public CanvasGroup DarkTrans;
    public RectTransform LightScale;
    public RectTransform DarkScale;
    public Transform Staff;
    public Animator Defeat;
    private bool Lost = false; 

    private void Start()
    {
        DarkTrans.alpha = 9.8f;
        LightScale.localScale = new Vector2 (1, 1);
        DarkScale.localScale = new Vector2(10f, 10f);
    }
    private void Update()
    {
        if (health <= 0 && Lost == false && GetComponent<TutorialTriggers>().enabled == false)
        {
            DarkTrans.alpha = 1.0f;
            LightScale.localScale = new Vector2(0.0f, 0.0f);
            Staff.localScale = new Vector2(0, 0);
            Defeat.SetTrigger("Defeat");
            Lost = true;
            Invoke("BackToMenu", 10f);
        }
        else if (health <= 0 && Lost == false && GetComponent<TutorialTriggers>().enabled == false)
        {
            health = 20f; 
        }
        else if (health <= 30 && Lost == false)
        {
            DarkTrans.alpha = -0.02f * health + 0.96f;
            LightScale.localScale = new Vector2(health * 0.04f + 0.12f, health * 0.04f + 0.12f);
            Staff.localScale = new Vector2(health * 0.06f + 0.50f, health * 0.06f + 0.50f);
        }
        else if (health > 30 && Lost == false)
        {
            health = 30;
        }
        
        if (Lost == true)
        {
            Debug.Log("Lost");
        }
    }

    //Campfire Heal
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Campfire"))
        {
            health += 10;
        }
    }

    //Back to menu
    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
