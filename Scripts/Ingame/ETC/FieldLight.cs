using UnityEngine;

public class FieldLight : MonoBehaviour
{
    [SerializeField] Transform s_DiceTarget;

    Light filedLight;

    const int LIGHT_SPEED = 90;
    void Awake()
    {
        filedLight = GetComponent<Light>();
    }
    void Update()
    {
        if (GameManager.Instance.GameOver)
        {
            transform.LookAt(s_DiceTarget);

            if (filedLight.innerSpotAngle >= 20)
            {
                filedLight.spotAngle = filedLight.innerSpotAngle;
                filedLight.innerSpotAngle -= LIGHT_SPEED * Time.deltaTime;
            }
        }
    }
}
