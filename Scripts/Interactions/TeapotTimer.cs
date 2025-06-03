using TMPro;
using UnityEngine;

public class TeapotTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;       // TextMeshProUGUI
    //public TextMeshPro timerText; // TextMeshPro

    //�������
    public ParticleSystem firstEffect;
    public ParticleSystem secondEffect;     //���������� ����� 15 ���

    //�����
    public AudioSource lowBoilAudio;
    public AudioSource highBoilAudio;

    private float countdown = 15f;
    private bool timerActive = false;
    private bool effectPlayed = false;

    void Start()
    {
        //������ ������
        if (timerText != null)
            timerText.gameObject.SetActive(false);
        //������� ��������� � ������
        if (firstEffect != null)
            firstEffect.Stop();

        if (secondEffect != null)
            secondEffect.Stop();

        //����� ��������� � ������
        if (lowBoilAudio != null)
            lowBoilAudio.Stop();

        if (highBoilAudio != null)
            highBoilAudio.Stop();
    }

    void Update()
    {
        //Debug.Log("������ ������: " + countdown);
        if (!timerActive || effectPlayed)
            return;

        countdown -= Time.deltaTime;
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(countdown).ToString();

        if (countdown <= 0f)
        {
            countdown = 0f;
            timerActive = false;
            effectPlayed = true;

            // ��������� ������ ������ � ���������� � I
            //if (secondEffect != null)
            //    secondEffect.Play();

            if (firstEffect != null)
                firstEffect.Stop();

            if (secondEffect != null)
                secondEffect.Play();

            if (lowBoilAudio != null)
                lowBoilAudio.Stop();

            if (highBoilAudio != null)
                highBoilAudio.Play();

            if (timerText != null)
            {
                timerText.text = "0";
                timerText.gameObject.SetActive(false); // �������� ������ ����� ���������� �������
            }
        }
    }

    public void StartTimer()
    {
        countdown = 15f;
        timerActive = true;
        effectPlayed = false;

        if (secondEffect != null)
            secondEffect.Stop();

        if (lowBoilAudio != null)
            lowBoilAudio.Play();

        if (highBoilAudio != null)
            highBoilAudio.Stop();

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = "15";
        }
    }

    public void StopTimer()
    {
        timerActive = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (lowBoilAudio != null)
            lowBoilAudio.Stop();

        if (highBoilAudio != null)
            highBoilAudio.Stop();

        //�������� ��� �������� 
        if (secondEffect != null)
            secondEffect.Stop();
    }

    //��� ������������
    public void ToggleEffects()
    {
        if (firstEffect != null && secondEffect != null)
        {
            if (firstEffect.isPlaying)
            {
                firstEffect.Stop();
                if (lowBoilAudio != null) lowBoilAudio.Stop();

                secondEffect.Play();
                if (highBoilAudio != null) highBoilAudio.Play();
            }
            else if (secondEffect.isPlaying)
            {
                secondEffect.Stop();
                if (highBoilAudio != null) highBoilAudio.Stop();

                firstEffect.Play();
                if (lowBoilAudio != null) lowBoilAudio.Play();
            }
        }
    }
}