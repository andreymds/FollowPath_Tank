using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //distribuição de informações que transmitem dados para diferentes tipos de classes

//constroem as conexões entre os pontos
public struct Link{  
    public enum direction { UNI, BI } //define se é unidirecional ou bidirecional
    public GameObject node1; 
    public GameObject node2; 
    public direction dir;
}
public class WpManager : MonoBehaviour
{
    public GameObject[] waypoints; 
    public Link[] links; 
    public Graph graph = new Graph(); //cria objeto Grafo

    void Start()
    {
        if (waypoints.Length > 0) //condição de existência do Grafo
        { 
            foreach (GameObject wp in waypoints)
            { 
                graph.AddNode(wp); 
            } 
            foreach (Link l in links) //relacionando o array com os links criados
            { 
                graph.AddEdge(l.node1, l.node2); //adiciona uma aresta entre os nós 
                
                //se caso for bidirecional adiciona uma aresta entre os mesmos nós na direção contrária
                if (l.dir == Link.direction.BI)                     
                    graph.AddEdge(l.node2, l.node1); 
            } 
        }
    }
        
    void Update()
    {
        graph.debugDraw(); // traça visualmente as arestas
    }
}
