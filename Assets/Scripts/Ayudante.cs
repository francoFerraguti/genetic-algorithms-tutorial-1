using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Ayudante
{
    public static System.Random rand;

    public static void Inicializar()
    {
        rand = new System.Random();
    }

    public static string ObtenerFraseAleatoria(int nLetras)
    {
        string frase = ""; //Inicializa la frase vacía

        for (int i = 0; i < nLetras; i++) //Recorre un bucle una cantidad de veces igual al número de letras que se le pasa a la función, es decir nLetras
        {
            frase += ObtenerLetraAleatoria(); //Agrega una letra aleatoria a la frase
        }

        return frase; //Devuelve la frase
    }

    public static char ObtenerLetraAleatoria()
    {
        string caracteresValidos = "abcdefghijklmnopqrstuvwxyz";
        int num = rand.Next(0, caracteresValidos.Length - 1); //Elige una posición desde la 0 hasta la última de la variable caracteresValidos
        return caracteresValidos[num]; //Devuelve el caracter que está en la posición elegida en la línea anterior
    }

    public static List<GameObject> ObtenerPobladoresConMayorFitness(List<GameObject> poblacion, int nPobladoresPasanDirecto)
    {
        List<GameObject> pobladoresConMayorFitness = new List<GameObject>();

        for (int i = 0; i < nPobladoresPasanDirecto; i++)
        {
            GameObject pobladorConMayorFitnessTemporal = poblacion[0];

            for (int j = 1; j < poblacion.Count; j++)
            {
                if (poblacion[j].GetComponent<Poblador>().ObtenerFitness() > pobladorConMayorFitnessTemporal.GetComponent<Poblador>().ObtenerFitness())
                {
                    pobladorConMayorFitnessTemporal = poblacion[j];
                }
            }

            pobladoresConMayorFitness.Add(pobladorConMayorFitnessTemporal);
            poblacion.Remove(pobladorConMayorFitnessTemporal);
        }

        return pobladoresConMayorFitness;
    }
}
