using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera KitchenCamera;
    public Camera grannyCamera;

    //c���������
    public Camera WindowCamera;
    public Camera flowerCloseCamera;

    //������ ��� ���������
    public Button FlowerEnterBtn;
    public Button FlowerExitBtn;
    public Button WindowEnterBtn;
    public Button WindowExitBtn;

    private void Start()
    {
        // ��������������
        FlowerEnterBtn.onClick.AddListener(() => SwitchToFlowerClose());
        FlowerExitBtn.onClick.AddListener(() => SwitchToMainRoom());

        WindowEnterBtn.onClick.AddListener(() => SwitchToWindow());
        WindowExitBtn.onClick.AddListener(() => SwitchToMainRoom());

        // �������� ������ ������ �� ���������
        FlowerExitBtn.gameObject.SetActive(false);
        WindowExitBtn.gameObject.SetActive(false);

        SwitchToMainRoom();
    }
    /*����� ���������*/
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

    //������ ��� ����������� � ������
    //������ - ������ ��� ����������� � ����


    /*���������� �����������*/
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

    /* ���������� ���������� ������ */
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
        // �������� ������ exit button �� ������ ���� �� ��� �������
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
