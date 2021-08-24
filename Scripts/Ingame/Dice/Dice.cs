using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] GameObject diceDead_go;
    [SerializeField] GameObject gameOver_go;
    [SerializeField] GameObject invCheck_go;

    [SerializeField] Transform target;

    const float SLOW_MOVE_SPEED = 1.5f;
    const int SLOW_ROTATE_SPEED = 150;

    UpgradeManager upgradeManager;
    BallBasket ballBasket;
    Rigidbody rigid;

    bool isInvincible;

    void Awake()
    {
        GameManager.Instance.IsFalling = false;
        rigid = GetComponent<Rigidbody>();
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        ballBasket = GameObject.Find("BallBasket").GetComponent<BallBasket>();
    }
    void Update()
    {
        if (GameManager.Instance.IsMoving) { DiceMove(); }
        else { transform.rotation = Quaternion.identity; }
    }
    void OnCollisionEnter(Collision collision)
    {
        WhenIHitBall(collision);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Falling"))
        {
            GameManager.Instance.IsFalling = true;
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            StartCoroutine(Falling());
        }
    }
    void WhenIHitBall(Collision _col)
    {
        if (_col.gameObject.tag == "Ball"
            || _col.gameObject.tag == "Laser"
            || _col.gameObject.tag == "BlueBall")
        {
            if (!isInvincible)
            {
                if (UpgradeManager.Instance.PlayerLife > 0)
                {
                    if (GameManager.Instance.IsVibe == true)
                    {
#if UNITY_ANDROID
                        Handheld.Vibrate();
#endif
                    } // 진동
                    StartCoroutine(InvincibleMode());
                }
                else
                {
                    if (GameManager.Instance.IsVibe == true)
                    {
#if UNITY_ANDROID
                        Handheld.Vibrate();
#endif
                    } // 진동
                    FindObjectOfType<AudioManager>().Play("DiceDead");

                    Instantiate(diceDead_go, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                    gameOver_go.SetActive(true);
                    GameManager.Instance.GameStart = false;
                    GameManager.Instance.GameOver = true;
                }
            }
        }
        else if (_col.gameObject.tag == "GreenBall")
        {
            int ball_index = _col.transform.GetSiblingIndex();
            if (!isInvincible) { upgradeManager.SetCoverColor(ColorType.slow); }
            ballBasket.ReturnBall(ball_index, true);
        }
    }
    void DiceMove() // DiceTarget 위치를 구르면서 따라간다
    {
        if (GameManager.Instance.IsSlowMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, SLOW_MOVE_SPEED * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, SLOW_ROTATE_SPEED * Time.deltaTime);
        }
        else // nomal speed
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, UpgradeManager.Instance.DiceMove_Speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, UpgradeManager.Instance.DiceRotate_Speed * Time.deltaTime);
        }

        if (transform.position == target.position) { GameManager.Instance.IsMoveEnd = true; }
    }
    IEnumerator InvincibleMode()
    {
        FindObjectOfType<AudioManager>().Play("InvincibleTime");
        isInvincible = true;
        invCheck_go.SetActive(true);
        UpgradeManager.Instance.LifeDown();
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().Stop("InvincibleTime");
        FindObjectOfType<AudioManager>().Play("InvincibleEnd");
        isInvincible = false;
        invCheck_go.SetActive(false);
    }
    IEnumerator Falling()
    {
        yield return new WaitForSeconds(1f);
        gameOver_go.SetActive(true);
        GameManager.Instance.GameStart = false;
        GameManager.Instance.GameOver = true;
    }
}