using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite musicOn, musicOff, soundOn, soundOff;
    public Button soundButton, musicButton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("allowMusic") == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
        else musicButton.GetComponent<Image>().sprite = musicOff;
        if (PlayerPrefs.GetInt("allowSound") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
        }
        else soundButton.GetComponent<Image>().sprite = soundOff;

    }
}
