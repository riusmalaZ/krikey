using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Vérifier si la liste de clips audio n'est pas vide
        if (musicClips.Length > 0)
        {
            // Jouer une musique aléatoire au démarrage
            PlayRandomMusic();
        }
        else
        {
            Debug.LogWarning("Aucun clip audio n'est spécifié dans la liste.");
        }
    }

    // Fonction pour jouer une musique aléatoire
    public void PlayRandomMusic()
    {
        // Générer un index aléatoire dans la plage des indices de la liste
        int randomIndex = Random.Range(0, musicClips.Length);

        // Sélectionner le clip audio correspondant à l'index aléatoire
        AudioClip randomClip = musicClips[randomIndex];

        // Jouer le clip audio sélectionné
        audioSource.clip = randomClip;
        audioSource.Play();
    }
}
