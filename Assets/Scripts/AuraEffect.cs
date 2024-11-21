using UnityEngine;

public class AuraEffect : MonoBehaviour
{
    private CircleCollider2D auraCollider;
    private SpriteRenderer spriteRenderer;
    private float maxScale;
    private float duration;
    private float timer = 0f;

    // Parámetros para la animación de sprites
    public Sprite[] explosionSprites; // Array de sprites para la animación
    private int currentSpriteIndex = 0;
    public float animationInterval = 0.05f; // Tiempo entre cambio de cada sprite
    private float animationTimer = 0f; // Temporizador para la animación

    // Tamaño máximo del collider, independiente de la escala visual
    public float colliderMaxRadius = 2f; // Ajusta este valor para cambiar el rango de colisión

    public void Initialize(float maxScale, float duration)
    {
        this.maxScale = maxScale;
        this.duration = duration;
    }

    void Start()
    {
        auraCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        auraCollider.radius = 0.1f; // Comienza con un radio pequeño

        // Configura el primer sprite de la explosión si hay sprites asignados
        if (explosionSprites.Length > 0)
        {
            spriteRenderer.sprite = explosionSprites[0];
        }
    }

    void Update()
    {
        // Expansión del aura
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0.1f, maxScale, timer / duration);
            transform.localScale = new Vector3(scale, scale, 1);

            // Expandir el collider de acuerdo con la duración hasta el tamaño máximo
            auraCollider.radius = Mathf.Lerp(0.1f, colliderMaxRadius, timer / duration);
        }

        // Animación de la explosión usando sprites
        if (explosionSprites.Length > 0)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= animationInterval)
            {
                // Avanza al siguiente sprite en el arreglo
                currentSpriteIndex = (currentSpriteIndex + 1) % explosionSprites.Length;
                spriteRenderer.sprite = explosionSprites[currentSpriteIndex];
                animationTimer = 0f; // Reinicia el temporizador
            }
        }

        // Desvanecer y destruir el aura después del tiempo total de duración
        if (timer >= duration)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime / 0.5f; // Ajusta el tiempo de desvanecimiento
            spriteRenderer.color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Calcular dirección de repulsión
            Vector2 direction = (other.transform.position - transform.position).normalized;
            float force = 5f; // Ajusta la fuerza de empuje
            other.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);

            // Llamar a RegresarAPosicionInicial en el enemigo
            MovimientoEnemigo enemigo = other.GetComponent<MovimientoEnemigo>();
            if (enemigo != null)
            {
                enemigo.RegresarAPosicionInicial();
            }
        }
    }
}