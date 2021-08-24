using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameLoad : MonoBehaviour
{
    [SerializeField] Transform titleDice;
    [SerializeField] GameObject titleCamera;

    Vector3 dice_Anim_Done_Position = new Vector3(1, 0, -1);

    Animator titleCameraAni;

    bool runOnce = true;

    void Awake()
    {
        titleCameraAni = titleCamera.GetComponent<Animator>();
    }
    void Update()
    {
        To_The_IngameScene();
    }
    void To_The_IngameScene()
    {
        if (titleDice.position == dice_Anim_Done_Position)
        {
            if (runOnce)
            {
                runOnce = false;
                titleCameraAni.SetTrigger("TitleCameraMove");
            }
        }
    }
    void Anim_To_The_IngameScene()
    {
        SceneManager.LoadScene(1);
    }
}
