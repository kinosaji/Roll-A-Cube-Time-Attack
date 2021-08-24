using UnityEngine;

public class DiceDead : MonoBehaviour
{
    [SerializeField] GameObject debris_go;
    
    const int EXPLOSION_FORCE = 10;

    void Awake()
    {
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        Explosion();
    }
    void Explosion()
    {
        GameObject debris_clone = Instantiate(debris_go, transform.position, Quaternion.identity);
        Rigidbody[] debris_rigid = debris_clone.GetComponentsInChildren<Rigidbody>();

        for (int debris_count = 0; debris_count < debris_rigid.Length; debris_count++)
        {
            debris_rigid[debris_count].AddExplosionForce(EXPLOSION_FORCE, transform.position, 10f);
        }
    }

}
