using UnityEngine;

public class BtnWall : MonoBehaviour
{
    GameObject parentBtn;

    int holdBall;
    bool runOnce1 = true;
    bool runOnce2 = true;
    void Awake()
    {
        parentBtn = transform.parent.gameObject;
    }
    void Update()
    {
        if (parentBtn.transform.position.y == 0)
        {
            if (holdBall == 0)
            {
                if (runOnce1)
                {
                    runOnce1 = false;
                    runOnce2 = true;
                    ChildWallEnable();
                }
            }
        }
        else if (parentBtn.transform.position.y == -2.5f)
        {
            if (runOnce2)
            {
                runOnce2 = false;
                runOnce1 = true;
                holdBall = 0;
                ChildWallDisable();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        holdBall++;
    }
        void OnTriggerExit(Collider other)
    {
        holdBall--;
    }
    void ChildWallEnable()
    {
        for (int col_index = 0; col_index < 4; col_index++)
        {
            transform.GetChild(col_index).gameObject.SetActive(true);
        }
    }
    void ChildWallDisable()
    {
        for (int col_index = 0; col_index < 4; col_index++)
        {
            transform.GetChild(col_index).gameObject.SetActive(false);
        }
    }
}
