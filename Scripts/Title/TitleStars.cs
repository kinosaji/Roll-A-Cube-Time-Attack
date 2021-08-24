using UnityEngine;

public class TitleStars : MonoBehaviour
{
    Vector3 starsMovePos = new Vector3(0, 0, 0.1f);
    const float SPEED = 0.4f;
    void Update()
    {
        transform.position += starsMovePos * SPEED * Time.deltaTime;
    }
}
