using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{
    cat01, cat02, cat03
}

public class DataMgr : MonoBehaviour
{
    public static DataMgr instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        //DontDestroyOnLoad(gameObject);
    }

    public Character currentCharacter;
}
