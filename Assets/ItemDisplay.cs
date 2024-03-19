using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField]private GameObject[] itemSlots;
    // Start is called before the first frame update
    void Start()
    {
        List<RelicScriptableObject> relics = ItemManager.instance.GetRelics().Values.ToList<RelicScriptableObject>();
        for (int i = 0; i< itemSlots.Length; i++){
            if (i < relics.Count){
                itemSlots[i].GetComponent<InventoryItem>().GiveItem(relics[i]);
            }
            else{
                itemSlots[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
