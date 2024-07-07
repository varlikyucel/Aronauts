using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip hoverSound; // Assign in the inspector
    public AudioClip selectSound; // Assign in the inspector
    public float hoverSoundCooldown = 1.0f; // Time in seconds before hover sound can be played again

    private AudioSource audioSource;
    private float lastHoverSoundTime = -Mathf.Infinity; // Initialize to allow immediate play

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHoverSound()
    {
        // Check if the selected sound is playing or if hover sound is on cooldown
        if (!IsPlayingSelectSound() && Time.time >= lastHoverSoundTime + hoverSoundCooldown)
        {
            PlaySound(hoverSound);
            lastHoverSoundTime = Time.time; // Update the last played time
        }
    }

    public void PlaySelectSound()
    {
        PlaySound(selectSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying)
            audioSource.Stop(); // Stop any currently playing sound

        audioSource.clip = clip;
        audioSource.Play();
    }

    private bool IsPlayingSelectSound()
    {
        return audioSource.isPlaying && audioSource.clip == selectSound;
    }
}
