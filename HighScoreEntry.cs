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

        //public int HSScore;
        static string dir = "..\\..\\..\\";
        static string path = Path.GetFullPath(dir);
        int[] hsScore = new int[3] { 1, 10, 100 };
        string[] name = new string[3] {"NN","NN","NN"};

        public HighScoreEntry(string contents, int count)
        {
            string[] field = contents.Split(",");
            name[count] = field[count];
            hsScore[count] = Convert.ToInt32(field[count++]);
        }
     
        public static List<HighScoreEntry> highScoreEntries = new();
        public static void ReadHighScoreFromFile()
        {
            string line;
            int counter = 0;
            using (StreamReader file = new(path+"HSlist.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    HighScoreEntry addition = new(line, counter);
                    counter++;
                }
            }
        }
        static void EnterHighScore() 
        { 
            Write("Congratulations! \nYou reached our top three highest scores, well played. \nEnter your name: \n> ");
            string name = ReadLine();

        }
        public static void AddHighScoreToFile(string inpName, int newHScore)
        {
            string input = inpName+","+newHScore.ToString();
            using (StreamWriter file = new("..\\..\\..\\HSlist.txt", true))
            {
                file.WriteLine(inpName + "," + newHScore);
            }
        }
        public static void PrintList()
        {
            WriteLine(path);
    /*        highScoreEntries.Sort((x, y) => y.HSScore.CompareTo(x.HSScore));
            foreach (HighScoreEntry highScoreEntry in highScoreEntries)
            {
                WriteLine($"{highScoreEntry.Name}, {highScoreEntry.HSScore} ");
            }
         */   //highScoreEntries.Clear();
        }
    }

}

