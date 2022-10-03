using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOverPanel, gamePlayPanel, haloBestScore;
    [SerializeField] TextMeshProUGUI scoreText, scoreTextInGameOver, bestScore;
    public bool isMissed, isHit, gameOver, isFever;
    public int score, streak;
    // properties for shotgun
    public int headshot, bodyshot;
    public int hitCount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance = this;
            instance = this;
        }
        isHit = false;
        isMissed = false;
        gameOver = false;
        streak = 0;
        isFever = false;
        score = 0;
        player = Instantiate(GameManager.instance.characters[PlayerPrefs.GetInt("CharacterIdSelect")].prefab);
        hitCount = 0;
    }
    void Update()
    {
        executeShot();
        scoreText.text = score.ToString();
        if (gameOver == true)
        {
            gameOver = false;
            StartCoroutine(ActiveGameOverPanel(0.5f));
        }
    }
    void executeShot()
    {
        switch (PlayerPrefs.GetInt("CharacterIdSelect"))
        {
            case 1:
                if (Shotgun.instance.isFired == true && Shotgun.instance.allActive() == false)
                {
                    if (hitCount == 0)
                    {
                        hitCount = -1;
                        isMissed = true;
                        UI_GamePlay.instance.missShot();
                    }
                }
                break;
            //case 0:
            //    if (LightGun.instance.isFired == true && LightGun.instance.isActive() == false)
            //    {
            //        if (headshot > 0)
            //        {
            //            headshot = 0;
            //            increaseScore(2);
            //            streak++;
            //        }
            //        else if (bodyshot > 0)
            //        {
            //            streak = 0;
            //            increaseScore(1);
            //            bodyshot = 0;
            //        }
            //    }
            //    break;
            //case 2:
            //    if (LightGun.instance.isFired == true && LightGun.instance.isActive() == false)
            //    {
            //        if (headshot > 0)
            //        {
            //            headshot = 0;
            //            increaseScore(2);
            //            streak++;
            //        }
            //        else if (bodyshot > 0)
            //        {
            //            streak = 0;
            //            increaseScore(1);
            //            bodyshot = 0;
            //        }
            //    }
            //    break;

        }
    }
    public void resetPlayer()
    {
        player.GetComponent<PlayerBehavior>().resetPlayer();
    }
    public void resetGun()
    {
        player.GetComponent<PlayerBehavior>().resetGun();
    }
    IEnumerator ActiveGameOverPanel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //open it
        //Time.timeScale = 0;
        scoreTextInGameOver.text = score.ToString();
        AudioManager.Instance.Play("GameOver");
        if (PlayerPrefs.GetInt("allowSound") == 0)
            AudioManager.Instance.stopMusic = true;
        ExecuteBestScore();
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void increaseScore(int num)
    {
        if (isHit == true && isMissed == false)
        {
            if (num == 1)
            {
                UI_GamePlay.instance.addOnePoint();
            }
            else if (num == 2)
            {
                if (streak == 3)
                {
                    UI_GamePlay.instance.crazyStatus();
                    isFever = true;
                }
                else if (streak == 4)
                {
                    UI_GamePlay.instance.ultraKillStatus();
                    num = 5;
                    isFever = false;
                    streak = 0;
                }
                else if (streak < 3)
                {
                    UI_GamePlay.instance.addTwoPoint();
                    isFever = false;
                }
            }
            score += num;
        }
    }
    public void makePlayerRun()
    {
        player.GetComponent<PlayerBehavior>().run();
    }
    public void makePlayerStop()
    {
        player.GetComponent<PlayerBehavior>().stop();
    }
    public void ExecuteBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore.text = "New best";
            haloBestScore.SetActive(true);
        }
        else
        {
            bestScore.text = "Best " + PlayerPrefs.GetInt("BestScore").ToString();
        }
    }
    public void addCoin(int num)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + num);
    }
}
