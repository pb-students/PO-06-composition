
// Autor: Najlepszy Wyraz Twarzy

using System;

// Nie jest potrzebny "using Klasy", ponieważ elementy Point itd. są w tym samym namespace'ie)

namespace ConsoleApp1
{
    public enum ElementType { Tris, Quad, Image }

    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Image image = new Image();
            char key;
            Draw.Menu();
            while (true)
            {
                key = Console.ReadKey(true).KeyChar;
                Draw.Menu();
                switch (key)
                {
                    case '1': /// Display image
                        Draw.Image(image);
                        Draw.Menu();
                        break;
                    case '2': /// Add triangle
                        Console.WriteLine("Specify 3 points (write 6 coordinates x,y,x,y,... separated by space):");
                        image.AddFigure(Console.ReadLine(), 3);
                        Console.WriteLine("Successfully added new triangle...");
                        break;
                    case '3': /// Move triangle
                        MoveElement(image, ElementType.Tris);
                        Console.WriteLine("Successfully moved triangle...");
                        break;
                    case '4': /// Remove triangle
                        if (image.QuadsCount == 0)
                        {
                            Console.WriteLine("There is no quads in the image to remove!");
                            break;
                        }
                        Console.WriteLine("Specify index of quad to remove (there's currently {0} of them):", image.TrisCount);
                        image.RemoveFigure(Convert.ToInt32(Console.ReadLine()), ElementType.Tris);
                        Console.WriteLine("Successfully removed triangle...");
                        break;
                    case '5': /// Add quad
                        Console.WriteLine("Specify 4 points (write 8 coordinates x,y,x,y,... separated by space):");
                        image.AddFigure(Console.ReadLine(), 4);
                        Console.WriteLine("Successfully added new quad...");
                        break;
                    case '6': /// Move quad
                        MoveElement(image, ElementType.Quad);
                        Console.WriteLine("Successfully moved quad...");
                        break;
                    case '7': /// Remove quad
                        if(image.QuadsCount == 0)
                        {
                            Console.WriteLine("There is no quads in the image to remove!");
                            break;
                        }
                        Console.WriteLine("Specify index of quad to remove (there's currently {0} of them):", image.QuadsCount);
                        image.RemoveFigure(Convert.ToInt32(Console.ReadLine()), ElementType.Quad);
                        Console.WriteLine("Successfully removed quad...");
                        break;
                    case '8': /// Create predefined image
                        image = CreatePredefinedImage();
                        Console.WriteLine("Successfully created predefined image...");
                        break;
                    case '9': /// Move whole image
                        MoveElement(image, ElementType.Image);
                        Console.WriteLine("Successfully moved whole image...");
                        break;
                    case '0': /// Exit
                        return;
                    default:
                        Console.WriteLine("Error: Wrong key...");
                        break;
                }
            }
        }

        /// <summary>
        /// Logika do przesuwania obiektów
        /// </summary>
        /// <param name="image"> Reference to image </param>
        /// <param name="elementType"> Type of element to move</param>
        static void MoveElement(Image image, ElementType elementType)
        {
            int numberOfElements = 0;

            if (elementType == ElementType.Tris)
                numberOfElements = image.TrisCount;
            else if (elementType == ElementType.Quad)
                numberOfElements = image.QuadsCount;
            else if (elementType == ElementType.Image)
                numberOfElements = 1;

            // Error catching
            if(numberOfElements <= 0)
            {
                Console.WriteLine("There are no elements of type {0} to move!");
                return;
            }
            if(elementType != ElementType.Image) // Dispaly only if type is not an Image
                Console.WriteLine("There are {0} {1}", numberOfElements, elementType);
            Console.WriteLine("Specify (separated by spaces): x coordiante movement, y coordinate movement" + (elementType == ElementType.Image ? ":" : ", index:"));

            string[] splited = Console.ReadLine().Split(' ');
            int dx = Convert.ToInt32(splited[0]);
            int dy = Convert.ToInt32(splited[1]);
            int index = 0;
            if (elementType != ElementType.Image)
                index = Convert.ToInt32(splited[2]);

            // Error catching
            if (splited.Length < 3 - (elementType == ElementType.Image ? 1 : 0)) // This if is for lower amount of arguments in case if element to move is whole image
            {
                Console.WriteLine("To low number of arguments {0}!");
                return;
            }
            else if (elementType != ElementType.Image)
            {
                if (index >= numberOfElements)
                {
                    Console.WriteLine("Index out of range {0}!");
                    return;
                }
            }

            if(elementType == ElementType.Image)
                image.Move(dx, dx);
            else
                image.Move(dx, dx, index, elementType);
        }

        static Image CreatePredefinedImage()
        {
            Image image = new Image();

            image.AddFigure(new Quad());

            Quad quad = new Quad();
            quad.Move(2, 5);
            image.AddFigure(quad);

            quad = new Quad(new Point(), new Point(3, 0), new Point(7, 7), new Point(0, 3));
            quad.Move(-4, -1);
            image.AddFigure(quad);

            Triangle tris = new Triangle();
            tris.Move(-5, 1);
            image.AddFigure(tris);

            return image;
        }
    }

    

    static class Draw
    {
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Main Menu:");
            Console.WriteLine(" 1. Display image");
            Console.WriteLine(" 2. Add triangle");
            Console.WriteLine(" 3. Move triangle");
            Console.WriteLine(" 4. Remove triangle");
            Console.WriteLine(" 5. Add quad");
            Console.WriteLine(" 6. Move quad");
            Console.WriteLine(" 7. Remove quad");
            Console.WriteLine(" 8. Create predefined image");
            Console.WriteLine(" 9. Move whole image");
            Console.WriteLine(" 0. Exit");
        }

        public static void Image(Image image)
        {
            Console.Clear();

            Console.WriteLine("Press any key to go back...\n");
            Console.Write(image);

            Console.ReadKey(true);
        }
    }
}
