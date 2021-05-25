using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameISPaused = false;

    public GameObject PauseMenuUI;

    public playerController player;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.isDead)
        {
            Debug.Log("Nikita loh");
            if (GameISPaused)
            {

                Resume();

            }
            else
            {
                Pause();
               

            }

        }
    }



     void Pause ()
    {

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameISPaused = true;
        

    }


    public void Resume ()
    {

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameISPaused = false;

    }


    public void LoadMenu()
    {

        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

}
