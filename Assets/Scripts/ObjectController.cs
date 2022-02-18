using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //Velocidad en la que se mueven los objetos
    public float speed = 15f;
    //Limite de la pantalla en x
    private float xLim = 15f;

    
    void Update()
    {
        //Objetos van hacia la derecha
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        //Si llega al final de la pantalla o es recolectado, desaparece
        if ((transform.position.x > xLim && speed > 0))
        {
            Destroy(gameObject);
        }
            

    }
}
