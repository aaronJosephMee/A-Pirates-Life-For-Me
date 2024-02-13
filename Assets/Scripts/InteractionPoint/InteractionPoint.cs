using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactText;
    public GameObject map;
    private bool mapOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.E) && !mapOpen){
            Instantiate(map);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            interactText.SetActive(false);
        }
    }
}
