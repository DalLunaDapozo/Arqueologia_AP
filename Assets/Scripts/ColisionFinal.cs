using UnityEngine;
using System;

public class ColisionFinal : MonoBehaviour
{
    public event EventHandler jugador_colisiona_con_final;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugador_colisiona_con_final?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jugador_colisiona_con_final?.Invoke(this, EventArgs.Empty);
        }
    }
}
