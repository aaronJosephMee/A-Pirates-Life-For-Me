using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelRotation : MonoBehaviour
{
    public float RotatePower;
    
    private Rigidbody2D rbody;
    int inRotate;
    float stop;

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
                GetReward();
                inRotate = 0;
                t = 0;
                stop = Random.Range(500f, 1500f); // Generate a new random stop value
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
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,45-23);
            Win(1);
        }
        else if (rot > 45 && rot <= 90)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,90-23);
            Win(2);
        }
        else if (rot > 90 && rot <= 135)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,135-23);
            Win(3);
        }
        else if (rot > 135 && rot <= 180)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,180-23);
            Win(4);
        }
        else if (rot > 180 && rot <= 225)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,225-23);
            Win(5);
        }
        
        else if (rot > 225 && rot <= 270)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,270-23);
            Win(6);
        }
        
        else if (rot > 270 && rot <= 315)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315-23);
            Win(7);
        }
        
        else if (rot > 315 && rot <= 360)
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0,0,315+23);
            Win(8);
        }
    }

    public void Win(int Score)
    {
        print(Score);
        //OverworldMapManager.Instance.TransitionBackToMap();
    }
}