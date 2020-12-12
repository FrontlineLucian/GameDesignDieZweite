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
        SceneManager.LoadScene("LaborLevel0", LoadSceneMode.Single);
    }
    public void __Exit()
    {
        
    }
    //geiles hardcoding, aber klappt muss ja fertig werden
    public void __Back()
    {
        SceneManager.LoadScene("__MENU", LoadSceneMode.Single);
    }

    public void __Level0()
    {
        SceneManager.LoadScene("LaborLevel0", LoadSceneMode.Single);
    }
    public void __Level1()
    {
        SceneManager.LoadScene("LaborLevel1", LoadSceneMode.Single);
    }
    public void __Level2()
    {
        SceneManager.LoadScene("LaborLevel2", LoadSceneMode.Single);
    }
    public void __Level3()
    {
        SceneManager.LoadScene("LaborLevel3", LoadSceneMode.Single);
    }
    public void __Level4()
    {
        SceneManager.LoadScene("LaborLevel4", LoadSceneMode.Single);
    }
    public void __Level5()
    {
        SceneManager.LoadScene("LaborLevel5", LoadSceneMode.Single);
    }
    public void __Level6()
    {
        SceneManager.LoadScene("LaborLevel6", LoadSceneMode.Single);
    }
    public void __Level7()
    {
        SceneManager.LoadScene("LaborLevel7", LoadSceneMode.Single);
    }
    public void __Level8()
    {
        SceneManager.LoadScene("LaborLevel8", LoadSceneMode.Single);
    }
}
