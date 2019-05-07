using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Esfera : MonoBehaviour
{

    

    private Rigidbody rb;

    public GameObject manager;
    public Manager managerScript;

    private NavMeshAgent myNavMeshAgent;

    //Quité el contador de monedas de esta clase y lo hice en la clase Manager porque al momento de usar un 
    //Pelota(Clone) no funcionaba la UI, ahora en el Manager es  un contador GLOBAL
    //public int ContMonedas;

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Hago referencia al GameObject Manager y a su Script
        manager = GameObject.Find("Manager");
        managerScript = manager.GetComponent<Manager>();

        myNavMeshAgent = GetComponent<NavMeshAgent>();
        //ContMonedas = 0;

    }

    private void FixedUpdate()
    {
        if (rb)
        {
            rb.AddForce(Input.GetAxis("Vertical") * force, 0, Input.GetAxis("Horizontal") * force);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        //Para detectar la posiciion del Mouse en cualquier parte de la pantalla
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Se ha presionado el mouse");
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hitInfo;

            

            if (Physics.Raycast(myRay, out hitInfo))
            {
                //Debug.Log("Se ha presionado el Mouse en la posicion: " + hitInfo.transform.position.ToString());
                Debug.DrawRay(myRay.origin, myRay.direction * 100, Color.yellow);
                Debug.Log(hitInfo.point);

                if (true)
                {
                    //Debug.Log("Se ha presionado el Mouse en la posicion: " + hitInfo.transform.position.ToString());
                    myNavMeshAgent.SetDestination(hitInfo.point);
                    //rb.AddForce(-hitInfo.normal * force, ForceMode.Impulse);
                }

            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        //Las monedas se destruyen en el script Moneda, para que sea propiedad de la moneda y no pelota
        if (other.gameObject.tag == "Moneda")
        {
            //En el Laboratorio 2
            //ContMonedas += 1;

            //Laboratorio 3 para variable Global
            managerScript.contadorMonedas += 1;
        }

        //Solo se necesita 1 power up para sobrevivir la lava
        if (other.gameObject.tag == "Lava" && managerScript.contadorMonedas < 1)
        {
            Destroy(gameObject);
        }

        //Solo se necesita 1 power up para sobrevivir la lava
        if (other.gameObject.tag == "SanicMob")
        {
            Destroy(gameObject);
        }

    }
    //Esto salta
    private void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.005f)
        {
            rb.AddForce(0, 2*force, 0, ForceMode.Impulse);
        }
    }
}
