using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.OverworldMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button title;
    public TextMeshProUGUI tmp;
    public Image bg;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.menuOpen = true;
        title.onClick.AddListener(delegate{
            OverworldMapManager.Instance?.MarkMapForReset();
            GameManager.instance.LoadScene(SceneName.TitleScreen);});
    }

    // Update is called once per frame
    void Update()
    {
        if (bg.color.a < 1)
            bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, bg.color.a + Time.deltaTime/2f);
        if (bg.color.a > 0.8)
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a + Time.deltaTime/3f);
        if (tmp.color.a > 0.8){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            title.gameObject.SetActive(true);
        }
    }
}
