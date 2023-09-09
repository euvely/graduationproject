using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    public GameObject obj;
    
    public void OnEnable()
    {
        //UnityEngine.Debug.Log("on enable " + obj);
        obj.SetActive(true);
    }

    public void OnDisable()
    {
        //UnityEngine.Debug.Log("on disable " + obj);
        obj.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
