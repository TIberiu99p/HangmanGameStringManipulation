using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;

namespace StringManipulation
{
    public class positionOfCharacter
    {
        public string? nameOfWord { get; set; }
        public string? c { get; set; }
        public int pos { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            string contents = "";
            List<positionOfCharacter> list = new List<positionOfCharacter>();
            
            try
            {
                // Get file name.
                string path = @"C:\Users\pahar\OneDrive\Desktop\Peregrine\Work\Hangman\HangmanGameStringManipulation\StringManipulation\Files\Words.txt";
                // Get path name.
                string filename = Path.GetFileName(path);
                // Open the text file using a stream reader. Read into a string
                using (var sr = new StreamReader(path))
                {
                    // Read the stream as a string, and write the string to the console.
                    contents = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("---------");
           
            var array = contents.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
            
            Random random = new Random();
            string selectWord = array[random.Next(array.Length)].Trim().ToLower();
            char[] guessedWord = new char[selectWord.Length];

            for(int i = 0; i < selectWord.Length; i++)
            {
                guessedWord[i] = '_';
            }

            int attempts = 6;

            while (attempts > 0)
            {
                Console.WriteLine("Current word: " + new string(guessedWord));
                Console.WriteLine("Attempts left: " + attempts);
                Console.Write("Enter a letter: ");
                char guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                bool correctGuess = false;

                for (int i = 0; i < selectWord.Length; i++)
                {
             
                    if (i < guessedWord.Length && selectWord[i] == guess)
                    {
                        guessedWord[i] = guess;
                        correctGuess = true;
                    }
                }

                if (!correctGuess)
                {
                    attempts--;
                    Console.WriteLine("Incorrect guess! Try again.");
                }

                if (new string(guessedWord) == selectWord)
                {
                    Console.WriteLine("Congratulations! You guessed the word: " + selectWord);
                    break;
                }
            }

            if (attempts == 0)
            {
                Console.WriteLine("Sorry, you ran out of attempts. The correct word was: " + selectWord);
            }


            Console.ReadLine();
        }
    }
}

