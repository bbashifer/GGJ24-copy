using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private Transform listener;

    [Header("Audio Settings")]
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;
    public float volumeMultiplier = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject!");
        }

        // Set other audio settings for spatial sound
        audioSource.spatialBlend = 1.0f;  // 3D spatial sound
        audioSource.rolloffMode = AudioRolloffMode.Linear;  // Linear rolloff for distance attenuation

        // Assuming the listener is the main camera, you can modify this if needed
    }

    void Update()
    {
        // Check if the listener is set
        if (listener != null)
        {
            // Calculate the distance between the audio source and the listener
            float distance = Vector3.Distance(transform.position, listener.position);

            // Adjust the volume based on distance
            float adjustedVolume = Mathf.Clamp01(1.0f - (distance - minDistance) / (maxDistance - minDistance));
            audioSource.volume = adjustedVolume * volumeMultiplier;
        }
        else
        {
            Debug.LogError("Listener not set! Make sure to assign the listener.");
        }
    }
}


