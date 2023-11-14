using UnityEngine;

public class CamaraPrimeraPersona : MonoBehaviour
{
    public float sensibilidadX;
    public float sensibilidadY;

    public Transform orientacion;

    float rotacion_x;
    float rotacion_y;

    public MovimientoJugador jugador;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {

        if (!jugador.esta_vivo) return;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensibilidadX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensibilidadY;

        rotacion_y += mouseX;

        rotacion_x -= mouseY;
        rotacion_x = Mathf.Clamp(rotacion_x, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotacion_x, rotacion_y, 0f);
        orientacion.rotation = Quaternion.Euler(0f, rotacion_y, 0f);
    }
}
