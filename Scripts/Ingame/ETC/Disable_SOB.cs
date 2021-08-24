using UnityEngine;

public class Disable_SOB : MonoBehaviour
{
    GameObject sideOfBall;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SideOfBall"))
        {
            sideOfBall = other.gameObject;
            sideOfBall.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void OnDisable()
    {
        if (sideOfBall != null) { sideOfBall.GetComponent<BoxCollider>().enabled = true; }
    }
}
