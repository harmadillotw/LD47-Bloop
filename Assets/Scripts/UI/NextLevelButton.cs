using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    
    public void NextLevel()
    {
        if (SceneData.currentLevel.Count > 0)
        {
            SceneData.currentLevel[0]++;
            if (SceneData.currentLevel[0] >= SceneData.levels.Count)
            {
                SceneManager.LoadScene("ExitScreen");
            }
            else
            {
                SceneManager.LoadScene("SceneMain");
            }
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
