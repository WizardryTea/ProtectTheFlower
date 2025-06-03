using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera KitchenCamera;
    public Camera grannyCamera;

    //cпецкамеры
    public Camera WindowCamera;
    public Camera flowerCloseCamera;

    //кнопки для спецкамер
    public Button FlowerEnterBtn;
    public Button FlowerExitBtn;
    public Button WindowEnterBtn;
    public Button WindowExitBtn;

    private void Start()
    {
        // Инициализируем
        FlowerEnterBtn.onClick.AddListener(() => SwitchToFlowerClose());
        FlowerExitBtn.onClick.AddListener(() => SwitchToMainRoom());

        WindowEnterBtn.onClick.AddListener(() => SwitchToWindow());
        WindowExitBtn.onClick.AddListener(() => SwitchToMainRoom());

        // Скрываем кнопки выхода по умолчанию
        FlowerExitBtn.gameObject.SetActive(false);
        WindowExitBtn.gameObject.SetActive(false);

        SwitchToMainRoom();
    }
    /*Между комнатами*/
    public void SwitchToKitchen()
    {
        SetActiveCamera(KitchenCamera);
        HideAllSpecialButtons();
    }

    public void SwitchToGrannyRoom()
    {
        SetActiveCamera(grannyCamera);
        HideAllSpecialButtons();
    }

    public void SwitchToMainRoom()
    {
        SetActiveCamera(mainCamera);
        ShowEnterButtons();
        HideExitButtons();
    }

    private void SetActiveCamera(Camera newCamera)
    {
        mainCamera.gameObject.SetActive(false);
        KitchenCamera.gameObject.SetActive(false);
        grannyCamera.gameObject.SetActive(false);
        flowerCloseCamera.gameObject.SetActive(false);
        WindowCamera.gameObject.SetActive(false);

        newCamera.gameObject.SetActive(true);
    }

    //скрипт для приближения к цветку
    //логика - скрипт для приближения к окну


    /*Спецкамеры приближения*/
    public void SwitchToFlowerClose()
    {
        SetActiveCamera(flowerCloseCamera);
        ShowExitButton(FlowerExitBtn);
        HideEnterButtons();
    }

    public void SwitchToWindow()
    {
        SetActiveCamera(WindowCamera);
        ShowExitButton(WindowExitBtn);
        HideEnterButtons();
    }

    /* Управление видимостью кнопок */
    private void ShowEnterButtons()
    {
        FlowerEnterBtn.gameObject.SetActive(true);
        WindowEnterBtn.gameObject.SetActive(true);
    }

    private void HideEnterButtons()
    {
        FlowerEnterBtn.gameObject.SetActive(false);
        WindowEnterBtn.gameObject.SetActive(false);
    }

    private void ShowExitButton(Button exitBtn)
    {
        exitBtn.gameObject.SetActive(true);
        // Скрываем другой exit button на случай если он был активен
        if (exitBtn == FlowerExitBtn)
        {
            WindowExitBtn.gameObject.SetActive(false);
        }
        else
        {
            FlowerExitBtn.gameObject.SetActive(false);
        }
    }

    private void HideExitButtons()
    {
        FlowerExitBtn.gameObject.SetActive(false);
        WindowExitBtn.gameObject.SetActive(false);
    }

    private void HideAllSpecialButtons()
    {
        HideEnterButtons();
        HideExitButtons();
    }
}
