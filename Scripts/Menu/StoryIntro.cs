using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class StoryIntro : MonoBehaviour
{
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private Image storyImage;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;

    private Sprite[] storySprites;
    private string[] storyLines;
    private int currentIndex = 0;

    private void Awake()
    {
        // Инициализируем массивы
        storySprites = new Sprite[8];
        storyLines = new string[8];

        // Загружаем картинки (0-7)
        for (int i = 0; i < 8; i++)
        {
            string path = $"History/{i}";
            storySprites[i] = Resources.Load<Sprite>(path);

            if (storySprites[i] == null)
            {
                Debug.LogError($"Не удалось загрузить слайд {i} по пути: Resources/{path}");
                Debug.Log($"Проверьте что файл существует: Assets/Resources/{path}.png (или .jpg)");
                Debug.Log($"Тип файла должен быть 'Sprite' в импорт-настройках");
            }
            else
            {
                Debug.Log($"Успешно загружен слайд {i}");
            }
        }

        // Заполняем текст
        storyLines[0] = "Посади цветочек";
        storyLines[1] = "Не дай ему засохнуть";
        storyLines[2] = "Регулярно поддерживай водный баланс";
        storyLines[3] = "Поливай только чистой водой";
        storyLines[4] = "Следи за котом и обстоятельствами";
        storyLines[5] = "Ведь ты никогда не знаешь что может вызвать проблему";
        storyLines[6] = "Помни об этих простых правилах и у тебя все получится";
        storyLines[7] = "Удачи!";

        // Настраиваем кнопки
        nextButton.onClick.AddListener(ShowNextStory);
        closeButton.onClick.AddListener(CloseStory);

        storyCanvas.SetActive(false);
    }

    public void StartStory()
    {
        currentIndex = 0;
        storyCanvas.SetActive(true);
        ShowCurrentStory();
    }

    public void ShowNextStory()
    {
        currentIndex++;
        ShowCurrentStory();
    }

    public void ShowCurrentStory()
    {
        Debug.Log($"ShowCurrentStory - показать слайд {currentIndex}");

        if (currentIndex >= storySprites.Length)
        {
            CloseStory();
            return;
        }

        // Показываем только если есть спрайт
        if (storySprites[currentIndex] != null)
        {
            Debug.Log($"Есть спрайт для слайда {currentIndex}");
            storyImage.sprite = storySprites[currentIndex];
            storyImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Нет спрайта для слайда {currentIndex}");
            storyImage.gameObject.SetActive(false);
        }

        storyText.text = storyLines[currentIndex];
        Debug.Log($"Текущий слайд {currentIndex}");
    }

    private void CloseStory()
    {
        Debug.Log($"CloseStory");
        storyCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}