using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueCube : MonoBehaviour
{
    Button button;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(updateFlag);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.choices.CheckFlag("Yellow") == 1 && GameManager.instance.choices.CheckFlag("Green") == 0){
            mat.color = Color.black;
        }
    }
    public void updateFlag(){
        GameManager.instance.choices.SetFlag("Blue", 1);
    }
}
