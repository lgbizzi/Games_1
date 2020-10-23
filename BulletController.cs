using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	//Componente RigidBody2D contida no projétil
    Rigidbody2D rb2d;

    void Start()
    {
        //Adiciona intensidade ("força") ao projétil e o destrói após 5s

        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddRelativeForce(Vector2.up * 800);

        Destroy(gameObject, 5.0f);
    }

    //Ação de dano do projétil
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
