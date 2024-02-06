using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenCube : MonoBehaviour
{
    public Button button;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
        button.onClick.AddListener(updateFlag);
        Debug.Log("In");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateFlag(){
        GameManager.choices.SetFlag("Green", 1);
    }
    public int changeColor1(GameObject gameObject){
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        return 0;
    }
    public int changeColor2(GameObject gameObject){
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        return 0;
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1);
        GameManager.choices.CreateDependency("Yellow", this.gameObject, 1, changeColor1);
        GameManager.choices.CreateDependency("Orange", this.gameObject, 1, changeColor2);
    }
}
