using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WoodMiniGame : MonoBehaviour
{
    public Canvas WImage;
    public Canvas AImage;
    public Canvas SImage;
    public Canvas DImage;
    public int keyNumber;
    public int treeHP = 6;

    private int chops;
    private float SwingTime;

    private bool input;
    public TextMeshProUGUI howToPlayText;

    public GameObject particlePrefab;
    public Transform goalObject;
    private GameObject particle;

    void Start()
    {
        WImage.enabled = false;
        AImage.enabled = false;
        SImage.enabled = false;
        DImage.enabled = false;
        input = false;
        chops = 0;
        SwingTime = Time.time + Random.Range(1.5f, 3.0f);
        howToPlayText.gameObject.SetActive(true);
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
        keyNumber = Random.Range(1,5);
        if (keyNumber == 1) {
            WImage.enabled = true;
        }
        else if (keyNumber == 2) {
            AImage.enabled = true;
        }
        else if (keyNumber == 3) {
            SImage.enabled = true;
        }
        else if (keyNumber == 4) {
            DImage.enabled = true;
        }
        input = true;
        StartCoroutine(HideKey());
        Destroy(particle);
    }

    void CheckInput()
    {
        if ( (keyNumber == 1 && Input.GetKeyDown(KeyCode.W)) ||
        (keyNumber == 2 && Input.GetKeyDown(KeyCode.A)) || (keyNumber == 3 && Input.GetKeyDown(KeyCode.S))
        || (keyNumber == 4 && Input.GetKeyDown(KeyCode.D)))
        {
            howToPlayText.gameObject.SetActive(false);
            chops++;
            Debug.Log("Chop " + chops + "/" + treeHP);
            Vector3 offset = new Vector3(1.0f, 2.0f, -3.0f);
            particle = Instantiate(particlePrefab, goalObject.position + offset, Quaternion.identity);
            if (chops >= treeHP)
            {
                Debug.Log("tree defeated");
                chops = 0;
                OverworldMapManager.Instance.TransitionBackToMap();
            }
            input = false;
        }

    }

    IEnumerator HideKey()
    {
        yield return new WaitForSeconds(1.0f);
        if (keyNumber == 1) {
            WImage.enabled = false;
        }
        else if (keyNumber == 2) {
            AImage.enabled = false;
        }
        else if (keyNumber == 3) {
            SImage.enabled = false;
        }
        else if (keyNumber == 4) {
            DImage.enabled = false;
        }
        input = false;
    }
}
