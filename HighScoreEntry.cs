using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Breakout
{
    public class HighScoreEntry
    {
        // Highscore-listan skapas
        static HighScoreEntry topThree = new();
        public HighScoreEntry TopThree { get => topThree; set => topThree = value; }

        // Listans innehåll från start
        int[] hsScore = new int[3];
        string[] name = new string[3];
        public int[] HsScore { get => hsScore; set => hsScore = value; }
        public string[] Name { get => name; set => name = value; }


        // Hittar sparfilen 
        static string path = Path.GetFullPath("..\\..\\..\\");
        static string hsFile = path + "HSlist.txt";

        // Läser in highscore från sparfilen
        public static void ReadHighScoreFromFile()
        {
            string line;
            int counter = 0;
            using (StreamReader file = new(hsFile))
            {
                while ((line = file.ReadLine()!) != null)
                {
                    string[] field = line.Split(",");
                    topThree.name[counter] = field[0];
                    topThree.hsScore[counter] = int.Parse(field[1]);
                    counter++;
                }
            }
        }

        // Kollar och möblerar om toppliste-placeringen
        public static void CheckPlacement()
        {
            int score = Program.PreviousScore;
            if (score < topThree.hsScore[2])
                return;
            else if (score >= topThree.hsScore[0])
            {
                topThree.hsScore[2] = topThree.hsScore[1];
                topThree.name[2] = topThree.name[1];
                topThree.hsScore[1] = topThree.hsScore[0];
                topThree.name[1] = topThree.name[0];
                EnterHighScore(0);
            }
            else if (score >= topThree.hsScore[1])
            {
                topThree.hsScore[2] = topThree.hsScore[1];
                EnterHighScore(1);
            }
            else if (score >= topThree.hsScore[2])
            {
                EnterHighScore(2);
            }
            return;
        }

        // Om poängen når topplistan
        static void EnterHighScore(int place)
        {
            
            Write("Congratulations! \nYou reached our top three highest scores, well played. \nEnter your name: \n> ");
            topThree.name[place] = ReadLine()!;
            topThree.hsScore[place] = Program.PreviousScore;
            Write("Temporarily added to Highscore.\nSave highscore list permanently? (y/n) ");
            string ans = ReadLine()!;
            if (char.ToLower(ans[0]) == 'y')
            {
                AddHighScoreToFile();
                WriteLine("Saved.");
                ReadKey(true);
            }
            else
            {
                WriteLine("Highscore will revert to last save when ending program.");
                ReadKey(true);
            }
            Clear();
            return;
        }

        // Skriver över sparfilen med nya topp tre
        public static void AddHighScoreToFile()
        {
            using (StreamWriter file = new(hsFile))
            {
                for (int i = 0; i < topThree.hsScore.Length; i++)
                {
                    file.WriteLine(topThree.name[i] + "," + topThree.hsScore[i]);
                }
            }
        }
        public static void PrintList()
        {
            Clear();
            for (int i = 0; i < 3; i++)
            {
                WriteLine($"   ~~~~  {i + 1} place  ~~~~  \n {topThree.name[i],-10}  ::  {topThree.hsScore[i]} points.\n");
            }
            ReadKey(true);
            Clear();
            return;
        }
    }

}

