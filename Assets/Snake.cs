using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.left;
    private Vector2 input;
    private List<Transform> segments = new List<Transform>();
    public Transform segentPrefab;
    public int initialSize = 4;

    private void Start() 
    {
      ResetState(); 
    }
    
    private void Update()
    {
        
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
            {
              input = Vector2.up;
            } 
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
            {
              input = Vector2.down;
            }
        }
        
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
            {
              input = Vector2.right;
            } 
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
              input = Vector2.left;
            }
        }
    }
    private void FixedUpdate() 
    {
      if (input != Vector2.zero) 
      {
        direction = input;
      }      
      for(int i = segments.Count - 1; i > 0 ; i--)
      {
        segments[i].position = segments[i-1].position;
      }
      this.transform.position = new Vector3(
        Mathf.Round(this.transform.position.x) + direction.x,
        Mathf.Round(this.transform.position.y) + direction.y,
        0.0f);
    }

    private void Grow()
    {
      Transform segment = Instantiate(this.segentPrefab);
      segment.position = segments[segments.Count - 1].position;

      segments.Add(segment);

    }
    
    private void ResetState()
    {
       for (int i = 1; i < segments.Count; i++)
        {
          Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        for(int i = 1;i<this.initialSize;i++)
        {
          segments.Add(Instantiate(this.segentPrefab));
        }
        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
      if(other.tag == "Food")
      {
        Grow();
      }

      else if(other.tag == "Obstacle")
      {
        Vector3 position = transform.position;

        if (direction.x != 0f) 
        {
            position.x = -other.transform.position.x + direction.x;
        } 
        else if (direction.y != 0f) 
        {
            position.y = -other.transform.position.y + direction.y;
        }

        transform.position = position;
      }  

      else if(other.tag == "Self")
      {
        ResetState();
      }  
    }
}
