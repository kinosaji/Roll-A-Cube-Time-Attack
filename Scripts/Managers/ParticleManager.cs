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

    ParticleSystem CreateWhiteEffect() // ��Ȱ��ȭ ȭ��Ʈ����Ʈ ����
    {
        ParticleSystem newEffect = Instantiate(whiteEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateGreenEffect() // ��Ȱ��ȭ �׸�����Ʈ ����
    {
        ParticleSystem newEffect = Instantiate(greenEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateBlueEffect() // ��Ȱ��ȭ �������Ʈ ����
    {
        ParticleSystem newEffect = Instantiate(blueEffect, transform).GetComponent<ParticleSystem>();
        newEffect.gameObject.SetActive(false);
        return newEffect;
    }
    ParticleSystem CreateRedEffect() // ��Ȱ��ȭ ��������Ʈ ����
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
    public ParticleSystem GetWhiteEffect(Transform _transform) // ȭ��Ʈ����Ʈ �����ֱ�
    {
        ParticleSystem effect = whiteEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        whiteEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetGreenEffect(Transform _transform) // ȭ��Ʈ����Ʈ �����ֱ�
    {
        ParticleSystem effect = greenEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        greenEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetBlueEffect(Transform _transform) // �������Ʈ �����ֱ�
    {
        ParticleSystem effect = blueEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        blueEffectQueue.Enqueue(effect);
        return effect;
    }
    public ParticleSystem GetRedEffect(Transform _transform) // ��������Ʈ �����ֱ�
    {
        ParticleSystem effect = redEffectQueue.Dequeue();
        effect.transform.position = _transform.position + (Vector3.down * 0.5f);
        effect.gameObject.SetActive(true);
        effect.gameObject.GetComponent<ParticleSystem>().Play();
        redEffectQueue.Enqueue(effect);
        return effect;
    }
}
