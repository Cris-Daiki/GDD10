using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play_Exit : MonoBehaviour
{
    public void Inicio(){
        SceneManager.LoadScene("Juego");
    }
    public void Exit(){
        Application.Quit();
    }
}

