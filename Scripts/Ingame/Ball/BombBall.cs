using UnityEngine;

public class BombBall : MonoBehaviour
{
    Cannon cannon;

    Transform bombTargetBasket;
    Transform target;

    const float BOMB_SPEED = 30;

    bool notFirstSpawn;
    void Awake()
    {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
        bombTargetBasket = GameObject.Find("BombTargetBasket").transform;
    }
    void OnEnable()
    {
        if (notFirstSpawn) { target = bombTargetBasket.GetChild(0).transform; }
        notFirstSpawn = true;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, BOMB_SPEED * Time.deltaTime);
        if (transform.position == target.position)
        {
            ObjectPoolManager.Instance.GetFire(this.transform);
            ReturnBomb();
        }
    }
    void ReturnBomb()
    {
        cannon.GetTarget();

        BombBall bombBall = this.gameObject.GetComponent<BombBall>();
        bombBall.transform.SetParent(ObjectPoolManager.Instance.transform);
        ObjectPoolManager.Instance.BombBallQueue.Enqueue(bombBall);
        bombBall.transform.position = new Vector3(0, 3, 8.5f);
        bombBall.gameObject.SetActive(false);
    }
}
