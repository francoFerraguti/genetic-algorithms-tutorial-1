using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgoritmoGenetico : MonoBehaviour
{

    private static List<GameObject> poblacion;
    private static List<GameObject> nuevaPoblacion;

    private static int nGeneraciones = 0;
    private static bool hasWon = false;

    public static void Inicializar()
    {
        poblacion = new List<GameObject>();
    }

    public static void GenerarPoblacion()
    {
        for (int i = 0; i < Config.nPobladores; i++) //Recorre un bucle una cantidad de veces igual al número de pobladores que vamos a tener
        {
            int posY = 210 - 25 * i; //Se le asigna una posición en Y inicial, y se va restando 25 por cada poblador para que se vayan creando uno debajo del otro
            Vector3 pos = new Vector3(0, 210 - 25 * i, 0); //Posición de cada texto
            Quaternion rot = new Quaternion(0, 0, 0, 0); //Rotación en cero

            GameObject texto = (GameObject)Instantiate(Resources.Load("Poblador"), pos, rot); //Crea un GameObject en base al prefab de Poblador ubicado en Assets/Resources/Poblador
            texto.transform.SetParent(GameObject.Find("Canvas").transform, false); //Le asigna como padre al Canvas para que pueda ser visualizado

            texto.GetComponent<Poblador>().Inicializar(); //Inicializamos al poblador

            poblacion.Add(texto); //Agregamos el poblador a una lista, para poder acceder a él luego
        }
    }

    public static void ModificarPoblacion(List<GameObject> siguienteGeneracion)
    {
        for (int i = 0; i < siguienteGeneracion.Count; i++) //Recorre un bucle una cantidad de veces igual al número de pobladores que vamos a tener
        {
            poblacion[i].GetComponent<Poblador>().PonerFrase(siguienteGeneracion[i].GetComponent<Poblador>().ObtenerFrase());
        }
    }

    public static void AvanzarGeneracion()
    {

        nuevaPoblacion = new List<GameObject>(); //Inicializa una lista vacía para la próxima generación de pobladores
        int totalFitness = 0; //Esta variable representa la suma de todos los fitness de los pobladores de la generación actual
        int avgFitness = 0; //Esta variable representa el promedio de fitness de la generación

        for (int i = 0; i < poblacion.Count; i++) //Recorre todos los pobladores y calcula su fitness. Luego los va sumando para calcular el fitness total de la generación
        {
            totalFitness += poblacion[i].GetComponent<Poblador>().CalcularFitness();
        }

        avgFitness = totalFitness / Config.nPobladores;

        List<GameObject> pobladoresPasanDirecto = Ayudante.ObtenerPobladoresConMayorFitness(poblacion, Config.nPobladoresPasanDirecto); //Los mejores pobladores pasan directo

        GameObject.Find("FraseMejor").GetComponent<Text>().text = pobladoresPasanDirecto[0].GetComponent<Poblador>().ObtenerFrase();

        for (int i = 0; i < pobladoresPasanDirecto.Count; i++)
        {
            Debug.Log(pobladoresPasanDirecto[i].GetComponent<Poblador>().ObtenerFrase() + " pasa directo con un fitness de: " + pobladoresPasanDirecto[i].GetComponent<Poblador>().ObtenerFitness());
            poblacion.Add(pobladoresPasanDirecto[i]); //En Ayudante.ObtenerPobladoresConMayorFitness los sacamos de la lista, así que los volvemos a agregar
            nuevaPoblacion.Add(pobladoresPasanDirecto[i]); //Agrega esos pobladores a la nueva población
        }

        int pobladoresRestantes = Config.nPobladores - Config.nPobladoresPasanDirecto; //Calcula cuántos pobladores faltan para rellenar la nueva población

        for (int i = 0; i < pobladoresRestantes; i++)
        {
            GameObject pobladorHijo = Ruleta.ObtenerNuevoPoblador(poblacion, totalFitness);
            nuevaPoblacion.Add(pobladorHijo);
        }

        nGeneraciones++;

        ActualizarDatos(avgFitness);

        ModificarPoblacion(nuevaPoblacion);
    }

    public static void ActualizarDatos(int avgFitness) {
        int minFitness = 999999;
        int maxFitness = -999999;

        for (int i = 0; i < poblacion.Count; i++){
            int fitnessActual = poblacion[i].GetComponent<Poblador>().ObtenerFitness();

            if (fitnessActual < minFitness) {
                minFitness = fitnessActual;
            }

            if (fitnessActual > maxFitness) {
                maxFitness = fitnessActual;
            }
        }

        if (!hasWon && maxFitness == Config.nLetrasPorFrase) {
            GameObject.Find("winnerGenN").GetComponent<Text>().text = "Generación ganadora: " + nGeneraciones;
            hasWon = true;
        }

        GameObject.Find("generacionN").GetComponent<Text>().text = "Generación n°: " + nGeneraciones;
        
        GameObject.Find("minFitness").GetComponent<Text>().text = "Fitness mínimo: " + minFitness;
        GameObject.Find("avgFitness").GetComponent<Text>().text = "Fitness promedio: " + avgFitness;
        GameObject.Find("maxFitness").GetComponent<Text>().text = "Fitness máximo: " + maxFitness;

    }
}
