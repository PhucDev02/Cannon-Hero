using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserGun : MonoBehaviour
{
    // Start is called before the first frame update
    public static LaserGun instance;
    [SerializeField] GameObject bullet, feverBullet;
    [SerializeField] Animator anim;
    [SerializeField] Explosion bulletExplosion;
    [SerializeField] GameObject angleTrace;
    float rotateSpeed;
    [SerializeField] bool isTouched, isFired;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        isTouched = false;
        isFired = false;
        rotateSpeed = 50.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (bullet.activeInHierarchy == true)
            bulletExplosion.gameObject.transform.position = bullet.transform.position;
        if (!IsMouseOverUI() && Input.GetMouseButtonDown(0) && isFired == false)
        {
            isTouched = true;
            // angleTrace.SetActive(true);
        }
        if (isTouched == true)
        {
            rotateGun();
        }
        if (IsTouchRealeased() && isFired == false && isTouched == true)
        {
            fire();
        }
    }
    void rotateGun()
    {
        if (transform.rotation.eulerAngles.z <= 90)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        else transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    void fire()
    {
        //angleTrace.SetActive(false);
        isTouched = false;
        isFired = true;
        anim.Play("GunRecoil");
        if (GameController.instance.isFever)
        {
            feverBullet.SetActive(true);
            feverBullet.GetComponent<Laser>().fire();
        }
        else
        {
            bullet.SetActive(true);
            bullet.GetComponent<Laser>().fire();
        }
    }
    public void reset()
    {
        bulletExplosion.gameObject.transform.SetParent(gameObject.transform);
        bulletExplosion.reset();
        isTouched = false;
        isFired = false;
        bullet.GetComponent<Laser>().reset();
        feverBullet.GetComponent<Laser>().reset();
        bullet.transform.rotation = gameObject.transform.rotation;
        feverBullet.transform.rotation = gameObject.transform.rotation;
    }
    bool IsTouchRealeased()
    {
        return (Input.touchCount == 0);
    }
    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(0);
    }
    private float rotationInRad()
    {
        return transform.rotation.eulerAngles.z * Mathf.PI / 180;
    }
    public void makeExplosion()
    {
        bulletExplosion.makeExplode(0);
    }
}
