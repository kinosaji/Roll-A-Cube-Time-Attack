using UnityEngine;

public class BallBasket : MonoBehaviour
{
    [SerializeField] Transform dice_transform;
    [SerializeField] GameObject tempField;

    public void ReturnBall(int ball_index, bool isBallCol)
    {
        GameObject returnBall;

        try
        {
            returnBall = transform.GetChild(ball_index).gameObject;
        }
        catch (System.Exception)
        {
            return;
        }
        if (isBallCol)
        {
            GameObject sideOfBall = tempField.transform.GetChild(ball_index).gameObject;
            string sideOfBallTag = sideOfBall.tag;

            switch (sideOfBallTag)
            {
                case "Ball":
                    WhiteBall w_Ball = returnBall.GetComponent<WhiteBall>();
                    ObjectPoolManager.Instance.WhiteBallQueue.Enqueue(w_Ball);
                    break;
                case "GreenBall":
                    GreenBall g_ball = returnBall.GetComponent<GreenBall>();
                    ObjectPoolManager.Instance.GreenBallQueue.Enqueue(g_ball);
                    break;
                case "RedBall":
                    RedBall r_Ball = returnBall.GetComponent<RedBall>();
                    ObjectPoolManager.Instance.RedBallQueue.Enqueue(r_Ball);
                    break;
                case "BlueBall":
                    BlueBall b_Ball = returnBall.GetComponent<BlueBall>();
                    ObjectPoolManager.Instance.BlueBallQueue.Enqueue(b_Ball);
                    break;
            }
            ObjectPoolManager.Instance.ReturnSideOfBall(sideOfBall, true);
        }
        else
        {
            if (returnBall.name == "WhiteBall(Clone)")
            {
                WhiteBall white_ball = returnBall.GetComponent<WhiteBall>();
                ParticleManager.Instance.GetWhiteEffect(dice_transform);
                ObjectPoolManager.Instance.WhiteBallQueue.Enqueue(white_ball);
            }
            else if (returnBall.name == "GreenBall(Clone)")
            {
                GreenBall green_ball = returnBall.GetComponent<GreenBall>();
                ParticleManager.Instance.GetGreenEffect(dice_transform);
                ObjectPoolManager.Instance.GreenBallQueue.Enqueue(green_ball);
            }
            else if (returnBall.name == "BlueBall(Clone)")
            {
                BlueBall blue_ball = returnBall.GetComponent<BlueBall>();
                ParticleManager.Instance.GetBlueEffect(dice_transform);
                ObjectPoolManager.Instance.BlueBallQueue.Enqueue(blue_ball);
            }
            else if (returnBall.name == "RedBall(Clone)")
            {
                RedBall red_ball = returnBall.GetComponent<RedBall>();
                ParticleManager.Instance.GetRedEffect(dice_transform);
                ObjectPoolManager.Instance.RedBallQueue.Enqueue(red_ball);
            }
        }
        returnBall.gameObject.SetActive(false);
        returnBall.transform.SetParent(ObjectPoolManager.Instance.transform);
    }
}
