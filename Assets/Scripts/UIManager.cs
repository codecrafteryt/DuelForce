using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject OptionPoup;
    public GameObject RulesPopup;

    public string levelSceneName;
    public string gameplaySceneName;
    public string menuSceneName;

    public void OpenOption() {
        OptionPoup.SetActive(true);
    }
    public void CloseOption() {
        OptionPoup.SetActive(false);
    }

    public void OpenRule() {
        RulesPopup.SetActive(true);
    }

    public void CloseRule() {
        RulesPopup.SetActive(false);
    }

    public void OnStart() {
        SceneManager.LoadScene(levelSceneName);
    }

    public void OnLevel() {
        SceneManager.LoadScene(levelSceneName);
    }

    public void CloseLevel() {
        SceneManager.LoadScene(menuSceneName);
    }


}
