using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodySensor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerPrefs.GetInt("CharacterIdSelect") != 3)
            if (collision.gameObject.CompareTag("Bullet") && GameController.instance.isMissed == false)
            {
                gameObject.GetComponentInParent<EnemyController>().explodeBodyHit();
                collision.gameObject.SetActive(false);

                GameController.instance.bodyshot++;
                GameController.instance.isHit = true;
                GameController.instance.resetPlayer();
                GameController.instance.hitCount++;
                GameController.instance.increaseScore(1);
                GameController.instance.streak = 0 ;
            }
    }
}
