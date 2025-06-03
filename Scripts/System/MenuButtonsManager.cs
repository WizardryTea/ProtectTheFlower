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
        // ��������� ������ ����������
        Inventory savedData = GameDataManager.LoadInventory();
        bool isNewGame = savedData == null || savedData.newGameReload;

        // ��������� ���������� ������
        BtnStartGame.gameObject.SetActive(isNewGame);
        BtnStartGame2.gameObject.SetActive(!isNewGame);
        BtnContinueGame.gameObject.SetActive(!isNewGame);

        // �������������� ������ (���� �����)
        if (isNewGame)
        {
            Debug.Log("���������� ����������� ���� (����� ����)");
        }
        else
        {
            Debug.Log("���������� ����������� ���� (�����������)");
        }
    }
}