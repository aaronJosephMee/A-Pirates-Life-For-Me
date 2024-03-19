using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;


public class CharacterAiming : MonoBehaviour
{
    [HideInInspector] public CinemachineVirtualCamera vCam;

    public float mouseSense = 15;
    public float aimDuration = 0.3f;

    //Camera mainCamera;
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camLookAt;


    public Rig handLayer;
    public Rig aimLayer;
    public Rig gunLayer;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject sword;

    WeaponManager weapon;


    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weapon = GetComponentInChildren<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        xAxis.Value += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis.Value -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis.Value = Mathf.Clamp(yAxis.Value, -80, 80);
        

        if (Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
            MeleeAttack melee = GetComponent<MeleeAttack>();

            if (melee != null)
            {
                melee.enabled = false;
            }
            sword.SetActive(false);
            gun.SetActive(true);
            handLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
            MeleeAttack melee = GetComponent<MeleeAttack>();

            if (melee != null)
            {
                melee.enabled = true;
            }
            sword.SetActive(true);
            gun.SetActive(false);
            handLayer.weight -= Time.deltaTime / aimDuration;
        }

    }


    
    void LateUpdate()
    {
        camLookAt.localEulerAngles = new Vector3(yAxis.Value, camLookAt.localEulerAngles.y, camLookAt.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);

    }

}
