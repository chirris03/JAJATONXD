using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Los waypoints por los que se moverá el tren
    public float speed = 5f;       // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotación para que el tren se gire suavemente
    private int currentWaypoint = 0; // El waypoint actual al que se dirige el tren

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            // Obtiene el siguiente waypoint (punto de destino)
            Transform target = waypoints[currentWaypoint];
            // Calcula la dirección desde la posición actual hacia el waypoint
            Vector3 direction = target.position - transform.position;
            
            // Rotar suavemente el tren hacia la dirección del próximo waypoint
            Vector3 targetDirection = direction.normalized;  // Dirección de destino normalizada
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Calcula la rotación necesaria
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Mover el tren hacia el waypoint
            transform.position += targetDirection * speed * Time.deltaTime;

            // Si el tren está lo suficientemente cerca del waypoint, cambia al siguiente
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypoint++;
            }
        }
    }
}
