using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject prefabEnemigo; // Prefab del enemigo
    public int cantidadEnemigos = 6; // Número de enemigos a generar
    public Vector2 limitesMapa = new Vector2(13f, 2f); // Límites del mapa (ajusta según el tamaño de tu escena)
    
    // Lista para almacenar las referencias a los enemigos instanciados
    private List<GameObject> enemigosInstanciados = new List<GameObject>();

    // Método para generar enemigos
    public void GenerarEnemigos()
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            // Determinar la posición en los extremos superiores del mapa
            float xPos = (i % 2 == 0) ? -limitesMapa.x : limitesMapa.x;
            float yPos = limitesMapa.y;
            Vector2 posicionInicial = new Vector2(xPos, yPos);

            // Instanciar el enemigo y guardar su referencia
            GameObject enemigo = Instantiate(prefabEnemigo, posicionInicial, Quaternion.identity);
            enemigosInstanciados.Add(enemigo); // Añadir enemigo a la lista
        }
    }

    // Método para eliminar todos los enemigos en escena
    public void EliminarEnemigos()
    {
        foreach (GameObject enemigo in enemigosInstanciados)
        {
            if (enemigo != null)
            {
                Destroy(enemigo); // Destruir el enemigo
            }
        }
        enemigosInstanciados.Clear(); // Vaciar la lista después de eliminar los enemigos
    }
}