using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//Relics can be obtained from battle, events, and the shop. They give permanent stat buffs and are very important to surving the harsh seas. They can give passive or active stat buffs. Passive buffs are always active, wheras active buffs require a condition such as a melee to be activated.
//Items can be bought from the shop and used in combat by pressing 'E'. They can also be used from the inventory by clicking on the item if it is highlighted. Items give temporary stat buffs and healing so use them to give yourself the edge in battle.
[System.Serializable] public struct tutorialEntry{
    public string title;
    public string body;
    public Color titleColor;
}
public class Tutorial : MonoBehaviour
{
    public List<tutorialEntry> tutorialMsgs = new List<tutorialEntry>();
    public Button next;
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI title;

    int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp.text = tutorialMsgs[idx].body;
        title.text = tutorialMsgs[idx].title;
        title.color = tutorialMsgs[idx].titleColor;
        next.onClick.AddListener(GoNext);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GoNext(){
        idx++;
        if (idx >= tutorialMsgs.Count){
            Destroy(this.gameObject);
            return;
        }
        tmp.text = tutorialMsgs[idx].body;
        title.text = tutorialMsgs[idx].title;
        title.color = tutorialMsgs[idx].titleColor;
    }
}
