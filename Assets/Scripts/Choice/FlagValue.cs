using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagValue : MonoBehaviour
{
    public Text test;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        GameManager.choices.CreateDependency("Wood", this.gameObject, 1, Activate);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<FlagValue>().test.text = "" + GameManager.choices.CheckFlag("Wood");
    }
    int Activate(GameObject gameObject){
        gameObject.SetActive(true);
        return 0;
    }
}
