using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //variables
    public float FireAmount = 0;
    public TextMeshProUGUI CampfireUI;
    public GameObject Darkness;
    public Animator Victory;

    void Update()
    {
        CampfireUI.text = FireAmount + "/10";
       
        if (FireAmount == 10)
        {
            Destroy(Darkness);
            Victory.SetTrigger("Victory");
            FireAmount += 1;
            Invoke("BackToMenu", 10f);
        }
        else if (FireAmount > 10)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FireAmount += 1;
    }

    //Back to menu
    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
