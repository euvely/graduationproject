using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static TutorialText;
using static TutorialStim;
using static TutorialStimBox;
using static SquareImageWithOutlineForTutorial;
using static ObjectActivate;

public class Trigger : MonoBehaviour
{
    public static bool dialEndTrig;
    public int dialTxtNum = 100;
    public string[] dialTxts;

    public static bool stimEndTrig;
    public int curPos;

    public static bool dialBoxTrig;
    public static bool stimBoxTrig;

    public bool dialBoxActive;

    void IsDialEnd()
    {
        // dialEndTrig = TutorialText.dialEnd;

        if (dialEndTrig)
        {
            dialBoxTrig = true;
        }
        else {dialBoxTrig = false;}
    }

    void IsStimEnd()
    {
        // stimEndTrig = false;

        curPos = SquareImageWithOutlineForTutorial.currentPosition;

        if (curPos == 99)
        {
            // UnityEngine.Debug.Log("stim end");
            stimEndTrig = true;
        }
        else
        {
            stimEndTrig = false;
        }
    }

    void IsDialBox()
    {
        // dialBoxTrig = true;
        dialEndTrig = TutorialText.dialEnd;

        if ((dialEndTrig == false) && (stimEndTrig = true))
        {
            dialBoxTrig = true;
        }
        else
        {
            dialBoxTrig = false;
            IsDialEnd();
        }
    }

    void IsStimBox()
    {
        stimBoxTrig = false;
        dialEndTrig = TutorialText.dialEnd;

        if (dialEndTrig == true)
        {
            stimBoxTrig = true;
        }
        else
        {
            if (dialBoxTrig == false)
            {
                stimBoxTrig = true;
            }
            else {stimBoxTrig = false;}
        }
    }

    void Update()
    {
        dialEndTrig = TutorialText.dialEnd;

        IsStimEnd();
        IsDialBox();
        IsStimEnd();
        IsStimBox();
    }
}
