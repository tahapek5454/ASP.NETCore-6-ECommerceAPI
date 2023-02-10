using E_CommerceAPI.Application.Validators.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.Infrastructure.Filters;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. -> kendi injectionlarimizi metodu extension ederek dahil ettik
builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();  // -> ozel dosya kayd� i�in hangi storage�n kullan�lacag�n� at�yoruz -> alternatifi de var anlars�n :)

//Cors politikalarinin duzenleyecegimiz kisim cors politikasi browserdan gelen isteklerin hangi turlerinin kabul edilecegini soyleyecegiz
//Bu tan�mlama tum isteklere izin verir -> ornek olmasi acisindan yazd�m biz projede sadece frontendimizin backendden yararlanmasini istiyoruz
//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//                            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));    

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
                        policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
                        ));




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()) // -> son olarak kendi filter�m�z tan�mlatt�k
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()) // -> asl�nda validation i�in bu yeterli
    //contolle�m�za gelen verileri olustudgumuz validatorlar kontrol et diyoruz
    // kendisi reflection ile calisma aninda kontrol islemleri yap�yor .NetCore Gucu ->
    // VER�LEN sinifin bulundugu assempleddki tum validatorlar� kendisi ekinlestiricek bizde application'� tarif eden bir s�n�f koyduk
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    // Controller normalde ustte tan�tt�m�z validatorlar� kullnarak kendi filter�ndan gecirir ve gerekli davran�s� sergiler
    // biz yapm�s oldugumuz hamleyle kendi filter�m�z� olusturuyoruz senin filter�n� eziyoruz dedik egitim amacli
                                                                                                            
                                                                                                                    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//cors politikasii middleware e ekeleyip programda calistiricaz
app.UseCors();

app.UseStaticFiles(); // wwwroot

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
