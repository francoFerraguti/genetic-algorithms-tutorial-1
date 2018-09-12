using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGeneral : MonoBehaviour
{

    void Awake() //Ponemos acá todas las funciones de inicialización
    {
        Ayudante.Inicializar();
        AlgoritmoGenetico.Inicializar();
        GameObject.Find("FraseModelo").GetComponent<FraseModelo>().Inicializar();

        GameObject.Find("populationN").GetComponent<Text>().text = "N° de pobladores: " + Config.nPobladores;
        GameObject.Find("mutation%").GetComponent<Text>().text = "% de mutación: " + Config.mutationRate;
    }

    void Start()
    {
        AlgoritmoGenetico.GenerarPoblacion();
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            AlgoritmoGenetico.AvanzarGeneracion();
        }
    }
}
