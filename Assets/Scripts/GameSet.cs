using UnityEngine;

public class GameSet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject flyingGround;
    [SerializeField] GameObject mainGround;
    [SerializeField] GameObject enemy;
    Vector3 startPosition;
    private void Awake()
    {
        startPosition= new Vector3(-3f, 4.5f, 0);
    }
    void Start()
    {
        reset();
    }
    private void Update()
    {
        if (enemy.transform.position.x < 3)
        {
            enemy.SetActive(true);
        }
        else enemy.SetActive(false);
    }
    public void reset()
    {
        flyingGround.transform.localPosition = startPosition;
        flyingGround.transform.localPosition += randomPosition();
        
        enemy.GetComponent<EnemyController>().reset();
        enemy.transform.position = mainGround.transform.position + new Vector3(0.05f, 0.5f, 0);
    }
    private Vector3 randomPosition()
    {
        float x = Random.Range(0.0f, 0.5f*GameSetHolder.instance.moveDistance);
        float y = Random.Range(0, 1.5f*x);
        return new Vector3(x,y,0);
    }
}
