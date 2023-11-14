using UnityEngine;

[ExecuteInEditMode]
public class SeguirObjeto : MonoBehaviour
{
    public Transform objetoASeguir;

    private void Update()
    {
        transform.position = objetoASeguir.position;
    }
}
