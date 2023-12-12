using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Node : MonoBehaviour
{
    //Cambiarlo de color cada vez que se setee como nodo inicial, final, o nodo vecino

    Renderer _renderer;
    List<Node> _neighbors = new List<Node>();

    GridGenerator _grid;

    Coordinates coordinates;

    int _cost = 1;
    public int Cost { get { return _cost; } }
    //[SerializeField] TextMeshProUGUI _costUI;

    private bool _blocked;
    public bool Blocked
    {
        get { return _blocked; }
        //set { _blocked = value; NewColor(value ? Color.black : PathFindingManager.instance._previousNodeColor); }
    }

    public void Initialize(GridGenerator grid, int x, int y)
    {
        _grid = grid;
        _renderer = GetComponent<Renderer>();
        coordinates = new Coordinates(x, y);
        //Blocked = false;
        SetCost(1);
    }

    //void OnMouseDown() //Objeto clickeado con collider
    //{
    //    //PathFindingManager.instance.SetMyStartingNode(this);
    //}

    //private void OnMouseOver() //Hacer algo cuando la posicion del mouse esta encima de este objeto con el script asignado
    //{
    //    //if(Input.GetMouseButtonDown(0)) PathFindingManager.instance.SetMyStartingNode(this);
    //    //if(Input.GetMouseButtonDown(1)) PathFindingManager.instance.SetMyGoalNode(this); //Goal
    //    //if (Input.GetMouseButtonDown(2)) //Ruedita del mouse
    //    //{
    //    //    //Blocked = !_blocked;
    //    //}
    //    //if (Input.mouseScrollDelta.y > 0) SetCost(_cost + 5);
    //    //if (Input.mouseScrollDelta.y < 0) SetCost(_cost - 5);
    //}

    private void SetCost(int cost)
    {
        _cost = cost < 1 ? 1 : cost; //Operador ternario
        if (cost == 6) _cost = 5;
        //NewColor(_cost > 1 ? new Color(0f, 0.3396226f, 0.01141823f) : new Color(1f, 0.5718155f,0f));
        //_costUI.enabled = _cost != 1;
        //_costUI.text = _cost.ToString();
    }

    public void NewColor(Color color)
    {
        _renderer.material.color = color;
    }

    public Color PreviousColor()
    {
        return _renderer.material.color;
    }

    public List<Node> GetNeighbors()
    {
        if(_neighbors.Count == 0)
        {
            _neighbors = _grid.GetNeighborsAtPosition(coordinates.x, coordinates.y);
        }
        return _neighbors;
    }
}

struct Coordinates
{
    public int x, y;

    public Coordinates(int X, int Y)
    {
        x = X;
        y = Y;
    }
}
