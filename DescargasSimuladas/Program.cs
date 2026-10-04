using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace DescargasSimuladas;

class Program
{
    static async Task Main()
    {
        string[] archivos =
        {
            "documento.pdf",
            "fotografia.jpg",
            "video.mp4",
            "presentacion.pptx",
            "datos.csv",
            "musica.mp3",
            "backup.zip",
        };
        int[] tiempos =
        {
            2000,
            4000,
            3000,
            5000,
            2500,
            3500,
            6000
        };
        Descargador descargador = new();
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine(" DESCARGAS SIMULADAS");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("1. Descargas secuenciales");
            Console.WriteLine("2. Descargas concurrentes");
            Console.WriteLine("3. Descargas concurrentes con cancelación");
            Console.WriteLine("4. Salir");
            Console.WriteLine("5. Descargar todos los archivos y mostrar un resumen");
            Console.WriteLine();
            Console.Write("Selecciona una opción: ");
            string? opcion = Console.ReadLine();
            Console.WriteLine();
            switch (opcion)
            {
                case "1":
                    await EjecutarSecuencialAsync(

                    descargador,
                    archivos,
                    tiempos

                    );
                    break;
                case "2":
                    await EjecutarConcurrenteAsync(
                    descargador,
                    archivos,
                    tiempos

                    );
                    break;
                case "3":
                    await EjecutarConCancelacionAsync(
                    descargador,
                    archivos,
                    tiempos

                    );
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");

                    break;
                case "5":
                    await EjecutarResumenAsync(descargador, archivos, tiempos);
                    break;
            }
            if (!salir)
            {
                Console.WriteLine();
                Console.WriteLine(
                "Pulsa ENTER para volver al menú."
                );
                Console.ReadLine();
            }
        }
    }
    static async Task EjecutarSecuencialAsync(
    Descargador descargador,
    string[] archivos,
    int[] tiempos)
    {
        Console.WriteLine("========================================");
        Console.WriteLine(" DESCARGAS SECUENCIALES");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Stopwatch cronometro = Stopwatch.StartNew();
        for (int i = 0; i < archivos.Length; i++)
        {
            await descargador.DescargarAsync(
            archivos[i],
            tiempos[i],
            CancellationToken.None
            );
        }
        cronometro.Stop();

        Console.WriteLine();
        Console.WriteLine(
        $"Tiempo total: {cronometro.Elapsed.TotalSeconds:F2}segundos"
        );
    }
    static async Task EjecutarConcurrenteAsync(
    Descargador descargador,
    string[] archivos,
    int[] tiempos)
    {
        Console.WriteLine("========================================");
        Console.WriteLine(" DESCARGAS CONCURRENTES");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Stopwatch cronometro = Stopwatch.StartNew();
        Task<bool>[] tareas =
        new Task<bool>[archivos.Length];
        for (int i = 0; i < archivos.Length; i++)
        {
            tareas[i] = descargador.DescargarAsync(
            archivos[i],
            tiempos[i],
            CancellationToken.None
            );
        }
        bool[] resultados = await Task.WhenAll(tareas);
        cronometro.Stop();
        int completadas = 0;
        foreach (bool resultado in resultados)
        {
            if (resultado)
            {
                completadas++;
            }
        }
        Console.WriteLine();
        Console.WriteLine(
        $"Descargas completadas: {completadas}/{archivos.Length}"
        );
        Console.WriteLine(
        $"Tiempo total: {cronometro.Elapsed.TotalSeconds:F2}segundos"
        );
    }
    static async Task EjecutarConCancelacionAsync(
    Descargador descargador,
    string[] archivos,
    int[] tiempos)
    {
        Console.WriteLine("========================================");
        Console.WriteLine(" DESCARGAS CON CANCELACIÓN");
        Console.WriteLine("========================================");
        Console.WriteLine();

        Console.WriteLine(
        "Las descargas comenzarán simultáneamente."
        );
        Console.WriteLine(
        "La cancelación se solicitará después de 3 segundos."
        );
        Console.WriteLine();
        using CancellationTokenSource source = new();
        CancellationToken token = source.Token;
        Stopwatch cronometro = Stopwatch.StartNew();
        Task<bool>[] tareas =
        new Task<bool>[archivos.Length];
        for (int i = 0; i < archivos.Length; i++)
        {
            tareas[i] = descargador.DescargarAsync(
            archivos[i],
            tiempos[i],
            token
            );
        }
        await Task.Delay(3000);
        Console.WriteLine();
        Console.WriteLine(
        ">>> SOLICITANDO CANCELACIÓN..."
        );
        source.Cancel();
        bool[] resultados = await Task.WhenAll(tareas);
        cronometro.Stop();
        int completadas = 0;
        int canceladas = 0;
        foreach (bool resultado in resultados)
        {
            if (resultado)
            {
                completadas++;
            }
            else
            {
                canceladas++;
            }
        }
        Console.WriteLine();
        Console.WriteLine(
        $"Descargas completadas: {completadas}"
        );
        Console.WriteLine(
        $"Descargas canceladas: {canceladas}"
        );

        Console.WriteLine(
        $"Tiempo total: {cronometro.Elapsed.TotalSeconds:F2}segundos"
        );
    }

    static async Task EjecutarResumenAsync(Descargador descargador,string[] archivos,int[] tiempos)
    {
        Console.WriteLine("========================================");
        Console.WriteLine(" RESUMEN");
        Console.WriteLine("========================================");
        Console.WriteLine();
        Stopwatch cronometro = Stopwatch.StartNew();
        Task<bool>[] tareas =
        new Task<bool>[archivos.Length];
        for (int i = 0; i < archivos.Length; i++)
        {
            tareas[i] = descargador.DescargarAsync(
            archivos[i],
            tiempos[i],
            CancellationToken.None
            );
        }
        bool[] resultados = await Task.WhenAll(tareas);
        cronometro.Stop();
        int completadas = 0;
        int canceladas = 0;
        foreach (bool resultado in resultados)
        {
            if (resultado)
            {
                completadas++;
            }
            else
            {
                canceladas++;
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Total de archivos: {archivos.Length}");
        Console.WriteLine($"Completados: {completadas}");
        Console.WriteLine($"Cancelados: {canceladas}");
        Console.WriteLine($"Tiempo total: {cronometro.Elapsed.TotalSeconds:F2}segundos");
    }
}
