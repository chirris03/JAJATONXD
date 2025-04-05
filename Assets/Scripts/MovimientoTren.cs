using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Los waypoints por los que se moverá el tren
    public float speed = 2f;       // Velocidad de movimiento
    private int currentWaypoint = 0; // El waypoint actual al que se dirige el tren
    private float t = 0f;          // Parámetro de la curva (0-1)

    void Update()
    {
        // Asegurarse de que hay más de 1 waypoint a seguir
        if (currentWaypoint < waypoints.Length - 2) // Al menos 3 puntos para hacer la curva
        {
            // Obtener los puntos para la curva de Bezier: P0, P1, P2
            Transform P0 = waypoints[currentWaypoint];            // El waypoint actual
            Transform P1 = waypoints[currentWaypoint + 1];        // El punto de control
            Transform P2 = waypoints[currentWaypoint + 2];        // El siguiente waypoint

            // Calcula la posición en la curva de Bezier
            Vector3 position = CalculateBezier(P0.position, P1.position, P2.position, t);

            // Mueve el tren a lo largo de la curva
            transform.position = position;

            // Incrementa el parámetro t (que va de 0 a 1)
            t += speed * Time.deltaTime;

            // Si t ha alcanzado 1 (ha llegado al final de la curva), cambia al siguiente waypoint
            if (t >= 1f)
            {
                t = 0f;  // Resetea el parámetro t
                currentWaypoint++; // Avanza al siguiente waypoint
            }
        }
    }

    // Función que calcula la posición en la curva de Bezier de 3 puntos
    Vector3 CalculateBezier(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        // Fórmula de Bezier cuadrática
        float u = 1 - t;
        return u * u * P0 + 2 * u * t * P1 + t * t * P2;
    }
}
