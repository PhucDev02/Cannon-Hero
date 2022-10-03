using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask layersToHit;
    RaycastHit2D hit;
    [SerializeField] Sprite sprite;
    float deltaScale;
    void Start()
    {
        deltaScale = 1f;
        reset();
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.transform.localScale -= new Vector3(0, deltaScale * Time.deltaTime, 0);
            if (transform.localScale.y < 0) transform.localScale = new Vector3(0, 0, 0);
            //
            if (hit.collider == null)
            {
                transform.localScale = new Vector3(50f, transform.localScale.y, 1);
                return;
            }
            transform.localScale = new Vector3(hit.distance * 1.52f, transform.localScale.y, 1);

            //
        }
    }
    public void fire()
    {
        AudioManager.Instance.Play("LaserShot");
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        hit = Physics2D.Raycast(transform.position, dir, 50.0f);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(50f, transform.localScale.y, 1);
            return;
        }
        transform.localScale = new Vector3(hit.distance * 1.52f, transform.localScale.y, 1);
        Debug.Log(hit.collider.gameObject.tag);
        GameObject trace = Instantiate(gameObject, transform.parent);
        trace.transform.SetParent(null);
        Destroy(trace, 2f);
        //EditorApplication.isPaused = true;
        switch (hit.collider.gameObject.tag)
        {
            case "EnemyHead" :
                hit.collider.gameObject.GetComponentInParent<EnemyController>().explodeCoin();
                hit.collider.gameObject.GetComponentInParent<EnemyController>().explodeCoprse();
                hit.collider.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(200,200));
                hit.collider.gameObject.GetComponentInParent<EnemyController>().setTrigger();
                gameObject.SetActive(false);
                GameController.instance.isHit = true;
                GameController.instance.resetPlayer();
                GameController.instance.streak++;
                GameController.instance.increaseScore(2);
                AudioManager.Instance.Play("CoinDrop");
                Debug.Log("headshot");
                break;
            case "EnemyBody":
                hit.collider.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(200, 200));
                hit.collider.gameObject.GetComponentInParent<EnemyController>().explodeBodyHit();
                hit.collider.gameObject.GetComponentInParent<EnemyController>().setTrigger();
                gameObject.SetActive(false);
                GameController.instance.streak = 0;
                GameController.instance.isFever = false;
                GameController.instance.isHit = true;
                GameController.instance.resetPlayer();
                GameController.instance.increaseScore(1);
                break;
            case "Ground":
                hit.collider.gameObject.GetComponentInParent<GroundShake>().Shake();
                UI_GamePlay.instance.missShot();
                GameController.instance.isMissed = true;
                break;
            case "Sensor":
                UI_GamePlay.instance.missShot();
                GameController.instance.isMissed = true;
                break;
        }
    }
    public void reset()
    {
        gameObject.transform.localScale = new Vector3(1, 0.3f, 1);
    }
}
