using UnityEngine;

public class BlueBall : MonoBehaviour
{
    Vector3 startPos;
    Vector3 movePos;
    Vector3 firstForce;
    Vector3 secondForce;

    const float MOVE_SPEED = 150;
    const float FORCE = 3;
    float randForce = 0;
    bool firstBump;
    bool afterBump;

    BallBasket ballBasket;
    Rigidbody rigid;

    void Awake()
    {
        ballBasket = GameObject.Find("BallBasket").GetComponent<BallBasket>(); ;
        rigid = this.GetComponent<Rigidbody>();
        while (randForce <= 1) { randForce = Random.Range(-FORCE, FORCE); }
        firstForce = new Vector3(0, 0, -FORCE);
        secondForce = new Vector3(randForce, 0, FORCE);
    }
    void OnEnable()
    {
        firstBump = true;
        afterBump = false;

        rigid.velocity = Vector3.zero;
        rigid.AddForce(firstForce, ForceMode.VelocityChange);
    }
    void Update()
    {
        if (afterBump)
        {
            rigid.velocity = movePos * MOVE_SPEED * Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "InvincibleCube")
        {
            FindObjectOfType<AudioManager>().Play("Bump");
        }
        BallDirection(col);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SideOfBall"))
        {
            int ball_index = transform.GetSiblingIndex();
            ballBasket.ReturnBall(ball_index, true);
            ObjectPoolManager.Instance.GetObstacle(other.transform);
        }
    }
    void BallDirection(Collision _col)
    {
        if (firstBump)
        {
            startPos = transform.position;
            rigid.AddForce(secondForce, ForceMode.VelocityChange);
            firstBump = false;
        }
        else
        {
            afterBump = true;
            Vector3 hitPos = _col.contacts[0].point;

            Vector3 incomingVec = hitPos - startPos;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, _col.contacts[0].normal);

            movePos = reflectVec.normalized;
            startPos = transform.position;
        }
    }
}
