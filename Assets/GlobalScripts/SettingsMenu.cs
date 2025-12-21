using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    [Header("Difficulty")]
    public Dropdown difficultyDropdown;

    void Start()
    {
        LoadSettings();
    }

    // 🔊 Volume
    public void SetVolume(float value)
    {
        GameSettings.masterVolume = value;

        // Convert linear (0–1) to decibels
        audioMixer.SetFloat(
            "MasterVolume",
            Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f
        );

        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    // Difficulty
    public void SetDifficulty(int index)
    {
        float multiplier = 1f;

        switch (index)
        {
            case 0: multiplier = 0.75f; break; // Easy
            case 1: multiplier = 1f;    break; // Normal
            case 2: multiplier = 1.5f;  break; // Hard
        }

        GameSettings.enemyDamageMultiplier = multiplier;

        PlayerPrefs.SetInt("Difficulty", index);
    }

    void LoadSettings()
    {
        // Volume
        float volume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        volumeSlider.value = volume;
        SetVolume(volume);

        // Difficulty
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);
        difficultyDropdown.value = difficulty;
        SetDifficulty(difficulty);
    }
}
