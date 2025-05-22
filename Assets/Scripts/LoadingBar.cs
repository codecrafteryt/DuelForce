using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {
    public Slider loadingSlider;
    public string sceneToLoad = "YourSceneName";
    [SerializeField] private float speed;

    void Start() {
    }

    

    private void Update() {
        loadingSlider.value += Time.deltaTime * speed;

        if (loadingSlider.value == loadingSlider.maxValue) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
