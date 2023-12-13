using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace MetodoParalelo
{
    internal class MParalelo
    {
        // propiedad ruta
        public string Ruta { get; set; }

        // Constructor
        public MParalelo(string ruta)
        {
            Ruta = ruta;
        }

        // funcion principal para experimentar con parallel foreach
        public double Paralelo()
        {
            // Creamos un arreglo de archivos usando la ruta que recibiremos
            string[] archivos = Directory.GetFiles(Ruta);
            // Como queremos medir el tiempo de inicio, lo obtenemso con datetime
            Console.WriteLine($"Tiempo de Inicio: {DateTime.Now:HH:mm:ss:fff}");
            // para medir el tiempo de ejecucion, usamos stopwatch y lo iniciamos
            Stopwatch tiempoS = Stopwatch.StartNew();
            //aqui aplicaremos el objetivo del proyecto usar parallel
            Parallel.ForEach(archivos, archivo =>
            {
                //como simplemente quiero observar como se comporta el parallel, lo que haremos es
                // usar readalltext, ya que esta funcion abre cada archivo que le demos y lo lee
                // y guarda en una variable, lo que toma tiempo y esfuerzo del procesador
                // pero como no usamos el texto guardado, siempre se resetea
                string texto = File.ReadAllText(archivo);

            });
            // detenemos el stopwatch
            tiempoS.Stop();
            Console.WriteLine($"Tiempo que se tardo: {tiempoS.ElapsedMilliseconds}");
            Console.WriteLine($"Tiempo Final: {DateTime.Now:HH:mm:ss:fff}");

            //devolvemos el tiempo de ejecucion para comparar
            return tiempoS.ElapsedMilliseconds;
        }

        // metodo para comparar los tiempos de ejecucion, utilizando unos if para comparar
        public void Comparar(double tiempoP, double tiempoS)
        {
            if (tiempoP > tiempoS)
            {
                Console.WriteLine("El método sincrono es más rapido");
            }
            else if (tiempoS > tiempoP)
            {
                Console.WriteLine("El método paralelo es más rapido");
            }
        }

        // como queria saber como utilizar parallel, me encontre este metodo para comprobar
        // ya que este metodo es un calculo en el que el parallel destaca mas
        static long Prueba()
        {
            long total = 0;
            for (int i = 1; i < 50000000; i++)
            {
                total += i;
            }
            return total;
        }

        // un metodo que previamente crei que funcionaria como ejemplo, pero creo que estaba equivocado
        // actualmente se utilza simplemente para crear y modificar cierta cantidad de txt, para
        // comporbar si funciona correctamente la funcion principal
        public void CreandoArchivos(string contenido)
        {
            Parallel.For(0, 10000, i =>
            {
                File.WriteAllText($@"{Ruta}Prueba" + i + ".txt", $"{i} {contenido} ");
            });
        }

        // metodo breve y simple para combrobar el rendimiento en procesos largos utilizando parallel
        public void PruebaParalelo()
        {
            Console.WriteLine($"Tiempo de Inicio: {DateTime.Now:HH:mm:ss:fff}");
            Stopwatch tiempoS = Stopwatch.StartNew();
            Parallel.For(0, 10, i =>
            {
                long total = Prueba();
                Console.WriteLine("{0} - {1}", i, total);
            });
            tiempoS.Stop();
            Console.WriteLine($"Tiempo que se tardo: {tiempoS.ElapsedMilliseconds}");
            Console.WriteLine($"Tiempo Final: {DateTime.Now:HH:mm:ss:fff}");
        }
    }
}
