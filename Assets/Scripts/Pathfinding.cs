using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    public Transform seeker, target;
    
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();    
    }

    void Update()
    {
        FindPath(seeker.position, target.position);//abra o unity e defina o jogador como alvo Target em scripts, e os monstros como seeker    
    }


    void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);//adiciona node inicial a lista Open

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // primeiro valor a lista open é o valor do atual node
            for (int i =1; i < openSet.Count; i++)
            {
                // considere todos os valores dentro da lista open
                //se o valor F de algum nodo for menor que o atual
                //ou se for igual e o valor H for menor do que o do atual
                // o novo nodo se tornará o nodo atual
                if (openSet[i].fCost < currentNode.fCost || ( openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost ) )
                {
                    currentNode = openSet[i];
                }

            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                Caminho_Reverso(startNode, targetNode);
                return;
            }

            foreach (Node neighbours in grid.GetNeighbours(currentNode))
            {
                //se nao da pra passar ou ja ta na lista dos fechados
                if(!neighbours.walkable || closedSet.Contains(neighbours)){
                    continue;
                }
                    
                

                int new_MovCost = currentNode.gCost + GetDistanceA_B(currentNode, neighbours);

                if(new_MovCost < neighbours.gCost || !openSet.Contains(neighbours) )
                {
                    neighbours.gCost = new_MovCost;
                    neighbours.hCost = GetDistanceA_B(neighbours, targetNode);
                    neighbours.parent = currentNode;

                    if (!openSet.Contains(neighbours))
                    {
                        openSet.Add(neighbours);
                    }
                }
            }
        }
    }
    void Caminho_Reverso(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
    }



    //calcular distancia
    int GetDistanceA_B(Node nodeA, Node nodeB)
    {
        int moduloX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int moduloY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return moduloX + moduloY;

    }
}


//G cost = distance from start node
//H cost (heurist) = distance from target
//F cost = G + H
//choose the one with the lowest F cost
//if both nodes have the same cost, choose the one if lowest H cost
