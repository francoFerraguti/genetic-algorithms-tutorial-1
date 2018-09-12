using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ruleta
{

    public static int GirarRuleta(int totalFitness)
    {
        return Ayudante.rand.Next(0, totalFitness);
    }

    public static GameObject ObtenerNuevoPoblador(List<GameObject> poblacion, int totalFitness)
    {
        GameObject padre = ObtenerPadre(poblacion, GirarRuleta(totalFitness));
        GameObject madre;
        do
        {
            madre = ObtenerPadre(poblacion, GirarRuleta(totalFitness));
        } while (padre == madre);

        GameObject hijo = padre;
        hijo.GetComponent<Poblador>().AsignarMitadDeADN(madre);
        hijo.GetComponent<Poblador>().Mutar();
        return hijo;
    }

    public static GameObject ObtenerPadre(List<GameObject> poblacion, int resultadoDeLaRuleta)
    {
        int contadorDeFitness = 0;

        for (int i = 0; i < poblacion.Count; i++)
        {
            if (poblacion[i].GetComponent<Poblador>().ObtenerFitness() + contadorDeFitness > resultadoDeLaRuleta)
            {
                return poblacion[i];
            }

            contadorDeFitness += poblacion[i].GetComponent<Poblador>().ObtenerFitness();
        }

        Debug.LogError("Error en la ruleta, el resultado fue: " + resultadoDeLaRuleta);
        return null;
    }
}
