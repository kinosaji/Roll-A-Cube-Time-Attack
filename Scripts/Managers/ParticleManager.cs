using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] GameObject whiteEffect;
    [SerializeField] GameObject greenEffect;
    [SerializeField] GameObject blueEffect;
    [SerializeField] GameObject redEffect;

    Queue<ParticleSystem> whiteEffectQueue = new Queue<ParticleSystem>();
    Queue<ParticleSystem> greenEffectQueue = new Queue<ParticleSystem>();
    Queue<ParticleSystem> blueEffectQueue = new Queue<ParticleSystem>();
    Queue<ParticleSystem> redEffectQueue = new Queue<ParticleSystem>();

    private static ParticleManager instance;
    public static ParticleManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ParticleManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<ParticleManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    } // SINGLETON

    void Awake()
    {
        var objs = FindObjectsOfType<ParticleManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        Effect_Initialize(5);
    }

    ParticleSystem CreateWhiteEffect() // 비활성화 화이트이펙트 생성
    {
        ParticleSystem newEffect = Instantiate(whiteEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateGreenEffect() // 비활성화 그린이펙트 생성
    {
        ParticleSystem newEffect = Instantiate(greenEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateBlueEffect() // 비활성화 블루이펙트 생성
    {
        ParticleSystem newEffect = Instantiate(blueEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateRedEffect() // 비활성화 레드이펙트 생성
    {
        ParticleSystem newEffect = Instantiate(redEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }

    void Effect_Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            whiteEffectQueue.Enqueue(CreateWhiteEffect());
            greenEffectQueue.Enqueue(CreateGreenEffect());
            blueEffectQueue.Enqueue(CreateBlueEffect());
            redEffectQueue.Enqueue(CreateRedEffect());
        }
    }
    public ParticleSystem GetWhiteEffect(Transform _transform) // 화이트이펙트 빌려주기
    {
        ParticleSystem effect = whiteEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        whiteEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetGreenEffect(Transform _transform) // 화이트이펙트 빌려주기
    {
        ParticleSystem effect = greenEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        greenEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetBlueEffect(Transform _transform) // 블루이펙트 빌려주기
    {
        ParticleSystem effect = blueEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        blueEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetRedEffect(Transform _transform) // 레드이펙트 빌려주기
    {
        ParticleSystem effect = redEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        redEffectQueue.Enqueue(effect);
        return effect;
    }
}
