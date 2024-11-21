using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 currentPosition;
    public  MouseDrag arrastre;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) // Mientras se mantiene presionado
        {
            currentPosition = Input.mousePosition;
            Vector3 dragDelta = currentPosition - startPosition;
            // Aquí puedes hacer algo con el arrastre
            Debug.Log("Arrastre: " + dragDelta);
           
        }

        if (Input.GetMouseButtonUp(0)) // Cuando se suelta el botón
        {
            Debug.Log("Arrastre finalizado");
        }
    }
}