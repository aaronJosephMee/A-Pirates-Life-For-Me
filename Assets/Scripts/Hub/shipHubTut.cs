using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class shipHubTut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject page1IMG;
    public GameObject page1Text;
    public GameObject page2IMG;
    public GameObject page2Text;

    void Start()
    {
        
        page1IMG.gameObject.SetActive(true);
        page1Text.gameObject.SetActive(true);
        page2IMG.gameObject.SetActive(false);
        page2Text.gameObject.SetActive(false);


        
        Cursor.visible = true;
        GetComponent<Button>().onClick.AddListener(next);
    }

    // Update is called once per frame
        void Update()
        {

        }

        void next()
        {
            page1IMG.gameObject.SetActive(false);
            page1Text.gameObject.SetActive(false);
            page2IMG.gameObject.SetActive(true);
            page2Text.gameObject.SetActive(true);
            this.GetComponent<Button>().onClick.AddListener(delegate{Destroy(this.transform.parent.gameObject);});
        }
        

}

