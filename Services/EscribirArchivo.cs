using System;

namespace WebApiAutor.Services
{
    public class EscribirArchivo: IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "File1.txt";
        private Timer timer;

        public EscribirArchivo(IWebHostEnvironment environment)
        {
            env = environment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Escribir("Proceso Finalizado");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Escribir("Proceso iniciado" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void Escribir(string mensaje)
        {
            var ruta = $@"{env.ContentRootPath}wwwRoot/{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}

