using UnityEngine;

public class FadeToBlackEffect : MonoBehaviour
{
    public float fadeDuration = 3f; // Duración en segundos del fundido
    private SpriteRenderer spriteRenderer;
    private Color color;

    private float timer = 0f;
    private bool isFading = false;

    public SpriteRenderer creditosImage; // Imagen de los créditos
    public float creditosFadeDuration = 2f; // Duración del fade-in de los créditos
    private bool mostrarCreditos = false;
    private float creditosTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Este script necesita estar en un GameObject con un SpriteRenderer.");
            return;
        }

        color = spriteRenderer.color;
        color.a = 0; // Comienza completamente transparente
        spriteRenderer.color = color;

        if (creditosImage != null)
        {
            // Configurar la imagen de los créditos como completamente transparente
            Color creditosColor = creditosImage.color;
            creditosColor.a = 0;
            creditosImage.color = creditosColor;
        }
        else
        {
            Debug.LogWarning("No se ha asignado una imagen para los créditos.");
        }

        StartFade(); // Llama a StartFade automáticamente al inicio
    }

    void Update()
    {
        if (isFading)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration); // Valor entre 0 y 1

            // Incrementa la transparencia a medida que avanza el tiempo
            color.a = progress;
            spriteRenderer.color = color;

            // Detiene el efecto una vez que la transparencia llega a 1 (completamente negro)
            if (progress >= 1f)
            {
                isFading = false;
                mostrarCreditos = true; // Indica que los créditos deben comenzar a aparecer
            }
        }

        if (mostrarCreditos && creditosImage != null)
        {
            creditosTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(creditosTimer / creditosFadeDuration);

            // Incrementa la opacidad de la imagen de los créditos
            Color creditosColor = creditosImage.color;
            creditosColor.a = progress;
            creditosImage.color = creditosColor;

            if (progress >= 1f)
            {
                mostrarCreditos = false; // Finaliza el fade-in de los créditos
            }
        }
    }

    public void StartFade()
    {
        timer = 0f;
        isFading = true;
    }
}