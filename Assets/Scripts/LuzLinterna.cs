using System.Collections;
using UnityEngine;

public class LuzLinterna : MonoBehaviour
{
    public Transform personaje; // El personaje que sigue la luz
    public Vector2 offset; // Ajuste para posicionar la luz con respecto al mouse o personaje
    public Sprite[] secuenciaSprites; // Arreglo de sprites para la secuencia de la luz
    public float intervaloCambioSprite = 0.1f; // Tiempo entre cada cambio de sprite

    private SpriteRenderer spriteRenderer;
    private int indiceSpriteActual = 0; // Índice para seguir el sprite actual
    private float contadorTiempo = 0f; // Contador para el tiempo transcurrido

    void Start()
    {
        // Obtiene el SpriteRenderer del objeto para modificar los sprites
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Verificar que el arreglo de sprites esté cargado
        if (secuenciaSprites.Length > 0)
        {
            spriteRenderer.sprite = secuenciaSprites[0];
        }
        else
        {
            Debug.LogWarning("No se han asignado sprites para la secuencia de la luz.");
        }
    }

    void Update()
    {
        // Obtener la posición del mouse
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = 0; // Mantener el plano en 2D

        // Posicionar la máscara de oscuridad en la posición del mouse con el offset
        transform.position = posicionMouse + (Vector3)offset;

        // Actualizar la secuencia de sprites
        contadorTiempo += Time.deltaTime;
        if (contadorTiempo >= intervaloCambioSprite)
        {
            CambiarSprite();
            contadorTiempo = 0f;
        }
    }

    void CambiarSprite()
    {
        // Cambia al siguiente sprite en la secuencia
        indiceSpriteActual = (indiceSpriteActual + 1) % secuenciaSprites.Length;
        spriteRenderer.sprite = secuenciaSprites[indiceSpriteActual];
    }
}
