# Hurricane-Evacuation-Planner
## IAI191 assignment 5
### Goal:
Sequential decision making under uncertainty using belief-state MDP for decision-making: the Hurrucane Evacuation problem. (This is an obscure variant of the Canadian traveler problem.) 

### Domain:
The domain description is similar to that described in assignment 1 (Hurricane-Evacuation), except that again we do not know the locations of the blocakges. For simplicity, however, we will assume that the blockages occur independently, with a known given probability. They are revealed with certainty when the agent reaches a neigbouring vertex.

We will also assumes that the number of evacuees at each vertex is known, and is always less than 5.
Thus, in the current problem to be solved, we are given a weighted undirected graph, where each edge has a known probability of being blocked. These distributions are jointly independent.
- The agent's only actions are traveling between vertices.
- Traversal times are the weight of the edge.
- or simplicity, we will assume only one agent, starting at s, and only one shelter at vertex t, The problem is to find a policy that saves (in expectation) as many people as possible before the deadline.

The graph can be provided in a manner similar to previous assignments, for example:
```
#V 5    ; number of vertices n in graph (from 1 to n)

#P 3 2  ; Vertex 3, has 2 evacuees
#P 4 1  ; Vertex 4, has 1 evacuee

#E 1 1 2 3   ; Edge from vertex 1 to vertex 2, weight 3
#E 2 2 3 2   ; Edge from vertex 2 to vertex 3, weight 2
#E 3 3 4 3 B 0.3  ; Edge from vertex 3 to vertex 4, weight 3, probability of blockage 0.3
#E 4 4 5 1   ; Edge from vertex 4 to vertex 5, weight 1
#E 5 2 4 4   ; Edge from vertex 2 to vertex 4, weight 4

#Deadline 10
#Start 1
#Shelter 5
```
In the above graph the start vertex is 1, and the goal (shelter) vertex is 5. 
