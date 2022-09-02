using System;

namespace Buscaminas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("... Bienvenido a Buscaminas en C# ...");
          int filas = 10;
          int columnas = 10;
          int minas = crearMinas();            

            string[,] tablero = new string[filas, columnas];
            string[,] tableroJuego = new string[filas, columnas];

            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    tablero[i, j] = " - ";
                }
            }

            for (int i = 0; i < tableroJuego.GetLength(0); i++)
            {
                for (int j = 0; j < tableroJuego.GetLength(1); j++)
                {
                    tableroJuego[i, j] = " - ";
                }
            }


            AgregarMinas(tablero,filas,columnas,minas);
            Jugar(tablero,tableroJuego, minas);
            //mostrarTablero(tableroJuego);
        }
        public static void Jugar(string[,] matriz, string[,] tableroJugador, int minasTotales)
        {
            int filaDigitada = 0;
            int columnaDigitada = 0;
            string validacion = "";
            
            int minasDesactivadas = 0;
            bool life = true;
            Console.WriteLine("Las minas han sido ubicadas con exito");
            Console.WriteLine("Para poder jugar debe digitar la fila y la columna donde desea mirar si existen o no minas,");
            
            while (life || (minasDesactivadas < minasTotales))
            {
                Console.WriteLine("Digite la Fila ");
                filaDigitada = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite la Columna ");
                columnaDigitada = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite la S para Desactivar o N para destapar");
                validacion = Console.ReadLine();
                tableroJugador[filaDigitada, columnaDigitada] = matriz[filaDigitada, columnaDigitada];
                if (validacion == "S")
                {
                    if (matriz[filaDigitada, columnaDigitada] == " X ")
                    {
                        minasDesactivadas++;
                        Console.WriteLine("Felicitaciones desactivo una mina, quedan " + (minasTotales- minasDesactivadas).ToString());                        
                    }
                    tableroJugador[filaDigitada, columnaDigitada] = " # ";
                }
                else if(matriz[filaDigitada, columnaDigitada] == " - ")
                {
                    tableroJugador[filaDigitada, columnaDigitada] = " O ";
                }else if (matriz[filaDigitada, columnaDigitada] == " X ")
                {
                    life = false;
                    mostrarTablero(matriz);
                }
                Console.WriteLine("Minas totales : " + (minasTotales - minasDesactivadas).ToString()  + "| Minas Desactivadas: " + minasDesactivadas.ToString());
                mostrarTablero(tableroJugador);
            }
        }

        public static int crearMinas()
        {
            int minas2 = 0;
            Console.WriteLine("Digite la cantidad de minas");
            minas2 = int.Parse(Console.ReadLine());
            Console.WriteLine("sus minas: " + minas2);
            return minas2;
        }
        public static void AgregarMinas(string[,] matriz,int filas,int columnas, int minas)
        {
            Random rnd = new Random();
            int agregarMinas = 0;
            while (agregarMinas < minas)
            {
                int randomFila = rnd.Next(filas);
                int randomColumna = rnd.Next(filas);
                if (matriz[randomFila, randomColumna] != " X ")
                {
                    matriz[randomFila, randomColumna] = " X ";
                    if ((randomFila == 0 && randomColumna == 0) || (randomFila == 0 && randomColumna == 9) || (randomFila == 9 && randomColumna == 9) || (randomFila == 0 && randomColumna == 0) || (randomFila == 9 && randomColumna == 0))
                    {
                        validarMinasEsquinas(matriz, randomFila, randomColumna);
                    }
                    else
                    {
                        validarMinaCercana(matriz, randomFila, randomColumna);
                    }
                    
                    agregarMinas++;
                }                
            }

        }

        public static void mostrarTablero(string[,] matriz)
        {
            Console.WriteLine(" Tablero : ");
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }  
        public static Boolean validarMinasEsquinas(string[,] matriz,int i2, int j2)
        {
            if (i2 == 0 && j2 == 0)
            {
                matriz[i2 + 1, j2] = " 1 ";
                matriz[i2, j2 + 1] = " 1 ";        
                matriz[i2 + 1, j2 + 1] = " 1 ";
           
            }
            if (i2 == 0 && j2 == 9)
            {
                matriz[i2 + 1, j2 ] = " 1 ";
                matriz[i2, j2 - 1] = " 1 ";
                matriz[i2 + 1, j2 - 1 ] = " 1 ";
            }
            if (i2 == 9 && j2 == 0)
            {
                matriz[i2 , j2 + 1] = " 1 ";
                matriz[i2 - 1, j2 + 1] = " 1 ";
                matriz[i2 - 1, j2 ] = " 1 ";
            }
            if (i2 == 9 && j2 == 9)
            {
                matriz[i2, j2 - 1] = " 1 ";
                matriz[i2 - 1, j2 - 1] = " 1 ";
                matriz[i2 - 1, j2] = " 1 ";
            }
            bool validacion = true;
            if ( matriz[0, j2] == " X " ) 
            {
                return validacion;
            }
            else
            {
                return !validacion;
            }
          
        }
        public static void sumarMinas(string[,] matriz, int i2, int j2)
        {
            //mostrarTablero(matriz);
            if (matriz[i2, j2] == " - ")
            {
                matriz[i2, j2] = " 1 ";
            }
            else if(matriz[i2,j2] == " 1 ")
            {
                matriz[i2, j2] = " 2 ";
            }else if (matriz[i2, j2] == " 2 ")
            {
                matriz[i2, j2] = " 3 ";
            }
            else if (matriz[i2, j2] == " 3 ")
            {
                matriz[i2, j2] = " 4 ";
            }
            else if (matriz[i2, j2] == " 4 ")
            {
                matriz[i2, j2] = " 5 ";
            }
            else if (matriz[i2, j2] == " 6 ")
            {
                matriz[i2, j2] = " 6 ";
            }
        }

        public static void validarMinaCercana(string[,] matriz, int i2, int j2)
        {
            int mi2 = 0;
            int mj2 = 0;

            if (i2 >= 0 && i2 < 9)
            {
                mi2 = i2 + 1;
                sumarMinas(matriz, mi2, j2);
            }
            if ((i2 < 9 && j2 < 9))//Diagonal derecha baja
            {
                mi2 = i2 + 1;
                mj2 = j2 + 1;
                sumarMinas(matriz, mi2, mj2);
            }
            if (  (i2 <= 9 && j2 < 9) )//Agregar un AND para valdiar la ultima fila
            {
                mi2 = i2;
                mj2 = j2 + 1;
                sumarMinas(matriz, mi2, mj2);
            }
            if ((i2 <= 9 && j2 >= 0) && (i2 > 0) && (j2 < 9))//Diagonal derecha alta
            {
                mi2 = i2 - 1;
                mj2 = j2 + 1;
                sumarMinas(matriz, mi2, mj2);
            }
            if (i2 > 0 && i2 <= 9)
            {
                mi2 = i2 - 1;
                sumarMinas(matriz, mi2, j2);
            }
            if ((j2 <= 9 && j2 > 0) && (i2 > 0))//Diagonal izq alta
            {
                mi2 = i2 - 1;
                mj2 = j2 - 1;
                sumarMinas(matriz, mi2, mj2);
            }
            if ((j2 <= 9 && j2 > 0) )
            {
                mi2 = i2  ;
                mj2 = j2 - 1;
                sumarMinas(matriz, mi2, mj2);
            }
            if ((j2 > 0 && j2 <= 9) && (i2 < 9))//Agregar un AND para valdiar la ultima fila
            {
                mi2 = i2 + 1 ;
                mj2 = j2 - 1;
                sumarMinas(matriz, mi2, mj2);
            }
        }       
    }

}
