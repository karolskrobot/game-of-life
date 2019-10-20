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

            builder.RegisterType<ConsoleWrapper>().As<IConsole>();
            builder.RegisterType<FileWrapper>().As<IFile>();
            builder.RegisterType<DirectoryWrapper>().As<IDirectory>();
            builder.RegisterType<IntroScreenPrinter>().As<IIntroScreenPrinter>();
            builder.RegisterType<OptionKeyReader>().As<IOptionKeyReader>();
            builder.RegisterType<BoardEvolver>().As<IBoardEvolver>();
            builder.RegisterType<BoardGenerator>().As<IBoardGenerator>();
            builder.RegisterType<BoardPrinter>().As<IBoardPrinter>();
            builder.RegisterType<FileNamesProvider>().As<IFileNamesProvider>();
            builder.RegisterType<Application>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<Application>().Run();
            }
        }
    }
}
