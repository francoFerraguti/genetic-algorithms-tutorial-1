using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poblador : MonoBehaviour
{
    public string frase = "Poblador";
    public int fitness = 0;

    public void Inicializar()
    {
        frase = Ayudante.ObtenerFraseAleatoria(Config.nLetrasPorFrase); //Le asigna una frase aleatoria a la variable
        this.GetComponent<Text>().text = frase; //Le asigna la frase al componente de Text del GameObject
    }

    public int CalcularFitness()
    {
        string fraseModelo = GameObject.Find("FraseModelo").GetComponent<FraseModelo>().ObtenerFrase();
        int fitnessTemporal = 0;

        for (int i = 0; i < frase.Length; i++)
        {
            if (frase[i] == fraseModelo[i])
            {
                fitnessTemporal++;
            }
        }

        fitness = fitnessTemporal;
        return fitness;
    }

    public void AsignarMitadDeADN(GameObject madre)
    {
        string nuevaFrase = "";

        for (int i = 0; i < frase.Length; i++)
        {
            if (i % 2 == 0)
            {
                nuevaFrase += frase[i];
            }
            else
            {
                nuevaFrase += madre.GetComponent<Poblador>().ObtenerFrase()[i];
            }
        }
    }

    public void Mutar()
    {
        string fraseMutada = "";

        for (int i = 0; i < frase.Length; i++)
        {
            if (Ayudante.rand.Next(0, 100) < Config.mutationRate)
            {
                fraseMutada += Ayudante.ObtenerLetraAleatoria();
            }
            else
            {
                fraseMutada += frase[i];
            }
        }

        frase = fraseMutada;
    }

    public string ObtenerFrase()
    {
        return frase;
    }

    public void PonerFrase(string nuevaFrase)
    {
        frase = nuevaFrase;
        this.GetComponent<Text>().text = frase; //Le asigna la frase al componente de Text del GameObject
    }

    public int ObtenerFitness()
    {
        return fitness;
    }
}
