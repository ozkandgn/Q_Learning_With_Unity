# Q_Learning_With_Unity
Q-Learning With Unity

---------------------------------------

The goal is to ensure that the designed agent is out from the labyrinth.

Agent is uses reinforcement learning(Q-Learning) techniques.

![](https://github.com/ozkandgn/Q_Learning_With_Unity/blob/master/images/unty_labyrinth.PNG)

White Cube = Agent
Green Cube = Destination
Gray Cubes = Borders(for little map)

-------------------------------------

Used different function from normal q-learning function because we needed negative and positive values.

#### normal q-function:
<img src="https://github.com/ozkandgn/Q_Learning_With_Unity/blob/master/images/q_function.png" width="450" height="80">

#### design q-function: 
<img src="https://github.com/ozkandgn/Q_Learning_With_Unity/blob/master/images/designed_q_function.PNG" width="520" height="50">

---------------------------------------

The _**reward table**_ and q-table starts at zero and is filled according to certain conditions.
corner is -100 and goal is 100 in the reward table.

<img src="https://github.com/ozkandgn/Q_Learning_With_Unity/blob/master/images/reward_table.PNG" width="250" height="250">

<img src="https://github.com/ozkandgn/Q_Learning_With_Unity/blob/master/images/q_table.PNG" width="450" height="250">

------------------------------------------------

[You can look detail for q-learning info](https://medium.com/@ozkandgn/q-learninge-giriş-4c4758120d6)
