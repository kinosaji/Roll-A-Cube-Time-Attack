using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] GameObject blackFade_out;

    Animator blackFadeOutAni;
    void Awake()
    {
        blackFadeOutAni = blackFade_out.GetComponent<Animator>();
    }
    void Anim_blackFadeOutSet()
    {
        blackFadeOutAni.SetTrigger("Ingame_Load");
    }
}
