using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject whiteBall;
    [SerializeField] GameObject greenBall;
    [SerializeField] GameObject blueBall;
    [SerializeField] GameObject redBall;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject bombBall;
    [SerializeField] GameObject fireFlash;
    [SerializeField] GameObject o_Debris;

    [SerializeField] GameObject whiteBallSpawner;
    [SerializeField] GameObject greenBallSpawner;
    [SerializeField] GameObject blueBallSpawner;
    [SerializeField] GameObject redBallSpawner;

    [SerializeField] Transform ballBasKet;
    [SerializeField] Transform field;

    [HideInInspector] public Material black_mat, white_mat;

    BallBasket ballBasket;
    

    Animator whiteBallSpawner_Ani;
    Animator greenBallSpawner_Ani;
    Animator blueBallSpawner_Ani;
    Animator redBallSpawner_Ani;

    public Queue<WhiteBall> WhiteBallQueue = new Queue<WhiteBall>();
    public Queue<GreenBall> GreenBallQueue = new Queue<GreenBall>();
    public Queue<BlueBall> BlueBallQueue = new Queue<BlueBall>();
    public Queue<RedBall> RedBallQueue = new Queue<RedBall>();
    public Queue<Obstacle> ObstacleQueue = new Queue<Obstacle>();
    public Queue<Laser> LaserQueue = new Queue<Laser>();
    public Queue<BombBall> BombBallQueue = new Queue<BombBall>();
    public Queue<FireFlash> FireFlashQueue = new Queue<FireFlash>();
    public Queue<O_Debris> O_DebrisQueue = new Queue<O_Debris>();

    Vector3 whiteBallSpawnPos = new Vector3(-6.2f, 0, 8);
    Vector3 greenBallSpawnPos = new Vector3(-2.3f, 0, 8);
    Vector3 blueBallSpawnPos = new Vector3(2.3f, 0, 8);
    Vector3 redBallSpawnPos = new Vector3(6.2f, 0, 8);
    Vector3 bombBallSpawnPos = new Vector3(0, 3, 8f);

    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ObjectPoolManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<ObjectPoolManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    } // SINGLETON
    void Awake()
    {
        var objs = FindObjectsOfType<ObjectPoolManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        whiteBallSpawner_Ani = whiteBallSpawner.GetComponent<Animator>();
        greenBallSpawner_Ani = greenBallSpawner.GetComponent<Animator>();
        blueBallSpawner_Ani = blueBallSpawner.GetComponent<Animator>();
        redBallSpawner_Ani = redBallSpawner.GetComponent<Animator>();
        ballBasket = ballBasKet.GetComponent<BallBasket>();

        black_mat = GetComponent<MeshRenderer>().materials[0];
        white_mat = GetComponent<MeshRenderer>().materials[1];

        Ball_Initialize(20);
    }
    void Ball_Initialize(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            WhiteBallQueue.Enqueue(CreateWhiteBall());
        }
        for (int i = 0; i < _count; i++)
        {
            RedBallQueue.Enqueue(CreateRedBall());
        }
        for (int i = 0; i < _count; i++)
        {
            BlueBallQueue.Enqueue(CreateBlueBall());
        }
        for (int i = 0; i < _count; i++)
        {
            ObstacleQueue.Enqueue(CreateObstacle());
        }
        for (int i = 0; i < _count; i++)
        {
            LaserQueue.Enqueue(CreateLaser());
        }
        for (int i = 0; i < _count; i++)
        {
            BombBallQueue.Enqueue(CreateBombBall());
        }
        for (int i = 0; i < _count; i++)
        {
            FireFlashQueue.Enqueue(CreateFireFlash());
        }
        for (int i = 0; i < _count; i++)
        {
            GreenBallQueue.Enqueue(CreateGreenBall());
        }
        for (int i = 0; i < _count; i++)
        {
            O_DebrisQueue.Enqueue(CreateO_Debris());
        }
    }
    WhiteBall CreateWhiteBall() // 비활성화 화이트볼 생성
    {
        WhiteBall newBall = Instantiate(whiteBall, transform).GetComponent<WhiteBall>();
        newBall.gameObject.SetActive(false);
        return newBall;
    }
    GreenBall CreateGreenBall() // 비활성화 그린볼 생성
    {
        GreenBall newBall = Instantiate(greenBall, transform).GetComponent<GreenBall>();
        newBall.gameObject.SetActive(false);
        return newBall;
    }
    BlueBall CreateBlueBall() // 비활성화 블루볼 생성
    {
        BlueBall newBall = Instantiate(blueBall, transform).GetComponent<BlueBall>();
        newBall.gameObject.SetActive(false);
        return newBall;
    }
    RedBall CreateRedBall() // 비활성화 레드볼 생성
    {
        RedBall newBall = Instantiate(redBall, transform).GetComponent<RedBall>();
        newBall.gameObject.SetActive(false);
        return newBall;
    }
    Obstacle CreateObstacle() // 비활성화 장애물 생성
    {
        Obstacle newObstacle = Instantiate(obstacle, transform).GetComponent<Obstacle>();
        newObstacle.gameObject.SetActive(false);
        return newObstacle;
    }
    Laser CreateLaser() // 비활성화 레이져 생성
    {
        Laser newLaser = Instantiate(laser, transform).GetComponent<Laser>();
        newLaser.gameObject.SetActive(false);
        return newLaser;
    }
    BombBall CreateBombBall() // 비활성화 폭탄 생성
    {
        BombBall newBall = Instantiate(bombBall, transform).GetComponent<BombBall>();
        newBall.gameObject.SetActive(false);
        return newBall;
    }
    FireFlash CreateFireFlash() // 비활성화 폭탄화염 생성
    {
        FireFlash newFire = Instantiate(fireFlash, transform).GetComponent<FireFlash>();
        newFire.gameObject.SetActive(false);
        return newFire;
    }
    O_Debris CreateO_Debris() // 비활성화 장해물파편 생성
    {
        O_Debris newDebris = Instantiate(o_Debris, transform).GetComponent<O_Debris>();
        return newDebris;
    }
    public WhiteBall GetWhiteBall() // 화이트볼 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("SetBall");
        whiteBallSpawner_Ani.SetTrigger("SpawnerOpen");

        WhiteBall ball = WhiteBallQueue.Dequeue();
        ball.transform.SetParent(ballBasKet);
        ball.transform.position = whiteBallSpawnPos;
        ball.gameObject.SetActive(true);
        return ball;
    }
    public GreenBall GetGreenBall() // 그린볼 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("SetBall");
        greenBallSpawner_Ani.SetTrigger("SpawnerOpen");

        GreenBall ball = GreenBallQueue.Dequeue();
        ball.transform.SetParent(ballBasKet);
        ball.transform.position = greenBallSpawnPos;
        ball.gameObject.SetActive(true);
        return ball;
    }
    public BlueBall GetBlueBall() // 블루볼 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("SetBall");
        blueBallSpawner_Ani.SetTrigger("SpawnerOpen");

        BlueBall ball = BlueBallQueue.Dequeue();
        ball.transform.SetParent(ballBasKet);
        ball.transform.position = blueBallSpawnPos;
        ball.gameObject.SetActive(true);
        return ball;
    }
    public RedBall GetRedBall() // 레드볼 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("SetBall");
        redBallSpawner_Ani.SetTrigger("SpawnerOpen");

        RedBall ball = RedBallQueue.Dequeue();
        ball.transform.SetParent(ballBasKet);
        ball.transform.position = redBallSpawnPos;
        ball.gameObject.SetActive(true);
        return ball;
    }
    public Obstacle GetObstacle(Transform sideOfBall) // 장애물 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("Grow");

        Obstacle obstacle = ObstacleQueue.Dequeue();
        obstacle.transform.position = sideOfBall.position + Vector3.up;
        obstacle.gameObject.SetActive(true);
        return obstacle;
    }
    public Laser GetLaser(Transform redBall) // 레이져 빌려주기
    {
        Laser laser = LaserQueue.Dequeue();
        laser.transform.SetParent(redBall);
        laser.transform.position = redBall.position;
        laser.gameObject.SetActive(true);
        return laser;
    }
    public BombBall ReloadBamb(Transform cannon) // 폭탄 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("CannonFired");
        BombBall bomb = BombBallQueue.Dequeue();
        bomb.transform.SetParent(cannon);
        bomb.transform.position = bombBallSpawnPos;
        bomb.gameObject.SetActive(true);
        return bomb;
    }
    public FireFlash GetFire(Transform bomb) // 폭탄화염 빌려주기
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        FireFlash fire = FireFlashQueue.Dequeue();
        fire.transform.position = bomb.position;
        fire.gameObject.SetActive(true);
        return fire;
    }
    public O_Debris GetDebris(Transform obstacle) // 장애물파편 빌려주기
    {
        O_Debris newDebris = O_DebrisQueue.Dequeue();
        newDebris.transform.position = obstacle.position;
        newDebris.gameObject.SetActive(true);
        return newDebris;
    }
    public void ReturnSideOfBall(GameObject _sideOfBall,bool isBallCol)
    {
        if (isBallCol)
        {
            // empty
        }
        else
        {
            int ball_index = _sideOfBall.transform.GetSiblingIndex();
            ballBasket.ReturnBall(ball_index, isBallCol);
        }
        int parseNum = int.Parse(_sideOfBall.name);

        if (parseNum % 2 == 0) // 짝수는 하얀색, 홀수는 검은색
        { _sideOfBall.GetComponent<MeshRenderer>().material = white_mat; }
        else // (parseNum % 2 != 0)
        { _sideOfBall.GetComponent<MeshRenderer>().material = black_mat; }

        _sideOfBall.GetComponent<BoxCollider>().enabled = false;
        _sideOfBall.transform.SetParent(field);
    }
}
