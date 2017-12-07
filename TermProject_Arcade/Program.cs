using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProject_Arcade
{
    //
    //
    //Title: NMC Arcade
    //Application Type: Console
    //Description: Arcade where users buy coins, play games and earn tickets with coins, and trade-in tickets for coins
    //Author: Richie Tarkowski
    //Date Created: 11/29/17
    //Last Modified: 12/06/17
    //
    //

    class Program
    {
        //
        //Declare Enums
        //
        enum package
        {
            BRONZE,
            SILVER,
            GOLD,
            PLATINUM,
            DIAMOND,
            NONE
        }

        enum yesNo
        {
            YES,
            NO
        }

        //
        //Global Variables
        //
        static int coins = 0;
        static int tickets = 0;

        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayOpeningScreen();

            BuyCoins();
            MainMenu();
            Store();

            DisplayClosingScreen();
        }

        /// <summary>
        /// User Purchases Coins
        /// </summary>
        static void BuyCoins()
        {
            string userResponse;
            package[] packageType = new package[1];

            DisplayHeader("Choose a coin amount to purchase.");

            Console.WriteLine("Bronze: 100 coins - $0.99");
            Console.WriteLine("Silver: 600 coins - $4.99");
            Console.WriteLine("Gold: 1200 coins - $9.99");
            Console.WriteLine("Platinum: 2800 coins - $19.99");
            Console.WriteLine("Diamond: 8000 coins - $49.99");
            Console.WriteLine();

            //
            //Validate purchase type w/ Enum
            //
            do
            {
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.Green;
                userResponse = Console.ReadLine().ToUpper();
                Console.ForegroundColor = ConsoleColor.White;

                if (!Enum.TryParse<package>(userResponse, out packageType[0]))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid purchase type.");
                    Console.WriteLine();
                }
            } while (!Enum.TryParse<package>(userResponse, out packageType[0]));

            //
            //Add coins to account based on purchase
            //
            switch (packageType[0])
            {
                case package.BRONZE:
                    coins += 100;
                    break;
                case package.SILVER:
                    coins += 600;
                    break;
                case package.GOLD:
                    coins += 1200;
                    break;
                case package.PLATINUM:
                    coins += 2800;
                    break;
                case package.DIAMOND:
                    coins += 8000;
                    break;
                case package.NONE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Main Menu Loop
        /// </summary>
        /// <returns></returns>
        static List<string> MainMenu()
        {
            //
            //Declare Variables
            //
            string userResponse;
            string magicNumber = "Magic Number";
            string slotMachine = "Slot Machine";
            string guessingGame = "Guessing Game";
            string doubleTickets = "Double Tickets";
            int game = 0;
            bool buyMoreCoins = true;
            bool exiting = false;
            bool confirmExit = false;

            //
            //Create a list to keep track of games played
            //
            List<string> played = new List<string>();

            //
            //Main Menu
            //
            while (buyMoreCoins == true)
            {
                while (exiting == false)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    Console.Write(" Arcade Menu".PadRight(105));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Coins: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{coins}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("".PadRight(103));
                    Console.Write("Tickets: ");

                    if (tickets > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write(tickets);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.WriteLine("1) Magic Number - 100 coins");
                    Console.WriteLine("2) Slot Machine - 100 coins");
                    Console.WriteLine("3) Guessing Game - 100 coins");
                    Console.WriteLine("4) Double Tickets - 100 coins");
                    Console.WriteLine("5) Store");
                    Console.WriteLine("6) Account");
                    Console.WriteLine("7) Purchase more coins");
                    Console.WriteLine("8) Exit");
                    Console.WriteLine();

                    //
                    //Validate Menu Options
                    //
                    do
                    {
                        Console.Write(">");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        userResponse = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;

                        if (!int.TryParse(userResponse, out game))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid game entry. Try again.");
                            Console.WriteLine();
                        }
                    } while (!int.TryParse(userResponse, out game));

                    //
                    //Execute User's Choice
                    //If game - Add to List
                    //
                    switch (game)
                    {
                        case 1:
                            MagicNumber();
                            played.Add(magicNumber);
                            break;
                        case 2:
                            SlotMachine();
                            played.Add(slotMachine);
                            break;
                        case 3:
                            GuessingGame();
                            played.Add(guessingGame);
                            break;
                        case 4:
                            DoubleTickets();
                            played.Add(doubleTickets);
                            break;
                        case 5:
                            Store();
                            break;
                        case 6:
                            Account(played);
                            break;
                        case 7:
                            BuyCoins();
                            break;
                        case 8:
                            confirmExit= Exit();
                            if(confirmExit == true)
                            {
                                exiting = true;
                                buyMoreCoins = false;
                            }
                            else { exiting = false; }
                            break;
                        default:
                            break;
                    }

                    //
                    //When out of coins
                    //
                    if (coins <= 0)
                    {
                        exiting = true;
                    }
                }
                //
                //Decides whether to exit or prompt user to buy more coins
                //
                if (coins <= 0)
                {
                    buyMoreCoins = BuyMoreCoins();
                    if (buyMoreCoins == true)
                    {
                        exiting = false;
                    }
                }
                else
                {
                    exiting = true;
                }

                if (exiting == true)
                {
                    buyMoreCoins = false;
                }
            }
            return played;
        }

        /// <summary>
        /// Confirmation that the user wants to exit
        /// </summary>
        /// <returns></returns>
        static bool Exit()
        {
            //
            //Declare Variables
            //
            bool exitGame;
            string userResponse;
            yesNo[] leaveGame = new yesNo[1];

            DisplayHeader("Confirm");
            Console.WriteLine("If you exit, your coins will be lost.");
            Console.WriteLine();

            //
            //Validate User Response w/ Enum
            //
            do
            {
                Console.WriteLine("Do you wish to continue?(Yes / No)");
                Console.Write(">");
                userResponse = Console.ReadLine().ToUpper();

                if (!Enum.TryParse<yesNo>(userResponse, out leaveGame[0]))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Entry. Type in Yes or No.");
                    Console.WriteLine();
                }
            } while (!Enum.TryParse<yesNo>(userResponse, out leaveGame[0]));

            //
            //Exit or Return to Main Menu
            //
            switch (leaveGame[0])
            {
                case yesNo.YES:
                    exitGame = true;
                    break;
                case yesNo.NO:
                    exitGame = false;
                    break;
                default:
                    exitGame = false;
                    break;
            }

            return exitGame;
        }

        /// <summary>
        /// Ask user if they want to buy more coins
        /// </summary>
        /// <returns></returns>
        static bool BuyMoreCoins()
        {
            //
            //Declare variables
            //
            bool buyMoreCoins;
            string userResponse;
            yesNo[] buyMore = new yesNo[1];

            DisplayHeader("Out of Coins");

            //
            //Validate user response
            //
            do
            {
                Console.WriteLine("Would you like to buy more coins?");
                Console.Write(">");
                userResponse = Console.ReadLine().ToUpper();

                if (!Enum.TryParse<yesNo>(userResponse, out buyMore[0]))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid Entry. Type in Yes or No.");
                    Console.WriteLine();
                }
            } while (!Enum.TryParse<yesNo>(userResponse, out buyMore[0]));

            switch (buyMore[0])
            {
                case yesNo.YES:
                    buyMoreCoins = true;
                    BuyCoins();
                    break;
                case yesNo.NO:
                    buyMoreCoins = false;
                    break;
                default:
                    buyMoreCoins = false;
                    break;
            }

            return buyMoreCoins;
        }

        /// <summary>
        /// Display Account Information
        /// </summary>
        /// <param name="gamesPlayed"></param>
        static void Account(List<string> gamesPlayed)
        {
            //
            //Declare Variables
            //
            int magicNumber = 0;
            int slotMachine = 0;
            int guessingGame = 0;
            int doubleTickets = 0;

            DisplayHeader("Account Information");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Games Played Today:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            //
            //Loop through parameter list to get each game played
            //
            foreach (string game in gamesPlayed)
            {
                if (game == "Magic Number")
                {
                    magicNumber += 1;
                }
                else if (game == "Slot Machine")
                {
                    slotMachine += 1;
                }
                else if(game == "Guessing Game")
                {
                    guessingGame += 1;
                }
                else
                {
                    doubleTickets += 1;
                }
            }

            //
            //Display Games Played
            //
            Console.WriteLine($"Magic Number ({magicNumber})");
            Console.WriteLine($"Slot Machine ({slotMachine})");
            Console.WriteLine($"Guessing Game ({guessingGame})");
            Console.WriteLine($"Double Tickets ({doubleTickets})");

            //
            //Display Account Balance
            //
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Account Balance:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("Current Coins: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{coins}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Current Tickets: ");

            if (tickets > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write($"{tickets}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// User buys Extra Credit Points with Tickets
        /// </summary>
        static void Store()
        {
            //
            //Declare Variables
            //
            string userResponse;
            int extraCredit = 0;
            bool check;

            DisplayHeader("Arcade Store");

            Console.WriteLine("Buy your extra credit points here with your tickets!");
            Console.WriteLine("Extra credit points are guarenteed to apply to any of your classes.");
            Console.WriteLine("Maximum: 10 points per class.\n");

            DisplayContinuePrompt();

            DisplayHeader("Purchase Extra Credit Points:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Current Tickets: ");
            if (tickets > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write($"{tickets}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine("1) 1 Extra Credit Point - 100 tickets");
            Console.WriteLine("2) 2 Extra Credit Point - 200 tickets");
            Console.WriteLine("3) 5 Extra Credit Point - 500 tickets");
            Console.WriteLine("4) 10 Extra Credit Point - 1000 tickets");
            Console.WriteLine("5) 20 Extra Credit Point - 2000 tickets");
            Console.WriteLine("6) 50 Extra Credit Point - 5000 tickets");
            Console.WriteLine("7) No thank you");
            Console.WriteLine();

            //
            //Validate Store Purchase
            //
            do
            {
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.Cyan;
                userResponse = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (!int.TryParse(userResponse, out extraCredit))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Try again.");
                    Console.WriteLine();
                }
            } while (!int.TryParse(userResponse, out extraCredit));

            //
            //Send user's selection to function to check ticket amount
            //If true - proceed w/ purchase
            //If false - cancel purchase
            //
            switch (extraCredit)
            {
                case 1:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 1;
                        tickets = tickets - 100;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }

                    break;
                case 2:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 2;
                        tickets = tickets - 200;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }
                    break;
                case 3:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 5;
                        tickets = tickets - 500;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }
                    break;
                case 4:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 10;
                        tickets = tickets - 1000;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }
                    break;
                case 5:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 20;
                        tickets = tickets - 2000;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }
                    break;
                case 6:
                    check = checkTickets(extraCredit);
                    if (check == true)
                    {
                        extraCredit = 50;
                        tickets = tickets - 5000;
                        EnoughTickets(extraCredit);
                    }
                    else
                    {
                        NotEnoughTickets();
                    }
                    break;
                case 7:
                    break;

                default:
                    break;
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Returns Bool value if user has enough tickets for a purchase
        /// </summary>
        /// <param name="extraCredit"></param>
        /// <returns></returns>
        static bool checkTickets(int extraCredit)
        {
            //
            //Declare Variables
            //
            bool check = false;
            int neededTickets = 0;

            switch (extraCredit)
            {
                case 1:
                    neededTickets = 100;
                    break;
                case 2:
                    neededTickets = 200;
                    break;
                case 3:
                    neededTickets = 500;
                    break;
                case 4:
                    neededTickets = 1000;
                    break;
                case 5:
                    neededTickets = 2000;
                    break;
                case 6:
                    neededTickets = 5000;
                    break;
                default: break;
            }

            if (tickets >= neededTickets)
            {
                check = true;
            }
            else
            {
                check = false;
            }

            return check;
        }

        /// <summary>
        /// Executes when user has enough tickets for the store purchase
        /// </summary>
        /// <param name="extraCredit"></param>
        static void EnoughTickets(int extraCredit)
        {
            Console.WriteLine();
            Console.WriteLine($"Congradulations! {extraCredit} extra credit points have been added to your account!");
            Console.WriteLine("Apply them to your classes in your NMC Moodle.");
        }

        /// <summary>
        /// Executes when the user does not have enough tickets for the store purchase
        /// </summary>
        static void NotEnoughTickets()
        {
            Console.WriteLine();
            Console.WriteLine("Sorry, there were not enough tickets in the account.");
        }

        /// <summary>
        /// Game 1 - Magic Number
        /// </summary>
        static void MagicNumber()
        {
            //
            //Declare Variables
            //
            int ticketsWon = 0;
            int tolerance = 20;
            Random rnd = new Random();
            int magicNumber = rnd.Next(1, 100);
            int yourNumber;
            string userResponse;

            DisplayHeader("Magic Number");

            Console.WriteLine("You will choose a number and the computer will choose a number.");
            Console.WriteLine("If your number is within 20 of the computer's number, you win 20 tickets.");

            //
            //Validate user number bewteen 1 & 100
            //
            do
            {
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("Pick a Number (1-100)");
                    Console.Write(">");
                    Console.ForegroundColor = ConsoleColor.Green;
                    userResponse = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    if (!int.TryParse(userResponse, out yourNumber))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine();
                    }
                } while (!int.TryParse(userResponse, out yourNumber));

                if(yourNumber <= 0 || yourNumber > 100)
                {
                    Console.WriteLine();
                    Console.WriteLine("Number is too large or too small.");
                }

            } while (yourNumber <= 0 || yourNumber > 100);

            Console.Clear();

            //
            //Display Results
            //
            DisplayHeader("Magic Number");
            Console.WriteLine($"Magic number: {magicNumber}");
            Console.WriteLine();

            Console.Write($"Your number: ");
            Console.Write(yourNumber);
            Console.WriteLine();

            //
            //Award tickets
            //
            if (yourNumber > magicNumber && yourNumber <= magicNumber + tolerance)
            {
                ticketsWon = 20;
            }
            else if(yourNumber < magicNumber && yourNumber >= magicNumber - tolerance)
            {
                ticketsWon = 20;
            }
            else if(yourNumber == magicNumber)
            {
                ticketsWon = 50;
            }
            else
            {
                ticketsWon = 0;
            }

            Console.WriteLine();
            Console.Write($"You have won ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(ticketsWon);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" tickets!");
            Console.WriteLine();

            tickets += ticketsWon;
            coins = coins - 100;

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Game 2 - Slot Machine
        /// </summary>
        static void SlotMachine()
        {
            //
            //Array of Fruit
            //
            string[] Fruit = new string[10];
            Fruit[0] = "APPLE";
            Fruit[1] = "BANANA";
            Fruit[2] = "PEACH";
            Fruit[3] = "GRAPE";
            Fruit[4] = "ORANGE";
            Fruit[5] = "PEAR";
            Fruit[6] = "MANGO";
            Fruit[7] = "WATERMELON";
            Fruit[8] = "LIME";
            Fruit[9] = "LEMON";

            //
            //Array of the three chosen fruit
            //
            string fruit1 = "";
            string fruit2 = "";
            string fruit3 = "";
            string[] EachFruit = new string[3] { fruit1, fruit2, fruit3 };

            Random random = new Random();
            int ticketsWon;

            DisplayHeader("Slot Machine");

            Console.WriteLine("Three fruits will display on your screen, one per slot.");
            Console.WriteLine("If two of them are the same, you win 40 tickets. If all three are the same, you win 100 tickets.");

            DisplayContinuePrompt();

            //
            //Get three random fruit and add it to the selected fruit array
            //
            for (int index = 0; index < 3; index++)
            {
                int rnd = random.Next(0, 9);
                EachFruit[index] = Fruit[rnd];
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            //
            //Display each fruit in the selected fruit array
            //
            foreach (string fruit in EachFruit)
            {
                Console.Write(fruit.PadRight(20));
            }
            Console.ForegroundColor = ConsoleColor.White;

            //
            //Award tickets
            //
            if (EachFruit[0] == EachFruit[1] || EachFruit[0] == EachFruit[2] || EachFruit[1] == EachFruit[2])
            {
                ticketsWon = 40;
            }
            else if(EachFruit[0] == EachFruit[1] && EachFruit[0] == EachFruit[2])
            {
                ticketsWon = 100;
            }
            else
            {
                ticketsWon = 0;
            }

            tickets += ticketsWon;
            coins -= 100;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"You have won: {ticketsWon} tickets!");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Game 3 - Guessing Game
        /// </summary>
        static void GuessingGame()
        {
            //
            //Declare variables
            //
            int ticketsWon;
            int numberOfGuesses = 0;
            bool exiting = false;
            bool correctNumber = false;
            bool tooManyGuesses = false;
            int guessValue;
            string userResponse;
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 100);

            DisplayHeader("Guessing Game");

            Console.WriteLine("You have 7 tries to guess a number between 1 and 100.");
            Console.WriteLine("The amount of tickets you win depends on your number of guesses.\n");

            //
            //Run loop while:
            //1) The guessed number is not correct
            //2) The user is not out of guesses
            //
            while (exiting == false)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("Enter a number (1-100):");
                        Console.Write(">");
                        userResponse = Console.ReadLine();
                        if (!int.TryParse(userResponse, out guessValue))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid Entry.");
                            Console.WriteLine();
                        }
                    } while (!int.TryParse(userResponse, out guessValue));

                    //
                    //Feedack for the user
                    //
                    if (guessValue > randomNumber)
                    {
                        Console.WriteLine("Too high.\n");
                    }
                    else if (guessValue < randomNumber)
                    {
                        Console.WriteLine("Too low.\n");
                    }
                    else
                    {
                        Console.WriteLine("Correct!");
                        correctNumber = true;
                        exiting = true;
                    }

                    numberOfGuesses++;

                    //
                    //Execute if the user is out of guesses
                    //
                    if (numberOfGuesses >= 7 && guessValue != randomNumber)
                    {
                        Console.WriteLine();
                        Console.WriteLine("You are out of guesses.");
                        exiting = true;
                        tooManyGuesses = true;
                        numberOfGuesses++;
                    }
                }
                while (correctNumber == false && tooManyGuesses == false);
            }

            //
            //Award tickets
            //
            switch (numberOfGuesses)
            {
                case 1:
                    ticketsWon = 500;
                    break;
                case 2:
                    ticketsWon = 200;
                    break;
                case 3:
                    ticketsWon = 150;
                    break;
                case 4:
                    ticketsWon = 100;
                    break;
                case 5:
                    ticketsWon = 80;
                    break;
                case 6:
                    ticketsWon = 60;
                    break;
                case 7:
                    ticketsWon = 40;
                    break;
                default:
                    ticketsWon = 0;
                    break;     
            }

            Console.WriteLine();
            Console.WriteLine($"You have won {ticketsWon} tickets!");

            tickets += ticketsWon;
            coins -= 100;

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Game 4 - Double Tickets
        /// </summary>
        static void DoubleTickets()
        {
           //
           //List of flipped coins
           //
            List<int> Coins = new List<int>();

            //
            //Variables
            //
            Random rnd = new Random();
            int flipCoin;

            DisplayHeader("Double Tickets");

            Console.WriteLine("You will flip three coins, Heads = 0, Tails = 1.");
            Console.WriteLine("If all three coins land on the same side, your ticket total will double.\n");

            //
            //'Flip' a coin three times
            //
            for (int index = 0; index < 3; index++)
            {
                Console.WriteLine($"Flip Coin {index + 1}:");
                Console.ReadKey();

                flipCoin = rnd.Next(0, 2);
                Coins.Add(flipCoin);
                Console.WriteLine(flipCoin);
                Console.WriteLine();
            }

            //
            //Award Tickets
            //
            if(Coins[0] == Coins[1] && Coins[0] == Coins[2])
            {
                tickets = tickets * 2;
                Console.WriteLine("Congradulations! Your ticket count has doubled.");
            }
            else
            {
                Console.WriteLine("Sorry, your ticket count will not be doubled.");
            }

            coins -= 100;

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Opening Screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            string userName;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(" Welcome to the NMC Arcade.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("At this arcade, you can purchase coins to play our games.");
            Console.WriteLine("You can win tickets from the arcade games and redeem your tickets for extra credit points.");
            Console.WriteLine("Extra credit points can be applied to any class with a maximum of 10 per class per semester.");

            DisplayContinuePrompt();

            userName = getUserName();
        }
        
        /// <summary>
        /// Get User's NMC ID
        /// </summary>
        /// <returns></returns>
        static string getUserName()
        {
            string userName;

            DisplayHeader("Log In");

            Console.Write("Enter your username: ");
            userName = Console.ReadLine();

            return userName;
        }

        /// <summary>
        /// Closing Screen
        /// </summary>
        static void DisplayClosingScreen()
        {

            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("Thank you for using our arcade!");
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Pause / Continue Prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Display Header
        /// </summary>
        /// <param name="headerTitle"></param>
        static void DisplayHeader(string headerTitle)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(" " + headerTitle);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
