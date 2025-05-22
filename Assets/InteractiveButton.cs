using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractiveButton : MonoBehaviour
{
    private Animator animator;
    private static readonly int KlikHash = Animator.StringToHash("klik");

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing!", gameObject);
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleButtonState();
        }
    }

    void ToggleButtonState()
    {
        bool currentState = animator.GetBool(KlikHash);
        animator.SetBool(KlikHash, !currentState);

        Debug.Log($"Button state: {!currentState}", gameObject);
    }
}