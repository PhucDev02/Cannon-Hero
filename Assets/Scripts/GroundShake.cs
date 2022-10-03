using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShake : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            anim.Play("GroundShake");
        }
    }
    public void Shake()
    {
        Debug.Log("shake");
        anim.Play("GroundShake");
    }
}
