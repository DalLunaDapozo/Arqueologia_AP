using UnityEngine;
using System;

public class Reliquia : MonoBehaviour
{
    public event EventHandler craneo_agarrado;
    public GameObject craneo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            craneo_agarrado?.Invoke(this, EventArgs.Empty);
            Destroy(craneo);
        }
    }
}
