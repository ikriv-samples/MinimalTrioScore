import sys

class Graph:
  def __init__(self, from_arr, to_arr):
    if len(from_arr) != len(to_arr):
      raise ValueError("From and to arrays must have the same length")
    self.graph = {}
    for i in range(len(from_arr)):
      self._add_edge(from_arr[i], to_arr[i])
      self._add_edge(to_arr[i], from_arr[i])
    
  def _add_edge(self, f, t):
    if not f in self.graph:
      self.graph[f] = {t}
    else:
      self.graph[f].add(t)
      
  def _get_adjacent(self, node):
    if not node in self.graph:
      return {}
    else:
      return self.graph[node]
      
  def _get_score(self, i,j,k):
    return len(self._get_adjacent(i)) + len(self._get_adjacent(j)) + len(self._get_adjacent(k)) - 6
    
  def get_min_score(self):
    min_score = None
    for node in self.graph:
      adjacent = self._get_adjacent(node)
      for i in adjacent:
        if node >= i:
          continue
        for j in adjacent:
          if i>=j:
            continue
          if j in self._get_adjacent(i):
            score = self._get_score(node, i, j)
            if min_score is None or score < min_score:
              min_score = score
    return min_score
      
def main():
  if len(sys.argv) != 3:
    print("Expecting two arguments: from array and to array")
    return

  from_arr = [int(item) for item in sys.argv[1].split(",")]
  to_arr = [int(item) for item in sys.argv[2].split(",")]

  score = Graph(from_arr, to_arr).get_min_score()
  if score is None:
    score = -1
    
  print(score)
    
main()