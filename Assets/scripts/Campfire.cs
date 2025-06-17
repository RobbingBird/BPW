using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnFireOn : MonoBehaviour
{
    public Animator On;
    public Transform target;
    public RectTransform uiElement;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            On.SetBool("Lit", true);
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(uiElement.gameObject); 
        } 
    }

    void Update()
    {
        if (target != null && uiElement != null && On.GetBool("Lit") == false)
        {
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(target.position);
            uiElement.position = targetScreenPos;
        }
    }
}
