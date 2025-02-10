

namespace Banking_Application;
public class Menu 
{
    private readonly IBankManager _bankManager;

    public Menu()
    {
        _bankManager = new BankManager();
    }
    public void BankMenu()
    {
        bool running = true;
      while (running)
        {

            PrintMenu();
            string choice = Console.ReadLine()!; 

            switch (choice)
            {
                case "1":
                    _bankManager.Login();
                    break;
                case "2":
                    _bankManager.SignIn();
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Exiting Application....");
                    running = false;
                    Console.ResetColor();
                break;    
                
            }
        }
    }


    private static void PrintMenu()
    {
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=======Welcome To ALIM Banking System=======");
        
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Enter 1 to  Login to an Existing account");
        Console.WriteLine("Enter 2 to sign in a new account");
        Console.WriteLine("Enter 3 to Exit");
        Console.ResetColor();
    }
}