using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combateEnemigo : MonoBehaviour
{
    public float danio;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<combateJugador>().recibirDanio(danio, other.gameObject);
        }
    }
}
