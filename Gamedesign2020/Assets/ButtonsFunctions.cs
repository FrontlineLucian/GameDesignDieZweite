using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour
{
    public void __SelectLevel()
    {
        SceneManager.LoadScene("__MENU_SELECT_LEVEL", LoadSceneMode.Single);
    }
    public void __Continue()
    {
        SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Single);
    }
    public void __Exit()
    {
        
    }

    public void __Back()
    {
        SceneManager.LoadScene("__MENU", LoadSceneMode.Single);
    }
}
