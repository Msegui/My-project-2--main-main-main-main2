using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAnimacion : MonoBehaviour
{
    public Sprite[] sprites; // Arreglo de sprites para la animación normal
    public Sprite[] spritesRegreso; // Arreglo de sprites para la animación de regreso
    public float intervaloCambio = 0.5f; // Intervalo de tiempo entre cambios de sprite
    private int indiceActual = 0; // Índice del sprite actual
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private float tiempoTranscurrido = 0f; // Control del tiempo entre cambios
    private bool enRegreso = false; // Estado que indica si está regresando
    public AudioClip sonidoCristalRobado;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verificar que haya sprites asignados
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[indiceActual];
        }
        else
        {
            Debug.LogError("No se han asignado sprites en el inspector.");
        }
        audioSource  = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonidoCristalRobado;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= intervaloCambio)
        {
            CambiarSprite();
            tiempoTranscurrido = 0f; // Reiniciar el temporizador
        }
    }

    // Método para cambiar al siguiente sprite en el arreglo correspondiente
    void CambiarSprite()
    {
        // Usar el arreglo de sprites adecuado dependiendo del estado
        Sprite[] arregloActual = enRegreso ? spritesRegreso : sprites;

        // Avanzar al siguiente sprite en el arreglo, haciendo bucle al principio si es necesario
        indiceActual = (indiceActual + 1) % arregloActual.Length;
        spriteRenderer.sprite = arregloActual[indiceActual];
    }

    // Método para activar el estado de regreso
    public void ActivarModoRegreso()
    {
        enRegreso = true;
        indiceActual = 0; // Reiniciar el índice de animación al cambiar de modo
        if (audioSource != null && sonidoCristalRobado != null)
            {
                audioSource.Play();
               
            }
    }

    // Método para activar el estado normal
    public void ActivarModoNormal()
    {
        enRegreso = false;
        indiceActual = 0; // Reiniciar el índice de animación al cambiar de modo
    }
}
