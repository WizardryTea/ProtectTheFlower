using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OvenInteraction : MonoBehaviour
{
    public ParticleSystem smokeParticlesTeapot; // система частиц из чайника
    public TeapotTimer teapotTimer; // ссылка на таймер чайника

    private bool isSmokeOn = false;
    private bool isTimerFinished = false;
    private float timerFinishTime;
    private const float DeathDelay = 5f; // 5 секунд после окончания таймера

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
        // Проверяем завершение таймера через флаг effectPlayed
        if (teapotTimer != null && !isTimerFinished)
        {
            // Используем reflection для доступа к приватному полю (не лучшая практика)
            // Лучше добавить public свойство в TeapotTimer
            var effectPlayedField = typeof(TeapotTimer).GetField("effectPlayed",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (effectPlayedField != null && (bool)effectPlayedField.GetValue(teapotTimer))
            {
                isTimerFinished = true;
                timerFinishTime = Time.time;
            }
        }

        // Если таймер закончился и прошло 5 секунд, и дым всё ещё идёт
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
        // Устанавливаем причину смерти
        Inventory inv = GameDataManager.LoadInventory() ?? new Inventory();
        inv.deathReasonKey = "Fire";
        inv.showDeathMessage = true;
        GameDataManager.SaveInventory(inv);

        // Загружаем сцену поражения
        SceneManager.LoadScene("GameOverScene");
    }
}