using UnityEngine;

public class FireFlash : MonoBehaviour
{
    const float FIRE_FIGHTING_SPEED = 15;

    Vector3 originScale = new Vector3(3, 3, 3);
    Vector3 originPosition = new Vector3(0, 20, 0);

    BoxCollider boxCollider;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    void OnEnable()
    {
        boxCollider.enabled = true;
    }
    void Update()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * FIRE_FIGHTING_SPEED * Time.deltaTime;
        if (transform.localScale.x < 0)
        {
            ReturnFire();
        }
        if (transform.localScale.x < 2)
        {
            boxCollider.enabled = false;
        }
    }
    void ReturnFire()
    {
        FireFlash oldFire = this.gameObject.GetComponent<FireFlash>();
        ObjectPoolManager.Instance.FireFlashQueue.Enqueue(oldFire);
        gameObject.SetActive(false);
        gameObject.transform.position = originPosition;
        transform.localScale = originScale;
    }
}