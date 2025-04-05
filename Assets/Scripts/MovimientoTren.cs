using System.Collections;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Los waypoints de las vías
    public float speed = 5f;       // Velocidad de movimiento
    private int currentWaypoint = 0;

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            // Mover el tren hacia el siguiente waypoint
            Transform target = waypoints[currentWaypoint];
            Vector3 direction = target.position - transform.position;
            
            // Girar el tren hacia la dirección de movimiento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
            
            // Mover el tren hacia el waypoint
            transform.position += direction.normalized * speed * Time.deltaTime;

            // Cambiar al siguiente waypoint si llegamos al actual
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypoint++;
            }
        }
    }
}
