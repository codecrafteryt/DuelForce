using UnityEngine;
using UnityEngine.UI;

public class OPtionUI : MonoBehaviour
{
    public Slider music;
    public Slider sound;

    private void Start() {
        music.onValueChanged.AddListener(UpdateMusic);
        sound.onValueChanged.AddListener(UpdateSound);

        sound.value = PlayerPrefs.GetFloat("S", 1);
        music.value = PlayerPrefs.GetFloat("M", 1);
    }


    void UpdateMusic(float value) {
       AudioManager.Instance.SetMusic(value);   
        
    }

    void UpdateSound(float value) {
        AudioManager.Instance.SetSound(value);

    }
}
