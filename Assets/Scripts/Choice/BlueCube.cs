using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueCube : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(updateFlag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateFlag(){
        GameManager.instance.choices.SetFlag("Blue", 0);
    }
}
