using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    Animator _animator;
    AdmobManager admobManager;

    void Awake()
    {
        admobManager = GameObject.Find("AdmobManager").GetComponent<AdmobManager>();
        _animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        StartCoroutine(IGameOver());
    }
    IEnumerator IGameOver()
    {
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("GameOver");
        yield return new WaitForSeconds(2f);
        if (AudioListener.volume != 0) { AudioListener.volume = 0; }
        if (GoogleManager.Instance.playerData.HasAds) { admobManager.ShowInterstitial(); }
        SceneManager.LoadScene(2);
    }
}
