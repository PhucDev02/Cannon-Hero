using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject head;
    [SerializeField] Animator animator;
    [SerializeField] Explosion corpseExplosion;
    [SerializeField] GameObject[] bodyParts;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        head.transform.rotation = Quaternion.AngleAxis(gun.transform.rotation.z * Mathf.Rad2Deg / 1.5f, Vector3.forward);
    }
    public void resetPlayer()
    {
        head.transform.rotation = Quaternion.Euler(0, 0, 0);
        gun.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void resetGun()
    {
        switch (PlayerPrefs.GetInt("CharacterIdSelect"))
        {
            case 0:
                gun.GetComponent<LightGun>().reset();
                break;
            case 1:
                gun.GetComponent<Shotgun>().reset();
                break;
            case 2:
                gun.GetComponent<LightGun>().reset();
                break;
            case 3:
                gun.GetComponent<LaserGun>().reset(); 
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            makeCorpseExplode();
            collision.gameObject.SetActive(false);
            GameController.instance.gameOver = true;
        }
    }
    public void run()
    {
        animator.SetBool("isRunning", true);
    }
    public void stop()
    {
        animator.SetBool("isRunning", false);
    }
    public void makeCorpseExplode()
    {
        foreach (GameObject obj in bodyParts)
            obj.SetActive(false);
        gun.SetActive(false);
        head.SetActive(false);
        corpseExplosion.makeExplode(0);
    }
}
