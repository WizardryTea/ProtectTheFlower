using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;   // �������� ��������� ������
    public Slider soundVolumeSlider;   // �������� ��������� ������

    private AudioSource[] musicSources;  // �������� ��� ������ (� ����� "Music")
    private AudioSource[] soundSources;  // �������� ��� ������ (� ����� "Sound")

    void Start()
    {
        // �������� ��� ������� � ����� "Music" � "Sound"
        musicSources = GameObject.FindGameObjectsWithTag("Music").Select(obj => obj.GetComponent<AudioSource>()).ToArray();
        soundSources = GameObject.FindGameObjectsWithTag("Sound").Select(obj => obj.GetComponent<AudioSource>()).ToArray();

        // ������������� �������� �� PlayerPrefs, ���� ��� ����
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // �� ��������� 1
        soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f); // �� ��������� 1

        // �������� ��������� ��� ������
        SetMusicVolume(musicVolumeSlider.value);
        SetSoundVolume(soundVolumeSlider.value);

        // ������� ���������� �� ��������
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
    }

    // ������������� ��������� ������
    public void SetMusicVolume(float volume)
    {
        foreach (var musicSource in musicSources)
        {
            musicSource.volume = volume;  // ������������� ��������� ��� ���� �������� � ����� "Music"
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);  // ��������� ��������� ������
    }

    // ������������� ��������� ������
    public void SetSoundVolume(float volume)
    {
        foreach (var soundSource in soundSources)
        {
            soundSource.volume = volume;  // ������������� ��������� ��� ���� �������� � ����� "Sound"
        }
        PlayerPrefs.SetFloat("SoundVolume", volume);  // ��������� ��������� ������
    }
}
