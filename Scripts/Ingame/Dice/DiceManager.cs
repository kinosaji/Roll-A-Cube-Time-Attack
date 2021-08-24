using UnityEngine;

enum SphereDir
{
    UpSide,
    BottomSide,
    RightSide,
    LeftSide,
    FrontSide,
    BackSide
}
enum DM_Target
{
    DM_Back,
    DM_Front,
    DM_Left,
    DM_Right,
    DM_Bottom,
    DM_Up
}
public class DiceManager : MonoBehaviour
{
    [SerializeField] Transform[] diceManager_transform;
    [SerializeField] GameObject[] diceSet_go;

    SphereDir sphereDir;

    int dice_set_index;
    int diceManagerPos_index;
    void Update()
    {
        if (GameManager.Instance.IsSphereMove) { SphereMove(); }

        if (GameManager.Instance.DiceTopSideSet) { DiceSet(); }
    }
    void SphereMove()
    {
        FindObjectOfType<AudioManager>().Play("PlayerMove");

        Vector3 spherePos = SpherePos(sphereDir, GameManager.Instance.SomeKey);
        transform.position = spherePos;
        GameManager.Instance.IsSphereMove = false;
    }
    void DiceSet()
    {
        for (int index = 0; index < diceSet_go.Length; index++)
        {
            if (index != dice_set_index)
            {
                diceSet_go[index].SetActive(false);
            }
            diceSet_go[dice_set_index].SetActive(true);
        }
        GameManager.Instance.DiceTopSideSet = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BackSide"))
        {
            sphereDir = SphereDir.BackSide;
            dice_set_index = (int)DM_Target.DM_Back;
        }
        else if (other.CompareTag("FrontSide"))
        {
            sphereDir = SphereDir.FrontSide;
            dice_set_index = (int)DM_Target.DM_Front;
        }
        else if (other.CompareTag("LeftSide"))
        {
            sphereDir = SphereDir.LeftSide;
            dice_set_index = (int)DM_Target.DM_Left;
        }
        else if (other.CompareTag("RightSide"))
        {
            sphereDir = SphereDir.RightSide;
            dice_set_index = (int)DM_Target.DM_Right;
        }
        else if (other.CompareTag("BottomSide"))
        {
            sphereDir = SphereDir.BottomSide;
            dice_set_index = (int)DM_Target.DM_Bottom;
        }
        else if (other.CompareTag("UpSide"))
        {
            sphereDir = SphereDir.UpSide;
            dice_set_index = (int)DM_Target.DM_Up;
        }
    }
    Vector3 SpherePos(SphereDir sphereDir, int key)
    {
        const int rightArrow = 0;
        const int leftArrow = 1;
        const int upArrow = 2;
        const int downArrow = 3;

        switch (key)
        {
            case rightArrow: // 오른쪽키를 눌렸는데
                switch (sphereDir)
                {
                    case SphereDir.RightSide: // Sphere가 오른쪽에있다면
                        diceManagerPos_index = (int)DM_Target.DM_Bottom; // Sphere 아래로 이동
                        break;
                    case SphereDir.LeftSide:
                        diceManagerPos_index = (int)DM_Target.DM_Up;
                        break;
                    case SphereDir.UpSide:
                        diceManagerPos_index = (int)DM_Target.DM_Right;
                        break;
                    case SphereDir.BottomSide:
                        diceManagerPos_index = (int)DM_Target.DM_Left;
                        break;
                    case SphereDir.FrontSide:
                        break;
                    case SphereDir.BackSide:
                        break;
                }
                break;
            case leftArrow:
                switch (sphereDir)
                {
                    case SphereDir.RightSide:
                        diceManagerPos_index = (int)DM_Target.DM_Up;
                        break;
                    case SphereDir.LeftSide:
                        diceManagerPos_index = (int)DM_Target.DM_Bottom;
                        break;
                    case SphereDir.UpSide:
                        diceManagerPos_index = (int)DM_Target.DM_Left;
                        break;
                    case SphereDir.BottomSide:
                        diceManagerPos_index = (int)DM_Target.DM_Right;
                        break;
                    case SphereDir.FrontSide:
                        break;
                    case SphereDir.BackSide:
                        break;
                }
                break;
            case upArrow:
                switch (sphereDir)
                {
                    case SphereDir.RightSide:
                        break;
                    case SphereDir.LeftSide:
                        break;
                    case SphereDir.UpSide:
                        diceManagerPos_index = (int)DM_Target.DM_Back;
                        break;
                    case SphereDir.BottomSide:
                        diceManagerPos_index = (int)DM_Target.DM_Front;
                        break;
                    case SphereDir.FrontSide:
                        diceManagerPos_index = (int)DM_Target.DM_Up;
                        break;
                    case SphereDir.BackSide:
                        diceManagerPos_index = (int)DM_Target.DM_Bottom;
                        break;
                }
                break;
            case downArrow:
                switch (sphereDir)
                {
                    case SphereDir.RightSide:
                        break;
                    case SphereDir.LeftSide:
                        break;
                    case SphereDir.UpSide:
                        diceManagerPos_index = (int)DM_Target.DM_Front;
                        break;
                    case SphereDir.BottomSide:
                        diceManagerPos_index = (int)DM_Target.DM_Back;
                        break;
                    case SphereDir.FrontSide:
                        diceManagerPos_index = (int)DM_Target.DM_Bottom;
                        break;
                    case SphereDir.BackSide:
                        diceManagerPos_index = (int)DM_Target.DM_Up;
                        break;
                }
                break;
        }
        return diceManager_transform[diceManagerPos_index].position;
    }
}
