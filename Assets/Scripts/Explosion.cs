using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject[] objs;
    Vector3 startPosition;
    [SerializeField] bool isFade;
    bool isExploded;
    Color initColor;
    // Start is called before the first frame update
    void Awake()
    {
        startPosition = objs[0].transform.localPosition;
        initColor = objs[0].GetComponent<SpriteRenderer>().color;
        isExploded = false;
    }
    public void reset()
    {
        isExploded = false;
        foreach (GameObject obj in objs)
        {
            obj.transform.localPosition = startPosition;
            obj.SetActive(false);
            obj.GetComponent<SpriteRenderer>().color = initColor;
        }
    }
    private void Update()
    {
        if (isExploded && isFade)
        {
            Color color = objs[0].GetComponent<SpriteRenderer>().color;
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<SpriteRenderer>().color = color - new Color(0, 0, 0, 1f * Time.deltaTime);
            }
            for(int i=0;i<objs.Length;i++)
            {
                objs[i].transform.Rotate(0, 0,Mathf.Pow(-1,i)* i*90 * Time.deltaTime);
            }
        }
    }
    public void makeExplode(int num)
    {
        isExploded = true;
        if (num == 0)
            num = objs.Length;
        for (int i = 0; i < num; i++)
        {
            objs[i].SetActive(true);
            objs[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100, 100), Random.Range(80, 200)), ForceMode2D.Force);
        }
    }
}
