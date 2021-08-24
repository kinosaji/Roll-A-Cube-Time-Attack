using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoText : MonoBehaviour
{
    [SerializeField] float charSeconds;
    [SerializeField] GameObject startbutton;

    Text infoText;
    string playerInfo;
    int index;
    string ID;
    string originPassWard;
    string PW;

    Animator startButtonBurrow;

    void Awake()
    {
        startButtonBurrow = startbutton.GetComponent<Animator>();
        infoText = GetComponent<Text>();
        ID = GoogleManager.Instance.Player_Name;
        byte[] byteID = System.Text.Encoding.UTF8.GetBytes(ID);
        originPassWard = System.Convert.ToBase64String(byteID);
        if (Debug.isDebugBuild)
        {
            PW = "test";
        }
        else
        {
            PW = originPassWard.Substring(0, 5);
        }
    }

    void Start()
    {
        playerInfo = $"ID : {ID}\nPW : {PW}";
    }

    public void TextEffectCorutine()
    {
        StartCoroutine(IEffecting(charSeconds));
    }

    IEnumerator IEffecting(float charSpeed)
    {
        yield return new WaitForSeconds(0.5f);
        infoText.text = "";
        index = 0;

        while (true)
        {
            Effecting();
            index++;
            yield return new WaitForSeconds(charSpeed);
            if (infoText.text == playerInfo)
            {
                Invoke("PlayerLogin", 1f);
                break;
            }
        }
    }
    void Effecting()
    {
        FindObjectOfType<AudioManager>().Play("InputKey");
        infoText.text += playerInfo[index];
    }

    void PlayerLogin()
    {
        FindObjectOfType<AudioManager>().Play("LoginSuccess");

        infoText.alignment = TextAnchor.MiddleCenter;
        infoText.fontSize = 90;
        infoText.text = "Login Successful";
        startButtonBurrow.SetTrigger("Burrow");
    }
}
