using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelRotation : MonoBehaviour
{
    public float RotatePower;
    
    private Rigidbody2D rbody;
    int inRotate;
    float stop;

    public GameObject relicChoice;
    private int _delay = 1; 
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        stop = Random.Range(500f, 1500f); // Adjust the range as needed
        Rotate();
    }

    float t;

    private void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= stop * Time.deltaTime;
            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (Mathf.Approximately(rbody.angularVelocity, 0) && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                
                inRotate = 0;
                t = 0;
                stop = Random.Range(500f, 1500f); // Generate a new random stop value
                GetReward();
            }
        }
        
    }

    public void Rotate() 
    {
        if(inRotate == 0)
        {
            rbody.AddTorque(RotatePower);
            inRotate = 1;
        }
    }

    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0 && rot <= 45)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 45 - 23);
            StartCoroutine(relicWait());
        }
        else if (rot > 45 && rot <= 90)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,90-23);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
        }
        else if (rot > 90 && rot <= 135)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,135-23);
            StartCoroutine(relicWait()); 
        }
        else if (rot > 135 && rot <= 180)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,180-23);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
        }
        else if (rot > 180 && rot <= 225)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,225-23);
            ItemManager.instance.AddGold(-100);
            StartCoroutine(WinWait());
        }
        
        else if (rot > 225 && rot <= 270)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,270-23);
            ItemManager.instance.AddGold(100);
            StartCoroutine(WinWait());
            
        }
        
        else if (rot > 270 && rot <= 315)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315-23);
            ItemManager.instance.AddGold(-100);
            StartCoroutine(WinWait());
        }
        
        else if (rot > 315 && rot <= 360)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315+23);
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