using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZInput;
using System.IO;
using System.Threading;

namespace game_project
{
    class Program
    {
        static int score = 0;
        int bobX = 4;
        int bobY = 4;
        static void Main(string[] args)
        {
            int bobX = 4;
            int bobY = 4;
            char[] bob = new char[] { 'B', 'O', 'B' };

            char[,] maze = new char[10, 11] {
             { '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '%'},
             { '%', '%', '%', '%', '%', '%', '%', '%', '%', '%', '%'}
             };



            // Ghost 1 (Horizontal) Information
            char previous1 = ' ';
            int ghost1X = 8;
            int ghost1Y = 8;
            string ghost1direction = "left";
            int count1 = 0;

            int bulletX;
            int bulletY;
            int bulletCount = 0;



            printMaze(maze);

            Console.SetCursorPosition(bobY, bobX);
            Console.Write("B");

            bool gameRunning = true;
            while (true)
            {
                Thread.Sleep(90);
                printScore();
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    movebobUp(maze, ref bobX, ref bobY);
                }
                if (Keyboard.IsKeyPressed(Key.DownArrow))
                {
                    movebobDown(maze, ref bobX, ref bobY);
                }
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    movebobLeft(maze, ref bobX, ref bobY);
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    movebobRight(maze, ref bobX, ref bobY);
                }
                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    generateBullet();
                }
                count1++;

                int op = 0;
                if (op == 0)            // Slowest Movement
                {
                    gameRunning = moveGhostInLine(ref ghost1direction, maze, ref ghost1X, ref ghost1Y, ref previous1);
                    if (gameRunning == false)
                    {
                        break;
                    }
                    count1 = 0;
                }

