using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeetCode.Problems
{
    public class _587_Erect_the_Fence
    {
        public class Point
        {
            public int x; public int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return $"{x},{y}";
            }
        }

        public enum LineType { TL, TR, BL, BR, }
        public class Line
        {
            public Point P1;
            public Point P2;
            public LineType Type;
            public readonly float k;
            public readonly float b;
            public Line(int x1, int y1, int x2, int y2, LineType type)
                : this(new Point(x1, y1), new Point(x2, y2), type)
            {
            }

            public Line(Point p1, Point p2, LineType type)
            {
                P1 = p1;
                P2 = p2;
                Type = type;
                k = (float)(p2.y - p1.y) / (p2.x - p1.x);
                b = p1.y - k * p1.x;
            }

            public float CalcK(int x, int y)
            {
                return (float)(y - P1.y) / (x - P1.x);
            }

            public bool IsMoreAngled(int x, int y)
            {
                float check = (float)(y - P1.y) / (x - P1.x);
                switch (Type)
                {
                    case LineType.TL:
                        return check > k;
                    case LineType.TR:
                        return check > k;
                    case LineType.BL:
                        return check < k;
                    case LineType.BR:
                        return check < k;
                }
                return false;
            }

            public bool IsSameAngle(int x, int y)
            {
                float check = (float)(y - P1.y) / (x - P1.x);
                return Math.Abs(check - k) < .0001;
            }

            public bool IsPointInside(int x, int y)
            {
                float v = k * x + b;
                switch (Type)
                {
                    case LineType.TL:
                        return y < v;
                    case LineType.TR:
                        return y < v;
                    case LineType.BL:
                        return y > v;
                    case LineType.BR:
                        return y > v;
                }
                return false;
            }
        }
        public class Solution
        {
            SortedSet<int>[] xPoints = new SortedSet<int>[101];
            SortedSet<int>[] yPoints = new SortedSet<int>[101];
            int minX = int.MaxValue, minY = int.MaxValue,
                maxX = int.MinValue, maxY = int.MinValue;
            Dictionary<int, HashSet<int>> answer = new Dictionary<int, HashSet<int>>();
            List<Line> Lines = new List<Line>();
            List<Line> TLLines = new List<Line>();
            List<Line> TRLines = new List<Line>();
            List<Line> BLLines = new List<Line>();
            List<Line> BRLines = new List<Line>();

            public int[][] OuterTrees(int[][] trees)
            {
                if (trees.Length < 4)
                    return trees;
                SavePoints(trees);
                AddLine(minX, xPoints[minX].Max, yPoints[maxY].Min, maxY, LineType.TL);
                AddLine(yPoints[maxY].Max, maxY, maxX, xPoints[maxX].Max, LineType.TR);
                AddLine(minX, xPoints[minX].Min, yPoints[minY].Min, minY, LineType.BL);
                AddLine(yPoints[minY].Max, minY, maxX, xPoints[maxX].Min, LineType.BR);

                answer = new Dictionary<int, HashSet<int>>();
                AddPointsToAnswer(minX, xPoints[minX], minY, yPoints[minY]);
                AddPointsToAnswer(maxX, xPoints[maxX], maxY, yPoints[maxY]);

                foreach (var tree in trees.OrderBy(i => i[0]))
                {
                    int x = tree[0], y = tree[1];
                    if (IsPointInside(x, y))
                        continue;
                    var lineToChange = GetLineToChange(x, y);
                    if (lineToChange.P1.x == x && lineToChange.P1.y == y)
                        continue;
                    var lines = GetArrayToChange(lineToChange);
                    if (!lines.Any())
                        lines.Add(new Line(lineToChange.P1, new Point(x, y), lineToChange.Type));
                    else
                    {
                        bool IsAdded = false;
                        for (int i = 0; i < lines.Count; i++)
                        {
                            var line = lines[i];
                            if (line.IsMoreAngled(x, y))
                            {
                                lines[i] = new Line(line.P1, new Point(x, y), line.Type);
                                if (i + 1 < lines.Count)
                                    lines.RemoveRange(i + 1, lines.Count - i - 1);
                                IsAdded = true;
                                break;
                            }
                        }
                        if (!IsAdded)
                        {
                            var line = new Line(lines.Last().P2, lineToChange.P2, lineToChange.Type);
                            if (line.IsMoreAngled(x, y) || line.IsSameAngle(x,y))
                                lines.Add(new Line(lines.Last().P2, new Point(x, y), lineToChange.Type));
                        }
                    }
                }

                AddPointsToAnswer(TLLines);
                AddPointsToAnswer(TRLines);
                AddPointsToAnswer(BLLines);
                AddPointsToAnswer(BRLines);
                return ConvertAnswer();
            }

            private void AddLine(int minX1, int maxY1, int minX2, int maxY2, LineType type)
            {
                var l = new Line(minX1, maxY1, minX2, maxY2, type);
                if (!float.IsNaN(l.k))
                    Lines.Add(l);
            }

            private void AddPointsToAnswer(List<Line> lines)
            {
                foreach (var line in lines)
                {
                    AddPointToAnswer(line.P1.x, line.P1.y);
                    AddPointToAnswer(line.P2.x, line.P2.y);
                }
            }

            private List<Line> GetArrayToChange(Line line)
            {
                switch (line.Type)
                {
                    case LineType.TL:
                        return TLLines;
                    case LineType.TR:
                        return TRLines;
                    case LineType.BL:
                        return BLLines;
                    case LineType.BR:
                        return BRLines;
                }
                return null;
            }

            private Line GetLineToChange(int x, int y) => Lines.Where(l => !l.IsPointInside(x, y)).First();
            private bool IsPointInside(int x, int y) => Lines.All(l => l.IsPointInside(x, y));

            private int[][] ConvertAnswer()
            {
                var result = new List<int[]>();

                foreach (var item in answer)
                    foreach (var y in item.Value)
                        result.Add(new int[] { item.Key, y });

                return result.ToArray();
            }

            private void AddPointsToAnswer(int x, IEnumerable<int> ys, int y, IEnumerable<int> xs)
            {
                foreach (var item in ys)
                    AddPointToAnswer(x, item);
                foreach (var item in xs)
                    AddPointToAnswer(item, y);
            }

            private void AddPointToAnswer(int x, int y)
            {
                if (!answer.ContainsKey(x))
                    answer[x] = new HashSet<int>();
                answer[x].Add(y);
            }

            private void SavePoints(int[][] trees)
            {
                foreach (var tree in trees)
                {
                    int x = tree[0];
                    int y = tree[1];
                    if (xPoints[x] == null)
                        xPoints[x] = new SortedSet<int> { y };
                    else
                        xPoints[x].Add(y);
                    if (yPoints[y] == null)
                        yPoints[y] = new SortedSet<int> { x };
                    else
                        yPoints[y].Add(x);

                    minX = x < minX ? x : minX;
                    minY = y < minY ? y : minY;
                    maxX = x > maxX ? x : maxX;
                    maxY = y > maxY ? y : maxY;
                }
            }
        }

        private readonly Solution s;
        public _587_Erect_the_Fence()
        {
            s = new Solution();
        }

        [Fact]
        public void example1()
        {
            var l = new List<int[]>
            {
                CreatePoint(1, 1),
                CreatePoint(2, 2),
                CreatePoint(2, 0),
                CreatePoint(2, 4),
                CreatePoint(3, 3),
                CreatePoint(4, 2)
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(5, res.Length);
            Assert.Contains(res, EqualToPoint(1, 1));
            Assert.Contains(res, EqualToPoint(2, 0));
            Assert.Contains(res, EqualToPoint(3, 3));
            Assert.Contains(res, EqualToPoint(2, 4));
            Assert.Contains(res, EqualToPoint(4, 2));
        }

        private int[] CreatePoint(int x, int y) => new[] { x, y };

        [Fact]
        public void example2()
        {
            var l = new List<int[]>
            {
                CreatePoint(1, 1),
                CreatePoint(2, 2),
                CreatePoint(4, 2),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(3, res.Length);
            Assert.Contains(res, EqualToPoint(4, 2));
            Assert.Contains(res, EqualToPoint(2, 2));
            Assert.Contains(res, EqualToPoint(1, 1));
        }

        [Fact]
        public void fail1()
        {
            var l = new List<int[]>
            {
                CreatePoint(3, 7),
                CreatePoint(6, 8),
                CreatePoint(7, 8),
                CreatePoint(11, 10),
                CreatePoint(4, 3),
                CreatePoint(8, 5),
                CreatePoint(7, 13),
                CreatePoint(4, 13),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(6, res.Length);
            Assert.Contains(res, EqualToPoint(3, 7));
            Assert.Contains(res, EqualToPoint(4, 13));
            Assert.Contains(res, EqualToPoint(7, 13));
            Assert.Contains(res, EqualToPoint(11, 10));
            Assert.Contains(res, EqualToPoint(8, 5));
            Assert.Contains(res, EqualToPoint(4, 3));
        }

        [Fact]
        public void fail2()
        {
            // [[52,56],[42,61],[65,47],[90,69],[67,45],[49,1],[92,22],[54,56],[67,85],[97,23],[10,98],[92,51],[67,20],[10,59],[5,92],[56,71],[3,43],[13,5],[34,81],[32,27],[20,91],[50,52],[93,28],[34,0],[7,37],[23,40],[56,79],[43,17],[32,30],[4,90],[92,53],[85,24],[50,48],[81,81],[96,96],[92,60],[9,88],[48,23],[95,67],[46,29],[76,47],[36,33],[27,6],[79,33],[69,84],[36,66],[68,94],[48,14],[61,65],[11,88],[90,8],[30,3],[11,60],[18,23],[79,15],[39,61],[90,19],[33,3],[0,73],[46,23],[88,32],[75,64],[57,38],[26,9],[21,46],[12,79],[61,23],[6,31],[38,50],[41,10],[71,30],[41,58],[71,9],[32,65],[57,44],[33,94],[37,54],[60,47],[6,94],[84,23],[88,51],[37,55],[91,48],[25,69],[85,34],[3,15],[20,17],[4,17],[96,83],[48,53],[58,36],[62,95],[14,51],[54,30],[94,40],[8,3],[19,91],[33,75]]
            var l = new List<int[]>
            {
                CreatePoint(52, 56),
                CreatePoint(42, 61),
                CreatePoint(65, 47),
                CreatePoint(90, 69),
                CreatePoint(67, 45),
                CreatePoint(49, 1),
                CreatePoint(92, 22),
                CreatePoint(54, 56),
                CreatePoint(67, 85),
                CreatePoint(97, 23),
                CreatePoint(10, 98),
                CreatePoint(92, 51),
                CreatePoint(67, 20),
                CreatePoint(10, 59),
                CreatePoint(5, 92),
                CreatePoint(56, 71),
                CreatePoint(3, 43),
                CreatePoint(13, 5),
                CreatePoint(34, 81),
                CreatePoint(32, 27),
                CreatePoint(20, 91),
                CreatePoint(50, 52),
                CreatePoint(93, 28),
                CreatePoint(34, 0),
                CreatePoint(7, 37),
                CreatePoint(23, 40),
                CreatePoint(56, 79),
                CreatePoint(43, 17),
                CreatePoint(32, 30),
                CreatePoint(4, 90),
                CreatePoint(92, 53),
                CreatePoint(85, 24),
                CreatePoint(50, 48),
                CreatePoint(81, 81),
                CreatePoint(96, 96),
                CreatePoint(92, 60),
                CreatePoint(9, 88),
                CreatePoint(48, 23),
                CreatePoint(95, 67),
                CreatePoint(46, 29),
                CreatePoint(76, 47),
                CreatePoint(36, 33),
                CreatePoint(27, 6),
                CreatePoint(79, 33),
                CreatePoint(69, 84),
                CreatePoint(36, 66),
                CreatePoint(68, 94),
                CreatePoint(48, 14),
                CreatePoint(61, 65),
                CreatePoint(11, 88),
                CreatePoint(90, 8),
                CreatePoint(30, 3),
                CreatePoint(11, 60),
                CreatePoint(18, 23),
                CreatePoint(79, 15),
                CreatePoint(39, 61),
                CreatePoint(90, 19),
                CreatePoint(33, 3),
                CreatePoint(0, 73),
                CreatePoint(46, 23),
                CreatePoint(88, 32),
                CreatePoint(75, 64),
                CreatePoint(57, 38),
                CreatePoint(26, 9),
                CreatePoint(21, 46),
                CreatePoint(12, 79),
                CreatePoint(61, 23),
                CreatePoint(6, 31),
                CreatePoint(38, 50),
                CreatePoint(41, 10),
                CreatePoint(71, 30),
                CreatePoint(41, 58),
                CreatePoint(71, 9),
                CreatePoint(32, 65),
                CreatePoint(57, 44),
                CreatePoint(33, 94),
                CreatePoint(37, 54),
                CreatePoint(60, 47),
                CreatePoint(6, 94),
                CreatePoint(84, 23),
                CreatePoint(88, 51),
                CreatePoint(37, 55),
                CreatePoint(91, 48),
                CreatePoint(25, 69),
                CreatePoint(85, 34),
                CreatePoint(3, 15),
                CreatePoint(20, 17),
                CreatePoint(4, 17),
                CreatePoint(96, 83),
                CreatePoint(48, 53),
                CreatePoint(58, 36),
                CreatePoint(62, 95),
                CreatePoint(14, 51),
                CreatePoint(54, 30),
                CreatePoint(94, 40),
                CreatePoint(8, 3),
                CreatePoint(19, 91),
                CreatePoint(33, 75)
            };

            // [[90,8],[4,90],[10,98],[96,96],[49,1],[0,73],[3,15],[97,23],[5,92],[8,3],[34,0],[6,94]]
            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(12, res.Length);
            Assert.Contains(res, EqualToPoint(97, 23));
            Assert.Contains(res, EqualToPoint(4, 90));
            Assert.Contains(res, EqualToPoint(8, 3));
            Assert.Contains(res, EqualToPoint(90, 8));
            Assert.Contains(res, EqualToPoint(5, 92));
            Assert.Contains(res, EqualToPoint(6, 94));
            Assert.Contains(res, EqualToPoint(34, 0));
            Assert.Contains(res, EqualToPoint(3, 15));
            Assert.Contains(res, EqualToPoint(0, 73));
            Assert.Contains(res, EqualToPoint(49, 1));
            Assert.Contains(res, EqualToPoint(10, 98));
            Assert.Contains(res, EqualToPoint(96, 96));
        }

        [Fact]
        public void fail3()
        {
            //in: [[3,0],[4,0],[5,0],[6,1],[7,2],[7,3],[7,4],[6,5],[5,5],[4,5],[3,5],[2,5],[1,4],[1,3],[1,2],[2,1],[4,2],[0,3]]
            var l = new List<int[]>
            {
                CreatePoint(3, 0),
                CreatePoint(4, 0),
                CreatePoint(5, 0),
                CreatePoint(6, 1),
                CreatePoint(7, 2),
                CreatePoint(7, 3),
                CreatePoint(7, 4),
                CreatePoint(6, 5),
                CreatePoint(5, 5),
                CreatePoint(4, 5),
                CreatePoint(3, 5),
                CreatePoint(2, 5),
                CreatePoint(1, 4),
                CreatePoint(1, 3),
                CreatePoint(1, 2),
                CreatePoint(2, 1),
                CreatePoint(4, 2),
                CreatePoint(0, 3)
            };
            //ex: [[7,4],[5,0],[7,3],[2,1],[5,5],[4,5],[3,5],[7,2],[1,2],[1,4],[4,0],[2,5],[6,1],[6,5],[0,3],[3,0]]
            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(16, res.Length);
            Assert.Contains(res, EqualToPoint(7, 4));
            Assert.Contains(res, EqualToPoint(5, 0));
            Assert.Contains(res, EqualToPoint(7, 3));
            Assert.Contains(res, EqualToPoint(2, 1));
            Assert.Contains(res, EqualToPoint(5, 5));
            Assert.Contains(res, EqualToPoint(4, 5));
            Assert.Contains(res, EqualToPoint(3, 5));
            Assert.Contains(res, EqualToPoint(7, 2));
            Assert.Contains(res, EqualToPoint(1, 2));
            Assert.Contains(res, EqualToPoint(1, 4));
            Assert.Contains(res, EqualToPoint(4, 0));
            Assert.Contains(res, EqualToPoint(2, 5));
            Assert.Contains(res, EqualToPoint(6, 1));
            Assert.Contains(res, EqualToPoint(6, 5));
            Assert.Contains(res, EqualToPoint(0, 3));
            Assert.Contains(res, EqualToPoint(3, 0));

            //my: [[0,3],[3,0],[3,5],[4,0],[4,5],[5,0],[5,5],[7,2],[7,3],[7,4],[2,5],[6,5],[6,1],[1,4],[1,2]]
        }

        [Fact]
        public void testRhomb()
        {
            var l = new List<int[]>
            {
                CreatePoint(3, 4),CreatePoint(8, 4),
                CreatePoint(0, 0),CreatePoint(5, 0),
                CreatePoint(6, 1),
                CreatePoint(2, 3),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(6, res.Length);
            Assert.Contains(res, EqualToPoint(2, 3));
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(3, 4));
            Assert.Contains(res, EqualToPoint(8, 4));
            Assert.Contains(res, EqualToPoint(5, 0));
            Assert.Contains(res, EqualToPoint(6, 1));
        }

        [Fact]
        public void testRhomb2()
        {
            var l = new List<int[]>
            {
                CreatePoint(4, 3),CreatePoint(4, 8),
                CreatePoint(0, 0),CreatePoint(0, 5),
                CreatePoint(1, 6),
                CreatePoint(2, 7),
                CreatePoint(1, 7),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(5, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(4, 3));
            Assert.Contains(res, EqualToPoint(4, 8));
            Assert.Contains(res, EqualToPoint(0, 5));
            Assert.Contains(res, EqualToPoint(1, 7));
        }


        [Fact]
        public void test1()
        {
            var l = new List<int[]>
            {
                CreatePoint(1, 1),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(1, res.Length);
            Assert.Contains(res, EqualToPoint(1, 1));
        }

        [Fact]
        public void test2()
        {
            var l = new List<int[]>
            {
                CreatePoint(1, 1),
                CreatePoint(2, 2),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(2, res.Length);
            Assert.Contains(res, EqualToPoint(1, 1));
            Assert.Contains(res, EqualToPoint(2, 2));
        }

        [Fact]
        public void testsquare()
        {
            var l = new List<int[]>
            {
                CreatePoint(0, 0),
                CreatePoint(0, 1),
                CreatePoint(1, 0),
                CreatePoint(1, 1),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(4, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(0, 1));
            Assert.Contains(res, EqualToPoint(1, 0));
            Assert.Contains(res, EqualToPoint(1, 1));
        }

        [Fact]
        public void testrect()
        {
            var l = new List<int[]>
            {
                CreatePoint(0, 0),
                CreatePoint(0, 1),
                CreatePoint(1, 0),
                CreatePoint(2, 0),
                CreatePoint(2, 1),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(5, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(0, 1));
            Assert.Contains(res, EqualToPoint(1, 0));
            Assert.Contains(res, EqualToPoint(2, 0));
            Assert.Contains(res, EqualToPoint(2, 1));
        }

        [Fact]
        public void testsquareAndMiddlePoint()
        {
            var l = new List<int[]>
            {
                CreatePoint(0, 0),
                CreatePoint(0, 2),
                CreatePoint(2, 2),
                CreatePoint(2, 0),
                CreatePoint(1, 1),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(4, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(0, 2));
            Assert.Contains(res, EqualToPoint(2, 2));
            Assert.Contains(res, EqualToPoint(2, 0));
        }

        [Fact]
        public void testverticalLine()
        {
            var l = new List<int[]>
            {
                CreatePoint(0, 0),
                CreatePoint(0, 4),
                CreatePoint(0, 77),
                CreatePoint(0, 22),
                CreatePoint(0, 100),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(5, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(0, 4));
            Assert.Contains(res, EqualToPoint(0, 77));
            Assert.Contains(res, EqualToPoint(0, 22));
            Assert.Contains(res, EqualToPoint(0, 100));
        }

        [Fact]
        public void testhorizontalLine()
        {
            var l = new List<int[]>
            {
                CreatePoint(0, 0),
                CreatePoint(4, 0),
                CreatePoint(77, 0),
                CreatePoint(22, 0),
                CreatePoint(100, 0),
            };

            var res = s.OuterTrees(l.ToArray());
            Assert.Equal(5, res.Length);
            Assert.Contains(res, EqualToPoint(0, 0));
            Assert.Contains(res, EqualToPoint(4, 0));
            Assert.Contains(res, EqualToPoint(77, 0));
            Assert.Contains(res, EqualToPoint(22, 0));
            Assert.Contains(res, EqualToPoint(100, 0));
        }


        [Theory]
        [InlineData(1, 1)]
        public void onLineTest(int x, int y)
        {
            var lines = new List<Line>
            {
                new Line(0, 0, 2, 2, LineType.TL),
                new Line(0, 2, 2, 0, LineType.TR),
                new Line(0, 2, 2, 0, LineType.BL),
                new Line(0, 0, 2, 2, LineType.BR),
            };

            Assert.All(lines, l => Assert.False(l.IsPointInside(x, y)));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(2, 1)]
        [InlineData(2, -1)]
        public void PointsOutsideTests(int x, int y)
        {
            var lines = new List<Line>
            {
                new Line(0, 0, 2, 2, LineType.TL),
                new Line(2, 2, 4, 0, LineType.TR),
                new Line(0, 0, 2, -2, LineType.BL),
                new Line(2, -2, 4, 0, LineType.BR),
            };

            Assert.All(lines, l => Assert.True(l.IsPointInside(x, y)));
        }

        private static Predicate<int[]> EqualToPoint(int x, int y)
        {
            return i => i[0] == x && i[1] == y;
        }
    }
}
