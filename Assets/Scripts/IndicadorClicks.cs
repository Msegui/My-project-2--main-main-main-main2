using UnityEngine;

public class IndicadorClicks : MonoBehaviour
{
    public Sprite[] indicadoresClickIzquierdo; // Arreglo de sprites para el clic izquierdo
    public Sprite[] indicadoresClickDerecho;   // Arreglo de sprites para el clic derecho

    public float desplazamientoXIzquierdo = -0.5f; // Ajuste para el sprite del clic izquierdo
    public float desplazamientoXDerecho = 0.5f;    // Ajuste para el sprite del clic derecho

    public float velocidadSecuencia = 0.1f; // Velocidad de cambio entre sprites
    public float escalaMaxima = 1.5f;       // Tamaño más grande en los extremos
    public float escalaMinima = 0.7f;       // Tamaño más pequeño en el centro de la secuencia

    private SpriteRenderer spriteRenderer;
    private int indiceIzquierdo = 0;
    private int indiceDerecho = 0;
    private float tiempoCambioSprite = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null; // Comienza sin ningún sprite visible
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        tiempoCambioSprite += Time.deltaTime;

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            spriteRenderer.sprite = null;
        }
        else if (Input.GetMouseButton(0) && indicadoresClickIzquierdo.Length > 0)
        {
            if (tiempoCambioSprite >= velocidadSecuencia)
            {
                tiempoCambioSprite = 0f;
                indiceIzquierdo = (indiceIzquierdo + 1) % indicadoresClickIzquierdo.Length;
                spriteRenderer.sprite = indicadoresClickIzquierdo[indiceIzquierdo];

                AjustarEscala(indicadoresClickIzquierdo, indiceIzquierdo);
            }
            transform.position = mousePosition + new Vector3(desplazamientoXIzquierdo, 0, 0);
        }
        else if (Input.GetMouseButton(1) && indicadoresClickDerecho.Length > 0)
        {
            if (tiempoCambioSprite >= velocidadSecuencia)
            {
                tiempoCambioSprite = 0f;
                indiceDerecho = (indiceDerecho + 1) % indicadoresClickDerecho.Length;
                spriteRenderer.sprite = indicadoresClickDerecho[indiceDerecho];

                AjustarEscala(indicadoresClickDerecho, indiceDerecho);
            }
            transform.position = mousePosition + new Vector3(desplazamientoXDerecho, 0, 0);
        }
        else
        {
            spriteRenderer.sprite = null;
            tiempoCambioSprite = 0f;
            indiceIzquierdo = 0;
            indiceDerecho = 0;
        }
    }

    void AjustarEscala(Sprite[] arregloSprites, int indiceActual)
    {
        // Calcular el tamaño en función de la posición en la secuencia
        int puntoMedio = arregloSprites.Length / 2;
        float escala;

        if (indiceActual <= puntoMedio)
        {
            // De mayor a menor hasta el centro
            escala = Mathf.Lerp(escalaMaxima, escalaMinima, (float)indiceActual / puntoMedio);
        }
        else
        {
            // De menor a mayor desde el centro hasta el final
            escala = Mathf.Lerp(escalaMinima, escalaMaxima, (float)(indiceActual - puntoMedio) / puntoMedio);
        }

        transform.localScale = new Vector3(escala, escala, 1);
    }
}
