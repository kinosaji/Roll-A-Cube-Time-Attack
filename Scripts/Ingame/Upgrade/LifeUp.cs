using UnityEngine;

public class LifeUp : MonoBehaviour
{
    ParticleSystem redUpSparks;

    void Awake()
    {
        redUpSparks = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            FindObjectOfType<AudioManager>().Play("LifeUp");

            redUpSparks.Play();
            UpgradeManager.Instance.LifeUp();
        }
    }
}
