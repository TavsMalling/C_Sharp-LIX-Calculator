using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gruppe_Arbejde_Rød
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] text = TextInput();
           


            int longWords = LongWords(TextInput());
            string[] textNoDoubles = Duplicates();
            double[] wordReps = WordReps(text, textNoDoubles);
            double[] percent = WordPercent(textNoDoubles, wordReps, text);

            
           

            int dots = Punktummer(TextInput());//this will take the method op to the void
          
            Lix(text, dots, longWords);
            Console.ReadKey();

        }
        static int Punktummer(string[] text)
        {
           
            int dots = 0; //this is the starter vaule
            for (int i = 0; i < text.Length; i++)//this will loop the whole thing to look if the text contains one of the charateres
            {
                if (text[i].EndsWith(",") || text[i].EndsWith("?") || text[i].EndsWith(";") ||
                    text[i].EndsWith(":") || text[i].EndsWith(".") ||
                    text[i].EndsWith("!"))//if it ends whit one of the charateres it will add one up in dots
                {
                    dots++;
                }
            }
            return dots; //this will return the value of dots
        }
        static string[] TextInput()
        {
            //StreamReader reads the file so i can recall it later on
            StreamReader readInfo = new StreamReader("../../Text.txt");

            // I make 2 datas one to readline from my text file and the otherone is to backup the words
            string data = "";
            string data2 = "";
            while (data2 != null)
            {
                data2 = readInfo.ReadLine();
                data = data + " " + data2;
            }
            //Now we close our streamReader
            readInfo.Close();

            /*Here we trim the text for the first white space and
            we make a array and its length just need to be longet than the text*/
            data = data.TrimStart(' ');
            string[] text1 = new string[data.Length];

            /* We make counter to count the amount of words
            and we find the place of the first space */
            int counter = 0;
            int j = data.IndexOf(' ');

            /*
             * Here we find the the first word in data. Then we put the word into a array
             * Now we remove the word. Then we remove whitespace
             * Then we find the next white space to repeat the process
             */
            for (int i = 0; j != -1; i++)
            {
                text1[i] = data.Substring(0, j);
                data = data.Remove(0, j + 1);
                data = data.TrimStart(' ');
                j = data.IndexOf(' ');
                counter++;
            }

            //We generate a array that has the same amount of placeholders as counters value
            //The we put the first array into one that faster for the computer to use
            string[] text = new string[counter];
            for (int i = 0; i != counter; i++)
            {
                text[i] = text1[i];
            }
            return text;
        }
    
        static string[] Duplicates()
        {
            /*This method checks a given array of string for for all its words and puts them into a new array but without any duplicates*/
            string[] text = TextInput();
            string[] textNoDoubles = new string[10];

            int stringPosNoDoubles = 0;

            //Runs through each position of the initial array
            for (int i = 0; i < text.Length; i++)
            {
               
                if (textNoDoubles.Contains(text[i]) == false)
                {
                    textNoDoubles[stringPosNoDoubles] = text[i];
                    stringPosNoDoubles++;

                }
            }

            return textNoDoubles;

        }

        static int LongWords(string[] text)
        {
            /*Check for words over 6 characters long and counts them*/
            int longWords = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].EndsWith(".") || text[i].EndsWith("!") || text[i].EndsWith("?") || text[i].EndsWith(";") || text[i].EndsWith(":"))
                {
                    text[i] = text[i].Remove(text[i].Length - 1);
                }
                if (text[i].Length >= 6)
                {
                    longWords++;
                }
            }

            return longWords;
        }

        static double[] WordReps(string[] text, string[] textNoDoubles)
        {
            /*Counts word repetitions foe each word*/
            double[] wordRepetitions = new double[textNoDoubles.Length];
            

            for (int u = 0; u < textNoDoubles.Length; u++)
            {
                for(int wordCycle = 0; wordCycle < text.Length; wordCycle++)
                {
                    if(textNoDoubles[u] == text[wordCycle])
                    {
                        wordRepetitions[u]++;
                        
                    }
                    
                }
            }

            return wordRepetitions;
        }

        static double[] WordPercent(string[] textNoDoubles, double[] wordReps, string[] text)
        {
            /*Calculates the percent that each word makes of the text.*/
            double[] percent = new double[textNoDoubles.Length];
            for (int i = 0; i < percent.Length; i++)
            {
                percent[i] = wordReps[i] / text.Length * 100;
            }

            return percent;
                       
        }

        static void Lix(string[] text, int punctuation, int longWords)
        {
            //Words in text.
            int o = text.Length;
            //Dots etc. in the text.
            int p = punctuation;
            //Words with over 6 chars.
            int l = longWords;

            //The Calculation for LIX-Number
            double lix = (o / p) + (l * 100 / o);
            //Writes the LIX-Number out when calculated
            Console.WriteLine("Dit lix-tal ligger på -> " + lix);

            //This little beuty will tell what kind of text you've readed from the text-file. 
            if (lix >= 55)
            {
                Console.Write("Din tekst er meget svær, med et nivaeu som faglitteratur på akademisk niveau eller lovtekster.");
            }
            else if (lix >= 45)
            {
                Console.Write("Din tekst er svær, f.eks. saglige bøger, populærvidenskabelige værker eller akademiske udgivelser");
            }
            else if (lix >= 35)
            {
                Console.Write("Din tekst er middel, f.eks. dagblade og tidsskrifter.");
            }
            else if (lix > 25)
            {
                Console.Write("Din tekst er let for øvede læsere, f.eks. ugebladslitteratur og skønlitteratur for voksne.");
            }
            else
            {
                Console.Write("Din tekst er let tekst for alle læsere, f.eks. børnelitteratur.");
            }
        }



    }
}
