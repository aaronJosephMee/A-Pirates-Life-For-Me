using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate{Destroy(this.transform.parent.gameObject);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
