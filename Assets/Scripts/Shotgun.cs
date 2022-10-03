using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shotgun : MonoBehaviour
{
    // Start is called before the first frame update
    public static Shotgun instance;
    [SerializeField] GameObject[] bullet, feverBullet;
    [SerializeField] Animator anim;
    [SerializeField] Explosion bulletExplosion;
    [SerializeField] GameObject angleTrace;
    float rotateSpeed;
    float force;
    public bool isTouched, isFired;
    private void Awake()
    {
        if (instance == null) instance = this;
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].transform.position = gameObject.transform.position;
            bullet[i].transform.rotation = Quaternion.Euler(0, 0, (i - 1) * 30);
        }
        for (int i = 0; i < feverBullet.Length; i++)
        {
            feverBullet[i].transform.position = gameObject.transform.position;
            feverBullet[i].transform.rotation = Quaternion.Euler(0, 0, (i - 2) * 15);
        }
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
        AudioManager.Instance.Play("GunShot");
        isTouched = false;
        isFired = true;
        float x = -5*Mathf.Deg2Rad;
        if (GameController.instance.isFever)
        {
            for (int i = 0; i < feverBullet.Length; i++)
            {
                feverBullet[i].SetActive(true);
                feverBullet[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotationInRad()+x), Mathf.Sin(rotationInRad())+x) * force, ForceMode2D.Force);
                feverBullet[i].transform.SetParent(null);
                x += Mathf.PI / 36 ;
            }
            anim.Play("GunRecoil");
        }
        else
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                bullet[i].SetActive(true);
                bullet[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(rotationInRad()+x), Mathf.Sin(rotationInRad()+x)) * force, ForceMode2D.Force);
                bullet[i].transform.SetParent(null);
                x += Mathf.PI / 36;
            }
            anim.Play("GunRecoil");
        }
    }
    public void reset()
    {
        isTouched = false;
        isFired = false;
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].SetActive(false);
            bullet[i].transform.SetParent(gameObject.transform);
            bullet[i].transform.position = gameObject.transform.position;
            bullet[i].transform.rotation = Quaternion.Euler(0,0,(i-1)*30);
        }
        for(int i=0;i<feverBullet.Length;i++)
        {
            feverBullet[i].SetActive(false);
            feverBullet[i].transform.SetParent(gameObject.transform);
            feverBullet[i].transform.position = gameObject.transform.position;
            feverBullet[i].transform.rotation = Quaternion.Euler(0,0,(i-2)*15);
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
    private float rotationInRad()
    {
        return transform.rotation.eulerAngles.z * Mathf.PI / 180;
    }
    public void makeExplosion()
    {
        bulletExplosion.makeExplode(0);
    }
    public bool allActive()
    {
        for(int i=0;i<bullet.Length;i++)
        {
            if (bullet[i].activeInHierarchy == true) return true;
        }
        for (int i = 0; i < feverBullet.Length; i++)
        {
            if (feverBullet[i].activeInHierarchy == true) return true;
        }
        return false;
    }
}

       
