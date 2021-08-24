using UnityEngine;
enum TargetDir
{
    Idle,
    Right,
    Left,
    Up,
    Down
}
public class DiceTarget : MonoBehaviour
{
    Vector3 right_rotation = new Vector3(0, 0, -90);
    Vector3 left_rotation = new Vector3(0, 0, 90);
    Vector3 up_rotation = new Vector3(90, 0, 0);
    Vector3 down_rotation = new Vector3(-90, 0, 0);

    Vector3 right_position = new Vector3(1, 0, 0);
    Vector3 left_position = new Vector3(-1, 0, 0);
    Vector3 up_position = new Vector3(0, 0, 1);
    Vector3 down_position = new Vector3(0, 0, -1);

    bool isRightObsCol;
    bool isLeftObsCol;
    bool isFrontObsCol;
    bool isBackObsCol;

    int targetDir = 5;
    int dirNum;

    const int TARGET_DIRECTION_LEFT = 1;
    const int TARGET_DIRECTION_RIGHT = 2;
    const int TARGET_DIRECTION_UP = 3;
    const int TARGET_DIRECTION_DOWN = 4;
    const int TARGET_DIRECTION_STOP = 5;

    void Update()
    {
        if (GameManager.Instance.IsJoystickEnable
            && !GameManager.Instance.IsUgrading
            && !GameManager.Instance.IsFalling)
        {
            if (targetDir == TARGET_DIRECTION_LEFT) // 왼쪽
            {
                if (isRightObsCol) { return; }
                TargetMove(TargetDir.Left);
            }
            else if (targetDir == TARGET_DIRECTION_RIGHT) // 오른쪽
            {
                if (isLeftObsCol) { return; }
                TargetMove(TargetDir.Right);
            }
            else if (targetDir == TARGET_DIRECTION_UP) // 위
            {
                if (isFrontObsCol) { return; }
                TargetMove(TargetDir.Up);
            }
            else if (targetDir == TARGET_DIRECTION_DOWN) // 아래
            {
                if (isBackObsCol) { return; }
                TargetMove(TargetDir.Down);
            }
            else if (targetDir == TARGET_DIRECTION_STOP)
            {
                return;
            }
        }
    }
    public void MobileInput(int dir)
    {
        targetDir = dir;
        dirNum += dir;
    }
    public void MobileInputOut(int dir)
    {
        dirNum -= dir;
        if (dirNum == 0) { targetDir = 5; }
    }
    void TargetMove(TargetDir targetDir)
    {
        GameManager.Instance.IsJoystickEnable = false;
        transform.rotation = Quaternion.identity;

        switch (targetDir)
        {
            case TargetDir.Right:
                isRightObsCol = false;
                isFrontObsCol = false;
                isBackObsCol = false;
                transform.position += right_position;
                transform.Rotate(right_rotation);
                GameManager.Instance.SomeKey = 0;
                break;
            case TargetDir.Left:
                isLeftObsCol = false;
                isFrontObsCol = false;
                isBackObsCol = false;
                transform.position += left_position;
                transform.Rotate(left_rotation);
                GameManager.Instance.SomeKey = 1;
                break;
            case TargetDir.Up:
                isRightObsCol = false;
                isLeftObsCol = false;
                isBackObsCol = false;
                transform.position += up_position;
                transform.Rotate(up_rotation);
                GameManager.Instance.SomeKey = 2;
                break;
            case TargetDir.Down:
                isRightObsCol = false;
                isLeftObsCol = false;
                isFrontObsCol = false;
                transform.position += down_position;
                transform.Rotate(down_rotation);
                GameManager.Instance.SomeKey = 3;
                break;
        }
        GameManager.Instance.IsMoving = true;
        GameManager.Instance.IsSphereMove = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightObstacle"))
        {
            isRightObsCol = true;
        }
        else if (other.CompareTag("LeftObstacle"))
        {
            isLeftObsCol = true;
        }
        else if (other.CompareTag("FrontObstacle"))
        {
            isFrontObsCol = true;
        }
        else if (other.CompareTag("BackObstacle"))
        {
            isBackObsCol = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightObstacle"))
        {
            isRightObsCol = false;
        }
        else if (other.CompareTag("LeftObstacle"))
        {
            isLeftObsCol = false;
        }
        else if (other.CompareTag("FrontObstacle"))
        {
            isFrontObsCol = false;
        }
        else if (other.CompareTag("BackObstacle"))
        {
            isBackObsCol = false;
        }
    }
}

