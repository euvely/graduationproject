using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Trigger;
using static TutorialText;

public class TutorialStimBox : MonoBehaviour
{
    public GameObject obj;
    public bool stimBox;
    public bool allDialEndTrig;

    void BlockTutorialBox(bool stimBox)
    {
        if (allDialEndTrig != true)
        {
            if (stimBox == true)
            {
                //UnityEngine.Debug.Log("startStim: " + startStim);
                obj.SetActive(true);
                // UnityEngine.Debug.Log("stimBox active");
            }

            else
            {
                obj.SetActive(false);
                // UnityEngine.Debug.Log("stimBox inactive");
            }
        }

        else {UnityEngine.Debug.Log("tutorial end");}        
    }

    void Start()
    {
        obj.SetActive(false);
    }

    /* public void GameStatus(bool stim)
    {
        stimState = false;
        if (stim == true)
        {
            // UnityEngine.Debug.Log("stim: " + stim);
            stimState = true;
        }
    } */

    void Update()
    {
        stimBox = Trigger.stimBoxTrig;
        allDialEndTrig = TutorialText.allDialEnd;

        BlockTutorialBox(stimBox);
    }
}
