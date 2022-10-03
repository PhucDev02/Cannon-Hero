using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GamePlay : MonoBehaviour
{
    public static UI_GamePlay instance;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] GameObject add1Point, add2Point,miss,ultraKill,crazy;
    [SerializeField] GameObject crownScore;
    public Sprite musicOn, musicOff, soundOn, soundOff;
    public Button soundButton, musicButton;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Update()
    {
        if(GameController.instance.score> PlayerPrefs.GetInt("BestScore"))
        {
            crownScore.SetActive(true);
        }
        coin.text = PlayerPrefs.GetInt("Coins").ToString();

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
    public void addOnePoint()
    {
        add1Point.GetComponent<Animator>().Play("addPointAnim");
    }
    public void addTwoPoint()
    {
        add2Point.GetComponent<Animator>().Play("addPointAnim");
    }
    public void missShot()
    {
        miss.GetComponent<Animator>().Play("addPointAnim");
    }
    public void crazyStatus()
    {
        crazy.GetComponent<Animator>().Play("addPointAnim");
    }
    public void ultraKillStatus()
    {
        ultraKill.GetComponent<Animator>().Play("addPointAnim");
    }
}
