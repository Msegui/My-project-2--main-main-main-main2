using UnityEngine;

public class FinDelJuegoAnimation : MonoBehaviour
{
    public Sprite[] secuencia1; // Array de sprites para la primera secuencia
    public Sprite[] secuencia2; // Array de sprites para la segunda secuencia
    public Sprite[] secuencia3; // Array de sprites para la tercera secuencia
    public Sprite[] secuencia4; // Array de sprites para la cuarta secuencia

    public SpriteRenderer renderer1; // SpriteRenderer del primer objeto de la primera secuencia
    public SpriteRenderer renderer2; // SpriteRenderer del segundo objeto de la segunda secuencia
    public SpriteRenderer renderer3; // SpriteRenderer del tercer objeto de la tercera secuencia
    public SpriteRenderer renderer4; // SpriteRenderer del cuarto objeto de la cuarta secuencia

    public float frameDuration = 0.2f; // Duración de cada frame en segundos
    private float timer = 0f;
    private int currentFrame = 0;

    void Start()
    {
        // Asegúrate de que cada secuencia tenga 5 sprites
        if (secuencia1.Length < 5 || secuencia2.Length < 5 || secuencia3.Length < 5 || secuencia4.Length < 5)
        {
            Debug.LogError("Cada secuencia debe tener al menos 5 sprites.");
            return;
        }

        // Inicializa el primer sprite en cada SpriteRenderer
        renderer1.sprite = secuencia1[0];
        renderer2.sprite = secuencia2[0];
        renderer3.sprite = secuencia3[0];
        renderer4.sprite = secuencia4[0];
    }

    void Update()
    {
        // Temporizador para cambiar el frame
        timer += Time.deltaTime;
        if (timer >= frameDuration)
        {
            timer = 0f; // Reinicia el temporizador

            // Cambia al siguiente frame de cada secuencia
            currentFrame = (currentFrame + 1) % 5; // Usamos el módulo 5 para que sea un loop de 5 frames

            // Actualiza el sprite de cada secuencia en paralelo
            renderer1.sprite = secuencia1[currentFrame];
            renderer2.sprite = secuencia2[currentFrame];
            renderer3.sprite = secuencia3[currentFrame];
            renderer4.sprite = secuencia4[currentFrame];
        }
    }
}