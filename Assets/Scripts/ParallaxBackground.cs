using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class ParallaxComponent
    {
        public string name;
        public GameObject[] gameObject;
        public float moveSpeed;
        public bool isMoving;
        public float distance,startPosition;
    }
    [SerializeField]
    ParallaxComponent[] backgrounds;
    int num = 4; //number of cloud set
    #region singleton
    public static ParallaxBackground instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion
    void Start()
    {
        foreach (ParallaxComponent i in backgrounds)
        {
            //i.isMoving = false;
            i.startPosition = i.gameObject[0].transform.position.x;
            i.distance = i.gameObject[1].transform.position.x - i.gameObject[0].transform.position.x;
        }
        for(int i=0;i<num;i++)
        {
            backgrounds[i].isMoving = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (ParallaxComponent i in backgrounds)
        {
            if (i.isMoving == true)
            {
                for (int j = 0; j < i.gameObject.Length; j++)
                {
                    i.gameObject[j].transform.position -= new Vector3(i.moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                for (int j = 0; j < i.gameObject.Length; j++)
                {
                    if (i.gameObject[j].transform.position.x < i.startPosition - i.distance)
                    {
                        i.gameObject[j].transform.position = new Vector3(i.distance + i.startPosition, i.gameObject[j].transform.position.y);
                    }
                }
            }
        }
    }
    public void moveBackground()
    {
        for(int i=num;i<backgrounds.Length;i++)
        {
            backgrounds[i].isMoving = true;
        }
    }
    public void stopBackground()
    {
        for (int i = num; i < backgrounds.Length; i++)
        {
            backgrounds[i].isMoving = false;
        }
    }

}
