using UnityEngine;

public class GreenBall : MonoBehaviour
{
    Vector3 startPos;
    Vector3 movePos;
    Vector3 firstForce;
    Vector3 secondForce;

    const float MOVE_SPEED = 150;
    const float FORCE = 3;
    float randForce = 0;
    Rigidbody rigid;

    bool firstBump;
    bool afterBump;
    void Awake()
    {
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
        if (col.gameObject.tag == "Dice")
        {
            FindObjectOfType<AudioManager>().Play("Slow");
        }
        else if (col.gameObject.tag != "InvincibleCube")
        {
            FindObjectOfType<AudioManager>().Play("Bump");
        }
        BallDirection(col);
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
