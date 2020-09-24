using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewGroup : MonoBehaviour {
    
    public GameObject groupButton;
    public GameObject createButton;
    public Camera myCam;
    Transform parent;
    Vector2 neededScale;
    

    float verstep = 1.1f;
    float horstep;
    public int maxGroups = 21;
    float firstX;
    [SerializeField] int counter = 0;

	void Start () {
      
        neededScale = new Vector2(transform.localScale.x, transform.localScale.y);
        parent = gameObject.transform.parent;
	}
	
    public void CreateAll()
    {
        for (int i = 0; i < maxGroups; i++)
        {
            Create();
            
        }
    }
	// Update is called once per frame
    // cameraSize = 5, 2unityp ~ 80 rect
	public void Create () {
        verstep = (myCam.orthographicSize/5) * 1.1f;
        horstep = verstep * 1.3f;
        counter++;
        if (counter < maxGroups+1)      // счет кнопок групп ведется с 1, а не с 0
        {
            if (counter < maxGroups)
            {
                GameObject create = Instantiate(createButton, transform.position, Quaternion.identity);
                create.transform.parent = parent;
                create.transform.localScale = neededScale;
                if (counter%3 != 0)
                create.transform.position = new Vector2(transform.position.x + horstep, transform.position.y );
                else
                    create.transform.position = new Vector2(transform.position.x - horstep*2, transform.position.y - verstep);
            }
            GameObject group = Instantiate(groupButton, transform.position, Quaternion.identity);       // спавним новую кнопку-группы и новую кнопку-создания
            group.GetComponent<SelectGroup>().counter = this.counter-1;                                   // передаем счетчик в скрипт кнопки-группы, чтобы там по номеру кнопки узнать нужный цвет
            group.GetComponent<SelectGroup>().maxGroups = this.maxGroups;
            group.transform.parent = parent;                                                            // добавляем родителей, чтобы кнопки были в Canvas
            group.transform.localScale = neededScale;
            Text[] textChildren = group.GetComponentsInChildren<Text>();    
            textChildren[textChildren.Length-1].text = counter.ToString();
        }
            Destroy(gameObject);    // удаляем старую кнопку-создания на том месте, где появилась новая кнопка-группы
        
    }
}
