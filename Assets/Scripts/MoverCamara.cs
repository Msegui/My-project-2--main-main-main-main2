using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoverCamaraPorMouse : MonoBehaviour
{
    public float velocidadDesplazamiento = 5f; // Velocidad a la que se mueve la cámara
    public float umbralBorde = 200f;            // Distancia en píxeles al borde para comenzar a mover la cámara
    public float limiteIzquierdo = -10f;       // Límite izquierdo
    public float limiteDerecho = 10f;          // Límite derecho

    void Update()
    {
        // Obtener la posición del mouse en píxeles (coordenadas de la pantalla)
        Vector3 posicionMouse = Input.mousePosition;

        // Obtiene el ancho de la pantalla
        float anchoPantalla = Screen.width;

        // Mover la cámara a la izquierda si el mouse está cerca del borde izquierdo
        if (posicionMouse.x < umbralBorde && Camera.main.transform.position.x > limiteIzquierdo)
        {
            MoverCamara(-1); // Mover hacia la izquierda
        }

        // Mover la cámara a la derecha si el mouse está cerca del borde derecho
        if (posicionMouse.x > anchoPantalla - umbralBorde && Camera.main.transform.position.x < limiteDerecho)
        {
            MoverCamara(1); // Mover hacia la derecha
        }
    }

    // Método para mover la cámara en el eje horizontal
    void MoverCamara(int direccion)
    {
        // Desplazar la cámara en el eje X dependiendo de la dirección y la velocidad
        Camera.main.transform.position += new Vector3(direccion * velocidadDesplazamiento * Time.deltaTime, 0, 0);
    }
}

