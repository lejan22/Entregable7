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

    //Efecto de sonido del globo rebotando , explotando y recolectando dinero
    public AudioClip Boing;
    public AudioClip boom;
    public AudioClip blingbling;

    //Sistema de particulas
    public ParticleSystem explosion;
    public ParticleSystem fireworks;

    //Potencia de impulso
    public float impulso = 100f;

    //Limite del techo
    private float Ylim = 15f;

    //Limite del suelo
    private float Floor = 0f;

    //Variable que utilizaremos para contar el dinero
    public int Money;
  
    void Start()
    {
        //Asi tenemos el componente rigidbody al empezar
        playerRigidbody = GetComponent<Rigidbody>();

        playerAudioSource = GetComponent<AudioSource>();

        //Instanciamos particulas
        explosion = Instantiate(explosion, transform.position, explosion.transform.rotation);
        fireworks = Instantiate(fireworks, transform.position, fireworks.transform.rotation);
    }

    
    void Update()
    {
        //Si no estamos en gameover
        if (!gameOver)
        {
            //Al presionar espacio, el globo volara como un campeon
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Asi consigue un impulso
                playerRigidbody.AddForce(Vector3.up * impulso, ForceMode.Impulse);
                //Reproduce el sonido al saltar
                playerAudioSource.PlayOneShot(Boing, 0.3f);
            }

           
        }
        //Si llega al techo
        if (transform.position.y > Ylim)
        {
            //Player no supera el techo
            transform.position = new Vector3(transform.position.x, Ylim, transform.position.z);
            //Hace que el player no pueda quedarse atascado en el techo
            playerRigidbody.AddForce(Vector3.down * impulso, ForceMode.Impulse);
        }
        //Si te chocas con el suelo
        if (transform.position.y < Floor)
        {
           //Reproduce sistema de particulas
            explosion.Play();
            //reproduce el sonido boom(tarda demasado y no puede aparecer ya que el player desaparece antes)
            playerAudioSource.PlayOneShot(boom, 0.3f);
           //localizacion boom en el suelo
            explosion.transform.position = new Vector3(0, 0, 0);
            //Adios globo
            Destroy(gameObject);
            // game over
            GameOver();
        }

    }

    //Si se choca con un trigger
    private void OnTriggerEnter(Collider other)
    {
        //Si no hay game over
        if (!gameOver)
        {
            //Si se choca con la bomba
            if (other.gameObject.CompareTag("Bomb"))
            {
                //Tapate los oidos, o mejor bajo el volumen de la explosion
                playerAudioSource.PlayOneShot(boom, 0.5f);
                //Coloca la explosion donde la bomba colisiona con el player
                explosion.transform.position = other.gameObject.transform.position;
                //El sistema de particulas aparece
                explosion.Play();
                //Bomba desaperece
                Destroy(other.gameObject);

                //Player se destruye
                Destroy(gameObject);

                //Llama a la funcion game over
                GameOver();
            }
            
            //Si se choca con el dinero
            if (other.gameObject.CompareTag("Money"))
            {
                //Añade uno al dinero obtenido
                Money++;
                //Moneda be like, bling bling heheh
                playerAudioSource.PlayOneShot(blingbling, 1f);
                //Fuegos artificiales aparecen donde has recolectado el dinero
                fireworks.transform.position = other.gameObject.transform.position;
                //Y asi aparecen
                fireworks.Play();
                //La moneda se va
                Destroy(other.gameObject);
                Debug.Log($"Necesitamos" + Money + "euros menos para comprar un seguro anti pinchazos");
            }

            
        }
    }
    //Funcion de game over
    private void GameOver()
    {
        //Indica la finalizacion de la partida
        gameOver = true;
       

        
    }
    private void OnDestroy()
    {
        playerAudioSource.PlayOneShot(boom, 1f);
    }
}
