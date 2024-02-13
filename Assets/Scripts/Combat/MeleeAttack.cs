using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator animator;
    public Collider hitbox;
    public float attackDuration = 0.5f;
    public float blendSpeed = 0.5f;

    private bool attackActive = false;
    private bool isSwing1 = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAttack();
        }
    }

    //TODO: make modular combo system with a 3rd swing

    private void StartAttack()
    {
        if (!attackActive)
        {
            attackActive = true;

            if (isSwing1)
            {
                animator.Play("SwordSwing", 1, 0f);

            }
            else
            {
                animator.Play("swordSwing2", 1, 0f);

            }

            isSwing1 = !isSwing1;

            animator.SetLayerWeight(1, 1);

            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackDuration);

        float animationDuration = animator.GetCurrentAnimatorStateInfo(1).length;

        float middleStart = animationDuration / 4;
        float middleEnd = 2 * animationDuration /3;

        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < middleStart)
        {
            yield return null;
        }

        hitbox.enabled = true;

        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < middleEnd)
        {
            yield return null;
        }

        hitbox.enabled = false;

        float elapsedTime = 0f;
        while (elapsedTime < blendSpeed)
        {
            float newWeight = Mathf.Lerp(1, 0, elapsedTime / blendSpeed);
            animator.SetLayerWeight(1, newWeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        animator.SetLayerWeight(1, 0);

        attackActive = false;
    }
}
