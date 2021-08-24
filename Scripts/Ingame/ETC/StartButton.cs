using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClip;
    [SerializeField] GameObject playerInfoText;

    PlayerInfoText infoText;
    AudioSource audioSource;
    Material startMat;
    Animator animator;
    ParticleSystem greenUpSparks;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        infoText = playerInfoText.GetComponent<PlayerInfoText>();
        greenUpSparks = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        startMat = transform.GetChild(0).GetComponent<Renderer>().material;
        audioSource.clip = audioClip[0];
    }

    void Start()
    {
        animator.SetTrigger("Grow");
        startMat.EnableKeyword("_EMISSION");
        GetComponent<SphereCollider>().enabled = true;
        greenUpSparks.Stop();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartKey"))
        {
            greenUpSparks.Play();
            GameStart();
        }
    }
    void GameStart()
    {
        FindObjectOfType<AudioManager>().Play("StartButtonSound");

        GameManager.Instance.WatingRoom = false;
        startMat.DisableKeyword("_EMISSION");
        GetComponent<SphereCollider>().enabled = false;
        infoText.TextEffectCorutine();
    }
    void Anim_GameStart() // PlayerInfo¿¡¼­ Trigger
    {
        audioSource.Stop();
        audioSource.clip = audioClip[1];
        audioSource.volume = 0.7f;
        playerInfoText.SetActive(false);
        GameManager.Instance.GameStart = true;
        audioSource.Play();
    }
}
