using UnityEngine;

public class GoldObj : MonoBehaviour
{
    [SerializeField] Transform goldParent;

    Vector3 goldTarget;

    bool isGoldDrop;

    SphereCollider sphere;

    const int GOLD_LIFE = 10;

    float goldTimePasses;

    ParticleSystem sparks;
    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        sparks = GetComponentInChildren<ParticleSystem>();
    }
    void OnEnable()
    {
        goldTimePasses = 0;
        sphere.enabled = true;
        isGoldDrop = false;
    }
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
        goldTimePasses += Time.deltaTime;
        if (isGoldDrop) {transform.position = Vector3.Lerp(transform.position, goldTarget, Time.deltaTime);}
        if (goldTimePasses > GOLD_LIFE) { GetGold(); }
    }
    void OnCollisionEnter(Collision collision)
    {
        DropGold();
    }
    void DropGold()
    {
        FindObjectOfType<AudioManager>().Play("Coin");
        sphere.enabled = false;
        GameManager.Instance.CurrentGold += 1;
        float randX = Random.Range(-5, 5);
        goldTarget = new Vector3(randX, 0, -15);
        goldTimePasses -= 3;
        sparks.Play();
        isGoldDrop = true;
    }
    void GetGold()
    {
        sparks.Stop();
        gameObject.transform.SetParent(goldParent);
        gameObject.transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }
}
