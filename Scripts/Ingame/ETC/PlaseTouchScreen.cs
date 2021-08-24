using UnityEngine;

public class PlaseTouchScreen : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.IsJoystickEnable = false;
        GameManager.Instance.WatingRoom = true;
    }
    public void TouchToGameStart() // On Click()
    {
        gameObject.SetActive(false);
        GameManager.Instance.IsJoystickEnable = true;
    }
}