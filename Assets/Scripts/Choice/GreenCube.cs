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
        button.onClick.AddListener(updateFlag);
        Debug.Log("In");
        GameManager.instance.choices.CreateDependency("Yellow", this.gameObject, 1, changeColor1);
        GameManager.instance.choices.CreateDependency("Orange", this.gameObject, 1, changeColor2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateFlag(){
        GameManager.instance.choices.SetFlag("Green", 1);
    }
    public int changeColor1(GameObject gameObject){
        gameObject.GetComponent<GreenCube>().mat.color = Color.red;
        return 0;
    }
    public int changeColor2(GameObject gameObject){
        gameObject.GetComponent<GreenCube>().mat.color = Color.white;
        return 0;
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1);
    }
}
