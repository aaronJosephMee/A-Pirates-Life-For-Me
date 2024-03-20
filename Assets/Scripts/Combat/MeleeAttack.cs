using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    public Collider hitbox;

    private bool attackActive = false;
    private bool isSwing1 = true;
    [System.NonSerialized]public bool enabld = true;

    public AudioClip swing1Audio;
    public AudioClip swing2Audio;

    [SerializeField] private AudioSource audioSource;


    public float blendSpeed = 0.1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && enabld && !attackActive )
        {
            StartAttack();
            attackActive = true;

        }
    }


    //TODO: make modular combo system with a 3rd swing

    private void StartAttack()
    {


        hitbox.enabled = false;
        if (isSwing1)
        {
            animator.Play("SwordSwing", 1, 0f);
            audioSource.PlayOneShot(swing1Audio);
        }
        if (!isSwing1)
        {

            animator.Play("swordSwing2", 1, 0f);
            audioSource.PlayOneShot(swing2Audio);
        }



        animator.SetLayerWeight(1, 1);

    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
        
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
        

    }
    //public void layerBlend()
    //{
    //    StartCoroutine(BlendLayer());
    //}
    

    //public IEnumerator BlendLayer()
    //{
    //    float elapsedTime = 0f;
    //    while (elapsedTime < blendSpeed)
    //    {
    //        float newWeight = Mathf.Lerp(1, 0, elapsedTime / blendSpeed);
    //        animator.SetLayerWeight(1, newWeight);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    animator.SetLayerWeight(1, 0);
    //    attackActive = false;
    //}

    public void AnimEnd()
    {
        isSwing1 = !isSwing1;
        attackActive = false;
        animator.SetLayerWeight(1, 0); //move after anim fix
    }


}
