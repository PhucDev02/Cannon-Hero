using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_Controller : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManager.Instance.stopMusic = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamePlay");
    }
    public void BackToMenu()
    {
        AudioManager.Instance.stopMusic = false;
        SceneManager.LoadScene("Menu");
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1;
        AudioManager.Instance.stopMusic = false;
        AudioManager.Instance.Stop("GameOver");
        Debug.Log(AudioManager.Instance.stopMusic);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    public void ClickSound()
    {
        AudioManager.Instance.Play("Click");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void ToggleSound()
    {
        AudioManager.Instance.ToggleSound();
    }
}
