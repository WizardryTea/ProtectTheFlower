using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsManager : MonoBehaviour
{ 
    public Button BtnStartGame;
    public Button BtnStartGame2;
    public Button BtnContinueGame;

    private void Start()
    {
        UpdateButtonsVisibility();
    }

    public void UpdateButtonsVisibility()
    {
        // Загружаем данные сохранения
        Inventory savedData = GameDataManager.LoadInventory();
        bool isNewGame = savedData == null || savedData.newGameReload;

        // Управляем видимостью кнопок
        BtnStartGame.gameObject.SetActive(isNewGame);
        BtnStartGame2.gameObject.SetActive(!isNewGame);
        BtnContinueGame.gameObject.SetActive(!isNewGame);

        // Дополнительная логика (если нужно)
        if (isNewGame)
        {
            Debug.Log("Показываем стандартное меню (новая игра)");
        }
        else
        {
            Debug.Log("Показываем расширенное меню (продолжение)");
        }
    }
}