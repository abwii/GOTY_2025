using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string sceneToLoad;
    public void onPlayClick(){
        SceneManager.LoadScene(sceneToLoad);
    }
    public void onQuitClick(){
        Application.Quit();
    }
}
