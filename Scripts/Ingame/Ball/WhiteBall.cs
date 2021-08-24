using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    Vector3 startPos;
    Vector3 movePos;
    Vector3 firstForce;
    Vector3 secondForce;

    const float MOVE_SPEED = 200;
    const float FORCE = 4;
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
        if (col.gameObject.tag != "InvincibleCube")
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
// 벽에 닿았을때의 WhiteBall 움직임 version 1
/*FindObjectOfType<AudioManager>().Play("Bump");

randomForce = Random.Range(MIN_FORCE, MAX_FORCE);

if (currentColWall == "RightWall") { randomForce = -randomForce; }
else if (currentColWall == "BackWall") { randomForce = -randomForce; }

if (colWall != currentColWall)
{
    switch (colWall)
    {
        case "RightWall":
            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(-MAX_FORCE, 0, randomForce));
            break;
        case "LeftWall":
            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(MAX_FORCE, 0, randomForce));
            break;
        case "FrontWall":
            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(randomForce, 0, MAX_FORCE));
            break;
        case "BackWall":
            rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(randomForce, 0, -MAX_FORCE));
            break;
    }
}
return currentColWall = colWall;*/