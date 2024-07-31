using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    public static ControladorSonido Instance;

    private AudioSource audioSource;

    // There is only one ControladorSonido in the scene.
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    // Other classes call this method when playing a sound.
    public void playSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
