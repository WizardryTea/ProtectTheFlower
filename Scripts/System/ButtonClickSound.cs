using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSoundManager : MonoBehaviour
{
    public AudioSource clickSound;  // Ссылка на AudioSource для воспроизведения звука
    public Button[] buttons;        // Массив кнопок, к которым привязывается звук

    void Start()
    {
        // Подключаем все кнопки в массиве
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);  // Привязываем звук к каждой кнопке
        }
    }

    void PlayClickSound()
    {
        // Воспроизводим звук при нажатии на любую кнопку
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
