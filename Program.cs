var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/oops");
}

// Added as service
builder.Services.AddSingleton<Service>();

app.Logger.LogInformation("The app started");

app.MapGet("/", () => "Hello World");
app.MapGet("/oops", () => "Oops! An error happened.");

app.MapGet("/domain", 
    (Service service) => $"List of the Domains avaiable");

app.MapGet("/domain/{domainId:int}", 
    (int domainId, Service service) => $"The domain {domainId} - pages");

app.MapGet("/domain/{domainId:int}/pages/{pageId:int}/data", 
    (int domainId, int pageId, Service service) => $"The domain {domainId} - Page ID is {pageId}");

app.MapPost("/data", (PerformanceData data, Service service) => service.AddData(data));

app.Run();

class Service { 
    public string AddData(PerformanceData data){
        return "Test added data";
    }

    public int AddDomain(){
        return 1;
    }

    public GraphData GetData(){
        return new GraphData();
    }
}

record GraphData();

record PerformanceData(
    int DomainId, 
    int PageId
);