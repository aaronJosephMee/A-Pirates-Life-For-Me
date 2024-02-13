using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactText;
    public GameObject toSpawn;
    private GameObject instance;
    public bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instance == null){
            spawned = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetKey(KeyCode.E) && !spawned){
            spawned = true;
            instance = Instantiate(toSpawn);
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
