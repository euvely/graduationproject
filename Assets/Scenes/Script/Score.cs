using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text

public class Score : MonoBehaviour
{
    public Text CatText;

    public static int CatScore;
    //public static Score instance;

    // void Awake()
    // {
    //     instance=this;
    // }
    
    void Start()
    {
        CatScore = 0;
        //FixedUpdateScore();
    }

    public void Update()
    {
        CatText.text = "Score : " + CatScore;
    }
}
