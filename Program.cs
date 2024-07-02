using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main(string[] args)
    {
        int option = 0;

        while(true)
        {
            menuOpciones();
            string optionStr = Console.ReadLine();
            // Valida que la opcion sea entero y una opcion correcta
            if (!(int.TryParse(optionStr, out option) && (option == 1 || option == 2 || option == 3)))
            {
                Console.WriteLine("No ingreso una opción permitida. Inténtelo nuevamente.");
                continue;  
            }             
            Console.Clear();
            Console.WriteLine("Ingrese los 16 dígitos de su tarjeta de credito: ");
            string cardNumber = Console.ReadLine();
            // Valida que se ingresen los 16 digitos de la tarjeta
            if (!validLengthCard(cardNumber))
            {
                Console.WriteLine("No ingreso los 16 dígitos de su tarjeta de credito. Inténtelo nuevamente más tarde.");
                return;
            }
            // Extrae primeros 4 caracteres de la tarjeta, el identificador       
            string idCompany = cardNumber.Substring(0, 4);
            // Valida que la opcion y el id de la tarjeta sean correctas
            if (!validCard(option,idCompany))
            {
                Console.WriteLine("La opción ingresada no es válida. Inténtelo nuevamente más tarde.");
                return;
            }
            // Extrae ultimos 4 caracteres de la tarjeta ingresada
            string lastNumbers = cardNumber.Substring(cardNumber.Length -4);
            switch (option)
            {
                case 1:
                    Console.WriteLine($"Movimientos de su cuenta Visa terminada en ..{lastNumbers}");
                    float[] ArrayTransactions = loadTransactionsVisa(idCompany);
                    int numberT = 0;
                    while(numberT < 5)
                    {
                        Console.WriteLine($"Transacción N°{numberT+1} - Monto ${ArrayTransactions[numberT]}");
                        numberT ++;
                    }
                    return;
                case 2:
                    Console.WriteLine($"Movimientos de su cuenta Mastercard terminada en ..{lastNumbers}");
                    List<float> listTransactions = loadTransactionsMarterCard(idCompany);
                    numberT = 0;
                    do
                    {
                        Console.WriteLine($"Transacción N°{numberT+1} - Monto ${listTransactions[numberT]}");
                        numberT ++;
                    }
                    while(numberT < 5);
                    return;
                case 3:
                    Console.WriteLine($"Movimientos de su cuenta Diners Club terminada en ..{lastNumbers}");
                    Dictionary<int, float> dictTransactions = loadTransactionsDiners(idCompany);
                    foreach (var item in dictTransactions)
                    {
                        Console.WriteLine($"Transacción N°{item.Key} - Monto ${item.Value}");
                    }
                    return;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Menu de opciones
    /// </summary>
    public static void menuOpciones()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               BIENVENIDO A RED MAJESTIC                ║");
        Console.WriteLine("║                                                        ║");
        Console.WriteLine("║        PARA OPERAR CON VISA          Ingrese 1         ║");
        Console.WriteLine("║        PARA OPERAR CON MASTERCARD    Ingrese 2         ║");
        Console.WriteLine("║        PARA OPERAR CON DINERS CLUB   Ingrese 3         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.Write("Ingrese su opción: ");
    }

    /// <returns>
    /// Devuelve un vector del tipo float que contiene los valores de las
    /// transacciones y que recibe como parámetro el identificador de la tarjeta
    /// </returns>
    public static float GenerateRandomTransaction()
    {
        Random rand = new Random();
        return (float)rand.NextDouble() * 1000;
    }

    /// <summary>
    /// Valida la tarjeta de crédito
    /// </summary>
    /// <returns>Un valor booleano true indicando si la validación se realizó correctamente</returns>
    public static bool validCard(int option, string idCompany)
    {
        bool flag = false;
        string idVisa = "4407";
        string idMaster = "3890";
        string idDiners = "7401";

        switch (option)
            {
                case 1:
                    if (idCompany == idVisa)
                        flag = true;
                    break;
                case 2:
                    if (idCompany == idMaster)
                        flag = true;
                    break;
                case 3:
                    if (idCompany == idDiners)
                        flag = true;
                    break;
                default:
                    break;
            }
        return flag; 
    }
    
    /// <summary>
    /// Valida que la tarjeta de crédito tenga 16 caracteres
    /// </summary>
    /// <returns>Un valor booleano true indicando si la validación se realizó correctamente</returns>
    public static bool validLengthCard(string cardNumber)
    {
        bool flag = false;

        if( cardNumber.Length == 16)
            flag = true;
        return flag;
    }

    /// <summary>
    /// Carga las transacciones de Visa y valida el id de la tarjeta
    /// </summary>
    /// <returns>Vector de transacciones</returns>
    public static float[] loadTransactionsVisa(string idCompany)
    {
        if(!validCard(1,idCompany))
        {
            return default;
        }
        float[] arrayTransactions = new float[5];  
        for (int i = 0; i < 5 ; i++)
        {
            arrayTransactions[i] = GenerateRandomTransaction();
        }
        return arrayTransactions;
    }

    /// <summary>
    /// Carga las transacciones de Mastercard y valida el id de la tarjeta
    /// </summary>
    /// <returns>Lista de transacciones</returns>
    public static List<float> loadTransactionsMarterCard(String idCompany)
    {
        if(!validCard(2,idCompany))
        {
            return default;
        }
        List<float> listTransactions = new List<float>();       
        for (int i = 0; i < 5 ; i++)
        {
            listTransactions.Add(GenerateRandomTransaction());
        }
        return listTransactions;
    }

    /// <summary>
    /// Carga las transacciones de Diners Club y valida el id de la tarjeta
    /// </summary>
    /// <returns>Diccionario de transacciones</returns>
    public static Dictionary<int,float> loadTransactionsDiners(string idCompany)
    {
        if(!validCard(3,idCompany))
        {
            return default;
        }
        Dictionary<int, float> dictTransactions= new Dictionary<int, float>();      
        for (int i = 1; i < 6 ; i++)
        {
            dictTransactions.Add(i,GenerateRandomTransaction());
        }
        return dictTransactions;
    }
}
