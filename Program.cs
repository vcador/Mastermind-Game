using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermindGame
{
    class Program
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 6;
        static void Main(string[] args)
        {
            MasterMethod();
        }

        private static void MasterMethod()
        {
            int hiddenCode = RandomCodeGenerator();
            int playerGuesses = 10;
            bool gameWon = false;

            //To compare the two values
            bool[] userInputArray = new bool[4];
            bool[] answerArray = new bool[4];

            //Guesses Loop
            Console.WriteLine("Welcome to my version of Mastermind!");
            while (playerGuesses > 0)
            {

                Console.WriteLine("You have: " + playerGuesses.ToString() + " chance left");

                Console.WriteLine("\nPick four number between 1 and 6:\n");
          
                string userInput = Console.ReadLine();


                if (FormatChecking(ref userInput, hiddenCode))
                {
                    int userAnswer = Int32.Parse(userInput);

                    if (userAnswer == hiddenCode)
                    {
                        gameWon = true;
                        break;
                    }

                    int rightPlacement = CorrectPlacement(userAnswer, userInputArray, answerArray, hiddenCode);
                    int wrongPlacement = IncorrectPlacement(userAnswer, userInputArray, answerArray, hiddenCode);

                    string trackProgress = "\nResult: ";

                    //Switch for results string.
                    switch (rightPlacement)
                    {
                        case 0:
                            break;
                        case 1:
                            trackProgress += "+";
                            break;
                        case 2:
                            trackProgress += "++";
                            break;
                        case 3:
                            trackProgress += "+++";
                            break;
                    }
                    switch (wrongPlacement)
                    {
                        case 0:
                            break;
                        case 1:
                            trackProgress += "-";
                            break;
                        case 2:
                            trackProgress += "--";
                            break;
                        case 3:
                            trackProgress += "---";
                            break;
                        case 4:
                            trackProgress += "----";
                            break;
                    }

                    Console.WriteLine(trackProgress + "\n");
                    Console.WriteLine("**********************\n");
                    playerGuesses--;
                }
                else
                    Console.WriteLine("Input 4 digits and less than 6.");
            }
            if (gameWon)
            {
                Console.WriteLine("**************************\n");
                Console.WriteLine("\nYou break the  Code! Hooray!");
            }
            else
            {
                Console.WriteLine("\nGame over!\n");
                Console.WriteLine("The hidden code was " + hiddenCode);
            }


        }
        private static int RandomCodeGenerator()
        {
            string newFormNumber = "";
            Random rdn = new Random();
            for (int i = 0; i < 4; i++)
            {
                newFormNumber = newFormNumber.Insert(newFormNumber.Length, rdn.Next(MIN_VALUE, MAX_VALUE).ToString());
            }
            return Int32.Parse(newFormNumber);
        }

        private static int CorrectPlacement(int userAnswer, bool[] userInputArray, bool[] answerArray, int hiddenCode)
        {
            for (int i = 0; i < 4; i++)
            {
                userInputArray[i] = false;
                answerArray[i] = false;
            }

            int rightPlaceCount = 0;

            for (int i = 0; i < 4; i++)
            {
                int guessInputDigit = userAnswer % 10;
                userAnswer /= 10;
                int rdnDigit = hiddenCode % 10;
                hiddenCode /= 10;

                if (guessInputDigit == rdnDigit)
                {
                    userInputArray[i] = true;
                    answerArray[i] = true;
                    rightPlaceCount++;
                }
            }
            return rightPlaceCount;
        }

        public static int IncorrectPlacement(int userGuess, bool[] userInputArray, bool[] answerArray, int hiddenCode)
        {
            int wrongPlaceCount = 0;
            int guessInputDigit;

            for (int i = 0; i < 4; i++)
            {
                guessInputDigit = userGuess % 10;
                userGuess /= 10;
                if (userInputArray[i] == false)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int rdnDigit = hiddenCode % 10;
                        hiddenCode /= 10;
                        if (answerArray[j] == false)
                        {
                            if (guessInputDigit == rdnDigit)
                            {
                                wrongPlaceCount++;
                                userInputArray[i] = true;
                                answerArray[j] = true;
                                break;
                            }
                        }
                    }
                }
            }
            return wrongPlaceCount;
        }

        private static bool FormatChecking(ref string userInput, int hiddenCode)
        {
            try
            {
               int userAnswer = Int32.Parse(userInput);
                int guessInputDigit = 0;
                for (int i = 0; i < 4; i++)
                {
                    guessInputDigit = userAnswer % 10;
                    if (guessInputDigit > MAX_VALUE || guessInputDigit < MIN_VALUE || userAnswer < 1111 || userAnswer > 6666)
                    {
                        throw (new Exception());
                    }
                }
            }
            catch
            {
                Console.WriteLine("\nMake sure your input is between 1111 and 6666, with each digit being no larger that 6.");
                Console.WriteLine("\nMake your pick:\n");
                userInput = Console.ReadLine();
                if (FormatChecking(ref userInput, hiddenCode))
                    return true;
                return false;
            }
            return true;
        }

    }
}