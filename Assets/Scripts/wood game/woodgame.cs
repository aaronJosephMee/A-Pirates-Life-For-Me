using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WoodMiniGame : MonoBehaviour
{
    public Canvas image;
    public int treeHP = 6;

    private int chops;
    private float SwingTime;

    private bool input;

    public GameObject particlePrefab;
    public Transform goalObject;
    private GameObject particle;

    void Start()
    {
        image.enabled = false;
        input = false;
        chops = 0;
        SwingTime = Time.time + Random.Range(1.5f, 3.0f);
    }

    void Update()
    {
        if (Time.time >= SwingTime)
        {
            DisplayKey();
            SwingTime = Time.time + Random.Range(1.5f, 3.0f);
        }

        if (input)
        {
            CheckInput();
        }
    }

    void DisplayKey()
    {
        image.enabled = true;
        input = true;
        StartCoroutine(HideKey());
        Destroy(particle);
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            chops++;
            Debug.Log("Chop " + chops + "/" + treeHP);
            Vector3 offset = new Vector3(1.0f, 2.0f, -3.0f);
            particle = Instantiate(particlePrefab, goalObject.position + offset, Quaternion.identity);
            if (chops >= treeHP)
            {
                Debug.Log("tree defeated");
                chops = 0;
                GameManager.choices.SetFlag("Wood", 1);
                GameManager.instance.LoadScene("Isle of Torrent", true);
            }
            input = false;
        }

    }

    IEnumerator HideKey()
    {
        yield return new WaitForSeconds(0.5f);
        image.enabled = false;
        input = false;
    }
}
