using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool PauseGame = false; //выкл по умолчаниюц
    public GameObject pauseGameMenu;
    public GameObject[] elementsToHide;

    void Update()
    {
        //Esc - внопка включени€ паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();  // если игра на паузе, продолжить игру
            }
            else
            {
                Pause();  // если игра не на паузе, поставить на паузу
            }
        }
    }
    public void Resume()
    {
        PauseGame = false;
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;

        // ¬ключаем скрытые элементы
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

        // —крываем все элементы из списка
        foreach (GameObject element in elementsToHide)
        {
            if (element != null)
                element.SetActive(false);
        }

        //пауза = вызов сохранени€
        // дл€ нескрытых элементов поиск не работает
        WaterLevel wl = FindFirstObjectByType<WaterLevel>();
        //WaterLevel wl = FindObjectOfType<WaterLevel>(true); // »щем даже неактивные
        if (wl == null)
        {
            Debug.LogError("WaterLevel не найден в сцене!");
        }
        else
        {
            Debug.Log($"—охранение из MenuPause WaterHP: {wl.WaterHP}");
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