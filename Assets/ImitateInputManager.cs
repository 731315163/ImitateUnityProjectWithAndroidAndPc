using System.Collections;
using System.Collections.Generic;
using Assets.TVAdapation.Scripts.InputHandle;
using UnityEngine;

namespace Assets.TVAdapation.Scripts
{
    public class ImitateInputManager : MonoBehaviour
    {
        protected Queue<KeyValuePair<KeyState,Vector2>> eventqueue = new Queue<KeyValuePair<KeyState, Vector2>>();
        // Use this for initialization
        void Start()
        {
            StartCoroutine(ImitateInputSend());
            
        }

       
       
        public void AddImitateEvent(Vector2 begin, Vector2 end)
        {
            AddImitateDownEvent(begin);
            if (begin != end)
            {
                int length = 4;
                float width = end.x - begin.x;
                float height = end.y - begin.y;
                for (int i = 1; i <= 4; i++)
                {
                    Vector2 move = new Vector2(width*i/length+begin.x,height*i/length+begin.y);
                    AddImitateMoveEvent(move);
                }
                
            }
            AddImitateUpEvent(end);
        }

        public void AddImitateEvent(Vector2 begin, Vector2 end,  Vector2[] moves)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Down,begin));

            foreach (var move in moves)
            {
                eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Hold,move));
            }

            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Up,end));
        }
        public void AddImitateDownEvent(Vector2 begin)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Down,begin));
        }

        public void AddImitateMoveEvent(Vector2 move)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Hold,move));
        }
        public void AddImitateUpEvent(Vector2 up)
        {
            eventqueue.Enqueue(new KeyValuePair<KeyState, Vector2>(KeyState.Up,up));
        }

 

        IEnumerator ImitateInputSend()
        {
            while (true)
            {
               SendEventToAndroid();
                yield return null;
            }
          
        }

        protected void SendEventToAndroid()
        {
            if (eventqueue.Count > 0)
            {
                var input = eventqueue.Dequeue();
                if (input.Key == KeyState.Down)
                {
                    ImitateInput.ImitateDown((int) input.Value.x, (int) input.Value.y);
                }
                else if (input.Key == KeyState.Hold)
                {
                    ImitateInput.ImitateMove((int)input.Value.x,(int)input.Value.y);
                    
                }
                else if (input.Key == KeyState.Up)
                {
                    ImitateInput.ImitateUp((int) input.Value.x, (int) input.Value.y);
                }
            }
        }
    }
}
