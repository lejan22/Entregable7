using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Para saber si el juego termina
    public bool gameOver;

    //El rigidbody
    private Rigidbody playerRigidbody;

    //Se consigue el AudioSource del Player 
    private AudioSource playerAudioSource;

    //Efecto de sonido del globo rebotando
    public AudioClip Boing;
    

    //Sistema de particulas
    public ParticleSystem explosion;
    public ParticleSystem fireworks;

    //Potencia de impulso
    public float impulso = 100f;

    //Limite del techo
    private float Ylim = 15f;

    //Limite del suelo
    private float Floor = 0f;

   
  
    // Start is called before the first frame update
    void Start()
    {
        //Asi tenemos el componente rigidbody al empezar
        playerRigidbody = GetComponent<Rigidbody>();

        playerAudioSource = GetComponent<AudioSource>();

        //Instanciamos particulas
        explosion = Instantiate(explosion, transform.position, explosion.transform.rotation);
        fireworks = Instantiate(fireworks, transform.position, fireworks.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            //Al presionar espacio, el globo volara como un campeon
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * impulso, ForceMode.Impulse);

                playerAudioSource.PlayOneShot(Boing, 0.3f);
            }

           
        }
        if (transform.position.y > Ylim)
        {
            transform.position = new Vector3(transform.position.x, Ylim, transform.position.z);

        }

        if (transform.position.y < Floor)
        {
            explosion.Play();
            Destroy(gameObject);


        }
    }

    private void GameOver()
    {
        //Indica la finalizacion de la partida
        gameOver = true;
        

    }
}
