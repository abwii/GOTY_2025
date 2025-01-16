using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string sceneToLoad;
    public void onPlayClick(){
        print("cliqué");
        SceneManager.LoadScene(sceneToLoad);
    }
    public void onQuitClick(){
        Application.Quit();
    }
}
