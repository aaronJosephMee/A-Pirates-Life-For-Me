using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    public GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate{Instantiate(toSpawn);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
