using Autofac;
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
            builder.RegisterType<Board>().As<IBoard>();
            builder.RegisterType<BoardGenerator>().As<IBoardGenerator>();

            Container = builder.Build();

            var console = new ConsoleWrapper();

            var application = new Application(
                new Game(console),
                new Board(console),
                new BoardGenerator(),
                console
                );

            application.Run();
        }
    }
}
