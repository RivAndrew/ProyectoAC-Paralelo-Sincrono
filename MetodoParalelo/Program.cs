using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using MetodoSincrono;

namespace MetodoParalelo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Estas son las rutas a dos carpetas que contienen 10,000 archivos de texto
            string ruta1 = @"C:\Users\DD\Documents\fotos1\";
            string ruta3 = @"C:\Users\DD\Documents\fotos3\";

            // usamos este string y utilizando el metodo de crear archivos, creamos 10,000 archivos
            // con la misma cadena de texto, solo para que se crean muchos datos
            string contenido = "usamos este string y utilizando el metodo de crear archivos, creamos 10,000 archivos\r\n";


            Console.WriteLine("Metodo Paralelo: ");
            // instanciamos el objeto donde se llevaran acabo las operaciones en paralelo
            MParalelo metodoP = new MParalelo(ruta1);
            // el metodo siguiente crea(si es que no existen) y modifica el texto de los archivos
            //metodoP.CreandoArchivos(contenido);
            // lo siguiente es la funcion principal en forma paralela
            double tiempoP = metodoP.Paralelo();
            // Este es el metodo que utilice para probar el parallel.for
            // que funciona mejor que lo que intente en la principal
            //metodoP.PruebaParalelo();

            Console.WriteLine("\nMetodo Sincrono: ");
            // instanciamos el objeto donde se llevaran acabo las operaciones en sincrono
            MSincrono metodoS = new MSincrono(ruta3);
            // el metodo siguiente crea(si es que no existen) y modifica el texto de los archivos
            //metodoS.CreandoArchivos(contenido);
            // lo siguiente es la funcion principal en forma sincrona
            double tiempoS = metodoS.Sincrono();
            // Este es el metodo que utilice para probar el for normal comparado con el parallel
            //metodoS.PruebaSincrono();

            // metodo que nos compara los dos tiempos y nos devulve quien es mas rapido
            metodoP.Comparar(tiempoP, tiempoS);

            Console.WriteLine("Se termino el programa.");
            Console.ReadKey();
        }


    }
}
