using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour
{
    public Sprite[] sprites; // Arreglo para guardar los sprites
    public float intervaloCambio = 0.08f; // Tiempo entre cambios de sprite
    private int indiceActual = 0; // Índice del sprite actual
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private float tiempoTranscurrido = 0f; // Para contar el tiempo entre cambios

    void Start()
    {
        // Obtener el componente SpriteRenderer del objeto al que está adjunto el script
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Asegurarse de que haya sprites cargados
        if (sprites.Length > 0)
        {
            // Establecer el sprite inicial
            spriteRenderer.sprite = sprites[indiceActual];
        }
        else
        {
            Debug.LogError("No se han asignado sprites en el inspector.");
        }
    }

    void Update()
    {
        // Hacer que el objeto siga la posición del mouse
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = 0; // Asegurarse de que el objeto esté en el mismo plano 2D
        transform.position = posicionMouse;

        // Contar el tiempo transcurrido
        tiempoTranscurrido += Time.deltaTime;

        // Cambiar el sprite después de un intervalo determinado
        if (tiempoTranscurrido >= intervaloCambio)
        {
            CambiarSprite();
            tiempoTranscurrido = 0f; // Reinicia el temporizador
        }
    }

    // Método cambiar al siguiente sprite
    void CambiarSprite()
    {
        indiceActual = (indiceActual + 1) % sprites.Length; // Pasar al siguiente sprite y hacer bucle
        spriteRenderer.sprite = sprites[indiceActual]; // Actualizar el sprite en el SpriteRenderer
    }
}
