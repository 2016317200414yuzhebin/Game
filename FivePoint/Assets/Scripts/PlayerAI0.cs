using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI0 : Player
{
    SortedList<string, int> toScore;
    // Start is called before the first frame update
    void Start()
    {
        toScore = new SortedList<string, int>();
        toScore.Add("_*_", 10);

        toScore.Add("_**", 50);
        toScore.Add("**_", 50);
        toScore.Add("_**_", 100);

        toScore.Add("***_", 500);
        toScore.Add("_***", 500);
        toScore.Add("_***_", 1000);

        toScore.Add("****_", 5000);
        toScore.Add("_****", 5000);
        toScore.Add("_****_", 10000);

        toScore.Add("*****", int.MaxValue);
        toScore.Add("_*****", int.MaxValue);
        toScore.Add("*****_", int.MaxValue);
        toScore.Add("_*****_", int.MaxValue);
    }

    int CheckOneline(int[] pos, int[] offset, int chess)
    {
        string temp = "*";

        for (int x = pos[0] + offset[0], y = pos[1] + offset[1]; x < 15 && x >= 0 && y < 15 && y >= 0; x += offset[0], y += offset[1])
        {
            if (board.grid[x, y] == (int)chess)
            {
                temp = temp + "*";
            }
            else if (board.grid[x, y] == 0)
            {
                temp = temp + "_";
                break;
            }
            else
            {
                break;
            }
        }
        for (int x = pos[0] - offset[0], y = pos[1] - offset[1]; x < 15 && x >= 0 && y < 15 && y >= 0; x -= offset[0], y -= offset[1])
        {
            if (board.grid[x, y] == (int)chess)
            {
                temp = "*" + temp;
            }
            else if (board.grid[x, y] == 0)
            {
                temp = "_" + temp;
                break;
            }
            else
            {
                break;
            }
        }

        int maxScore = 0;
        foreach (var item in toScore)
        {
            if(temp.Contains(item.Key) && toScore[item.Key] > maxScore)
            {
                maxScore = toScore[item.Key];
                Debug.Log(item.Key);
            }
        }

        return maxScore;
    }

    int SetScore(int[] pos)
    {
        int score = 0;
        score += CheckOneline(pos, new int[] { 0, 1 }, 1);
        score += CheckOneline(pos, new int[] { 1, 0 }, 1);
        score += CheckOneline(pos, new int[] { 1, 1 }, 1);
        score += CheckOneline(pos, new int[] { 1, -1 }, 1);
        score += CheckOneline(pos, new int[] { 0, 1 }, 2);
        score += CheckOneline(pos, new int[] { 1, 0 }, 2);
        score += CheckOneline(pos, new int[] { 1, 1 }, 2);
        score += CheckOneline(pos, new int[] { 1, -1 }, 2);
        return score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Play()
    {
        int maxX = 7, maxY = 7;
        int maxScore = 40; // 空き位置の最低点数は40になります
        if (board.grid[7, 7] != 0) maxScore = 0;
        for (int x = 0; x < 14; x++)
        {
            for (int y = 0; y < 14; y++)
            {
                if (board.grid[x, y] != 0) continue;
                int score = SetScore(new int[] { x, y });
                if (score > maxScore)
                {
                    maxX = x;
                    maxY = y;
                    maxScore = score;
                }
            }
        }
        board.PlayChess(new int[] { maxX, maxY });
    }
}
