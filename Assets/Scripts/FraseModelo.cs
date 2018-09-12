using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FraseModelo : MonoBehaviour
{
    public string frase = "Frase modelo";

    public void Inicializar()
    {
        frase = Ayudante.ObtenerFraseAleatoria(Config.nLetrasPorFrase); //Le asigna una frase aleatoria a la variable
        this.GetComponent<Text>().text = frase; //Le asigna la frase al componente de Text del GameObject
    }

    public string ObtenerFrase()
    {
        return frase;
    }
}
