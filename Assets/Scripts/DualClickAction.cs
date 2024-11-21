using UnityEngine;

public class DobleClickAura : MonoBehaviour
{
    public GameObject auraPrefab; // Prefab del aura de explosión
    public GameObject auraCargaPrefab; // Prefab del aura de carga

    private GameObject currentCargaAura; // Instancia activa del aura de carga
    private float holdTime = 1f; // Tiempo necesario para activar la explosión
    private float holdTimer = 0f; // Contador para el tiempo de mantención
    private bool bothClicksHeld = false;

    // Variables para el cooldown
    public float cooldownDuration = 3f; // Duración del cooldown en segundos
    private float cooldownTimer = 0f; // Temporizador para el cooldown
    private bool isOnCooldown = false; // Verifica si el aura está en cooldown

    void Update()
    {
        // Actualizar el temporizador de cooldown
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
            }
        }

        // Solo permite la activación del aura si no está en cooldown
        if (!isOnCooldown)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                holdTimer += Time.deltaTime;
                if (holdTimer >= holdTime)
                {
                    bothClicksHeld = true;
                }

                // Instancia el aura de carga solo si aún no ha sido creada
                if (currentCargaAura == null)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0; // Ajusta la posición Z a 0 para el plano 2D

                    currentCargaAura = Instantiate(auraCargaPrefab, mousePos, Quaternion.identity);
                    currentCargaAura.GetComponent<AuraCargaEffect>().Initialize(0.3f, holdTime); // Ajusta el tamaño y duración de la carga
                }
            }
            else
            {
                // Si suelta los clics después de mantenerlos el tiempo necesario, activa la explosión
                if (bothClicksHeld)
                {
                    CreateAura();
                    // Inicia el cooldown después de activar el aura
                    isOnCooldown = true;
                    cooldownTimer = cooldownDuration;
                }

                holdTimer = 0f;
                bothClicksHeld = false;

                // Elimina el aura de carga cuando se sueltan los clics
                if (currentCargaAura != null)
                {
                    Destroy(currentCargaAura);
                    currentCargaAura = null;
                }
            }

            // Si el aura de carga existe, actualiza su posición a la del mouse
            if (currentCargaAura != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // Asegúrate de que esté en el plano 2D
                currentCargaAura.transform.position = mousePos;
            }
        }
    }

    void CreateAura()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        GameObject aura = Instantiate(auraPrefab, mousePos, Quaternion.identity);
        aura.GetComponent<AuraEffect>().Initialize(1.5f, 0.5f); // Ajusta el tamaño y duración de la explosión
    }
}
