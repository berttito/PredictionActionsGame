using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Contiene un diccionario que se utilizara para guardar las keys
/// </summary>
public class DataRecord : MonoBehaviour {

    public Dictionary<char, int> counts;
    public int total;

    public DataRecord()
    {
        total = 0;
        counts = new Dictionary<char, int>();
    }
}
