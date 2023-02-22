namespace U2A3;

using System;
class Program
{
    static int tableWidth = 70;
  
  static void Main() {
    
    //Constantes de interes por estado de credito 
    const  double EXCELENTE = .05;
    const  double BUENO = .10;
    const  double REGULAR = .15;
    
    //Variables para datos de entrada
    double monto;
    int plazo;
    string estadoCredito;
    
    bool continuar = true;
    
    do {
        Console.Clear();
        Console.WriteLine("Bienvenido a Empeñanos tu alma!");
        Console.WriteLine("1.- Ingresar datos de credito");
        Console.WriteLine("2.- Calcular esquema de pago");
        Console.WriteLine("3.- Calcular reservas crediticias");
        Console.WriteLine("4.- Tasa de interes anual");
        Console.WriteLine("0.- Salir");
        char op = Char.ToUpper(GetKeyPress("Eliga una opcion: ", new Char[] { '1', '2', '3', '4', '0' } ));
        if (op == '0'){
            continuar = false;
        }
        else if (op == '1'){
            //Ejecuta metodo para recolectar datos de entrada
            Console.Clear();
            entryData(out monto, out plazo, out estadoCredito);
        }
        else if (op == '2'){
            //Ejecuta metodo para mostrar el esquema de pago
            PrintTable();
        }
        else if (op == '3'){
            //Ejecuta metodo para calcular reservas
        }
        else if (op == '4'){
            //Ejecuta metodo para mostrar tasa de interes anual
        }
    }while(continuar);

  }
  
  private static Char GetKeyPress(String msg, Char[] validChars){
      ConsoleKeyInfo keyPressed;
      bool valid = false;

      Console.WriteLine();
      do {
         Console.Write(msg);
         keyPressed = Console.ReadKey();
         Console.WriteLine();
         if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
            valid = true;
      } while (! valid);
      return keyPressed.KeyChar;
  }
   
  static void PrintLine(){
    Console.WriteLine(new string('-', tableWidth));
  }

  static void PrintRow(params string[] columns){
    int width = (tableWidth - columns.Length) / columns.Length;
    string row = "|";

    foreach (string column in columns){
        row += AlignCentre(column, width) + "|";
    }

    Console.WriteLine(row);
  }

static string AlignCentre(string text, int width)
{
    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

    if (string.IsNullOrEmpty(text))
    {
        return new string(' ', width);
    }
    else
    {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}
  
  static void entryData(out double monto, out int plazo, out string estadoCredito){
      Console.Write("Ingrese el monto del credito: ");
      monto = Convert.ToDouble(Console.ReadLine());
      Console.Write("Ingrese el plazo del credito (6, 12, 18, 24): ");
      plazo = Convert.ToInt32(Console.ReadLine());
      Console.Write("Estado crediticio ([E]xcelente, [B]ueno, [R]egular): ");
      estadoCredito = Console.ReadLine()!;
  }
  
  static void PrintTable(){
    PrintLine();
    PrintRow("Menusualidad", "Monto", "Capital","Interes","Saldo Final");
    for(int i = 1; i <= 12;i++){
        PrintLine();
        PrintRow("","","","","");
    }
    PrintLine();
    Console.ReadLine();
  }
}
