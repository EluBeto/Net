using System;
namespace WebApiAutor.Services
{
    public interface IServicio
    {
        Guid ObtenerTransient();
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        void RealizaTarea();
    }

    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;

        public ServicioA(ILogger<ServicioA> logger, ServiceTransient serviceTransient,
            ServiceScoped serviceScoped, ServiceSingleton serviceSingleton)
        {
            this.logger = logger;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
        }

        public Guid ObtenerTransient() { return serviceTransient.Guid; }
        public Guid ObtenerScoped() { return serviceScoped.Guid; }
        public Guid ObtenerSingleton() { return serviceSingleton.Guid; }

        public void RealizaTarea()
        {
            //throw new NotFiniteNumberException();
        }
    }

    public class ServicioB : IServicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizaTarea()
        {
            //throw new NotFiniteNumberException();
        }
    }

    public class ServiceTransient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiceScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiceSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}

