using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Material on_mat;
    [SerializeField] Material off_mat;

    const float GROW_SPEED = 11;

    int holdBall;
    bool setWall;
    bool isGrow;
    bool isShrink;

    MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void OnEnable()
    {
        holdBall = 0;
        isGrow = true;
        meshRenderer.material = on_mat;
    }
    void Update()
    {
        if (isGrow)
        {
            transform.localScale += new Vector3(0.1f, 0, 0.1f) * GROW_SPEED * Time.deltaTime;

            if (transform.localScale.x >= 1)
            {
                isGrow = false;
                setWall = true;
                ChildObsEnable();
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (setWall)
        {
            if (holdBall == 0)
            {
                setWall = false;
                ChildWallEnable();
            }
        }
        if (isShrink)
        {
            transform.position += new Vector3(0, 0.05f, 0) * GROW_SPEED * Time.deltaTime;

            if (transform.position.y >= 0.13f)
            {
                isShrink = false;
                ReturnObstacle();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser") || other.CompareTag("DiceCore") || other.CompareTag("InvincibleCube"))
        {
            isShrink = true;
        }
        else if (other.CompareTag("Ball") || other.CompareTag("GreenBall") || other.CompareTag("BlueBall"))
        {
            holdBall++;
        }
        else if (other.CompareTag("BlueCheck"))
        {
            FindObjectOfType<AudioManager>().Play("Shrink");
            meshRenderer.material = off_mat;
            isShrink = true;
        }
        else if (other.CompareTag("Falling"))
        {
            isShrink = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        holdBall--;
    }
    void ChildObsEnable()
    {
        for (int col_index = 0; col_index < 4; col_index++)
        {
            transform.GetChild(col_index).gameObject.SetActive(true);
        }
    }
    void ChildWallEnable()
    {
        for (int col_index = 4; col_index < 8; col_index++)
        {
            transform.GetChild(col_index).gameObject.SetActive(true);
        }
    }
    void ChildDisable()
    {
        for (int col_index = 0; col_index < 8; col_index++)
        {
            transform.GetChild(col_index).gameObject.SetActive(false);
        }
    }
    void ReturnObstacle()
    {
        FindObjectOfType<AudioManager>().Play("DiceDead");

        transform.localScale = new Vector3(0, 1, 0);
        ChildDisable();
        ObjectPoolManager.Instance.GetDebris(this.transform);
        ObjectPoolManager.Instance.ObstacleQueue.Enqueue(this);
        gameObject.SetActive(false);
    }
}
