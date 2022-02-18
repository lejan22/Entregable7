using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    //Aqui tenemos los prefabs
    public GameObject[] objectsPrefab;

    //Variables random
    
    private Vector3 randomPos;
    private int randomItem;
    
    private float randomY;
    private float randomX;
    private float Ytop = 14;
    private float Ybottom = 5f;
    private float XLim = 20f;
    
    private Vector3 spawnPosition;

    //Variable para poder agarrar despues el componente Gameover del player
    private PlayerController PlayerControllerScript;

    //Cada cuanto aparecen los items
    private float startTime = 2f;
    private float repeatRate = 1f;
   

    // Start is called before the first frame update
    void Start()
    {
        //Encontrar el player controller
        PlayerControllerScript = FindObjectOfType<PlayerController>();

        InvokeRepeating("spawnRandomItem", startTime, repeatRate);
    }

   

    public Vector3 RandomSpawnPosition()
    {
        int RandomSpawn = Random.Range(0, 2);
        //Aparicion en X aleatoria
        randomX = Random.Range(-XLim, XLim);
        //Aparicion en y aleatoria
        randomY = Random.Range(Ybottom, Ytop);

        if (RandomSpawn == 0)
        {
            return new Vector3(-XLim, randomY, 0);
        }
        else
        {
            return new Vector3(XLim, randomY, 0);
        }

        return new Vector3(randomX, randomY, 0);
        
    }

    private void spawnRandomItem()
    {
        //Si el juego sigue en marcha
        if (!PlayerControllerScript.gameOver)
        {
            //Aparicion de los items aleatoriamente
            randomItem = Random.Range(0, objectsPrefab.Length);

           
            spawnPosition = RandomSpawnPosition();

            Instantiate(objectsPrefab[randomItem], spawnPosition,
                objectsPrefab[randomItem].transform.rotation);
        }
    }
}
