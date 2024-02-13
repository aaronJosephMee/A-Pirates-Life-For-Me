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
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            chops++;
            Debug.Log("Chop " + chops + "/" + treeHP);
            if (chops >= treeHP)
            {
                Debug.Log("tree defeated");
                chops = 0;
                Destroy(gameObject);
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
