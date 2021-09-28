using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment_2__Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] possibleWords = new string[10] { "HUND", "KATT", "BANAN", "GRYTA", "ÄPPLE", "PRESIDENT", "SKOLMINISTER", "INTERNET", "MARÄNG", "ELEFANT"};
            Random random = new Random();
            StringBuilder incorrectLetters = new StringBuilder();
            List<String> guesses = new List<String>();
            String correctWord;
            int triesLeft;
            char[] correctLetters;
            char guessedLetter;
            bool correctGuess, gameWon;

            Console.WriteLine("Välkommen till Hänga Gubbe!");
            Console.WriteLine();

            do
            {
                gameWon = false;
                correctWord = possibleWords[random.Next(0, possibleWords.Length)];
                correctLetters = new char[correctWord.Length];
                triesLeft = 10;
                
                for (int i = 0; i < correctWord.Length; i++)
                {
                    correctLetters[i] = '_';
                }
                Console.WriteLine();

                while (triesLeft > 0 && !gameWon)
                {
                    correctGuess = false;
                    Console.Write("Ordet som söks: ");
                    
                    for (int i = 0; i < correctWord.Length; i++)
                    {
                        Console.Write(correctLetters[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Inkorrekta gissningar: " + incorrectLetters.ToString());
                    Console.WriteLine("Kvarvarande gissningar: " + triesLeft);
                    Console.WriteLine("Vad vill du göra?");
                    Console.WriteLine("1. Gissa bokstav");
                    Console.WriteLine("2. Gissa ord");
                    
                    switch (Console.ReadLine())
                    {
                        case "1":
                            guessedLetter = GuessLetter();

                            if (!guesses.Contains(guessedLetter.ToString()))
                            {
                                for (int i = 0; i < correctWord.Length; i++)
                                {
                                    if (guessedLetter == correctWord[i])
                                    {
                                        correctLetters[i] = guessedLetter;
                                        correctGuess = true;
                                    }
                                }
                                
                                if (correctGuess)
                                {
                                    Console.WriteLine("Bokstaven finns i ordet!");
                                }
                                else
                                {
                                    Console.WriteLine("Bokstaven finns inte i ordet.");
                                    incorrectLetters.Append(guessedLetter);
                                    triesLeft--;
                                }
                                
                                guesses.Add(guessedLetter.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Du har redan gissat den bokstaven.");
                            }
                            Console.WriteLine();
                            break;
                        case "2":
                            if (GuessWord() == correctWord)
                            {
                                Console.WriteLine(correctWord);
                                Console.WriteLine("Grattis, du har vunnit!");
                                gameWon = true;
                            }
                            else
                            {
                                Console.WriteLine("Det ordet var fel.");
                                triesLeft--;
                            }
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("Ogiltig input, försök igen");
                            break;
                    }

                    if (correctLetters.SequenceEqual(correctWord.ToCharArray()))
                    {
                        Console.WriteLine(correctWord);
                        Console.WriteLine("Grattis, du har vunnit!");
                        gameWon = true;
                    }
                }

                if (!gameWon)
                {
                    Console.WriteLine("Tyvärr, du förlorade. Rätt ord var: " + correctWord);
                }
                Console.Write("Vill du fortsätta (j/n)? ");
            }
            while (Console.ReadLine() != "n");
            
        }

        private static char GuessLetter()
        {
            char guess;
            bool success;

            do
            {
                Console.Write("Gissa en bokstav: ");
                success = char.TryParse(Console.ReadLine().ToString(), out guess);

                if (!success || !Char.IsLetter(guess))
                {
                    Console.WriteLine("Var vänlig skriv in en bokstav.");
                }
            }
            while (!success || !Char.IsLetter(guess));
            
            return Char.ToUpper(guess);
        }

        private static String GuessWord()
        {
            Console.Write("Gissa ett ord: ");
            String guess = Console.ReadLine().ToUpper();
            return guess;
        }

    }
}
