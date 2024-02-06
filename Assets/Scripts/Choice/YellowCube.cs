using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowCube : MonoBehaviour
{
    public Button button;
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(updateFlag);
        GameManager.choices.CreateDependency("Blue", this.gameObject, 1, changeColor);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateFlag(){
        GameManager.choices.SetFlag("Yellow", 1);
    }
    public int changeColor(GameObject gameObject){
        gameObject.GetComponent<Renderer>().material.color = Color.grey;
        return 0;
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(1);
    }
}
