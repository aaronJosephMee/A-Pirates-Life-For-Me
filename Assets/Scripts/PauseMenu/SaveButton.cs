using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    public Text savedText;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(SaveGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SaveGame(){
        GameManager.choices.SaveState();
        savedText.gameObject.SetActive(true);
    }
}
