using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueCube : MonoBehaviour
{
    public Button button;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(updateFlag);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.instance.choices.CheckFlag("Yellow"));
        //Debug.Log(GameManager.instance.choices.CheckFlag("Green"));
        if (GameManager.choices.CheckFlag("Yellow") == 1 && GameManager.choices.CheckFlag("Green") == 0){
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void updateFlag(){
        GameManager.choices.SetFlag("Blue", 1);
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1);
    }
}
