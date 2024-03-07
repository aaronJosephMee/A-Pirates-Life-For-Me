using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public int heightOfArea;
    public GameObject toSpawn;
    
    public void Spawn(Choice[] choices)
    {
        int curheight = 0;
        for (int i = 0; i<choices.Length; i++){
            GameObject instance = Instantiate(toSpawn, this.transform.position - new Vector3(0,curheight,0), Quaternion.identity);
            instance.transform.SetParent(this.transform.parent);
            instance.GetComponent<ChoiceButton>().DisplayChoice(choices[i]);
            curheight += heightOfArea/choices.Length;
        }
    }
}
