using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MetodoSincrono
{
    public class MSincrono
    {
        public string Ruta { get; set; }

        public MSincrono(string ruta)
        {
            Ruta = ruta;
        }

        // los comentarios aqui serian redundantes ya que es muy parecido a la forma en parallel
        // solo que aqui utilizamos un simple foreach.
        public double Sincrono()
        {
            string[] archivos = Directory.GetFiles(Ruta);
            Console.WriteLine($"Tiempo de Inicio: {DateTime.Now:HH:mm:ss:fff}");
            Stopwatch tiempoS = Stopwatch.StartNew();
            foreach (string archivo in archivos)
            {
                string texto = File.ReadAllText(archivo);

            }
            tiempoS.Stop();
            Console.WriteLine($"Tiempo que se tardo:  {tiempoS.ElapsedMilliseconds}");
            Console.WriteLine($"Tiempo Final: {DateTime.Now:HH:mm:ss:fff}");

            return tiempoS.ElapsedMilliseconds;
        }

        static long Prueba()
        {
            long total = 0;
            for (int i = 1; i < 50000000; i++)
            {
                total += i;
            }
            return total;
        }

        // como solo queria crear o modificar archivos no tome en cuanto en hacerlo de forma sincrona
        public void CreandoArchivos(string contenido)
        {
            Parallel.For(0, 10000, i =>
            {
                File.WriteAllText($@"{Ruta}Prueba" + i + ".txt", $"{i} {contenido} ");
            });
        }

        // lo mismo que en parallel, solo que aqui utilizamos un simple for para comparar con el parallel
        public void PruebaSincrono()
        {
            Console.WriteLine($"Tiempo de Inicio: {DateTime.Now:HH:mm:ss:fff}");
            Stopwatch tiempoS = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                long total = Prueba();
                Console.WriteLine("{0} - {1}", i, total);
            }
            tiempoS.Stop();
            Console.WriteLine($"Tiempo que se tardo: {tiempoS.ElapsedMilliseconds}");
            Console.WriteLine($"Tiempo Final: {DateTime.Now:HH:mm:ss:fff}");
        }
    }
}
