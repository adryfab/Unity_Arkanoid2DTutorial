using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaScript : MonoBehaviour
{
    public GameObject JugadorObjeto;
    public AudioClip hitSound;

    private bool PelotaEstaActiva;
    private Vector3 PelotaPosicion;
    private Vector2 PelotaFuerzaInicial;
    private Rigidbody2D rigidbody2D;
    private AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        // creando la fuerza
        PelotaFuerzaInicial = new Vector2(100.0f, 300.0f);

        // asignando como inactivo
        PelotaEstaActiva = false;

        // posicion de la pelota
        PelotaPosicion = transform.position;

        rigidbody2D = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // se verifica la tecla presionada del usuario
        if (Input.GetButtonDown("Jump") == true) //barra espaciadora
        {
            // se verifica si es la primera vez
            if (PelotaEstaActiva == false)
            {
                // se añade la fuerza
                rigidbody2D.AddForce(PelotaFuerzaInicial, ForceMode2D.Force);

                // se activa la pelota
                PelotaEstaActiva = true;
            }
        }

        //La pelota sigue al jugador antes de comenzar el juego
        if (PelotaEstaActiva == false && JugadorObjeto != null)
        {
            // la pelota se asigna a la posicion del jugador
            PelotaPosicion.x = JugadorObjeto.transform.position.x;

            // apply player X position to the ball
            transform.position = PelotaPosicion;
        }

        if (PelotaEstaActiva == true && transform.position.y < -6)
        {
            PelotaEstaActiva = false;
            PelotaPosicion.x = JugadorObjeto.transform.position.x;
            PelotaPosicion.y = -4.2f;
            transform.position = PelotaPosicion;
            //resetea la velocidad
            rigidbody2D.velocity = new Vector2(0f, 0f);
            // New code - Send Message
            JugadorObjeto.SendMessage("TakeLife");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (PelotaEstaActiva == true)
        {
            audio.PlayOneShot(hitSound);
        }
    }
}
