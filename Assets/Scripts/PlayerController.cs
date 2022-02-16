using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //El rigidbody
    private Rigidbody playerRigidbody;

    //Potencia de impulso
    public float impulso = 100f;

    //Limite del techo
    private float Ylim = 15f;

    //Limite del suelo
    private float Floor = 
    // Start is called before the first frame update
    void Start()
    {
        //Asi tenemos el componente rigidbody al empezar
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Al presionar espacio, el globo volara como un campeon
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * impulso, ForceMode.Impulse);
        }

        if (transform.position.y > Ylim)
        {
            transform.position = new Vector3(transform.position.x, Ylim, transform.position.z);
            //playerRigidbody.velocity = Vector3.zero;
        }
        
    }
}
