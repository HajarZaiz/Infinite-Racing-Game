using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnScript : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;

    public void Play()
    {
        clickSound.Play();
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        clickSound.Play();
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        clickSound.Play();
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void Back()
    {
        clickSound.Play();
        SceneManager.LoadScene(0);
    }
}
