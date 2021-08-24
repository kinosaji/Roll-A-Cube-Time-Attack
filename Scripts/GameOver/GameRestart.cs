using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    [SerializeField] GameObject restartButton;

    Animator animator;
    void Awake()
    {
        animator = restartButton.GetComponent<Animator>();
    }
    public void Restart()
    {
        animator.SetTrigger("RestartBlind");
        GameManager.Instance.GameOver = false;
        gameObject.SetActive(false);
        Invoke("DelayLoadScene", 1f);
    }
    void DelayLoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
