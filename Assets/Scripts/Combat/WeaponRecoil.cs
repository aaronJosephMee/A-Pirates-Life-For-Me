using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    public Animator rigController;

    public CharacterAiming characterAiming;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;
    float time;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();

    }


    public void GenerateRecoil()
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        rigController.Play("recoil_pistol", 0, 0.0f);







    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            characterAiming.yAxis.Value -= (verticalRecoil/1000) / duration;
            characterAiming.xAxis.Value -= (horizontalRecoil / 1000) / duration;
            time -= Time.deltaTime;
        }
        
    }
}
