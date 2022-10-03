using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadSensor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerPrefs.GetInt("CharacterIdSelect") != 3)
            if (collision.gameObject.CompareTag("Bullet") && GameController.instance.isMissed == false)
            {
                gameObject.GetComponentInParent<EnemyController>().explodeCoin();
                gameObject.GetComponentInParent<EnemyController>().explodeCoprse();
                AudioManager.Instance.Play("CoinDrop");
                collision.gameObject.SetActive(false);
                //
                GameController.instance.resetPlayer();
                GameController.instance.isHit = true;
                GameController.instance.headshot++;
                GameController.instance.hitCount++;
                GameController.instance.increaseScore(2);
                GameController.instance.streak++;
            }
    }
}
