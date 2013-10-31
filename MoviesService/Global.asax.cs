using System;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;

namespace MoviesService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MoviesService>().AsSelf();
            builder.RegisterType<MovieDataSourceProxy>().As<IMovieDataSourceProxy>().SingleInstance();
            builder.RegisterType<MoviesCache>().As<IMoviesCache>().SingleInstance();
            builder.RegisterType<MoviesRepository>().As<IMoviesRepository>().SingleInstance();

            var container = builder.Build();

            AutofacHostFactory.Container = container;
        }
    }
}