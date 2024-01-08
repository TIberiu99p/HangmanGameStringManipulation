using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Text.Json;
using static System.Net.WebRequestMethods;


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
            bool playAgain = true;
            while (playAgain)
            {
                HangManGame();
                Console.Write("Want to play again? (y/n): ");
                char response = Console.ReadKey().KeyChar;
                Console.WriteLine();
                playAgain = (response == 'y' || response == 'Y');
            }

        }

        static void HangManGame()
        {
            string contents = "";
            List<positionOfCharacter> list = new List<positionOfCharacter>();

            try
            {
                string jsonPath = @"C:\Users\pahar\OneDrive\Desktop\Peregrine\Work\Hangman\HangmanGameStringManipulation\StringManipulation\Files\Words.json"; 
                string jsonString = System.IO.File.ReadAllText(jsonPath);

              
                Dictionary<string, List<string>> difficultyLevels = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);

               
                string selectedDifficulty = ChooseDifficulty();

                
                if (difficultyLevels.ContainsKey(selectedDifficulty))
                {
                    List<string> words = difficultyLevels[selectedDifficulty];

                    Random random = new Random();
                    string selectWord = words[random.Next(words.Count)].Trim().ToLower();
                    char[] guessedWord = new char[selectWord.Length];

                    for (int i = 0; i < selectWord.Length; i++)
                    {
                        guessedWord[i] = '_';
                    }

                    int attempts = 6;

                    while (attempts > 0)
                    {
                        Console.Clear();
                        DrawHangman(attempts);
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

                        Console.Out.Flush();

                        if (new string(guessedWord) == selectWord)
                        {
                            Console.Clear();
                            DrawHangman(attempts);
                            Console.WriteLine("Congratulations! You guessed the word: " + selectWord);
                            break;
                        }
                    }

                    if (attempts == 0)
                    {
                        Console.Clear();
                        DrawHangman(attempts);
                        Console.WriteLine("Sorry, you ran out of attempts. The correct word was: " + selectWord);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid difficulty level selected.");
                }

                Console.ReadLine();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file:");
                Console.WriteLine(e.Message);
            }
        }
        static string ChooseDifficulty()
        {
            Console.WriteLine("Choose difficulty level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            while (true)
            {
                Console.Write("Enter the number corresponding to your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return "Easy";
                    case "2":
                        return "Medium";
                    case "3":
                        return "Hard";
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }

        static void DrawHangman(int incorrectAttempts)
        {
            switch (incorrectAttempts)
            {
                case 6:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    break;
                case 5:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    break;
                case 4:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    break;
                case 3:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /|");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    break;
                case 2:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /|\\");
                    Console.WriteLine("   |");
                    Console.WriteLine("___|___");
                    break;
                case 1:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /|\\");
                    Console.WriteLine("   |     /");
                    Console.WriteLine("___|___");
                    break;
                case 0:
                    Console.WriteLine("   ________");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /|\\");
                    Console.WriteLine("   |     / \\");
                    Console.WriteLine("___|___");
                    break;
                default:
                    break;
            }
        }
    }
}

