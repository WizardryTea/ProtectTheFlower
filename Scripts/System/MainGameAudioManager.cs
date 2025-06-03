using UnityEngine;
using System.Linq; //Select Where � �� (��� ������� GameObject[])
public class MainGameAudioManager : MonoBehaviour
{
    private AudioSource[] musicSources;  // �������� ��� ������ (� ����� "Music")
    private AudioSource[] soundSources;  // �������� ��� ������ (� ����� "Sound")

    void Start()
    {
        // �������� ��� ������� � ����� "Music" � "Sound"
        musicSources = GameObject.FindGameObjectsWithTag("Music").Select(obj => obj.GetComponent<AudioSource>()).ToArray();
        soundSources = GameObject.FindGameObjectsWithTag("Sound").Select(obj => obj.GetComponent<AudioSource>()).ToArray();

        // ��������������� ��������� �� PlayerPrefs
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f)); // ���� ��� � 1 �� ���������
        SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1f)); // ���� ��� � 1 �� ���������
    }

    // ������������� ��������� ������
    public void SetMusicVolume(float volume)
    {
        foreach (var musicSource in musicSources)
        {
            musicSource.volume = volume;  // ������������� ��������� ��� ���� �������� � ����� "Music"
        }
    }

    // ������������� ��������� ������
    public void SetSoundVolume(float volume)
    {
        foreach (var soundSource in soundSources)
        {
            soundSource.volume = volume;  // ������������� ��������� ��� ���� �������� � ����� "Sound"
        }
    }
}
