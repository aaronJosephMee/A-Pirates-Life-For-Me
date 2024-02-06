using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrangeCube : MonoBehaviour
{
    public Button button;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        while(!GameManager.instance.ready);
        button.onClick.AddListener(updateFlag);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateFlag(){
        GameManager.instance.choices.SetFlag("Orange", 1);
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1);
    }
}
