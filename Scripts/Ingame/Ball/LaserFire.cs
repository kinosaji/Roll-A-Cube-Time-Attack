using System.Collections;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    [SerializeField] GameObject hitEffect_go;

    [HideInInspector] public Vector3 DicePos;

    Laser laser;
    LineRenderer laserLine;
    GameObject redBall;
    void Awake()
    {
        laser = GetComponentInParent<Laser>();
        laserLine = GetComponent<LineRenderer>();
    }

    void OnEnable()
    {
        laserLine.SetPosition(0, Vector3.zero);
        laserLine.SetPosition(1, Vector3.zero);
        transform.LookAt(DicePos);
        redBall = transform.parent.parent.gameObject;
        StartCoroutine(IGetLaserFire(1));
    }

    void Update()
    {
        if (redBall == null) { GetLaserFire(); }

        LaserSetPosition();
    }
    void LaserSetPosition()
    {
        laserLine.SetPosition(0, transform.position);

        int layerMask = 
              (1 << LayerMask.NameToLayer("InvincibleDice"))
            + (1 << LayerMask.NameToLayer("Dice"))
            + (1 << LayerMask.NameToLayer("LaserWall"))
            + (1 << LayerMask.NameToLayer("Obstacle"));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30, layerMask))
        {
            laserLine.SetPosition(1, hit.point);
            hitEffect_go.transform.position = hit.point;
        }
    }
    IEnumerator IGetLaserFire(float laserLife)
    {
        yield return new WaitForSeconds(laserLife);
        GetLaserFire();
    }
    void GetLaserFire()
    {
        gameObject.SetActive(false);
        laser.ReturnLaser();
    }
}
