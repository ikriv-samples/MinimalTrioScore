﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumTrioScore
{
    class Graph
    {
        private Dictionary<int, HashSet<int>> _graph;

        public Graph(int[] from, int[] to)
        {
            if (from.Length != to.Length)
            {
                throw new ApplicationException("From and to array don't have the same length");
            }

            _graph = new Dictionary<int, HashSet<int>>();

            for (int i=0; i<from.Length; ++i)
            {
                AddEdge(from[i], to[i]);
                AddEdge(to[i], from[i]);
            }
        }

        private void AddEdge(int from, int to)
        {
            var adjacent = GetAdjacent(from);
            if (adjacent.Count == 0) _graph[from] = adjacent;
            adjacent.Add(to);
        }

        private HashSet<int> GetAdjacent(int node)
        {
            HashSet<int> list;
            if (!_graph.TryGetValue(node, out list)) return new HashSet<int>();
            return list;
        }

        private int GetScore(int i, int j, int k)
        {
            return GetAdjacent(i).Count + GetAdjacent(j).Count + GetAdjacent(k).Count - 6;
        }

        public int? GetMinScore()
        {
            int? minScore = null;

            foreach (var pair in _graph)
            {
                var node = pair.Key;
                var adjacent = pair.Value;
                if (adjacent.Count < 2) continue;
                foreach (var i in adjacent)
                {
                    if (node >= i) continue;
                    foreach (var j in adjacent)
                    {
                        if (i >= j) continue;
                        if (GetAdjacent(i).Contains(j))
                        {
                            var score = GetScore(node, i, j);
                            if (minScore == null || score < minScore)
                            {
                                minScore = score;
                            }
                        }
                    }
                }
            }

            return minScore;
        }
    }

    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.Error.WriteLine("Expecting two arguments: from array and to array");
                    return 1;
                }

                var from = args[0].Split(',').Select(Int32.Parse).ToArray();
                var to = args[1].Split(',').Select(Int32.Parse).ToArray();
                var graph = new Graph(from, to);
                int score = graph.GetMinScore() ?? -1;
                Console.WriteLine(score);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 2;
            }
        }
    }
}
