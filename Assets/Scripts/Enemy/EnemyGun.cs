using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet, player;
    [SerializeField] Animator anim;
    float rotateSpeed, force;
    bool isFired;
    void Start()
    {
        force = 700;
        isFired = false;
        rotateSpeed = 50.0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy == true)
            if (GameController.instance.isMissed == true && GameController.instance.isHit == false && isFired == false)
            {
                if(transform.rotation.eulerAngles.z>80)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                if (transform.rotation.eulerAngles.z > anglePlayerVsEnemy())
                {
                    transform.rotation = Quaternion.Euler(0, 0, anglePlayerVsEnemy());
                    fire();
                }
                else
                {
                    transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
                }
            }
    }
    void fire()
    {
        AudioManager.Instance.Play("GunShot");
        anim.Play("EnemyGunRecoil");
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(forceIn2D() * force, ForceMode2D.Force);
        isFired = true;
    }
    float anglePlayerVsEnemy()
    {
        return Vector2.Angle(player.transform.position - bullet.transform.position, Vector3.left);
    }
    Vector2 forceIn2D()
    {
        return new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad))*-1;
    }
}
