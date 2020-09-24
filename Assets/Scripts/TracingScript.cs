using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingScript : MonoBehaviour
{
    bool isAllowed;
    public GameObject EdgePrefab;
    GameObject[] foundVertices;
    List<Color> colors;
    public List<List<GameObject>> allVertices; // вершины для соединения

    class Edge
    {
        public GameObject vert1, vert2;
        public Edge(GameObject argvert1, GameObject argvert2)
        {
            vert1 = argvert1;
            vert2 = argvert2;
        }
    }
    public class Vertex // используется в метода Прима как temp
    {
        public GameObject vert;
        public bool isPlaced;
        public Vertex(GameObject vert)
        {
            this.vert = vert;
            isPlaced = false;
        }
    }
    public void Allow()
    {
        if (isAllowed) isAllowed = false;
        else if (!isAllowed) isAllowed = true;
    }
    public void Clear()
    {
        foundVertices = new GameObject[0];
        colors.Clear();
        allVertices.Clear();
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Vertex")) Destroy(gameobject);
        foreach (var gameobject in GameObject.FindGameObjectsWithTag("Edge")) Destroy(gameobject);
    }

    public void GetVertices()
    {
        colors = new List<Color>();

        foundVertices = GameObject.FindGameObjectsWithTag("Vertex");

        for (int i = 0; i < foundVertices.Length; i++)
            if (!colors.Contains(foundVertices[i].GetComponent<VertexInfoScript>().vertexColor))        // заносим в colors по одному цвету
                colors.Add(foundVertices[i].GetComponent<VertexInfoScript>().vertexColor);

        allVertices = new List<List<GameObject>>(colors.Count);

        for (int i = 0; i < colors.Count; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            foreach (var vert in foundVertices)
                if (vert.GetComponent<VertexInfoScript>().vertexColor == colors[i])
                    temp.Add(vert);

            allVertices.Add(temp);
        }

        for (int i = 0; i < allVertices.Count; i++)
        {
            foreach (var ver in allVertices[i])
                Debug.Log(i + "# ~ color = " + ver.GetComponent<VertexInfoScript>().vertexColor);
        }
    }
    List<Edge> Prime(List<GameObject> verticesList)       
    {
        double[,] lengthsMatrix = new double[verticesList.Count, verticesList.Count];
        for (int n = 0; n < verticesList.Count; n++)
        {
            for (int m = 0; m < verticesList.Count; m++)
            {
                lengthsMatrix[n, m] = Mathf.Sqrt(Mathf.Pow((verticesList[n].transform.position.x - verticesList[m].transform.position.x), 2) + Mathf.Pow((verticesList[n].transform.position.y - verticesList[m].transform.position.y), 2));
            }
        }

        List<Vertex> tempVertices = new List<Vertex>();
        foreach (var vert in verticesList)
            tempVertices.Add(new Vertex(vert));

        List<Edge> edges = new List<Edge>();
        int vert1 = 0; int vert2 = 0;
        int i = 0;
        tempVertices[0].isPlaced = true;

        while (i < verticesList.Count - 1)
        {
            i++;
            double minDistance = double.MaxValue;

            for (int r = 0; r < tempVertices.Count; r++)
            {
                if (tempVertices[r].isPlaced)
                {
                    for (int g = 0; g < tempVertices.Count; g++)
                    {
                        if (!tempVertices[g].isPlaced)
                        {
                            double temp = lengthsMatrix[r, g];
                            if (minDistance > temp)
                            {
                                minDistance = temp;
                                vert1 = r; vert2 = g;
                            }
                        }
                    }
                }
            }
            tempVertices[vert2].isPlaced = true;
            edges.Add(new Edge(tempVertices[vert1].vert, tempVertices[vert2].vert));
        }
        return edges;
    }
    void DrawEdges(List<Edge> edgesList)
    {
        foreach (var edge in edgesList)
        {
            Vector3[] tempCoords = new Vector3[2] { edge.vert1.transform.position, edge.vert2.transform.position };
            GameObject temp = Instantiate(EdgePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            temp.GetComponent<LineRenderer>().positionCount = 2;
            temp.GetComponent<LineRenderer>().SetPositions(tempCoords);
            temp.GetComponent<LineRenderer>().startColor = edge.vert1.GetComponent<VertexInfoScript>().vertexColor;
            temp.GetComponent<LineRenderer>().endColor = edge.vert1.GetComponent<VertexInfoScript>().vertexColor;

        }
    }
    void Trace()
    {
        if (GameObject.FindGameObjectsWithTag("Edge").Length > 0)       
            foreach (var gameobject in GameObject.FindGameObjectsWithTag("Edge")) Destroy(gameobject);
        

        GetVertices();
        foreach (var vertList in allVertices)
            DrawEdges(Prime(vertList));
    }

    void Update()
    {
        if (isAllowed) Trace();
    }

    [ContextMenu("FindElems")]
    public void RemoveCrosses()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("VertexButton").Length; i++)
        {
            Debug.Log(" Elem = " + (i+1));
        }
    }
}
// Сделать так, чтобы нельзя было размещать вершины без задания группы - СДЕЛАНО
// сделать всплывающую менюшку 
// Ограничить количество групп, которые можно создать, количеством доступных цветов - СДЕЛАНО
// Добавить предупреждение, что если задать новое поле, то все размещенные вершины удалятся
//
