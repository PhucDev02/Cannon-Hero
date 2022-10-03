using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D bulletRg;

    void Update()
    {
        float angle = Mathf.Atan2(bulletRg.velocity.y, bulletRg.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Sensor")) && GameController.instance.isHit == false)
        {
            if (PlayerPrefs.GetInt("CharacterIdSelect") != 1)
            {
                UI_GamePlay.instance.missShot();
                Debug.Log("MissBySensor");
                gameObject.SetActive(false);
                GameController.instance.isMissed = true;
            }
            else gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GameController.instance.isHit == false)
        {
            if (PlayerPrefs.GetInt("CharacterIdSelect") != 1)
            {
                gameObject.SetActive(false);
                UI_GamePlay.instance.missShot();
                Debug.Log("MissByGround");
                GameController.instance.isMissed = true;
            }
            else gameObject.SetActive(false);
        }
    }
}
