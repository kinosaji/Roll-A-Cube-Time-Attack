using System.Collections;
using UnityEngine;

public class RedBall : MonoBehaviour
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

    Transform dice_transform;
    Laser laser;

    float laserCharging;

    float min_CoolTime = 10;
    float max_CoolTime = 15;
    float coolTime;

    void Awake()
    {
        dice_transform = GameObject.Find("Dice").transform;
        rigid = this.GetComponent<Rigidbody>();
        while (randForce <= 1) { randForce = Random.Range(-FORCE, FORCE); }
        firstForce = new Vector3(0, 0, -FORCE);
        secondForce = new Vector3(randForce, 0, FORCE);
    }
    void Update()
    {
        LaserLaunch();

        if (transform.childCount != 1)
        {
            rigid.velocity = Vector3.zero;
        }
        else
        {
            if (afterBump)
            {
                rigid.velocity = movePos * MOVE_SPEED * Time.deltaTime;
            }
        }
    }
    void OnEnable()
    {
        firstBump = true;
        afterBump = false;

        rigid.velocity = Vector3.zero;
        rigid.AddForce(firstForce, ForceMode.VelocityChange);

        laserCharging = 0;
        coolTime = Random.Range(min_CoolTime, max_CoolTime);

        if (laser != null) { laser.ReturnLaser(); }
        else { return; }
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
    void LaserLaunch()
    {
        laserCharging += Time.deltaTime;

        if (laserCharging > coolTime)
        {
            LaserSet();
            coolTime += coolTime;
        }
    }
    void LaserSet()
    {
        FindObjectOfType<AudioManager>().Play("LaserSet");
        transform.LookAt(dice_transform);
        ObjectPoolManager.Instance.GetLaser(this.transform);
        laser = transform.GetChild(1).GetComponent<Laser>();
    }
}
