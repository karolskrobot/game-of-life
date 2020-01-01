using Autofac;
using GameOfLife.Core;
using GameOfLife.IO;
using GameOfLife.IO.Wrappers;
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

            builder.RegisterType<ConsoleWrapper>().As<IConsole>();
            builder.RegisterType<FileWrapper>().As<IFile>();
            builder.RegisterType<DirectoryWrapper>().As<IDirectory>();
            builder.RegisterType<ConsolePrinter>().As<IConsolePrinter>();
            builder.RegisterType<OptionKeyReader>().As<IOptionKeyReader>();
            builder.RegisterType<FileNamesProvider>().As<IFileNamesProvider>();
            builder.RegisterType<BoardFactory>().As<IBoardFactory>();
            builder.RegisterType<Application>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<Application>().Run();
            }
        }
    }
}
