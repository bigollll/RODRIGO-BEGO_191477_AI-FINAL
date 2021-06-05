using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour {

	float speed = 20.0F;              //velocidade do player.
    float rotationSpeed = 120.0F;     //velocidade de rotação.
    float health = 10.0f;           //define a vida inicial.

    public GameObject bulletPrefab;   //prefab da bala.
    
    public Transform bulletSpawn;     //ponto de spawn da bala.
   
    public Slider healthBar;        //barra de vida.

    
  


    private void Start()
    {
        
    }

    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;             //movimentação frontal.
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;      //movimentação rotativa.
       
        translation *= Time.deltaTime;                                     //movimentao pelo delta time.
        rotation *= Time.deltaTime;                                        //rotação pelo delta time.
       
        transform.Translate(0, 0, translation);                            //vetor que modifica qnd há movimentação
        transform.Rotate(0, rotation, 0);                                  //vetor que modifica qnd há rotação
        
        healthBar.value = (int)health;                                    //associando a barra de vida da UI com o a vida dada ao player.
        Vector3 healthBarPos = Camera.main.WorldToScreenPoint(this.transform.position);    //a barra de vida segue a camera.
        healthBar.transform.position = healthBarPos + new Vector3(0, 60, 0);                 //posição da barra de vida.

        if (Input.GetKeyDown("space"))          //se apertar Espaço
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);  //spawna o prefab da bala no ponto de bulletSpawn
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward*2000);                                                  //pegando o rigid body do prefab da bala e adcionando força para frente nela
        }

        if(health == 0)                                               //se a vida for = a 0.
        {
            transform.position = new Vector3(239.5f, 0, -218f);       //manda o player pra posição incluida no parametro.
            health = 100;                                             //reestabelece a vida para 100
        }
        
    }

    
    void OnCollisionEnter(Collision col)                 //detecta colisão
    {
        if (col.gameObject.tag == "bullet")              //se colidir com a bala, toma dano .
        {
            health -= 10;                               //tira 10 de vida por bala.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "out")
        {
            transform.position = new Vector3(239.5f, 0, -218f);       //manda o player pra posição incluida no parametro.
            health = 100;                                             //reestabelece a vida para 100
        }
    }



}
