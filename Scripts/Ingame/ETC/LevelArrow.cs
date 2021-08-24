using UnityEngine;
using UnityEngine.UI;

public class LevelArrow : MonoBehaviour
{
    RectTransform arrow_transform;
    Image gauge_image;

    Vector3 gaugeUpVec = new Vector3(0, 0, -10);

    bool setArrow_GameStart;
    bool setArrow_WaitingRoom;

    float currentPosZ = 359;
    float minusPosZ = 18;

    float currentTime;
    float nextLevelTime;

    int currentLevel = 0;

    void Awake()
    {
        gauge_image = transform.GetChild(1).GetComponent<Image>();
        arrow_transform = transform.GetChild(2).GetComponent<RectTransform>();
    }
    void Update()
    {
        if (currentLevel != 11)
        {
            if (!GameManager.Instance.WatingRoom) { LevelGaugeSet_WaitingRoom(); }
            if (GameManager.Instance.GameStart) { LevleGaugeSet_GameStart(); }
        }
    }
    void LevelGaugeSet_WaitingRoom()
    {
        if (gauge_image.color.a < 1)
        {
            gauge_image.color += new Color(0, 0, 0, 1) * Time.deltaTime;
            if (gauge_image.color.a > 1)
            {
                gauge_image.color = new Color(1, 1, 1, 1);
                setArrow_WaitingRoom = true;
            }
        }
        if (setArrow_WaitingRoom)
        {
            arrow_transform.eulerAngles += gaugeUpVec * Time.deltaTime;
            if (arrow_transform.eulerAngles.z > currentPosZ)
            {
                arrow_transform.eulerAngles = new Vector3(0, 0, currentPosZ);
                setArrow_WaitingRoom = false;
            }
        }
    }
    void LevleGaugeSet_GameStart()
    {
        currentTime += Time.deltaTime;

        if (currentTime > nextLevelTime)
        {
            nextLevelTime += 60;
            currentPosZ -= minusPosZ;
            currentLevel++;
            setArrow_GameStart = true;
        }

        if (setArrow_GameStart)
        {
            arrow_transform.eulerAngles += gaugeUpVec * Time.deltaTime;
            if (arrow_transform.eulerAngles.z < currentPosZ)
            {
                arrow_transform.eulerAngles = new Vector3(0, 0, currentPosZ);
                setArrow_GameStart = false;
            }
        }
    }
}
