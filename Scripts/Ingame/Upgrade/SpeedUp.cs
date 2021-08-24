using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    ParticleSystem blueUpSparks;

    void Awake()
    {
        blueUpSparks = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            FindObjectOfType<AudioManager>().Play("SpeedUp");

            blueUpSparks.Play();
            UpgradeManager.Instance.SpeedUp();
        }
    }
}