using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    // Modified method to accept a trigger name as a parameter
    public void TriggerAnimation(string triggerName)
    {
        if (animator != null && !string.IsNullOrEmpty(triggerName))
        {
            animator.SetTrigger(triggerName);
        }
    }
}
