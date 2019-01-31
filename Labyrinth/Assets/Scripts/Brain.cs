using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    float l_r = 0.5f;
    float gamma = 0.8f;

    float[,] q_table;
    int[,] reward_table;

    [SerializeField]
    int area = 10;

    [SerializeField]
    float no_find_time = 15;

    float time = 0;
    void Awake()
    {
        q_table = new float[area, area];
        reward_table = new int[area, area];
    }

    public int[] Q_Learning(int x, int y, List<int[]> roads)
    {
        if (roads.Count < 2 || time < Time.deltaTime)
        {
            time = no_find_time + Time.deltaTime;
            int[] array = { -2, -2 };
            reward_table[x, y] = -100;
            return array;
        }
        else if ((x == 29 || x == 1) && (y == 29 || y == 1) || time < Time.deltaTime)
        {
            time = no_find_time + Time.deltaTime;
            int[] array = { -2, -2 };
            reward_table[x, y] = 100;
            return array;
        }
        else
        {
            float max_val = -101;
            float min_val = 101;
            int new_x = 0, new_y = 0;
            for (int i = 0; i < roads.Count; i++)
            {
                if (max_val == -101 || max_val < q_table[x + roads[i][0], y + roads[i][1]])
                {
                    max_val = q_table[x + roads[i][0], y + roads[i][1]];
                    new_x = roads[i][0];
                    new_y = roads[i][1];
                }
                if (min_val == 101 || min_val > q_table[x + roads[i][0], y + roads[i][1]])
                {
                    min_val = q_table[x + roads[i][0], y + roads[i][1]];
                }
            }
            if (min_val == max_val || max_val == 0)
            {
                int road;
                int count = 0;
                do
                {
                    road = Random.Range(0, roads.Count);
                    if (q_table[x + roads[road][0], y + roads[road][1]] >= 0)
                    {
                        int new_x_val = x + roads[road][0];
                        int new_y_val = y + roads[road][1];
                        q_table[new_x_val, new_y_val] +=
                            l_r * (
                            reward_table[new_x_val, new_y_val]
                            + gamma * (
                                Max(new_x_val, new_y_val, roads[road][0], roads[road][1])
                                + Min(new_x_val, new_y_val, roads[road][0], roads[road][1])
                                - q_table[new_x_val, new_y_val])
                            );
                        new_x = roads[road][0];
                        new_y = roads[road][1];
                        break;
                    }
                } while (count++<1000);
                if (count>=900)
                {
                    Debug.Log("Count >>");
                }
            }
            else
            {
                q_table[x + new_x, y + new_y] += l_r *
                    (reward_table[x + new_x, y + new_y]
                        + gamma * (Max(x + new_x, y + new_y, new_x, new_y) 
                        + Min(x + new_x, y + new_y, new_x, new_y)
                        - q_table[x + new_x, y + new_y]));
            }
            int[] array = { new_x, new_y };
            string q = "";
            string r = "";
            for (int i = 0; i < area; i++)
            {
                for (int j = 0; j < area; j++)
                {
                    q += q_table[i, j].ToString() + " ";
                    r += reward_table[i, j].ToString() + " ";
                }
                q += "\n";
                r += "\n";
            }
            Debug.Log("q\n"+q);
            Debug.Log("r\n"+r);
            return array;
        }
    }

    float Max(int x, int y, int new_x, int new_y)
    {
        float max_q = 0;
        if (new_x != 1 && q_table[x - 1, y] > max_q)
        {
            max_q = q_table[x - 1, y];
        }
        if (new_x != -1 && q_table[x + 1, y] > max_q)
        {
            max_q = q_table[x + 1, y];
        }
        if (new_y != 1 && q_table[x, y - 1] > max_q)
        {
            max_q = q_table[x, y - 1];
        }
        if (new_y != -1 && q_table[x, y + 1] > max_q)
        {
            max_q = q_table[x, y + 1];
        }
        return max_q;
    }

    float Min(int x, int y, int new_x, int new_y)
    {
        float min_q = 0;
        if (new_x != 1 && q_table[x - 1, y] < min_q)
        {
            min_q = q_table[x - 1, y];
        }
        if (new_x != -1 && q_table[x + 1, y] < min_q)
        {
            min_q = q_table[x + 1, y];
        }
        if (new_y != 1 && q_table[x, y - 1] < min_q)
        {
            min_q = q_table[x, y - 1];
        }
        if (new_y != -1 && q_table[x, y + 1] < min_q)
        {
            min_q = q_table[x, y + 1];
        }
        return min_q;
    }
}
