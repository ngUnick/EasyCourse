
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());



app.MapGet("/", () => "Hello World!");


app.MapGet("/fetch", ([FromQuery] int moduleID) =>
{
    using (var conn = new MySqlConnection(
        "Server=dblabs.iee.ihu.gr;" +
        "User ID=iee2019067;" +
        "Password=Ka-697571;" +
        "Database=iee2019067"
        ))
    {
        conn.Open();
        var command = new MySqlCommand("SELECT m.Rating as rt, m.ModuleName as nam , m.Price as pr ," +
            "p.LecturerName as prof , c.CategoryName as catname , m.TotalHours as tot" +
            " FROM modules m inner join lecturer p on m.ModuleLeader = p.idLecturer" +
            " inner join modulecategories md on md.idModule = m.idModules " +
            " inner join categories c on c.idCategory = md.idCategory"+
            $" where idModules = {moduleID};"
            , conn);
        var reader = command.ExecuteReader();
        var module = reader.Read();
        var dict = new Dictionary<string, object>();
        dict.Add("Rating", reader.GetFloat("rt"));
        dict.Add("Prof", reader.GetString("prof"));
        dict.Add("Price", reader.GetDouble("pr"));
        dict.Add("CourseName", reader.GetString("nam"));
        dict.Add("Dept", reader.GetString("catname"));
        dict.Add("Hr", reader.GetInt32("tot"));
        conn.Clone();
        return Newtonsoft.Json.JsonConvert.SerializeObject(dict);
    }
});

app.MapGet("/search", ([FromQuery] string query) =>
{
    var q = Newtonsoft.Json.JsonConvert.DeserializeObject<Searcher.Query>(query);

    return Newtonsoft.Json.JsonConvert.SerializeObject(Searcher.Srch.search(q));
});


app.Run();

