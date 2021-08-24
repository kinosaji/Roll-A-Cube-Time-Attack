using UnityEngine;

public class InvCheck : MonoBehaviour
{
    [SerializeField] Transform s_Dice;

    BallBasket ballBasket;
    void Awake()
    {
        ballBasket = GameObject.Find("BallBasket").GetComponent<BallBasket>();
    }
    void Update()
    {
        transform.position = s_Dice.position;
    }
    void OnCollisionEnter(Collision _col)
    {
        if (_col.gameObject.tag == "Ball"
            || _col.gameObject.tag == "GreenBall"
            || _col.gameObject.tag == "RedBall"
            || _col.gameObject.tag == "BlueBall")
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            int ball_index = _col.transform.GetSiblingIndex();
            ballBasket.ReturnBall(ball_index, true);
        }
    }
}
