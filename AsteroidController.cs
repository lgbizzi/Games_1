using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    // 3 prefabs distintos
    [SerializeField]
    GameObject[] asteroids;

    bool division = true; // pode se dividir? não
    Rigidbody2D rb2d;

    void Start()
    { 
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Chamado quando objeto colide com este e o asteroide se divide
    // deverão estar no modo Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // flitragem por TAG
        if(other.CompareTag("Player"))
        {
			//Divisor dos asteroids
            if (division)
            {
                Vector3[] directions = new Vector3[3];
                directions[0] = other.transform.up;
                directions[1] = other.transform.right;
                directions[2] = -other.transform.right;

                for (int i = 0; i < 3; i++)
                {
					//solução para a nave não bater com o asteroide e fragmentos recém criados

                    GameObject asteroidClone = Instantiate(asteroids[i], transform.position + directions[i], transform.rotation);
                    asteroidClone.GetComponent<Rigidbody2D>().velocity = directions[i] * 1f;
                    asteroidClone.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
                    asteroidClone.GetComponent<AsteroidController>().SetDivision(false);
                }
            }
            
            Destroy(gameObject);
        }
    }

    public void SetDivision(bool value)
    {
        division = value;
    }
}
