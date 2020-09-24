using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetGridnVertices: MonoBehaviour {

	struct Coords
    {
        double x, y;
    }
    public GameObject element; // element = vertex + grid piece
    public GameObject gridPiece;
    public GameObject info;
    public Slider slider;
    public Vector3 neededScale;
    public bool trigger = false;
    float value;
    int n;
 
    public void Enable() {
        trigger = true;
    }
    public void Disable() {
        trigger = false;
    }
 
    public void SetCoordinateGrid() // nXn - size of coord grid
    {       
             n = (int)Mathf.Round((value * 10));
            foreach (var gameobject in GameObject.FindGameObjectsWithTag("Grid")) Destroy(gameobject);
            foreach (var gameobject in GameObject.FindGameObjectsWithTag("Vertex")) Destroy(gameobject);
            foreach (var gameobject in GameObject.FindGameObjectsWithTag("Edge")) Destroy(gameobject);


        double gridLength = 8;
            Vector2 origin = new Vector2(-4, -4); // начало координат, нижняя левая точка
            double oneElemSize = gridLength / (n + 1); // размер одного элемента в координартах Юнити
            double decrCoeff = oneElemSize / 4;
             neededScale = new Vector3((element.transform.localScale.x) * (float)decrCoeff, (element.transform.localScale.y) * (float)decrCoeff, 0);


            for (int i = 0; i < n; i++)     // размещаем вершины и кусочки координатной сетки
            {
                GameObject gridpiece1 = Instantiate(gridPiece, new Vector2(origin.x + (float)(oneElemSize * (i + 1)), 4f - (float)oneElemSize / 4), new Quaternion(0, 0, 90, 90));
                GameObject gridpiece2 = Instantiate(gridPiece, new Vector2(origin.x + (float)(oneElemSize * (i + 1)), -4f + (float)oneElemSize / 4), new Quaternion(0, 0, 90, 90));
                gridpiece1.transform.localScale = neededScale / 2;
                gridpiece2.transform.localScale = neededScale / 2;
                for (int j = 0; j < n; j++)
                {
                    if (i == 0)
                    {
                        GameObject gridpiece3 = Instantiate(gridPiece, new Vector2(-4f + (float)oneElemSize / 4, origin.y + (float)(oneElemSize * (j + 1))), Quaternion.identity);
                        GameObject gridpiece4 = Instantiate(gridPiece, new Vector2(4f - (float)oneElemSize / 4, origin.y + (float)(oneElemSize * (j + 1))), Quaternion.identity);
                        gridpiece3.transform.localScale = neededScale / 2;
                        gridpiece4.transform.localScale = neededScale / 2;
                    }
                    GameObject temp = Instantiate(element, new Vector2(origin.x + (float)(oneElemSize * (i + 1)), origin.y + (float)(oneElemSize * (j + 1))), Quaternion.identity);             
                    temp.transform.localScale = neededScale;
                }
            }
        
    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        value = slider.value;
        info.GetComponent<Text>().text = n + "x" + n;

        if (trigger)
        SetCoordinateGrid();
	}
}
