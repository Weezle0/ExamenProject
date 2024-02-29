using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public void ChangeScene(int sceneNumber) //Dit zorgt er voor dat de goede scene word geladen
    {
        SceneManager.LoadScene(sceneNumber);
    }
   public void QuitButton() //Dit zorgt er voor dat de applicatie wordt afgesloten
    {
        Application.Quit();
    }
}
