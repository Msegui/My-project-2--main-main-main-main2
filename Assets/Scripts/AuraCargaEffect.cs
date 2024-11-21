using UnityEngine;

public class AuraCargaEffect : MonoBehaviour
{
    public Sprite[] cargaSprites; // Arreglo de sprites para la animación de carga
    private SpriteRenderer spriteRenderer;
    private float animSpeed = 0.1f; // Tiempo entre cambios de sprite
    private int currentSpriteIndex = 0;
    private float animTimer = 0f;

    private float maxScale;
    private float duration;
    private float timer = 0f;

    public void Initialize(float maxScale, float duration)
    {
        this.maxScale = maxScale;
        this.duration = duration;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (cargaSprites.Length > 0)
        {
            spriteRenderer.sprite = cargaSprites[0]; // Inicia con el primer sprite
        }
        else
        {
            Debug.LogError("No se han asignado sprites de carga en el inspector.");
        }
    }

    void Update()
    {
        // Expansión y actualización de la secuencia de sprites
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(maxScale, 0.1f, timer / duration); // Se reduce el tamaño progresivamente
            transform.localScale = new Vector3(scale, scale, 1);

            // Control de la animación de sprites
            animTimer += Time.deltaTime;
            if (animTimer >= animSpeed)
            {
                animTimer = 0f;
                currentSpriteIndex = (currentSpriteIndex + 1) % cargaSprites.Length;
                spriteRenderer.sprite = cargaSprites[currentSpriteIndex];
            }
        }
        else
        {
            // Destruir la carga una vez que termina su duración
            Destroy(gameObject);
        }
    }
}