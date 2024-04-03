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
    public GameObject howToPlayText;
    public GameObject WinText;
    public GameObject LoseText;

    public GameObject particlePrefab;
    public Transform goalObject;
    private GameObject particle;

    public AudioClip swing1Audio;
    public Animator coconutsAnimator;
    public Animator coconutAnimator;

    [SerializeField] private AudioSource audioSource;

    public bool start = false;

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
        if (start)
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

        if (Input.GetKeyDown(KeyCode.Z) && !start)
        {
            start = true;
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
            audioSource.PlayOneShot(swing1Audio);
            coconutsAnimator.SetBool("IsHit", true);
            howToPlayText.gameObject.SetActive(false);
            chops++;
            Debug.Log("Chop " + chops + "/" + treeHP);
            Vector3 offset = new Vector3(1.0f, 2.0f, -3.0f);
            particle = Instantiate(particlePrefab, goalObject.position + offset, Quaternion.identity);
            if (chops >= treeHP)
            {
                Debug.Log("tree defeated");
                chops = 0;
                //StartCoroutine(PlayCoconutAnimation());
                coconutAnimator.SetBool("IsFall", true);
                OverworldMapManager.Instance.TransitionBackToMap();
            } else { coconutAnimator.SetBool("IsFall", false); }
            input = false;
        }
        else { coconutsAnimator.SetBool("IsHit", false); }

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

    //IEnumerator PlayCoconutAnimation()
    //{
    //    coconutAnimator.Play("Coconut-Fall");

    //    // Optional: Wait for the animation to complete if needed
    //    yield return null;

    //}
}
