using UnityEngine;
using System;

public class Trophy : MonoBehaviour
{
    private Animator animator;

    public static event Action OnTrophyReached;
    private bool alreadyTriggered = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyTriggered) return;

        if (collision.CompareTag("Player"))
        {
            alreadyTriggered = true;
            animator.SetTrigger("Pressed");
        }
    }
    public void OnPickupAnimationFinished()
    {
        OnTrophyReached?.Invoke();
    }
}