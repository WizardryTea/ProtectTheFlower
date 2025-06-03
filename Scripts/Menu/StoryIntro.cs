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
        // �������������� �������
        storySprites = new Sprite[8];
        storyLines = new string[8];

        // ��������� �������� (0-7)
        for (int i = 0; i < 8; i++)
        {
            string path = $"History/{i}";
            storySprites[i] = Resources.Load<Sprite>(path);

            if (storySprites[i] == null)
            {
                Debug.LogError($"�� ������� ��������� ����� {i} �� ����: Resources/{path}");
                Debug.Log($"��������� ��� ���� ����������: Assets/Resources/{path}.png (��� .jpg)");
                Debug.Log($"��� ����� ������ ���� 'Sprite' � ������-����������");
            }
            else
            {
                Debug.Log($"������� �������� ����� {i}");
            }
        }

        // ��������� �����
        storyLines[0] = "������ ��������";
        storyLines[1] = "�� ��� ��� ���������";
        storyLines[2] = "��������� ����������� ������ ������";
        storyLines[3] = "������� ������ ������ �����";
        storyLines[4] = "����� �� ����� � ����������������";
        storyLines[5] = "���� �� ������� �� ������ ��� ����� ������� ��������";
        storyLines[6] = "����� �� ���� ������� �������� � � ���� ��� ���������";
        storyLines[7] = "�����!";

        // ����������� ������
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
        Debug.Log($"ShowCurrentStory - �������� ����� {currentIndex}");

        if (currentIndex >= storySprites.Length)
        {
            CloseStory();
            return;
        }

        // ���������� ������ ���� ���� ������
        if (storySprites[currentIndex] != null)
        {
            Debug.Log($"���� ������ ��� ������ {currentIndex}");
            storyImage.sprite = storySprites[currentIndex];
            storyImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"��� ������� ��� ������ {currentIndex}");
            storyImage.gameObject.SetActive(false);
        }

        storyText.text = storyLines[currentIndex];
        Debug.Log($"������� ����� {currentIndex}");
    }

    private void CloseStory()
    {
        Debug.Log($"CloseStory");
        storyCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}