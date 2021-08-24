using UnityEngine;

public class ScoreBlind : MonoBehaviour
{
    [SerializeField] GameObject recordManager_go;
    void Anim_EnableRecordManager()
    {
        recordManager_go.SetActive(true);
        gameObject.SetActive(false);
    }
}
