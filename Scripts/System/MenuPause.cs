using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool PauseGame = false; //���� �� ����������
    public GameObject pauseGameMenu;
    public GameObject[] elementsToHide;

    void Update()
    {
        //Esc - ������ ��������� �����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();  // ���� ���� �� �����, ���������� ����
            }
            else
            {
                Pause();  // ���� ���� �� �� �����, ��������� �� �����
            }
        }
    }
    public void Resume()
    {
        PauseGame = false;
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;

        // �������� ������� ��������
        foreach (GameObject element in elementsToHide)
        {
            if (element != null)
                element.SetActive(true);
        }
    }
    public void Pause()
    {
        PauseGame = true;
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;

        // �������� ��� �������� �� ������
        foreach (GameObject element in elementsToHide)
        {
            if (element != null)
                element.SetActive(false);
        }

        //����� = ����� ����������
        // ��� ��������� ��������� ����� �� ��������
        WaterLevel wl = FindFirstObjectByType<WaterLevel>();
        //WaterLevel wl = FindObjectOfType<WaterLevel>(true); // ���� ���� ����������
        if (wl == null)
        {
            Debug.LogError("WaterLevel �� ������ � �����!");
        }
        else
        {
            Debug.Log($"���������� �� MenuPause WaterHP: {wl.WaterHP}");
            wl.SaveWaterHP();
        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
}