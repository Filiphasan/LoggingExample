using LoggingExample.Web.Models.OptionModels;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;

namespace LoggingExample.Web.Extensions;

public static class LoggingExtension
{
    public static void RegisterLogger(this IConfiguration configuration)
    {
        var model = configuration.GetSection("SeriLogConfig").Get<SeriLogConfigModel>();

        SelfLog.Enable(Console.Error); // Serilog'un kendi hatalarını loglamayı açmak için bunu kullanıyoruz

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information() // Tüm loglamalar için Information ve üstü loglansın (Information, Warning, Error, Fatal)
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) // Microsoft uygulamaları için Warning, Error, Fatal loglansın
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning) // System uygulamaları için Warning, Error, Fatal loglansın
            .WriteTo.Console() // Console uzerinden loglansın
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(model!.ElasticUri)) // Elasticsearch uzerinden loglansın
            {
                AutoRegisterTemplate = true,
                OverwriteTemplate = true,
                DetectElasticsearchVersion = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7, //Template versiyon
                IndexFormat = $"{model.ProjectName}-{model.Environment}-logs-" + "{0:yyyy.MM.dd}", //Index format ayarı
                ModifyConnectionSettings = s => s.BasicAuthentication(model.ElasticUser, model.ElasticPassword), //Authentication
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog, //SelfLog.Enable ile aynı işlev
                //ELK loglaması hata alırsa console üzerine loglasın
                FailureCallback = e => { Console.WriteLine("Unable to submit event -- " + e.RenderMessage() + " : " + e.Exception?.Message); }
            })
            .Enrich.FromLogContext() // Tüm loglarda ek field olarak logun kaynak bilgisini ekle (SourceContext)
            .Enrich.WithMachineName() // Tüm loglarda ek field olarak bilgisayarin ismi ekle (MachineName)
            .Enrich.WithProperty("Environment", model.Environment) // Tüm loglarda ek field olarak ortam bilgisini ekle (Environment)
            .CreateLogger();
    }
}