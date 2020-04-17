using System;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Klasa Punkt ze specyfikacji
    /// </summary>
    public class Point
    {
        public int x;
        public int y;

        #region >>> Constructors <<<
        public Point(Point original)
        {
            x = original.x;
            y = original.y;
        }
        public Point()
        {
            x = 0;
            y = 0;
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region >>> Methods <<<
        /// <summary>
        /// Metoda przesun() ze specyfikacji
        /// Przesuwa element o koordynaty dx i dy
        /// </summary>
        /// <param name="dx"> x coordinate</param>
        /// <param name="dy"> y coordinate</param>
        public void Move(int dx, int dy)
        {
            x += dx;
            y += dy;
        }
        public override string ToString()
        {
            return $"[{x},{y}]";
        }
        #endregion
    }

    /// <summary>
    /// Klasa Linia ze specyfikacji
    /// </summary>
    public class Line
    {
        public Point[] points = new Point[2];

        #region >>> Constructors <<<
        public Line(Line original)
        {
            points = original.points;
        }
        public Line()
        {            
            points[0] = new Point();
            points[1] = new Point(0, 1);
        }
        public Line(Point point0, Point point1)
        {
            // W C# nie ma obiektów automatycznych, więc konieczne jest wzywanie konstruktora kopiującego.
            // W C# używa się do takich rzeczy struktur, które są automatyczne, przez co kopiowanie zachodzi automatycznie.
            points[0] = new Point(point0);
            points[1] = new Point(point1);
        }
        #endregion

        #region >>> Methods <<<
        /// <summary>
        /// Metoda przesun() ze specyfikacji
        /// Przesuwa element o koordynaty dx i dy
        /// </summary>
        /// <param name="dx"> x coordinate</param>
        /// <param name="dy"> y coordinate</param>
        public void Move(int dx, int dy)
        {
            foreach (Point point in points)
                point.Move(dx, dy);
        }
        public override string ToString()
        {
            return $"<{points[0]},{points[1]}>";
        }
        #endregion
    }

    /// <summary>
    /// Klasa Trojkąt ze specyfikacji
    /// </summary>
    public class Triangle
    {
        public Line[] lines = new Line[3];

        #region >>> Constructors <<<
        public Triangle(Triangle original)
        {
            lines = original.lines;
        }
        /// <summary>
        /// Creates triangle (0,0),(1,0),(1,1)
        /// </summary>
        public Triangle()
        {
            lines[0] = new Line();
            lines[1] = new Line(new Point(1, 0), new Point(1, 1));
            lines[2] = new Line(new Point(1, 1), new Point());
        }
        public Triangle(Point point0, Point point1, Point point2)
        {
            lines[0] = new Line(point0, point1);
            lines[1] = new Line(point1, point2);
            lines[2] = new Line(point2, point0);
        }
        #endregion

        #region >>> Methods <<<
        /// <summary>
        /// Metoda przesun() ze specyfikacji
        /// Przesuwa element o koordynaty dx i dy
        /// </summary>
        /// <param name="dx"> x coordinate</param>
        /// <param name="dy"> y coordinate</param>
        public void Move(int dx, int dy)
        {
            foreach (Line line in lines)
                line.Move(dx, dy);
        }
        public override string ToString()
        {
            return $"({lines[0]} , {lines[1]} , {lines[2]})";
        }
        #endregion
    }

    /// <summary>
    /// Klasa Czworokąt ze specyfikacji
    /// </summary>
    public class Quad
    {
        public Line[] lines = new Line[4];

        #region >>> Constructors <<<
        public Quad(Quad original)
        {
            lines = original.lines;
        }
        /// <summary>
        /// Creates square (0,0),(1,0),(1,1),(0,1)
        /// </summary>
        public Quad()
        {
            lines[0] = new Line();
            lines[1] = new Line(new Point(1, 0), new Point(1, 1));
            lines[2] = new Line(new Point(1, 1), new Point(0, 1));
            lines[3] = new Line(new Point(0, 1), new Point());
        }
        public Quad(Point point0, Point point1, Point point2, Point point3)
        {
            lines[0] = new Line(point0, point1);
            lines[1] = new Line(point1, point2);
            lines[2] = new Line(point2, point3);
            lines[3] = new Line(point3, point0);
        }
        #endregion

        #region >>> Methods <<<
        /// <summary>
        /// Metoda przesun() ze specyfikacji
        /// Przesuwa element o koordynaty dx i dy
        /// </summary>
        /// <param name="dx"> x coordinate</param>
        /// <param name="dy"> y coordinate</param>
        public void Move(int dx, int dy)
        {
            foreach (Line line in lines)
                line.Move(dx, dy);
        }
        public override string ToString()
        {
            return $"({lines[0]} , {lines[1]} , {lines[2]} , {lines[3]})";
        }
        #endregion
    }

     /// <summary>
     /// Klasa Obraz ze specyfikacji
     /// </summary>
    public class Image
    {
        List<Triangle> triangles = new List<Triangle>();
        List<Quad> quads = new List<Quad>();

        public int TrisCount { get { return triangles.Count; } }
        public int QuadsCount { get { return quads.Count; } }

        #region >>> Methods <<<
        public void AddFigure(Triangle figure)
        {
            triangles.Add(figure);
        }
        public void AddFigure(Quad figure)
        {
            quads.Add(figure);
        }
        /// <summary>
        /// Trzecie przeciążenie które wyciąga koordynaty ze string'a i dodaje odpowiednią figurę
        /// </summary>
        /// <param name="message"></param>
        /// <param name="numberOfPoints"></param>
        public void AddFigure(string message, int numberOfPoints)
        {
            Point[] points = new Point[numberOfPoints];
            string[] splited = message.Split(' ');
            if(splited.Length < numberOfPoints)
            {
                Console.WriteLine("Error: Number of coordinates is to low!");
                return;
            }

            int x, y;
            for(int i = 0; i < numberOfPoints; i++)
            {
                x = Convert.ToInt32(splited[i * 2]);
                y = Convert.ToInt32(splited[i * 2 + 1]);
                points[i] = new Point(x, y);
            }

            if (numberOfPoints == 3)
                AddFigure(new Triangle(points[0], points[1], points[2]));
            else if (numberOfPoints == 4)
                AddFigure(new Quad(points[0], points[1], points[2], points[3]));
        }

        public void RemoveFigure(int index, ElementType elementType)
        {
            if(elementType == ElementType.Tris)
            {
                //if (triangles.Count == 0)
                //{
                //    Console.WriteLine("There is no triangles in the image!");
                //    Console.ReadKey(true);
                //    return;
                //}
                if (index >= triangles.Count)
                {
                    Console.WriteLine("Index is out of range {0}!");
                    return;
                }
                triangles.RemoveAt(index);
            }
            else
            {
                //if (triangles.Count == 0)
                //{
                //    Console.WriteLine("There is no quads in the image!");
                //    Console.ReadKey(true);
                //    return;
                //}
                if (index >= quads.Count)
                {
                    Console.WriteLine("Index is out of range {0}!");
                    return;
                }
                quads.RemoveAt(index);
            }
        }

        /// <summary>
        /// Metoda przesun() ze specyfikacji
        /// Przesuwa element o koordynaty dx i dy
        /// </summary>
        /// <param name="dx"> x coordinate</param>
        /// <param name="dy"> y coordinate</param>
        public void Move(int dx, int dy)
        {
            foreach (Triangle triangle in triangles)
                triangle.Move(dx, dy);
            foreach (Quad quad in quads)
                quad.Move(dx, dy);
        }

        /// <summary>
        /// Przeciążenie które przesuwa określony element
        /// </summary>
        /// <param name="dx"> X coordinate</param>
        /// <param name="dy"> Y coordinate</param>
        /// <param name="index"> Index of element</param>
        /// <param name="elementType"> Type of element to move</param>
        public void Move(int dx, int dy, int index, ElementType elementType)
        {
            if (elementType == ElementType.Tris)
                triangles[index].Move(dx, dy);
            else if (elementType == ElementType.Quad)
                quads[index].Move(dx, dy);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("\tImage:\n");

            builder.AppendLine($"Triangles (count {triangles.Count}):");

            int i = 0;
            foreach (Triangle triangle in triangles)
                builder.AppendLine("  " + i++ + triangle);

            i = 0;
            builder.AppendLine($"Quads (count {quads.Count}):");
            foreach (Quad quad in quads)
                builder.AppendLine("  " + i++ + quad);

            return builder.ToString();
        }
        #endregion
    }
}
