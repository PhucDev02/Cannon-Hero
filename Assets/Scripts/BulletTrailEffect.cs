using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailEffect : MonoBehaviour
{
    // Start is called before the first frame update
    float totalTime;
    public float spawnTime;
    public GameObject prefabTrail;
    void Start()
    {
        totalTime = 0;
    }

    // Update is called once per framefi
    private void FixedUpdate()
    {
        if (totalTime >= spawnTime)
        {
            GameObject instance = Instantiate(prefabTrail, transform.position, transform.rotation);
            Destroy(instance,0.5f);
            totalTime = 0;
        }
        else totalTime += Time.fixedDeltaTime;
    }
}
