using Microsoft.IdentityModel.Tokens;
using Quiz_Maktab.Interface.Repository;
using Quiz_Maktab.Interface.Service;
using Quiz_Maktab.Repository;
using Quiz_Maktab.Service;
using System.Transactions;

//ICardRepository cardRepository = new CardRepository();
ICardService cardService = new CardService();
ITransactionService transactionService = new TransactionService();

Console.WriteLine("Enter Your Card Number: ");
string cardNumber = Console.ReadLine();

Console.WriteLine("Enter Your Password: ");
string password = Console.ReadLine();

string result = cardService.CheckCard(cardNumber, password);
if (result != "Check Successful.")
{
    Console.WriteLine(result);
    return;
}

while (true)
{
    Console.WriteLine("1: transaction");
    Console.WriteLine("2: view Transaction");
    Console.WriteLine("3: Exit");
    Console.Write("Please Choice an Option: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.Write("Enter Amount: ");
            var amount = float.Parse(Console.ReadLine());
            Console.Write("Enter the Destination Card Number: ");
            var destinationCardNumber = Console.ReadLine();
            var mmd = transactionService.Transfer(cardNumber, destinationCardNumber, amount);
            Console.WriteLine(mmd);

            break;
        case "2":
            Console.Clear();
            var transactionList = transactionService.GetTransactions();
            foreach (var transaction in transactionService.GetTransactions())
            {
                Console.WriteLine($"Date : {transaction.TranceactionTime}, Amount : {transaction.Amount}, Success: {transaction.IsSuccessful}");
            }
            break;
        case "3":
            Console.WriteLine("thanks");
            return;
        default:
            Console.WriteLine("Wrong option.");
            break;
    }

}
