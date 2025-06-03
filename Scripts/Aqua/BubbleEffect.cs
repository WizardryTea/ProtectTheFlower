using UnityEngine;

public class BubbleEffect : MonoBehaviour
{
    private ParticleSystem bubbleParticles;

    void Start()
    {
        bubbleParticles = GetComponent<ParticleSystem>();

        PlayBubbles();
    }

    public void PlayBubbles()
    {
        if (bubbleParticles != null)
        {
            bubbleParticles.Play();
            //Debug.Log("Пузыри активированы!");
        }
    }

    public void StopBubbles()
    {
        if (bubbleParticles != null)
        {
            bubbleParticles.Stop();
        }
    }
}