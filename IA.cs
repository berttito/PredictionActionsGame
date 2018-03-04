using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestiona las predicciones de la IA en función de lo obtenido en el Predictor, 
/// se guardan el total de las elecciones y las elecciones a predecir y se muestran los diferentes mensajes por pantalla
/// </summary>

public class IA : MonoBehaviour {

    //window size que va a abarcar la IA
    public int windowSize;

    //Referencias a los mensajes que se muestran por pantalla
    public Text IAsays;
    public Text tasaAciertos;
    public Text decisiones;
    public Text ultimasDecisiones;
    public GameObject GIAsays;
    public GameObject GTasaAciertos;
    public GameObject GDecisiones;
    public GameObject GUltimasDecisiones;
    public Text tAScene1;
    public Text tAScene2;
    int countTAScene1=0;
    int countTAScene2=0;

    //opcion escogida por el jugador
    string opcion = "";

    //strings donde vamos a guardar el total de elecciones, las que se van a predecir y las que se van a registrar
    string totalElecciones = "", eleccionesPredecir = "", eleccionesRegistrar = "";

    //int para determinar opciones totales y los aciertos
    int total = 0, acierto = 0;

    //string donde guardamos la prediccion
    string prediccion;

    //rock paper scissors
    Predictor predictor = new Predictor("rps");
    // Use this for initialization
    void Start () {

        desactivateTexts();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Devuelve la prediccion
    public string getPrediccion()
    {
        return prediccion;
    }

    //Desactiva los diferentes textos que se muestran por pantalla
    public void desactivateTexts()
    {
        GIAsays.SetActive(false);
        GTasaAciertos.SetActive(false);
        GDecisiones.SetActive(false);
        GUltimasDecisiones.SetActive(false);
    }

    //Determinamos la prediccion de la IA y mostramos los diferentes mensajes por pantalla
    //Guardamos en los diferentes strings las elecciones
    public void IApredictions(int option)
    {
        //predictor.PrintData();
        //0 papel   1 tijera   2 piedra
        //guardamos la opcion que ha elegido el jugador
        if (option == 0)
        {
            opcion = "p";
        }
        else if (option == 1)
        {
            opcion = "s";
        }
        else if (option == 2)
        {
            opcion = "r";
        }

        total++;

        prediccion = predictor.GetMostLikely(eleccionesPredecir);
        IAsays.text = "LA IA DICE QUE HAS ELEGIDO :  " + prediccion;
        GIAsays.SetActive(true);


        if (prediccion == opcion)
        {
            acierto++;
            //Console.WriteLine("La IA ha acertado");
            tasaAciertos.text = "La IA ha acertado.  La tasa de aciertos es: " + (100 * (float)acierto / total) + "%";
            GTasaAciertos.SetActive(true);
        }

        else
        {
            //Console.WriteLine("la IA ha fallado");
            tasaAciertos.text = "La IA ha fallado.  La tasa de aciertos es: " + (100 * (float)acierto / total) + "%";
            
            GTasaAciertos.SetActive(true);
            
        }

        //se acumula la ultima opcion q he elegido
        totalElecciones += opcion;
        //se acumula la ultima opcion q ha hecho concatenando cn la penultima
        eleccionesPredecir += opcion;
        if (totalElecciones.Length - windowSize < 0)
        {
            eleccionesPredecir += opcion;
        }

        else
        {
            eleccionesPredecir = totalElecciones.Substring(totalElecciones.Length - windowSize);
        }

        if (totalElecciones.Length - windowSize - 1 < 0)
        {
            eleccionesRegistrar += opcion;
        }

        else
        {
            eleccionesRegistrar = totalElecciones.Substring(totalElecciones.Length - (windowSize + 1));
            predictor.RegisterSequence(eleccionesRegistrar);
        }

        decisiones.text = "Total decisiones: " + totalElecciones;
        GDecisiones.SetActive(true);
        ultimasDecisiones.text = "  Ultimas decisiones: " + eleccionesPredecir;
        GUltimasDecisiones.SetActive(true);

        //comparar tasas de aciertos entre distintas escenas
        if (windowSize == 2)
        {
            countTAScene1++;

            if (countTAScene1 > 3)
            {
                ComparateScene.SC1 = acierto;
                tAScene1.text = "La tasa de aciertos EN LA ESCENA 1 de la IA en el movimiento numero: " + countTAScene1 + "  es de: " + (100 * (float)ComparateScene.SC1 / total) + "%";
                
            }
        }

        if (windowSize == 3)
        {
            countTAScene2++;

            if (countTAScene2 > 3)
            {
                ComparateScene.SC2 = acierto;
                
                tAScene1.text = "La tasa de aciertos EN LA ESCENA 1 de la IA en el movimiento numero: " + countTAScene2 + "  es de: " + (100 * (float)ComparateScene.SC1 / total) + "%";
                tAScene2.text = "La tasa de aciertos EN LA ESCENA 2 de la IA en el movimiento numero: " + countTAScene2 + "  es de: " + (100 * (float)ComparateScene.SC2 / total) + "%";
            }
        }

    }
}
