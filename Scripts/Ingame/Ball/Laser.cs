using UnityEngine;

public class Laser : MonoBehaviour
{
    LaserFire laserFire;
    LineRenderer laserLine;
    Transform dice_transform;
    Material mat;

    Color originColor = new Color(1, 1, 0, 1);
    Color Transparency = new Color(0,0,0,0.5f);

    Vector3 TargetPos;
    void Awake()
    {
        dice_transform = GameObject.Find("Dice").transform;
        laserLine = GetComponent<LineRenderer>();
        laserFire = transform.GetChild(0).GetComponent<LaserFire>();
        mat = laserLine.material;
    }
    void OnEnable()
    {
        TargetPos = dice_transform.position;
        mat.color = originColor;
        transform.LookAt(dice_transform);
        LaserSetPosition();
    }
    void Update()
    {
        if (mat.color.a > 0)
        {
            mat.color -= Transparency * Time.deltaTime; 
        }
        else
        {
            mat.color = new Color(0, 0, 0, 0);
            LaserFireSet();
        }
    }
    void LaserSetPosition()
    {
        laserLine.SetPosition(0, transform.position);
        int layerMask = (1 << LayerMask.NameToLayer("LaserWall"));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward , out hit, 30, layerMask))
        {
            laserLine.SetPosition(1, hit.point);
        }
    }
    void LaserFireSet()
    {
        laserFire.DicePos = TargetPos;
        laserFire.gameObject.SetActive(true);
    }
    public void ReturnLaser()
    {
        gameObject.SetActive(false);
        laserFire.gameObject.SetActive(false);
        transform.SetParent(ObjectPoolManager.Instance.transform);
        ObjectPoolManager.Instance.LaserQueue.Enqueue(this);
    }
}
