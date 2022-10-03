using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightGun : MonoBehaviour
{
    // Start is called before the first frame update
    public static LightGun instance;
    [SerializeField] GameObject bullet, feverBullet;
    [SerializeField] Animator anim;
    [SerializeField] Explosion bulletExplosion;
    [SerializeField] Transform ground;
    [SerializeField] GameObject angleTrace;
    float rotateSpeed;
    float force;
    [SerializeField] bool isTouched; public bool isFired;
    private void Awake()
    {
        if (instance == null) instance = this;
        bullet.transform.position = gameObject.transform.position;
        bullet.transform.rotation = gameObject.transform.rotation;
    }
    void Start()
    {
        force = 550;
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
    bool IsTouchRealeased()
    {
        return (Input.touchCount == 0);
    }
    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(0);
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
        AudioManager.Instance.Play("GunShot");
        isTouched = false;
        isFired = true;
        anim.Play("GunRecoil");
        if (GameController.instance.isFever)
        {
            feverBullet.SetActive(true);
            feverBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotationInRad()), Mathf.Sin(rotationInRad())) * force, ForceMode2D.Force);
        }
        else
        {
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotationInRad()), Mathf.Sin(rotationInRad())) * force, ForceMode2D.Force);
        }
    }
    public void reset()
    {
        bulletExplosion.gameObject.transform.SetParent(gameObject.transform);
        bulletExplosion.reset();
        isTouched = false;
        isFired = false;
        bullet.transform.position = gameObject.transform.position;
        bullet.transform.rotation = gameObject.transform.rotation;
        feverBullet.transform.position = gameObject.transform.position;
        feverBullet.transform.rotation = gameObject.transform.rotation;
    }
    private float rotationInRad()
    {
        return transform.rotation.eulerAngles.z * Mathf.PI / 180;
    }
    public void makeExplosion()
    {
        bulletExplosion.gameObject.transform.SetParent(ground);
        bulletExplosion.makeExplode(0);
    }
    public bool isActive()
    {
        return bullet.activeInHierarchy;
    }
}
