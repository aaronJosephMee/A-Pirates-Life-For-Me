using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Choices choices;
    // Start is called before the first frame update
    void Start()
    {
        choices = new Choices();
        choices.AddFlag("Blue", 0);
        choices.AddFlag("Orange", 0);
        choices.AddFlag("Red", 0);
        choices.AddFlag("Green", 0);
    }
    void Awake(){
        if (instance == null){
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
