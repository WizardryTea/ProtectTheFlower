using UnityEngine;
using System.Linq; //Select Where и др (для массива GameObject[])
public class MainGameAudioManager : MonoBehaviour
{
    private AudioSource[] musicSources;  // Источник для музыки (с тегом "Music")
    private AudioSource[] soundSources;  // Источник для звуков (с тегом "Sound")

    void Start()
    {
        // Получаем все объекты с тегом "Music" и "Sound"
        musicSources = GameObject.FindGameObjectsWithTag("Music").Select(obj => obj.GetComponent<AudioSource>()).ToArray();
        soundSources = GameObject.FindGameObjectsWithTag("Sound").Select(obj => obj.GetComponent<AudioSource>()).ToArray();

        // Восстанавливаем громкость из PlayerPrefs
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f)); // если нет — 1 по умолчанию
        SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1f)); // если нет — 1 по умолчанию
    }

    // Устанавливаем громкость музыки
    public void SetMusicVolume(float volume)
    {
        foreach (var musicSource in musicSources)
        {
            musicSource.volume = volume;  // Устанавливаем громкость для всех объектов с тегом "Music"
        }
    }

    // Устанавливаем громкость звуков
    public void SetSoundVolume(float volume)
    {
        foreach (var soundSource in soundSources)
        {
            soundSource.volume = volume;  // Устанавливаем громкость для всех объектов с тегом "Sound"
        }
    }
}
