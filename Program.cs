namespace U2A3;

using System;
class Program
{
    static int tableWidth = 70;
    const  double EXCELENTE = .05;
    const  double BUENO = .10;
    const  double REGULAR = .15;
  
  static void Main() {
    
    //Constantes de interes por estado de credito 
    
    
    //Variables para datos de entrada
    double monto = 0;
    int plazo = 0;
    char estadoCredito = 'E';
    
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
            PrintTable(monto, plazo,estadoCredito);
        }
        else if (op == '3'){
            //Ejecuta metodo para calcular reservas
            Console.Clear();
            System.Console.WriteLine("Las reservas del credito son por: " + (calcReservas(monto)).ToString("C"));
            Console.ReadKey();
        }
        else if (op == '4'){
            //Ejecuta metodo para mostrar tasa de interes anual
            Console.Clear();
            Console.WriteLine("La tasa de interes anual es de " + 100*tasaInteres(estadoCredito) + "%");
            Console.ReadKey();
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
  
  static void entryData(out double monto, out int plazo, out char estadoCredito){
      Console.Write("Ingrese el monto del credito: ");
      monto = Convert.ToDouble(Console.ReadLine());
      Console.Write("Ingrese el plazo del credito (6, 12, 18, 24): ");
      plazo = Convert.ToInt32(Console.ReadLine());
      estadoCredito = Char.ToUpper(GetKeyPress("Estado crediticio ([E]xcelente, [B]ueno, [R]egular): ", new Char[] { 'E', 'B','R' } ));
  }
  
  static void PrintTable(double monto, int plazo, char estadoCredito){
    
    double interes = calcInteres(monto, plazo, tasaInteres(estadoCredito));
    double totalCredito = monto + interes;
    double saldo = totalCredito;
    
    PrintLine();
    PrintRow("Menusualidad", "Monto", "Capital","Interes","Saldo Final");
    for(int i = 1; i <= plazo;i++){
        saldo = saldo - (totalCredito / plazo);
        PrintLine();
        PrintRow(Convert.ToString(i),Convert.ToString((Math.Round(totalCredito / plazo,2))),Convert.ToString((monto/plazo).ToString("C")),Convert.ToString((interes/plazo).ToString("C")),Convert.ToString((saldo).ToString("C")));
    }
    PrintLine();
    Console.ReadLine();
  }
  
  static double calcInteres(double monto, int plazo, double interesAnual){
      double montoInteres = monto*(interesAnual/12)*plazo;
      return montoInteres;
  }
  
  static double tasaInteres(char estadoCredito){
      if(estadoCredito.Equals('E')){
          return EXCELENTE;
      }
      else if (estadoCredito.Equals('B')){
          return BUENO;
      }
      else{
          return REGULAR;
      }
  }

  static double calcReservas(double monto){
    double reservas = 0;
    double probIncumplimiento = .0346;
    if(monto < 100000){
        reservas = probIncumplimiento*.3*(monto/2);
    }
    else{
        reservas = probIncumplimiento*.45*(monto/2);
    }

    return reservas;
  }
}
