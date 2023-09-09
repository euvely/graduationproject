using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Trigger;
using static TutorialText;

public class TutorialStim : MonoBehaviour
{
    public GameObject obj;
    public static bool dialBox;
    public bool end;
    public bool stimEnd;

    void BlockTutorialStim()
    {
        end = TutorialText.dialEnd;
        stimEnd = Trigger.stimEndTrig;

        if ((end == false) || (stimEnd == true))
        {
            obj.SetActive(true);
            dialBox = true;
        }

        else
        {
            obj.SetActive(false);
            dialBox = false;
        }
    }

    void Update()
    {
        BlockTutorialStim();
    }
}
