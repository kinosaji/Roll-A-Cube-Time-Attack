using UnityEngine;

public class BluePowerUp : MonoBehaviour
{
    [SerializeField] Material skin_DefaultBlue;
    [SerializeField] Transform s_DiceManagerTransform;
    [SerializeField] Transform s_RightTarget;

    [SerializeField] GameObject[] diceSidePower1;
    [SerializeField] GameObject[] diceSidePower2;
    [SerializeField] GameObject[] blueCheck1;
    [SerializeField] GameObject[] blueCheck2;

    ParticleSystem BlueUpSparks;

    void Awake()
    {
        BlueUpSparks = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            FindObjectOfType<AudioManager>().Play("BluePowerUp");

            BlueUpSparks.Play();
            BluePowerUpTrigger(other.gameObject);
        }
    }
    void BluePowerUpTrigger(GameObject other)
    {
        if (UpgradeManager.Instance.PlayerBluePower == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                diceSidePower1[i].GetComponent<Renderer>().material = skin_DefaultBlue;
                GameObject diceSide = other.gameObject.transform.GetChild(i).gameObject;
                diceSide.SetActive(false);
            }
            s_DiceManagerTransform.position = s_RightTarget.position;
            other.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            for (int i = 0; i < blueCheck1.Length; i++)
            {
                blueCheck1[i].SetActive(true);
            }
        }
        else if (UpgradeManager.Instance.PlayerBluePower == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                diceSidePower2[i].GetComponent<Renderer>().material = skin_DefaultBlue;
                GameObject diceSide = other.gameObject.transform.GetChild(i).gameObject;
                diceSide.SetActive(false);
            }
            s_DiceManagerTransform.position = s_RightTarget.position;
            other.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            for (int i = 0; i < blueCheck2.Length; i++)
            {
                blueCheck2[i].SetActive(true);
            }
        }
        UpgradeManager.Instance.BluePowerUp();
    }
}
