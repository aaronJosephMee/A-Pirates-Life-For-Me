using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapScreen : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            parent.GetComponent<InteractionPoint>().mapOpen = false;
            Destroy(this.gameObject);
        }
    }
}
