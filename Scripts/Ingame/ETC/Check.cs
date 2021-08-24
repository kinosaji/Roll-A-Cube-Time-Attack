using UnityEngine;

public class Check : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SideOfBall"))
        {
            FindObjectOfType<AudioManager>().Play("Pop");
            UpgradeManager.Instance.GaugeUp();
            GameObject sideOfBall = other.gameObject;
            ObjectPoolManager.Instance.ReturnSideOfBall(sideOfBall,false);
        }
    }
}
