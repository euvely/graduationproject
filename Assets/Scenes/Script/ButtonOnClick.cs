using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnClick : MonoBehaviour
{
    public void ReplayLv1()
    {
        SceneManager.LoadScene("Lev1Scene");
    }

    public void Quit() //게임 레벨 선택 씬
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Home()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Play()
    {
        SceneManager.LoadScene("Lev1Scene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Setting()
    {
        SceneManager.LoadScene("SelectScene");
    }

}
