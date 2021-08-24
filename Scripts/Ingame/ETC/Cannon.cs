using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform bombTargetBasket;
    [SerializeField] GameObject bombTarget;

    const float AIM_SEARCHING_SPEED = 5;

    Queue<GameObject> TargetQueue = new Queue<GameObject>();

    BombBall bombBall;

    GameObject fireFlash;

    public bool IsTrigger;
    bool isSmoke;
    void Awake()
    {
        fireFlash = transform.GetChild(0).gameObject;
        Target_Initialize(5);
    }
    void Update()
    {
        if (bombTargetBasket.childCount != 0)
        {
            Fired();
        }

        if (isSmoke)
        {
            fireFlash.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
        else
        {
            if (fireFlash.transform.localScale.x > 0)
            {
                fireFlash.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
        }

    }
    void Target_Initialize(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            TargetQueue.Enqueue(CreateTarget());
        }
    }
    GameObject CreateTarget()
    {
        GameObject newTarget = Instantiate(bombTarget);
        newTarget.transform.SetParent(null);
        newTarget.gameObject.SetActive(false);
        return newTarget;
    }
    public GameObject SetTarget()
    {
        FindObjectOfType<AudioManager>().Play("Reloading");
        GameObject newTarget = TargetQueue.Dequeue();
        newTarget.transform.position = TargetPosition();
        newTarget.transform.SetParent(bombTargetBasket);
        newTarget.gameObject.SetActive(true);
        return newTarget;
    }
    public void GetTarget()
    {
        isSmoke = false;
        GameObject oldTarget = bombTargetBasket.GetChild(0).gameObject;
        TargetQueue.Enqueue(oldTarget);
        oldTarget.transform.SetParent(null);
        oldTarget.gameObject.SetActive(false);
    }
    void Fired()
    {
        Vector3 dir = bombTargetBasket.GetChild(0).transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, AIM_SEARCHING_SPEED * Time.deltaTime);
        if (IsTrigger)
        {
            IsTrigger = false;
            isSmoke = true;
            bombBall = ObjectPoolManager.Instance.ReloadBamb(this.transform);
        }
    }
    Vector3 TargetPosition()
    {
        Transform dice_transform;
        try
        {
            dice_transform = GameObject.Find("Dice").transform;
        }
        catch (System.Exception)
        {
            return Vector3.zero;
        }
        float posX = Random.Range(-2, 2);
        float posZ = Random.Range(-2, 2);

        Vector3 targetPos = new Vector3(posX, 0, posZ);
        Vector3 closePlayerPos = dice_transform.position + targetPos;

        if (closePlayerPos.x >= 5 || closePlayerPos.x <= -5 ||
            closePlayerPos.z >= 5 || closePlayerPos.z <= -5)
        {
            posX = Random.Range(-4, 4);
            posZ = Random.Range(-4, 4);
            targetPos = new Vector3(posX, 0, posZ);
        }
        else
        {
            targetPos = closePlayerPos;
        }
        targetPos.x = Mathf.Round(targetPos.x);
        targetPos.z = Mathf.Round(targetPos.z);
        return targetPos;
    }
}
