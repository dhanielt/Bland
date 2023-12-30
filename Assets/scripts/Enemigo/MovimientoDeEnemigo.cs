using System.Collections;
using UnityEngine;
using Photon.Pun;

public class MovimientoDeEnemigo : MonoBehaviourPunCallbacks
{
    private Transform objetivo;
    private Renderer objetivoRender;
    public float speed;
    private int jugador;
    public bool moverse;
    private Rigidbody2D rb;
    private int jugadoresMuertos;
    void Start()
    {
        StartCoroutine(BuscarJugadorMasCercano());
        rb = GetComponent<Rigidbody2D>();
    }
    
    IEnumerator BuscarJugadorMasCercano()
    {
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length != 0);
        yield return new WaitForSeconds(1f);
        jugadoresMuertos = 0;
        
        objetivo = null; 
        float distanciaMasCercana = float.MaxValue;
        
        for (int i = 1; i <= PhotonNetwork.PlayerList.Length; i++) 
        { 
            GameObject jugadorObject = GameObject.Find("jugador" + i);
            
            if (jugadorObject != null && jugadorObject.GetComponent<Renderer>().enabled) 
            { 
                float distancia = Vector3.Distance(transform.position, jugadorObject.transform.position);
                
                if (distancia < distanciaMasCercana) 
                { 
                    distanciaMasCercana = distancia; 
                    objetivo = jugadorObject.transform;
                    objetivoRender = jugadorObject.GetComponent<Renderer>();
                    jugador = i;
                }
            }
            else
            {
                jugadoresMuertos += 1;
                Debug.Log("Jugador" + i + " no está instanciado 0 muerto"); 
            } 
        }

        if (jugadoresMuertos < PhotonNetwork.PlayerList.Length)
        {
            moverse = true;
        }
        else
        {
            moverse = false;
        }
    }

    public int getObjetivo()
    {
        return jugador;
    }

    public void setObjetivo(int jugador)
    {
        this.jugador = jugador;
        GameObject jugadorObject = GameObject.Find("jugador" + jugador);
        objetivo = jugadorObject.transform;
        objetivoRender = jugadorObject.GetComponent<Renderer>();
    }
    
    private void FixedUpdate()
    {
        if (moverse)
        {
            if (objetivoRender.enabled)
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, objetivo.transform.position,
                    speed * Time.fixedDeltaTime));
            }
            else
            {
                StartCoroutine(BuscarJugadorMasCercano());
            }
        }
    }
}
