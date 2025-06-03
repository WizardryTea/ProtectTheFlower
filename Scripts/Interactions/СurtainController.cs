using UnityEngine;

public class CurtainController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        ToggleCurtain();
    }

    public void ToggleCurtain()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
    }
}