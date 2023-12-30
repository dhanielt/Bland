using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combateJugador : MonoBehaviour
{
    public float vida;
    private movimientoPersonaje rebote;

    private void Start()
    {
        rebote = GetComponent<movimientoPersonaje>();
    }

    public void recibirDanio(float danio, GameObject jugador)
    {
        if (vida - danio >= 0)
        {
            vida -= danio;
            StartCoroutine(invisible(jugador.GetComponent<Collider2D>()));
        }
        else
        {
            jugador.GetComponent<Renderer>().enabled = false;
            jugador.GetComponent<Collider2D>().enabled = false;
            vida = 0;
        }
        
    }

    IEnumerator invisible(Collider2D jugador)
    {
        jugador.isTrigger = true;
        yield return new WaitForSeconds(1f);
        jugador.isTrigger = false;
    }
    
}
