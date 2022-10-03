using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BoxCollider2D headCollider, bodyCollider;
    public Explosion coinExplosion,corpseExplosion;
    [SerializeField] GameObject entireBody;
    public void reset()
    {
        entireBody.SetActive(true);
        coinExplosion.reset();
        corpseExplosion.reset();
        headCollider.isTrigger = false;
        bodyCollider.isTrigger = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            setTrigger();
        }
    }
    public void explodeCoin()
    {
        int coin = randomCoin();
        GameController.instance.addCoin(coin);
        coinExplosion.makeExplode(coin);
    }
    int randomCoin()
    {
        return Random.Range(1, 4);
    }
    public void explodeCoprse()
    {
        entireBody.SetActive(false);
        corpseExplosion.makeExplode(0);
    }
    public void explodeBodyHit()
    {
        corpseExplosion.makeExplode(3);
    }
    public void setTrigger()
    {
        headCollider.isTrigger = true;
        bodyCollider.isTrigger = true;
    }
}
