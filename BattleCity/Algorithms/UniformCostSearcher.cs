using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BattleCity.Algorithms
{
    public class UniformCostSearcher : Searcher
    {
        public IEnumerable<Point> GetPath(Point start, Point finish)
        {
            Dictionary<Tuple<int, int>, int> cost = new Dictionary<Tuple<int, int>, int>();
            List<Tuple<int, Point>> queue = new List<Tuple<int, Point>>();
            
            Track = new Dictionary<Point, Point>();
            Visited = new List<Point>();

            queue.Add(new Tuple<int, Point>(0, start));

            while (queue.Any())
            {
                Point currentPoint = queue.Last().Item2;

                if (currentPoint.Equals(finish))
                {
                    return RouteRestore(finish);
                }

                exploredList.Add(currentPoint);
                if (maxNumberOfFrontierList < frontierList.Count()) maxNumberOfFrontierList = frontierList.Count();
                frontierList.Dequeue();

                for (int i = 0; i < currentPoint.children.Count; i++)
                {
                    currentPoint.children[i].costFromRoot = getCostFromRoot(currentPoint.children[i]);
                    if (!Contains(frontierList, currentPoint.children[i]) && !Contains(exploredList, currentPoint.children[i]))
                    {
                        frontierList.Enqueue(currentPoint.children[i].costFromRoot, currentPoint.children[i]);
                    }
                    else if (Contains(frontierList, currentPoint.children[i]))
                    {
                        CheckWhichHasHighCost(ref frontierList, currentPoint.children[i], "Uniform");
                    }
                }
            }
        }
        public void CheckWhichHasHighCost(ref PriorityQueue<int, Point> FrontierList, Point n, string nameOfCost)
        {
            int value = 0;

            if (nameOfCost.Equals("Uniform")) value = n.costFromRoot;
            else if (nameOfCost.Equals("Heuristic")) value = n.costHeuristic;

            PriorityQueue<int, Point> templateList = new PriorityQueue<int, Point>();
            List<Point> PointList = FrontierList.getHeapVariables();
            List<int> costList = FrontierList.getHeapCosts();
            int index = 0;
            bool willChange = false;

            for (int i = 0; i < PointList.Count(); i++)
            {
                if (PointList[i].isSamePuzzle(n.puzzle))
                {
                    if (value < costList[i])
                    {
                        willChange = true;
                        index = i;
                        break;
                    }
                }
            }

            if (willChange)
            {
                for (int i = 0; i < PointList.Count(); i++)
                {
                    if (i != index) templateList.Enqueue(costList[i], PointList[i]);
                }
                templateList.Enqueue(value, PointList[PointList.Count - 1]);

                FrontierList = templateList;
            }

        }

        public static bool Contains(PriorityQueue<int, Point> list, Point c)
        {
            bool contains = false;

            List<Point> PointList = list.getHeapVariables();

            for (int i = 0; i < list.Count(); i++)
            {
                if (PointList[i].isSamePuzzle(c.puzzle))
                    contains = true;
            }
            return contains;
        }

        public static int GetCostFromRoot(Point n)
        {
            Point current = n;
            List<Point> listUC = new List<Point>();
            listUC.Add(current);

            while (current.parent != null)
            {
                current = current.parent;
                listUC.Add(current);
            }
            return listUC.Count - 1;
        }
    }
}