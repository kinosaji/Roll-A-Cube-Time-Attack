using System.Collections;
using UnityEngine;

public class O_Debris : MonoBehaviour
{
    const int EXPLOSION_FORCE = 10;

    Rigidbody[] debris_rigid;
    Vector3[] vector3s;
    void Awake()
    {
        debris_rigid = new Rigidbody[transform.childCount];
        vector3s = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            debris_rigid = GetComponentsInChildren<Rigidbody>();
            vector3s[i] = transform.GetChild(i).transform.position;
        }
    }
    void OnEnable()
    {
        for (int i = 0; i < debris_rigid.Length; i++)
        {
            debris_rigid[i].AddExplosionForce(EXPLOSION_FORCE, transform.position, 10f);
        }
        StartCoroutine(IDestroy());
    }
    IEnumerator IDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        transform.SetParent(ObjectPoolManager.Instance.transform);
        ObjectPoolManager.Instance.O_DebrisQueue.Enqueue(this);
        gameObject.SetActive(false);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = vector3s[i];
        }
    }
}