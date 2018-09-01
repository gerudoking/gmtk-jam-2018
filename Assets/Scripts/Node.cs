using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//G cost = distance from start node
//H cost (heurist) = distance from target
//F cost = G + H
//choose the one with the lowest F cost
//if both nodes have the same cost, choose the one if lowest H cost

public class Node {

    public bool walkable;
    public Vector3 worldPosition;

    public int gCost; //modulo ate o inicio
    public int hCost; //modulo ate o alvo

    public int gridX;
    public int gridY;

    public Node parent;

    public Node(bool _walkable, Vector3 _wordPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _wordPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost // soma G + H
    {
        get
        {
            return gCost + hCost;// ja posso atribuir para dar retorno
        }

        //nao vamos determinar ele, ele sempre depende dos valores g + h, desnecessario set
     }
}