                Console.ReadKey();

            }
        }
        static void printScore()
        {
            Console.SetCursorPosition(79, 12);
            Console.WriteLine("Score: " + score);
        }
        static void help()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" #   #    #####   #       ##### ");
            Console.WriteLine(" #   #    #       #       #    # ");
            Console.WriteLine(" #####    ####    #       #####  ");
            Console.WriteLine(" #   #    #       #       #     ");
            Console.WriteLine(" #   #    #####   ######  #     ");
            Console.WriteLine();
            Console.WriteLine(" Here are the following instructions to play the game : ");
            Console.WriteLine(" 1. Use up arrow key to move the character upward. ");
            Console.WriteLine(" 2. Use down arrow key to move the character downward. ");
            Console.WriteLine(" 3. Use right arrow key to move the character right. ");
            Console.WriteLine(" 4. Use left arrow key to move the character left. ");
            Console.WriteLine(" 5. Use space bar to fire your enemies. Enemies will not remove but score will be incremented by 1. ");
            Console.WriteLine(" 6. The food pallets is in the form of '.'. If the player eat them, the score will be increased.");
            Console.WriteLine(" If you touch any of the enemies the game will be over. ");
            Console.WriteLine(" If you lose the game, then you have to press 'ctrl key' + c.");
            Console.WriteLine(" If you want to play the game again, you can just run the exe file of the game.");
        }




        static bool moveGhostInLine(ref string direction, char[,] maze, ref int X, ref int Y, ref char previous)
        {
            if (maze[X, Y - 1] == 'B' || maze[X, Y + 1] == 'B' || maze[X + 1, Y] == 'B' || maze[X - 1, Y] == 'B')
                if (direction == "left" && (maze[X, Y - 1] == ' ' || maze[X, Y - 1] == '.'))
                {
                    maze[X, Y] = previous;
                    Console.SetCursorPosition(Y, X);
                    Console.Write(previous);

                    Y = Y - 1;

                    previous = maze[X, Y];
                    Console.SetCursorPosition(Y, X);
                    Console.Write("S");
                    if (maze[X, Y - 1] == '|')
                    {
                        direction = "right";
                    }
                }
                else if (direction == "right" && (maze[X, Y + 1] == ' ' || maze[X, Y + 1] == '.'))
                {
                    maze[X, Y] = previous;
                    Console.SetCursorPosition(Y, X);
                    Console.Write(previous);

                    Y = Y + 1;

                    previous = maze[X, Y];
                    Console.SetCursorPosition(Y, X);
                    Console.Write("S");
                    if (maze[X, Y + 1] == '|')
                    {
                        direction = "left";
                    }
                }
                else if (direction == "up" && (maze[X - 1, Y] == ' ' || maze[X - 1, Y] == '.'))
                {
                    maze[X, Y] = previous;
                    Console.SetCursorPosition(Y, X);
                    Console.Write(previous);

                    X = X - 1;

                    previous = maze[X, Y];
                    Console.SetCursorPosition(Y, X);
                    Console.Write("S");
                    if (maze[X - 1, Y] == '%' || maze[X - 1, Y] == '#')
                    {
                        direction = "down";
                    }
                }
                else if (direction == "down" && (maze[X + 1, Y] == ' ' || maze[X + 1, Y] == '.'))
                {
                    maze[X, Y] = previous;
                    Console.SetCursorPosition(Y, X);
                    Console.Write(previous);

                    X = X + 1;

                    previous = maze[X, Y];
                    Console.SetCursorPosition(Y, X);
                    Console.Write("S");
                    if (maze[X + 1, Y] == '%' || maze[X + 1, Y] == '#')
                    {
                        direction = "up";
                    }
                }
            return true;
        }





        static void calculateScore()
        {
            score = score + 1;
        }

        static void movebobUp(char[,] maze, ref int bobX, ref int bobY)
        {
            if (maze[bobX - 1, bobY] == ' ' || maze[bobX - 1, bobY] == '.')
            {
                maze[bobX, bobY] = ' ';
                Console.SetCursorPosition(bobY, bobX);
                Console.Write(" ");
                bobX = bobX - 1;
                if (maze[bobX, bobY] == '.')
                {
                    calculateScore();
                }
                Console.SetCursorPosition(bobY, bobX);
                maze[bobX, bobY] = 'B';
                Console.Write("B");

            }
        }
        static void movebobDown(char[,] maze, ref int bobX, ref int bobY)
        {
            if (maze[bobX + 1, bobY] == ' ' || maze[bobX + 1, bobY] == '.')
            {
                maze[bobX, bobY] = ' ';
                Console.SetCursorPosition(bobY, bobX);
                Console.Write(" ");
                bobX = bobX + 1;
                Console.SetCursorPosition(bobY, bobX);
                if (maze[bobX, bobY] == '.')
                {
                    calculateScore();
                }
                maze[bobX, bobY] = 'B';
                Console.Write("B");

            }
        }

        static void movebobLeft(char[,] maze, ref int bobX, ref int bobY)
        {
            if (maze[bobX, bobY - 1] == ' ' || maze[bobX, bobY - 1] == '.')
            {
                maze[bobX, bobY] = ' ';
                Console.SetCursorPosition(bobY, bobX);
                Console.Write(" ");
                bobY = bobY - 1;
                if (maze[bobX, bobY] == '.')
                {
                    calculateScore();
                }
                Console.SetCursorPosition(bobY, bobX);
                maze[bobX, bobY] = 'B';
                Console.Write("B");

            }
        }

        static void movebobRight(char[,] maze, ref int bobX, ref int bobY)
        {
            if (maze[bobX, bobY + 1] == ' ' || maze[bobX, bobY + 1] == '.')
            {
                maze[bobX, bobY] = ' ';
                Console.SetCursorPosition(bobY, bobX);
                Console.Write(" ");
                bobY = bobY + 1;
                if (maze[bobX, bobY] == '.')
                {
                    calculateScore();
                }
                Console.SetCursorPosition(bobY, bobX);
                maze[bobX, bobY] = 'B';
                Console.Write("B");

            }
        }

        static void printMaze(char[,] maze)
        {
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Console.Write(maze[x, y]);
                }
                Console.WriteLine();
            }
        }
        static void generateBullet()
        {
            int bulletX;
            int bulletY;
            bool isBulletActive = true;
            int bulletCount = 0;
            bulletX[bulletCount] = bobX + 4;
            bulletY[bulletCount] = bobY;
            isBulletActive[bulletCount] = true;
            Console.SetCursorPosition(bobX + 4, bobY);
            Console.WriteLine( ".");
            bulletCount++;
        }

        static void moveBullet()
        {
            for (int x = 0; x < bulletCount; x++)
            {
                if (isBulletActive[x] == true)
                {
                    char next = getCharAtxy(bulletX[x] + 1, bulletY[x]);
                    if (next != ' ')
                    {
                        eraseBullet(bulletX[x], bulletY[x]);
                        makeBulletInactive(x);
                    }
                    else
                    {
                        eraseBullet(bulletX[x], bulletY[x]);
                        bulletX[x] = bulletX[x] + 1;
                        printBullet(bulletX[x], bulletY[x]);
                    }
                }
            }

        }
        static void printBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine( ".");
        }

        static void eraseBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine( " ");
        }

        static void makeBulletInactive(int index)
        {
            isBulletActive[index] = false;
        }
    }

