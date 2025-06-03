using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OvenInteraction : MonoBehaviour
{
    public ParticleSystem smokeParticlesTeapot; // ������� ������ �� �������
    public TeapotTimer teapotTimer; // ������ �� ������ �������

    private bool isSmokeOn = false;
    private bool isTimerFinished = false;
    private float timerFinishTime;
    private const float DeathDelay = 5f; // 5 ������ ����� ��������� �������

    private void Start()
    {
        if (smokeParticlesTeapot != null)
        {
            smokeParticlesTeapot.Stop();
        }
        if (teapotTimer != null)
        {
            teapotTimer.StopTimer();
        }
    }

    private void Update()
    {
        // ��������� ���������� ������� ����� ���� effectPlayed
        if (teapotTimer != null && !isTimerFinished)
        {
            // ���������� reflection ��� ������� � ���������� ���� (�� ������ ��������)
            // ����� �������� public �������� � TeapotTimer
            var effectPlayedField = typeof(TeapotTimer).GetField("effectPlayed",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (effectPlayedField != null && (bool)effectPlayedField.GetValue(teapotTimer))
            {
                isTimerFinished = true;
                timerFinishTime = Time.time;
            }
        }

        // ���� ������ ���������� � ������ 5 ������, � ��� �� ��� ���
        if (isTimerFinished && isSmokeOn && Time.time >= timerFinishTime + DeathDelay)
        {
            TriggerFireDeath();
        }
    }

    private void OnMouseDown()
    {
        if (smokeParticlesTeapot == null || teapotTimer == null)
            return;

        if (!isSmokeOn)
        {
            smokeParticlesTeapot.Play();
            teapotTimer.StartTimer();
            isSmokeOn = true;
            isTimerFinished = false;
        }
        else
        {
            smokeParticlesTeapot.Stop();
            teapotTimer.StopTimer();
            isSmokeOn = false;
            isTimerFinished = false;
        }
    }

    private void TriggerFireDeath()
    {
        // ������������� ������� ������
        Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
        inv.deathReasonKey = "Fire";
        inv.showDeathMessage = true;
        GameDataManager.SaveInventory(inv);

        // ��������� ����� ���������
        SceneManager.LoadScene("GameOverScene");
    }
}