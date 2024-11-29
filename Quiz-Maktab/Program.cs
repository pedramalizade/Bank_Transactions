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
if(result != "Check Successful.")
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
            void TransferMoney(TransactionService transactionService, CardRepository cardService , string sourceCardNumber)
            {
                Console.Write("Enter the destination card numnber: ");
                string destinationCardNmber = Console.ReadLine();
                Console.Write("enter the amount to transfer: ");
                if(float.TryParse(Console.ReadLine(), out float amount) && amount > 0)
                {
                    var sourceCard = cardService.GetCardByNumber(sourceCardNumber);
                }

            }
            break;
        case "2":
            void viewTransavtion(TransactionService transactionService, string cardNumber)
            {
                var transactions = transactionService.GetTransactions(cardNumber);
                if(transactions.Count > 0)
                {
                    Console.WriteLine("Your transaction: ");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"Date : {transaction.TranceactionTime}, Amount : {transaction.Amount}, Success: {transaction.IsSuccessful}");
                    }
                }
                else
                {
                    Console.WriteLine("No transaction found.");
                }
                void checkBalance(CardService cardService, string cardNumber)
                {
                    var card = cardService.GetCardByNumber(cardNumber);
                    Console.WriteLine($"your current balance is {card.Balance}");
                }
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