using UnityEngine;

public class Stars : MonoBehaviour
{
    Vector3 targetPos1 = new Vector3(10, -3, 6);
    Vector3 targetPos2 = new Vector3(-10, -3, 6);
    Vector3 targetPos3 = new Vector3(10, -3, -6);
    Vector3 targetPos4 = new Vector3(-10, -3, -6);

    const float SPEED = 0.15f;

    Vector3 targetPostion;
    void Start()
    {
        targetPostion = StarsTarget();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPostion, SPEED * Time.deltaTime);
        if (transform.position == targetPostion) { targetPostion = StarsTarget(); }
    }
    Vector3 StarsTarget()
    {
        Vector3 _targetPos = Vector3.zero;

        int randNum = Random.Range(1, 5);

        switch (randNum)
        {
            case 1:
                _targetPos = targetPos1;
                break;
            case 2:
                _targetPos = targetPos2;
                break;
            case 3:
                _targetPos = targetPos3;
                break;
            case 4:
                _targetPos = targetPos4;
                break;
        }

        return _targetPos;
    }
}
