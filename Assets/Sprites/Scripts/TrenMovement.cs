using UnityEngine;

public class TrenMovement : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 3f;
    public float tiempoGiro = 1f;
    public float anguloGiro = 90f;

    [Header("Estado")]
    private bool estaGirando = false;
    private float anguloInicial;
    private float anguloObjetivo;
    private float tiempoTranscurrido;

    void Update()
    {
        if (!estaGirando)
        {
            // Movimiento constante hacia adelante (relativo a su rotación actual)
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        else
        {
            // Giro progresivo
            tiempoTranscurrido += Time.deltaTime;
            float porcentajeGiro = Mathf.Clamp01(tiempoTranscurrido / tiempoGiro);
            float anguloActual = Mathf.LerpAngle(anguloInicial, anguloObjetivo, porcentajeGiro);
            transform.rotation = Quaternion.Euler(0, 0, anguloActual);

            // Mover durante el giro (para que no se detenga)
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);

            // Finalizar giro
            if (porcentajeGiro >= 1f)
            {
                estaGirando = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (estaGirando) return;

        if (other.CompareTag("Curva_Derecha"))
        {
            IniciarGiro(-anguloGiro); // Giro horario
        }
        else if (other.CompareTag("Curva_Izquierda"))
        {
            IniciarGiro(anguloGiro); // Giro antihorario
        }
    }

    void IniciarGiro(float angulo)
    {
        estaGirando = true;
        tiempoTranscurrido = 0f;
        anguloInicial = transform.eulerAngles.z;
        anguloObjetivo = anguloInicial + angulo;
    }
}