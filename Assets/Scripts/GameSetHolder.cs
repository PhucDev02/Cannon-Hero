using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetHolder : MonoBehaviour
{
    public static GameSetHolder instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        moveSpeed = 5.0f;
        distanceMoved = 0;
        //moveDistance = 10 * Screen.width / Screen.height;
        moveDistance = 5.625f;
        //if (Screen.height / Screen.width == 16 / 9)
        //{
        //    Debug.Log("5.625");
        //    moveDistance = 5.625f;
        //}
        //else moveDistance = 5.0f;
    }
    // Start is called before the first frame update
    public float moveDistance;
    float moveSpeed;
    [SerializeField] GameObject[] gameSet;
    float distanceMoved;
    void Start()
    {
        Debug.Log(Screen.currentResolution);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.isHit == true)
        {
            for (int i = 0; i < gameSet.Length; i++)
                gameSet[i].transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
            distanceMoved += moveSpeed * Time.deltaTime;
            ParallaxBackground.instance.moveBackground();
            executeOverDistanceMove();
            executeGroundOverScreen();
        }
    }
    void executeOverDistanceMove()
    {
        if (distanceMoved >= moveDistance)
        {
            for (int i = 0; i < gameSet.Length; i++)
                gameSet[i].transform.position += new Vector3(distanceMoved - moveDistance, 0, 0);
            distanceMoved = 0;
            GameController.instance.isHit = false;
            GameController.instance.resetGun();
            GameController.instance.hitCount=0;
            GameController.instance.makePlayerStop();
            ParallaxBackground.instance.stopBackground();
        }
        else GameController.instance.makePlayerRun();
    }
    void executeGroundOverScreen()
    {
        for (int i = 0; i < gameSet.Length; i++)
        {
            if(gameSet[i].transform.localPosition.x<-6.9)
            {
                gameSet[i].transform.position = gameSet[getIndex(i)].transform.position + new Vector3(moveDistance, 0, 0);
                gameSet[i].GetComponent<GameSet>().reset();
            }
        }
    }
    private int getIndex(int index)
    {
        if (index == 0) return gameSet.Length-1;
        else return index-1;
    }
}
