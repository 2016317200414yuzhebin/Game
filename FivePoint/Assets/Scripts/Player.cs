using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit hit;
    public Camera cam;
    public ChessBoard board;
    public ChessType chess;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() // AI
    {
        if (board.turn != chess || board.ifwin) return;
        Play();
    }

    // Update is called once per frame
    void Update() // Player
    {
        if (board.turn == chess || board.ifwin) return;
        Play();
    }

    public virtual void Play()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // マウス クリックがあるかどうかを登僅する
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) // クリックのオブジェクトを函誼する
            {
                board.PlayChess(new int[] { (int)(hit.point.x + 0.5f), (int)(hit.point.y + 0.5f) });
                // print(hit.point.x + " , " + hit.point.y);
            }
        }
    }
}
