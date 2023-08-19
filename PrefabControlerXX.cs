using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabControlerXX : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = GameManager.Instance.time;
        gameObject.tag = "Block";
        UpdateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameActive){
            time -= Time.deltaTime;
            if(time <= 0.0f){
                transform.position += new Vector3(0, -1, 0);
                UpdateGrid();
                time = GameManager.Instance.time;
            }
        }
    }
    void LateUpdate(){
        if(transform.position.y == 0){
            GameManager.Instance.GameOver();
        }
    }

    public void  UpdateGrid(){
        for(int y = 0; y < Playfield.h; ++y)
        {
            for(int x = 0; x < Playfield.w; ++x)
            {
                if (Playfield.grid[x, y] != null)
                {
                    if(Playfield.grid[x,y].transform == transform)
                    {
                        Playfield.grid[x, y] = null;
                        GameManager.Instance.GameManagerGrid[x, y] = 0;
                    }
                }
            }
        Vector2 v2 = Playfield.roundVec2(transform.position);
        Playfield.grid[(int)v2.x, (int)v2.y] = transform;
        GameManager.Instance.GameManagerGrid[(int)v2.x, (int)v2.y] = 1;
    }   
}
    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            //Debug.Log(col.gameObject.name);
            GameManager.Instance.GameOver();
        }
    }
    public void DestroyBlocks(){
        Destroy(gameObject);
    }
}
