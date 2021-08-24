using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] Transform s_TempField;
    [SerializeField] Transform s_DeletedField;
    [SerializeField] GameObject targetField;
    [SerializeField] GameObject fallingCol;

    List<int> randBlock = new List<int>();

    Material whiteMat;
    Material redMat;
    Material blueMat;
    Material greenMat;

    void Awake()
    {
        whiteMat = GetComponent<MeshRenderer>().materials[0];
        redMat = GetComponent<MeshRenderer>().materials[1];
        blueMat = GetComponent<MeshRenderer>().materials[2];
        greenMat = GetComponent<MeshRenderer>().materials[3];

        for (int i = 0; i < transform.childCount; i++)
        {
            randBlock.Add(i);
        }
    }
    public void RandomBlock(string ballType)
    {
        int randNum = Random.Range(0, randBlock.Count);
        GameObject selectedField = transform.GetChild(randNum).gameObject;
        int name = int.Parse(selectedField.name);

        bool isDelete = false;
        Material sideOfBallMat = null;

        switch (ballType)
        {
            case "whiteBall":
                sideOfBallMat = whiteMat;
                ObjectPoolManager.Instance.GetWhiteBall();
                break;
            case "redBall":
                sideOfBallMat = redMat;
                ObjectPoolManager.Instance.GetRedBall();
                break;
            case "blueBall":
                sideOfBallMat = blueMat;
                ObjectPoolManager.Instance.GetBlueBall();
                break;
            case "greenBall":
                sideOfBallMat = greenMat;
                ObjectPoolManager.Instance.GetGreenBall();
                break;
            case "deleteField":
                isDelete = true;
                while (name % 11 == 0) 
                {
                    randNum = Random.Range(0, randBlock.Count);
                    selectedField = transform.GetChild(randNum).gameObject;
                    int _name = int.Parse(selectedField.name);
                    if (_name % 11 != 0) { break; }
                }
                break;
        }
        if (isDelete)
        {
            selectedField.transform.SetParent(s_DeletedField);
            StartCoroutine(DisableField(selectedField));
            isDelete = false;
        }
        else
        {
            selectedField.GetComponent<MeshRenderer>().material = sideOfBallMat;
            selectedField.GetComponent<BoxCollider>().enabled = true;
            selectedField.transform.SetParent(s_TempField);
        }
        randBlock.RemoveAt(randNum);
    }
    IEnumerator DisableField(GameObject field)
    {
        FindObjectOfType<AudioManager>().Play("DeleteField");
        Vector3 InstancePos = field.transform.position + Vector3.up;
        targetField.transform.position = InstancePos;
        targetField.SetActive(true);
        yield return new WaitForSeconds(3f);
        FindObjectOfType<AudioManager>().Play("Falling");
        Instantiate(fallingCol, InstancePos, Quaternion.identity);
        targetField.SetActive(false);
        field.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(3f);
        field.SetActive(false);
    }
}
