using Autofac;
using GameOfLife.Wrappers;
using System.Diagnostics.CodeAnalysis;

namespace GameOfLife
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Game>().As<IGame>();
            builder.RegisterType<BoardProcessor>().As<IBoardProcessor>();
            builder.RegisterType<BoardGenerator>().As<IBoardGenerator>();

            Container = builder.Build();

            var console = new ConsoleWrapper();

            var application = new Application(
                new Game(console, new DirectoryWrapper()),
                new BoardProcessor(console),
                new BoardGenerator(new Board()),
                console, 
                new FileWrapper()
                );

            application.Run();
        }
    }
}
