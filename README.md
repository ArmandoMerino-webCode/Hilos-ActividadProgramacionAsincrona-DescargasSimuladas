# Descargas Simuladas — Programación Asíncrona en C#

Aplicación de consola desarrollada en **C# y .NET 10** para practicar programación asíncrona, concurrencia y cancelación de tareas.

El proyecto simula la descarga de varios archivos utilizando `Task.Delay()` y permite comparar la ejecución secuencial con la ejecución concurrente mediante `Task.WhenAll()`.

## 🚀 Funcionalidades

La aplicación incluye las siguientes opciones:

1. **Descargas secuenciales**

   * Cada descarga comienza después de finalizar la anterior.
   * Permite observar el tiempo total acumulado de todas las operaciones.

2. **Descargas concurrentes**

   * Todas las descargas se inician de forma concurrente.
   * Utiliza `Task.WhenAll()` para esperar la finalización de todas las tareas.
   * Permite comprobar la diferencia de rendimiento respecto a la ejecución secuencial.

3. **Descargas concurrentes con cancelación**

   * Las descargas se ejecutan de forma concurrente.
   * Se utiliza `CancellationTokenSource` para solicitar la cancelación.
   * Cada tarea recibe un `CancellationToken`.
   * Las operaciones canceladas se controlan mediante `OperationCanceledException`.

4. **Resumen de descargas**

   * Muestra el número total de archivos.
   * Muestra las descargas completadas.
   * Muestra las descargas canceladas.
   * Muestra el tiempo total de ejecución.

## 🧠 Conceptos practicados

* `async` / `await`
* `Task`
* `Task<bool>`
* `Task.Delay()`
* `Task.WhenAll()`
* `CancellationTokenSource`
* `CancellationToken`
* `OperationCanceledException`
* Programación concurrente
* Programación asíncrona
* Cancelación cooperativa
* Medición del tiempo de ejecución con `Stopwatch`

## 📁 Estructura del proyecto

```text
DescargasSimuladas/
│
├── Program.cs
├── Descargador.cs
└── DescargasSimuladas.csproj
```

## 🛠️ Tecnologías

* **C#**
* **.NET 10**
* **Visual Studio**

## ▶️ Ejecución

Clonar el repositorio y ejecutar el proyecto:

```bash
git clone https://github.com/ArmandoMerino-webCode/Hilos-ActividadProgramacionAsincrona-DescargasSimuladas.git
cd Hilos-ActividadProgramacionAsincrona-DescargasSimuladas
dotnet run
```

También puede ejecutarse directamente desde Visual Studio mediante `Ctrl + F5`.

## 📊 Comparación

Con los tiempos de descarga utilizados en el ejercicio:

| Ejecución                   | Tiempo aproximado |
| --------------------------- | ----------------: |
| Secuencial                  |            26,0 s |
| Concurrente                 |             6,0 s |
| Concurrente con cancelación |            ~3,0 s |

La ejecución concurrente permite que las operaciones independientes progresen simultáneamente, reduciendo el tiempo total respecto a la ejecución secuencial.

## 🎯 Objetivo

El objetivo del proyecto es comprender cómo funcionan las operaciones asíncronas y concurrentes en C#, así como el uso de mecanismos de cancelación mediante `CancellationToken`.

Este ejercicio forma parte de mi práctica de **programación con C#/.NET**.
