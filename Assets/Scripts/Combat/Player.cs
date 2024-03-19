using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 100f;

    Ragdoll ragdoll;
    public bool isDead;
    CharacterAiming aiming;

    CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        aiming = GetComponent<CharacterAiming>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void takeDamage(float damage)
    {
        this.currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }



    private void Die()
    {
        isDead = true;

        ragdoll.ActivateRagdoll();
        aiming.enabled = false;

        cameraManager.EnableKillCam();

    }
}
