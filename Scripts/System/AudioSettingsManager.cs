using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;   // Ползунок громкости музыки
    public Slider soundVolumeSlider;   // Ползунок громкости звуков

    private AudioSource[] musicSources;  // Источник для музыки (с тегом "Music")
    private AudioSource[] soundSources;  // Источник для звуков (с тегом "Sound")

    void Start()
    {
        // Получаем все объекты с тегом "Music" и "Sound"
        musicSources = GameObject.FindGameObjectsWithTag("Music").Select(obj => obj.GetComponent<AudioSource>()).ToArray();
        soundSources = GameObject.FindGameObjectsWithTag("Sound").Select(obj => obj.GetComponent<AudioSource>()).ToArray();

        // Устанавливаем значения из PlayerPrefs, если они есть
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // по умолчанию 1
        soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f); // по умолчанию 1

        // Настроим громкость при старте
        SetMusicVolume(musicVolumeSlider.value);
        SetSoundVolume(soundVolumeSlider.value);

        // Добавим слушателей на ползунки
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    // Устанавливаем громкость музыки
    public void SetMusicVolume(float volume)
    {
        foreach (var musicSource in musicSources)
        {
            musicSource.volume = volume;  // Устанавливаем громкость для всех объектов с тегом "Music"
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Сохраняем громкость музыки
    }

    // Устанавливаем громкость звуков
    public void SetSoundVolume(float volume)
    {
        foreach (var soundSource in soundSources)
        {
            soundSource.volume = volume;  // Устанавливаем громкость для всех объектов с тегом "Sound"
        }
        PlayerPrefs.SetFloat("SoundVolume", volume);  // Сохраняем громкость звуков
    }
}
