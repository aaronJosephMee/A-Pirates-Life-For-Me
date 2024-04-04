using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeTutorial : MonoBehaviour
{
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate{Instantiate(tutorial, this.transform.parent.transform);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
