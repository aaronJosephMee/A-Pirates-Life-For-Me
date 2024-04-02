using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnableFullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(EnableFullScreenMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableFullScreenMode()
    {
        print(Screen.fullScreenMode);
        if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            print("After if: " + Screen.fullScreenMode);
            var offText = FindObjectsOfType<TextMeshProUGUI>().ToList().Find(x => x.name == "OFFText (TMP)");
            offText.GameObject().SetActive(true);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            var onText = FindObjectsOfType<TextMeshProUGUI>().ToList().Find(x => x.name == "ONText (TMP)");
            onText.GameObject().SetActive(true);
        }
    }
}
