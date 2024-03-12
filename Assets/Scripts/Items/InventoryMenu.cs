using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    Button close;
    // Start is called before the first frame update
    void Start()
    {
        close = GetComponentInChildren<Button>();
        close.onClick.AddListener(delegate{Destroy(this);});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
