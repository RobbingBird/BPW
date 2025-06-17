using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string Scene;
    public void PlayGame()
    {
        SceneManager.LoadScene(Scene);
    }

}
