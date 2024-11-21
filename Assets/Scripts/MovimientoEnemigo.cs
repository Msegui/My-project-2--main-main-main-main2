using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidadMovimiento = 2f;
    public float cambioDireccionIntervalo = 3f;
    public Vector2 limitesMapa = new Vector2(20f, 5f);
    public float tiempoIrAlCentro = 20f;
    public float frecuenciaMinima = 5f;

    private Vector2 direccionMovimiento;
    private float tiempoDesdeUltimoCambio = 0f;
    private float tiempoContador = 0f;
    private Vector3 posicionInicial;
    private bool enCaminoAlCentro = false;
    private bool regresandoAlOrigen = false;
    private bool empujadoPorAura = false;

    private Transform objetivoPilar;
    private EnemigoAnimacion enemigoAnimacion; // Referencia al script de animación

    void Start()
    {
        posicionInicial = transform.position;
        CambiarDireccion();

        // Buscar el objeto "Pilar" en la escena
        GameObject pilarObj = GameObject.Find("Pilar");
        if (pilarObj != null)
        {
            objetivoPilar = pilarObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto 'Pilar' en la escena. Asegúrate de que el objeto esté presente y tenga el nombre correcto.");
        }

        // Obtener el componente EnemigoAnimacion
        enemigoAnimacion = GetComponent<EnemigoAnimacion>();
    }

    void Update()
    {
        if (!empujadoPorAura)
        {
            tiempoContador += Time.deltaTime;

            if (tiempoContador >= tiempoIrAlCentro && !enCaminoAlCentro && !regresandoAlOrigen)
            {
                enCaminoAlCentro = true;
            }

            if (enCaminoAlCentro)
            {
                MoverHaciaObjetivo(objetivoPilar.position);

                if (Vector3.Distance(transform.position, objetivoPilar.position) < 0.5f)
                {
                    enCaminoAlCentro = false;
                    regresandoAlOrigen = true;

                    // Cambia a modo regreso en el script de animación
                    if (enemigoAnimacion != null)
                    {
                        enemigoAnimacion.ActivarModoRegreso();
                    }
                }
            }
            else if (regresandoAlOrigen)
            {
                MoverHaciaObjetivo(posicionInicial);

                if (Vector3.Distance(transform.position, posicionInicial) < 0.5f)
                {
                    regresandoAlOrigen = false;
                    tiempoContador = 0f;

                    // Vuelve al modo normal en el script de animación
                    if (enemigoAnimacion != null)
                    {
                        enemigoAnimacion.ActivarModoNormal();
                    }

                    ActualizarFrecuenciaIrAlCentro();
                }
            }
            else
            {
                MovimientoNormal();
            }
        }
        else
        {
            MoverHaciaObjetivo(posicionInicial);

            if (Vector3.Distance(transform.position, posicionInicial) < 0.5f)
            {
                empujadoPorAura = false;
            }
        }
    }

    void MovimientoNormal()
    {
        transform.Translate(direccionMovimiento * velocidadMovimiento * Time.deltaTime);
        LimitarPosicion();

        tiempoDesdeUltimoCambio += Time.deltaTime;
        if (tiempoDesdeUltimoCambio >= cambioDireccionIntervalo)
        {
            CambiarDireccion();
            tiempoDesdeUltimoCambio = 0f;
        }
    }

    void CambiarDireccion()
    {
        float anguloAleatorio = Random.Range(0f, 360f);
        direccionMovimiento = new Vector2(Mathf.Cos(anguloAleatorio), Mathf.Sin(anguloAleatorio)).normalized;
    }

    void LimitarPosicion()
    {
        Vector3 posicionActual = transform.position;

        posicionActual.x = Mathf.Clamp(posicionActual.x, -limitesMapa.x, limitesMapa.x);
        posicionActual.y = Mathf.Clamp(posicionActual.y, -limitesMapa.y, limitesMapa.y);

        transform.position = posicionActual;
    }

    void MoverHaciaObjetivo(Vector3 objetivo)
    {
        Vector3 direccion = (objetivo - transform.position).normalized;
        transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
    }

    public void RegresarAPosicionInicial()
    {
        empujadoPorAura = true;
        enCaminoAlCentro = false;
        regresandoAlOrigen = false;
    }

    void ActualizarFrecuenciaIrAlCentro()
    {
        tiempoIrAlCentro = Mathf.Max(tiempoIrAlCentro * 0.9f, frecuenciaMinima);
        Debug.Log("Nuevo tiempoIrAlCentro: " + tiempoIrAlCentro);
    }
}