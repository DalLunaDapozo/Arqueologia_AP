using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;

public class ControladorDeJuego : MonoBehaviour
{
    public bool tiene_el_craneo;

    public ColisionFinal puerta_final;
    public TextMeshProUGUI texto_en_pantalla;
 
    public Image fondo_negro;
    public MovimientoJugador jugador;
    public Reliquia reliquia;

    private void Start()
    {
        tiene_el_craneo = false;
        puerta_final.jugador_colisiona_con_final += OnJugadorTocaFinal;
        jugador.toca_una_trampa += OnJugadorTocaTrampa;
        reliquia.craneo_agarrado += OnJugadorAgarraCraneo;

        StartCoroutine(EmpezarNivel());
    }
    private void OnJugadorTocaFinal(object sender, EventArgs e)
    {
        if (tiene_el_craneo)
        {
            StartCoroutine(TerminarNivel());
        }
        else
        {
            if (texto_en_pantalla.gameObject.activeSelf)
                texto_en_pantalla.gameObject.SetActive(false);
            else
            {
                texto_en_pantalla.gameObject.SetActive(true);
                texto_en_pantalla.text = "No me voy a ir sin la reliquia dorada.";
            }
        }
    }

    private void OnJugadorTocaTrampa(object sender, EventArgs e)
    {
        StartCoroutine(ReiniciarNivel());
    }

    private void OnJugadorAgarraCraneo(object sender, EventArgs e)
    {
        tiene_el_craneo = true;
        StartCoroutine(DesplegarTextoCiertoTiempo(4f, "Bien, ahora ya puedo marcharme de este lugar."));
        
    }

    private IEnumerator TerminarNivel()
    {
        StartCoroutine(DesplegarTextoCiertoTiempo(2f, "He logrado escapar a salvo con la reliquia!"));
       
        //CREAMOS UNA VARIABLE LOCAL PARA ALMACENAR TIEMPO
        float time = 0;
        //Y OTRA PARA LA DURACION DE LA TRANSICION
        float duration = 1f;

        //ABRIMOS UN WHILE, EN UNITY, LOS WHILE NO SE USAN EN CASI NINGUNA OCASION, EXCEPTO DENTRO DE CORRUTINAS :D 
        //BASICAMENTE ABRIMOS UN BUCLE QUE SE REPITE HASTA QUE NO SE CUMPLA LA CONDICION QUE ESTÁ ENTRE PARÉNTESIS
        //EN ESTE CASO: QUE EL TIEMPO QUE TRANSCURRE SEA MENOR A LA DURACION (QUE EN ESTE CASO VALOR 1)
        while (time < duration)
        {
            //ACA MOVEMOS LA POSICION EN X DE LA PANTALLA EN NEGRO Y USAMOS UN LERP QUE ES UNA FUNCION MATEMATICA PARA TRANSPOLAR DOS VALORES DE FORMA GRADUAL
            //ESO LE DA EL EFECTO DE DESLIZAMIENTO A LA PANTALLA
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(800, 0, time / duration), 0, 0);
            //CADA FRAME LE SUMAMOS UN DELTATIME A NUESTRA VARIABLE DE TIEMPO PARA PODER ROMPER EL LOOP EN ALGUN MOMENTO
            time += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ReiniciarNivel()
    {
        //CREAMOS UNA VARIABLE LOCAL PARA ALMACENAR TIEMPO
        float time = 0;
        //Y OTRA PARA LA DURACION DE LA TRANSICION
        float duration = 1f;

        //ABRIMOS UN WHILE, EN UNITY, LOS WHILE NO SE USAN EN CASI NINGUNA OCASION, EXCEPTO DENTRO DE CORRUTINAS :D 
        //BASICAMENTE ABRIMOS UN BUCLE QUE SE REPITE HASTA QUE NO SE CUMPLA LA CONDICION QUE ESTÁ ENTRE PARÉNTESIS
        //EN ESTE CASO: QUE EL TIEMPO QUE TRANSCURRE SEA MENOR A LA DURACION (QUE EN ESTE CASO VALOR 1)
        while (time < duration)
        {
            //ACA MOVEMOS LA POSICION EN X DE LA PANTALLA EN NEGRO Y USAMOS UN LERP QUE ES UNA FUNCION MATEMATICA PARA TRANSPOLAR DOS VALORES DE FORMA GRADUAL
            //ESO LE DA EL EFECTO DE DESLIZAMIENTO A LA PANTALLA
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(800, 0, time / duration), 0, 0);
            //CADA FRAME LE SUMAMOS UN DELTATIME A NUESTRA VARIABLE DE TIEMPO PARA PODER ROMPER EL LOOP EN ALGUN MOMENTO
            time += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator DesplegarTextoCiertoTiempo(float tiempo, string texto)
    {
        texto_en_pantalla.gameObject.SetActive(true);
        texto_en_pantalla.text = texto;

        yield return new WaitForSeconds(tiempo);

        texto_en_pantalla.gameObject.SetActive(false);
        
    }

    private IEnumerator EmpezarNivel()
    {
        //CREAMOS UNA VARIABLE LOCAL PARA ALMACENAR TIEMPO
        float time = 0;
        //Y OTRA PARA LA DURACION DE LA TRANSICION
        float duration = 1f;

        while (time < duration)
        {
            fondo_negro.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(0, 800, time / duration), 0, 0);
            time += Time.deltaTime;

            yield return null;
        }
    }
}
