using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class wheelRotation : MonoBehaviour
{
    public float RotatePower;
    
    private Rigidbody2D rbody;
    int inRotate;
    float stop;

    public GameObject winText;
    public GameObject loseText;
    public GameObject relicWinText;
    public GameObject howToPlayPanel; 
    public GameObject relicChoice;
    public AudioSource audioSource;
    public AudioClip wheelSFX;
    
    private int _delay = 2; // test later for 2 secs 

    private bool spin = false;
    private bool zPressed = false; 
    private void Start()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        relicWinText.gameObject.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);
        
        
        rbody = GetComponent<Rigidbody2D>();
        stop = Random.Range(500f, 1500f); 
       
    }

    private float t;
    
    private void Update()
    {
        
        if (!zPressed&&Input.GetKeyDown(KeyCode.Z))
        {
            zPressed = true;
            spin = true; 
            howToPlayPanel.gameObject.SetActive(false);
        }

        if (spin)
        {
            Rotate();
            if (rbody.angularVelocity > 0)
            {
                rbody.angularVelocity -= stop * Time.deltaTime;
                rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440); // increase for roulette 
            }

            if (Mathf.Approximately(rbody.angularVelocity, 0) && inRotate == 1)
            {
                t += 1 * Time.deltaTime;
                if (t >= 0.5f)
                {

                    inRotate = 0;
                    t = 0;
                    
                    stop = Random.Range(500f, 1500f);
                    spin = false;
                    GetReward();
                    
                }
                
            }
            
        }

    }

    public void Rotate() 
    {
        if(inRotate == 0)
        {
            rbody.AddTorque(RotatePower);
            inRotate = 1;
            audioSource.PlayOneShot(wheelSFX);
        }
    }

    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0 && rot <= 45)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 45 - 23);
            relicWinText.gameObject.SetActive(true);
            StartCoroutine(relicWait());
        }
        else if (rot > 45 && rot <= 90)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,90-23);
            winText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
        }
        else if (rot > 90 && rot <= 135)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,135-23);
            relicWinText.gameObject.SetActive(true);
            StartCoroutine(relicWait()); 
        }
        else if (rot > 135 && rot <= 180)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,180-23);
            winText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
        }
        else if (rot > 180 && rot <= 225)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,225-23);
            loseText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(-100);
            StartCoroutine(WinWait());
        }
        
        else if (rot > 225 && rot <= 270)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,270-23);
            winText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
            
        }
        
        else if (rot > 270 && rot <= 315)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315-23);
            loseText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(-100);
            StartCoroutine(WinWait());
        }
        
        else if (rot > 315 && rot <= 360)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315+23);
            winText.gameObject.SetActive(true);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
        }
    }
    
    
    private IEnumerator relicWait()
    {
        yield return new WaitForSeconds(_delay); 
        Instantiate(relicChoice);
    }
    
    
    public IEnumerator WinWait()
    {
        yield return new WaitForSeconds(_delay); 
        OverworldMapManager.Instance.TransitionBackToMap();
    }

    
}