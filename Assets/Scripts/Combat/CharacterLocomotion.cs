using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    [NonSerialized] public bool enabld = true; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabld){
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        
            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }
        
    }
}
