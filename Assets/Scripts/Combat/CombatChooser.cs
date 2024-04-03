using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatChooser : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int index;
        if (GameManager.instance.GetCombatIndex() != 0)
        {
            index = GameManager.instance.GetCombatIndex();
            GameManager.instance.SetCombatIndex(0);
        }
        else
        {
            index = OverworldMapManager.Instance.GetChoiceDepth();
            if (index > 5 && index < 11)
            {
                index -= 5;
            } else if (index > 11)
            {
                index -= 10;
            }
        }

        List<Transform> combats = new List<Transform>();
        foreach (Transform combat in this.transform)
        {
            combats.Add(combat);
        }

        if (combats.Count - 1 >= index - 1)
        {
            combats[index - 1].gameObject.SetActive(true);
        }
        else
        {
            combats[0].gameObject.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
