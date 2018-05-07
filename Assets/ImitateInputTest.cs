
using UnityEngine;

using System.Text;
using Assets.TVAdapation.Scripts;
using UnityEngine.UI;


[RequireComponent(typeof(ImitateInputManager))]
public class ImitateInputTest : MonoBehaviour
{
    
    private StringBuilder s = new StringBuilder("BUG在这里");
    public ImitateInputManager ImitateInput;
    public Text text;

    void Awake()
    {
        Screen.fullScreen = true;
        text = Object.FindObjectOfType<Text>();
       
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        move();
        update();


    }

  
    

    public void move()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ImitateInput.AddImitateMoveEvent(new Vector2(300,300));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
               ImitateInput.AddImitateDownEvent(new Vector2(0,0));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ImitateInput.AddImitateEvent(new Vector2(300,300),new Vector2(500,500));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ImitateInput.AddImitateUpEvent(new Vector2(0,0));
        }
    }

    void update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                var position = Input.touches[0].position;
                s.Append("Move");
                s.Append(" ");
                s.Append((int)position.x);
                s.Append(" ");
                s.Append((int)position.y);
            }
            else if (Input.touches[0].phase == TouchPhase.Began)
            {
                var position = Input.touches[0].position;
                s.Append("Down");
                s.Append(" ");
                s.Append((int)position.x);
                s.Append(" ");
                s.Append((int)position.y);
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                var position = Input.touches[0].position;
                s.Append("Up");
                s.Append(" ");
                s.Append((int)position.x);
                s.Append(" ");
                s.Append((int)position.y);
            }

            text.text = s.ToString();
        }
    }
  
}
