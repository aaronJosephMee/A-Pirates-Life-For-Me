using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.menuOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Destroy(this.gameObject);
        }
    }
    void OnDestroy()
    {
        GameManager.instance.menuOpen = false;
    }
}
