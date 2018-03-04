using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Se encarga de comprobar los resultados y mostrar los mensajes correspondientes
/// </summary>
public class GameManager : MonoBehaviour {

    //Score de los jugadores
    int playerScore;
    int enemyScore;

    //Referencias a los mensajes
    public Text score;
    public GameObject scoreGb;
    public GameObject yeah;
    public GameObject ouch;
    public GameObject IASays;
    public GameObject Acierto;
    public GameObject Decisiones;
    public GameObject UltimasDecisiones;
    public GameObject bocadillo;
    public GameObject image;

    //Resultados de (piedra, papel o tijera) de los jugadores
    public int playerResults;
    public int enemyResults;

    //Timer para mostrar los mensajes
    float timer;

    //Referencia a la imagen de winner
    public GameObject winImage;

    //Referencia al script de la IA
    IA IAscript;

	// Use this for initialization
	void Start () {

        //Se inicializan todos los valores a cero al empezar
        //y se ocultan los mensajes

        playerScore = 0;
        enemyScore = 0;

        yeah.SetActive(false);
        ouch.SetActive(false);
        winImage.SetActive(false);
        IAscript = GameObject.Find("IA").GetComponent<IA>();
		
	}
	
    //Setea los resultados del jugador
    public void setPlayerResults(int _value)
    {
        playerResults = _value;
    }

    //Setea los resultados del oponente
    public void setEnemyResults(int _value)
    {
        enemyResults = _value;
    }


    //Se comprueban los resultados para sumar puntuacion
    //al jugador o al oponente
    public void checkResults()
    {
        // 0 papel 1 tijera  2 piedra
        if (playerResults == enemyResults)
        {
            //empate
            //playerScore++;
            //enemyScore++;
        }

        //si player saca papel y enemigo piedra
        else if(playerResults==0 && enemyResults == 2)
        {
            playerScore++;
            yeah.SetActive(true);
        }
        //si el player saca tijera y enemigo papel
        else if(playerResults==1 && enemyResults == 0)
        {
            playerScore++;
            yeah.SetActive(true);
        }
        //si el player saca piedra y enemigo tijera
        else if(playerResults==2 && enemyResults == 1)
        {
            playerScore++;
            yeah.SetActive(true);
        }
        //resto de casos gana el enemigo
        else
        {
            enemyScore++;
            ouch.SetActive(true);
        }
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene("PPT2");

        //Gestion de la desaparicion de los mensajes
        score.text= "PLAYER :  " + playerScore + "  ----  "+ enemyScore +"  : ENEMY ";

        if (yeah.activeSelf || ouch.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                yeah.SetActive(false);
                ouch.SetActive(false);
                //aqui se desactivan los textos de la IA
                //IAscript.desactivateTexts();
                timer = 0;
            }
        }

        
        if (playerScore == 10)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                yeah.SetActive(false);
                ouch.SetActive(false);
                scoreGb.SetActive(false);
                Acierto.SetActive(false);
                Decisiones.SetActive(false);
                UltimasDecisiones.SetActive(false);
                IASays.SetActive(false);
                winImage.SetActive(true);
                timer = 0;

            }

            
        }


            
    }
}
