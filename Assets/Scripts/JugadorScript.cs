using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorScript : MonoBehaviour
{

    public float VelocidadJugador;
    public float Limites;
    public AudioClip pointSound;
    public AudioClip lifeSound;

    private Vector3 PosicionJugador;
    private int JugadorVida;
    private int JugadorPuntos;
    private AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        PosicionJugador = gameObject.transform.position;
        JugadorVida = 3;
        JugadorPuntos = 0;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        // movimiento horizontal
        PosicionJugador.x += Input.GetAxis("Horizontal") * VelocidadJugador;

        //// salir del juego
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        // actualiza el valor de la posicion del Transform del objeto Jugador
        transform.position = PosicionJugador;

        // limites
        if (PosicionJugador.x < -Limites)
        {
            transform.position = new Vector3(-Limites, PosicionJugador.y, PosicionJugador.z);
        }
        if (PosicionJugador.x > Limites)
        {
            transform.position = new Vector3(Limites, PosicionJugador.y, PosicionJugador.z);
        }

        // Check game state
        WinLose();
    }

    void addPoints(int points)
    {
        JugadorPuntos += points;
        audio.PlayOneShot(pointSound);
    }
        
    void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Vidas: " + JugadorVida + "  Puntaje: " + JugadorPuntos);
    }

    void TakeLife()
    {
        JugadorVida--;
        audio.PlayOneShot(lifeSound);
    }

    void WinLose()
    {
        // restart the game
        if (JugadorVida == 0)
        {
            //Application.LoadLevel("Level1");
            SceneManager.LoadScene("Nivel1");
        }

        // blocks destroyed
        if ((GameObject.FindGameObjectsWithTag("Bloques")).Length == 0)
        {
            // check the current level
            if (Application.loadedLevelName == "Nivel1")
            {
                //Application.LoadLevel("Level2");
                SceneManager.LoadScene("Nivel2");
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
