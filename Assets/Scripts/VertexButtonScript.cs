using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexButtonScript : MonoBehaviour
{

    public GameObject Vertex; // префаб вершины, которую будем размещать; Передаем в tracing для обработки
    GameObject trcButton; //пребаф кнопки tracing, чтобы передать в неё данные о вершинах // там будут хранится все данные для размещения 
    Image img;
    Color neededColor = new Color(100, 0, 0);
    public Sprite vertexSprite; // префабы спрайтов для кнопки-вершины
    public Sprite slotSprite;

    [SerializeField] Color groupColor; //данные о вершине, которую разместим, тянем из кнопки-группы
    [SerializeField] int groupNumber;
    GameObject newVertex; // создаваемая вершина

    void Start()
    {
        trcButton = GameObject.FindGameObjectWithTag("TraceButton");
        img = GetComponent<Image>();
        img.sprite = slotSprite;
        
    }

    public void Update()  // Просматриваем какая кнопка-группы нажата на данный момент, берем номер группы и цвет
    {
        bool noPressedButtons = true;
        foreach (var button in GameObject.FindGameObjectsWithTag("GroupButton"))
            if (button.GetComponent<SelectGroup>().isSelected)
            {
                groupColor = button.GetComponent<SelectGroup>().ownColor;
                groupNumber = button.GetComponent<SelectGroup>().counter;
                noPressedButtons = false;
            }

        if (noPressedButtons) groupNumber = -1;
        // обходит цикл и сохраняет отсутствие галки на последней вершине, надо сделать индикатор, чтобы ставился -1 только если нет галочек на всех вершинах
     
    }
    
    public void CreateVertex()  // удаляем или создаем вершину на кнопке-вершине. Добавляем в скрипт tracing для дальнейшего соединения                                  
    {                                       // т.к. индексация с 0, то если меньше чем 0, то нельзя спавнить


        if (groupNumber > -1)
        if (newVertex == null)
        {
            newVertex = Instantiate(Vertex, gameObject.transform.position, Quaternion.identity);
            newVertex.transform.localScale = gameObject.transform.parent.parent.localScale*2;
            newVertex.transform.GetComponent<SpriteRenderer>().color = newVertex.GetComponent<VertexInfoScript>().vertexColor = groupColor;
            newVertex.GetComponent<VertexInfoScript>().vertexGroupNumber = groupNumber-1;
           /*
             if (trcButton.GetComponent<TracingScript>().allVertices[groupNumber] == null)
                trcButton.GetComponent<TracingScript>().allVertices[groupNumber] = new List<TracingScript.VertexInfo>();         
             else if (trcButton.GetComponent<TracingScript>().allVertices[groupNumber] != null)
            {
                trcButton.GetComponent<TracingScript>().allVertices[groupNumber].Add(new TracingScript.VertexInfo(newVertex, groupColor));
            }
        */
        }
        else if (newVertex != null)
            Destroy(newVertex);
       
    }
    bool trigger = false;

    public void Switch()
    {
        if (!trigger) trigger = true;
        else if (trigger) trigger = false;

    }
}
