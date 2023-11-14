using System.Collections.Generic;
using UnityEngine;

public class EfectosDeSonido : MonoBehaviour
{
    private AudioSource audio_source;
    [SerializeField] private List<AudioClip> sonidos;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(string nombre)
    {
        int index = sonidos.FindIndex(i => i.name == nombre);
        audio_source.clip = sonidos[index];
        audio_source.Play();
    }
}
