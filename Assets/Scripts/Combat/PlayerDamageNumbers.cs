using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageNumbers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField]float timeToLive;
    string text;
    Color color;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Die());
    }
    public void SetText(string text, Color color){
        this.text = text;
        this.color = color;
        tmp.text = text;
        tmp.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + new Vector3(0,25f * Time.deltaTime, 0);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - 0.5f * Time.deltaTime);
        
    }
    private IEnumerator Die(){
        yield return new WaitForSeconds(timeToLive);
        Destroy(this.gameObject);
    }
}
