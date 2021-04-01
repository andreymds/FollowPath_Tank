using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;
    public float speed= 5.0f;
    public float accuracy= 1.0f;
    public float rotSpeed= 0.4f;
    public GameObject wpManager;
    GameObject[] wps; 
    GameObject currentNode;
    int currentWP= 0;
    Graph g;
    
    void Start()
    {
        wps = wpManager.GetComponent<WpManager>().waypoints; 
        g = wpManager.GetComponent<WpManager>().graph; 
        currentNode = wps[0]; //o nó que o tank se encontra no meio da matriz
    }
    public void GoToHeli() 
    { 
        g.AStar(currentNode, wps[1]); //algoritmo A*
        currentWP = 0; //define como destino final
    }
    public void GoToRuin() 
    { 
        g.AStar(currentNode, wps[5]); 
        currentWP = 0; 
    }
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) //envia o tamanho do caminho a ser percorrido
            return;
        //o nó atual
        currentNode= g.getPathPoint(currentWP);

        //se movimenta para o próximo nó
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy) 
        { 
            currentWP++;
            
        }
        
        if (currentWP < g.getPathLength()) 
        { 
            goal = g.getPathPoint(currentWP).transform; //define o destino 
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z); 
            Vector3 direction = lookAtGoal - this.transform.position; //define o vetor entre a posição atual e o destino

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, //rotação suave
                Quaternion.LookRotation(direction), 
                Time.deltaTime * rotSpeed); 
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}

