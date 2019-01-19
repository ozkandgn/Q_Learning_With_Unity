using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    float l_r = 0.15f;
    float gamma = 0.8f;

    float[,] q_table;
    int[,] reward_table;

    void Awake()
    {
        q_table = new float[35, 35];
        reward_table = new int[35, 35];
    }

    public int[] Q_Learning(int x, int y, List<int> roads)
    {
        if (roads.Count < 2)
        {
            int[] array = { -2, -2 };
            if (roads[0] == 1)
            {
                reward_table[x, y] = -100;
            }
            else if (roads[0] == 2)
            {
                reward_table[x, y] = -100;
            }
            else if (roads[0] == 3)
            {
                reward_table[x, y] = -100;
            }
            else
            {
                reward_table[x, y] = -100;
            }
            return array;
        }
        else if ((x == 29 || x == 1) && (y == 29 || y == 1))
        {
            int[] array = { -2, -2 };
            if (roads[0] == 1)
            {
                reward_table[x, y] = 100;
            }
            else if (roads[0] == 2)
            {
                reward_table[x, y] = 100;
            }
            else if (roads[0] == 3)
            {
                reward_table[x, y] = 100;
            }
            else
            {
                reward_table[x, y] = 100;
            }
            return array;
        }
        else
        {
            float max_val = -101;
            float min_val = 101;
            int new_x = 0, new_y = 0;
            for (int i = 0; i < roads.Count; i++)
            {
                if (roads[i] == 1)
                {
                    if (max_val == -101 || max_val < q_table[x + 1, y])
                    {
                        max_val = q_table[x + 1, y];
                        new_x = 1;
                        new_y = 0;
                    }
                    if (min_val == 101 || min_val > q_table[x + 1, y])
                    {
                        min_val = q_table[x + 1, y];
                    }
                }
                else if (roads[i] == 2)
                {
                    if (max_val == -101 || max_val < q_table[x - 1, y])
                    {
                        max_val = q_table[x - 1, y];
                        new_x = -1;
                        new_y = 0;
                    }
                    if (min_val == 101 || min_val > q_table[x - 1, y])
                    {
                        min_val = q_table[x - 1, y];
                    }
                }
                else if (roads[i] == 3)
                {
                    if (max_val == -101 || max_val < q_table[x, y + 1])
                    {
                        max_val = q_table[x, y + 1];
                        new_y = 1;
                        new_x = 0;
                    }
                    if (min_val == 101 || min_val > q_table[x, y + 1])
                    {
                        min_val = q_table[x, y + 1];
                    }
                }
                else
                {
                    if (max_val == -101 || max_val < q_table[x, y - 1])
                    {
                        max_val = q_table[x, y - 1];
                        new_y = -1;
                        new_x = 0;
                    }
                    if (min_val == 101 || min_val > q_table[x, y - 1])
                    {
                        min_val = q_table[x, y - 1];
                    }
                }
            }
            if (min_val == max_val || max_val == 0)
            {
                int road;
                int count = 0;
                do
                {
                    road = roads[Random.Range(0, roads.Count)];
                    if (road == 1 && q_table[x + 1, y] >= 0)
                    {
                        q_table[x + 1, y] += l_r * (reward_table[x + 1, y]
                            + gamma * (Max(x + 1, y, 1, 0) + Min(x + 1, y, 1, 0)
                            - q_table[x + 1, y]));
                        new_x = 1;
                        new_y = 0;
                        break;
                    }
                    else if (road == 2 && q_table[x - 1, y] >= 0)
                    {
                        q_table[x - 1, y] += l_r * (reward_table[x - 1, y]
                            + gamma * (Max(x - 1, y, -1, 0) + Min(x - 1, y, -1, 0)
                            - q_table[x - 1, y]));
                        new_x = -1;
                        new_y = 0;
                        break;
                    }
                    else if (road == 3 && q_table[x, y + 1] >= 0)
                    {
                        q_table[x, y + 1] += l_r * (reward_table[x, y + 1]
                            + gamma * (Max(x, y + 1, 0, 1) + Min(x, y + 1, 0, 1)
                            - q_table[x, y + 1]));
                        new_x = 0;
                        new_y = 1;
                        break;
                    }
                    else if (road == 4 && q_table[x, y - 1] >= 0)
                    {
                        q_table[x, y - 1] += l_r * (reward_table[x, y - 1]
                            + gamma * (Max(x, y - 1, 0, -1) + Min(x, y - 1, 0, -1)
                            - q_table[x, y - 1]));
                        new_x = 0;
                        new_y = -1;
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
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 35; j++)
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
        float max_q = 0;
        if (new_x != 1 && q_table[x - 1, y] < max_q)
        {
            max_q = q_table[x - 1, y];
        }
        if (new_x != -1 && q_table[x + 1, y] < max_q)
        {
            max_q = q_table[x + 1, y];
        }
        if (new_y != 1 && q_table[x, y - 1] < max_q)
        {
            max_q = q_table[x, y - 1];
        }
        if (new_y != -1 && q_table[x, y + 1] < max_q)
        {
            max_q = q_table[x, y + 1];
        }
        return max_q;
    }
}
