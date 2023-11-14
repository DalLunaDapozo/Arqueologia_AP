using UnityEngine;
using System;

public class MovimientoJugador : MonoBehaviour
{
    private float eje_z;
    private float eje_x;

    private Vector3 direccion;

    private Rigidbody fisicas;
    private EfectosDeSonido efectos_de_sonido;

    public Transform orientacion;
    public float velocidad_de_movimiento;
    public float fuerza_de_salto;

    public bool esta_en_suelo;
    public bool esta_saltando;

    public bool esta_vivo;

    public event EventHandler toca_una_trampa;

    public GameObject camara;

    private void Start()
    {
        esta_vivo = true;
        fisicas = GetComponent<Rigidbody>();
        efectos_de_sonido = transform.Find("Sonidos").GetComponent<EfectosDeSonido>();
    }

    private void Update()
    {
        eje_z = Input.GetAxisRaw("Vertical");
        eje_x = Input.GetAxisRaw("Horizontal");

        ControlDeVelocidad();

        if (Input.GetButtonDown("Jump") && esta_en_suelo)
        {
            esta_saltando = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (!esta_vivo) return;

        Movimiento();
        Saltar();
    }

    private void Movimiento()
    {
        direccion = orientacion.forward * eje_z + orientacion.right * eje_x;

        fisicas.AddForce(direccion.normalized * velocidad_de_movimiento * 10f, ForceMode.Force);
    }

    private void Saltar()
    {
        if (esta_saltando)
        {
            esta_saltando = false;
            fisicas.AddForce(transform.up * fuerza_de_salto, ForceMode.Impulse);
            efectos_de_sonido.ReproducirSonido("salto");
        }
        
    }

    private void ControlDeVelocidad()
    {
        Vector3 velocidad_flat = new Vector3(fisicas.velocity.x, 0f, fisicas.velocity.z);

        if (velocidad_flat.magnitude > velocidad_de_movimiento)
        {
            Vector3 limite_velocidad = velocidad_flat.normalized * velocidad_de_movimiento;
            fisicas.velocity = new Vector3(limite_velocidad.x, fisicas.velocity.y, limite_velocidad.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            esta_en_suelo = true;
        }

        if (collision.gameObject.CompareTag("Trampa"))
        {
            esta_vivo = false;
            toca_una_trampa?.Invoke(this, EventArgs.Empty);
            fisicas.freezeRotation = false;
            camara.transform.Rotate(0f, 0f, 90f);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            esta_en_suelo = false;
        }
    }
}
