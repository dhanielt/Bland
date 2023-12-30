using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class movimientoPersonaje : MonoBehaviourPunCallbacks
{

    public Joystick joystick;
    public int velocidad;
    private Rigidbody2D rb;
    public bool conFisicas;
    private Vector3 direccionMovimiento;
    public Vector2 velocidadRebote;
    
    void Start()
    {
        this.name = "jugador" + (this.GetComponent<PhotonView>().ViewID.ToString()[0]);
        rb = GetComponent<Rigidbody2D>();
        GameObject joystickCanva = GameObject.Find("Fixed Joystick");
        if (joystickCanva != null)
        {
            // Obtén el script del joystick
            joystick = joystickCanva.GetComponent<Joystick>();

            if (joystick == null)
            {
                Debug.LogError("El objeto encontrado no tiene el script JoystickScript.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el objeto del joystick en la escena.");
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 direccion = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
            direccionMovimiento = direccion * velocidad;
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (conFisicas)
            {
                rb.AddForce(direccionMovimiento * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rb.MovePosition(transform.position + direccionMovimiento * Time.fixedDeltaTime);
            }
        }
    }
    
}
