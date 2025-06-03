using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSoundManager : MonoBehaviour
{
    public AudioSource clickSound;  // ������ �� AudioSource ��� ��������������� �����
    public Button[] buttons;        // ������ ������, � ������� ������������� ����

    void Start()
    {
        // ���������� ��� ������ � �������
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);  // ����������� ���� � ������ ������
        }
    }

    void PlayClickSound()
    {
        // ������������� ���� ��� ������� �� ����� ������
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
