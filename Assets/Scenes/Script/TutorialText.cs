using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using static Trigger;

public class TutorialText : MonoBehaviour
{
    public TMP_Text tutorialTxt;
    public static int txtNum;
    public static int dialogNum;
    public string txt;
    // public int[] tutorialDialog = {1, 2, 3, 4};
    // public string[] startDialog = {"안녕!"};
    public string[] tutorialDialog1;
    public string[] tutorialDialog2;
    public string[] tutorialDialog3;
    public string[] tutorialDialog4;
    public static string[] txts;
    public static bool dialEnd; //false: 남은 튜토리얼 문장 O, true: 남은 튜토리얼 문장 X
    //public static bool txtStateEvent;
    //public static bool boxState = false; // false: box stim 진행, true: box stim 종료
    public static bool startStim = true;
    public static bool allDialEnd;

    public void StartTxt(string[] talk)
    {
        txts = talk;
        dialEnd = false;
        // boxState = false;
        
        //UnityEngine.Debug.Log("dialog length: " + txts.Length);

        StartCoroutine(Typing(txts[txtNum]));
    }

    public void NextTxt()
    {
        //tutorialTxt.text = null;
        txtNum++;
        
        // UnityEngine.Debug.Log("txtNum: " + txtNum);

        //dialEnd = Trigger.dialEndTrig;

        if (txtNum == txts.Length)
        {
            EndTalk();
            // return;
        }
    }

    public void EndTalk()
    {
        txtNum = 0;
        dialogNum++;
        dialEnd = true;
        
        if (dialogNum == 4)
        {
            UnityEngine.Debug.Log("all tutorial end");
            allDialEnd = true;
        }
    }
    
    IEnumerator Typing(string txt)
    {
        tutorialTxt.text = null;

        if (txt.Contains("  "))
        {
            txt = txt.Replace("  ", "\n");
        }

        for (int i=0; i < txt.Length; i++)
        {
            tutorialTxt.text += txt[i];
            yield return new WaitForSeconds(0.1f);
        }

        NextTxt();
    }

    /* public void Start()
    {        
        boxState = Player.gameEnd;
        UnityEngine.Debug.Log("boxState: " + boxState);

        if (boxState = true)
        {
            txtState = false;
            UnityEngine.Debug.Log("박스 stim 종료");
        }
    } */

    /* public void Update()
    {
        // txtState = txtState;
        boxState = Player.gameEnd;

        if (boxState = true)
        {
            txtState = false;
        }
    } */

    public void ButtonOnClick()
    {   
        // UnityEngine.Debug.Log("tutoridal dialog num: " + dialogNum);
        startStim = Trigger.dialBoxTrig;

        // StartTxt(tutorialDialog1);
        if (dialogNum == 0) {StartTxt(tutorialDialog1);}
        else 
        {
            if (startStim)
            {
                if (dialogNum == 1)
                {
                    StartTxt(tutorialDialog2);
                }
                else if (dialogNum == 2)
                {
                    StartTxt(tutorialDialog3);
                }
                else if (dialogNum == 3)
                {
                    StartTxt(tutorialDialog4);
                }
            }
        }
        /* if (dialogNum == 0) {StartTxt(tutorialDialog1);}
        else if (dialogNum == 1) {StartTxt(tutorialDialog2);}
        else if (dialogNum == 2) {StartTxt(tutorialDialog3);}
        else if (dialogNum == 3) {StartTxt(tutorialDialog4);} */
    }
}
