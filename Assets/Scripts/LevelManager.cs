using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    public List<Button> levelButtons;

    public Sprite UnlockSprite;
    public Sprite lockSprite;

    void Start() {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0); // Default is level 1

        for (int i = 0; i < levelButtons.Count; i++) {

            if (i <= unlockedLevel) {
                levelButtons[i].interactable = true;
                int index = i ; // Capture index for closure
                levelButtons[i].GetComponent<Image>().sprite = UnlockSprite; 
            }
            else {
                levelButtons[i].GetComponent<Image>().sprite = lockSprite;  
                levelButtons[i].interactable = false;
            }
        }
    }

    public void LoadLevel(int level) {
        // Replace with your actual level scene names or build indices
        PlayerPrefs.SetInt("currentLevel",level);
        SceneManager.LoadScene("2048");
    }

    // Call this method after a level is completed
    public static void UnlockNextLevel(int completedLevel) {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (completedLevel >= currentUnlocked) {
            PlayerPrefs.SetInt("UnlockedLevel", completedLevel + 1);
        }
    }

    // Optional: For resetting progress
    public void ResetProgress() {
        PlayerPrefs.DeleteKey("UnlockedLevel");
    }
}
