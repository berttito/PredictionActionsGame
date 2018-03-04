using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// se encarga de evaluar las acciones anteriores para tratar de elaborar una predicción, al principio, tiene un random de posibles acciones. 
/// También registra la secuencia de todas las acciones hechas hasta el momento.
/// </summary>
public class Predictor : MonoBehaviour {

    //Diccionario de los datos
    private Dictionary<string, DataRecord> data;
    //String de las posibles acciones
    private string possibleActions;
    //private Random rnd;
    private int rnd;

    public Predictor(string _possibleActions)
    {
        //rnd = new Random();
        data = new Dictionary<string, DataRecord>();
        possibleActions = _possibleActions;
    }

    //Funcion para tratar de elaborar una prediccion en funcion de las acciones anteriores
    public string GetMostLikely(string actions)
    {
        //string prediccion = "i";

        DataRecord keyData;
        int highestValue = 0;
        char bestAction = ' ';

        if (data.ContainsKey(actions))
        {
            keyData = data[actions];


            foreach (char action in keyData.counts.Keys)
            {
                if (keyData.counts[action] > highestValue)
                {
                    highestValue = keyData.counts[action];
                    bestAction = action;

                }
            }
        }

        else
        {
            //pasarle los primeros randoms del enemigo
            rnd = Random.Range(0, possibleActions.Length);
            bestAction = possibleActions[rnd];
        }



        return bestAction + "";
    }


    //Funcion que registra la sequencia de todas las acciones que ha habido hasta el momento
    public void RegisterSequence(string actions)
    {
        string key = actions.Substring(0, actions.Length - 1);
        char value = actions[actions.Length - 1];

        if (!data.ContainsKey(key))
        {
            data[key] = new DataRecord();
        }

        DataRecord keyData = data[key];

        if (!keyData.counts.ContainsKey(value))
        {
            keyData.counts[value] = 0;
        }

        keyData.counts[value]++;
        keyData.total++;

    }

    /*
    public void PrintData()
    {
        Console.WriteLine("PREDICTOR DATA");
        foreach (String key in data.Keys)
        {
            Console.WriteLine(key);
            DataRecord keyData = data[key];
            foreach (char action in keyData.counts.Keys)
            {
                Console.WriteLine("\t" + action + "->" + keyData.counts[action]);
            }
        }
    }*/
}
