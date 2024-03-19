using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private bool returnToMap;
    Button close;
    // Start is called before the first frame update
    void Start()
    {
        close = GetComponentInChildren<Button>();
        if (returnToMap)
        {
            close.onClick.AddListener(delegate { OverworldMapManager.Instance.TransitionBackToMap(); });
        }
        else
        {
            close.onClick.AddListener(delegate{Destroy(this.gameObject);});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
