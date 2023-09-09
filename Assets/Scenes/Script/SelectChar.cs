using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChar : MonoBehaviour
{
    public Character character;
    Animator anim;
    //SpriteRenderer sr;
    public SelectChar[] chars;

    void Start()
    {
        anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        if (DataMgr.instance.currentCharacter == character) OnSelect();
        else OnDeselect();
    }

    private void OnMouseUp()
    {
        DataMgr.instance.currentCharacter = character;
        OnSelect();
        for (int i = 0; i <chars.Length; i++)
        {
            if (chars[i] != this) chars[i].OnDeselect();
        }
    }

    void OnDeselect()
    {
        anim.SetBool("doJump", false);
        //sr.color = new Color(0.5f, 0.5f, 0.5f);
    }

    void OnSelect()
    {
        anim.SetBool("doHi", true);
        //sr.color = new Color(1f, 1f, 1f);
    }

}
