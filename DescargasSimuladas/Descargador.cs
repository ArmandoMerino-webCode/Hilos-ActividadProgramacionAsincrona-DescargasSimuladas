using System;
using System.Threading;
using System.Threading.Tasks;
namespace DescargasSimuladas;

public class Descargador
{
    public async Task<bool> DescargarAsync(
    string nombreArchivo,
    int tiempoDescarga,
    CancellationToken token)
    {
        Console.WriteLine(
        $"Iniciando descarga: {nombreArchivo}"
        );
        try
        {
            await Task.Delay(tiempoDescarga, token);
            Console.WriteLine(
            $"Descarga terminada: {nombreArchivo}"
            );
            return true;

        }
        catch (OperationCanceledException)
        {
            Console.WriteLine(
            $"Descarga cancelada: {nombreArchivo}"
            );
            return false;
        }
    }
}