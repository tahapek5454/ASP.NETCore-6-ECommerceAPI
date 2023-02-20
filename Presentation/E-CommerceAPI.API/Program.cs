using E_CommerceAPI.Application;
using E_CommerceAPI.Application.Validators.Products;
using E_CommerceAPI.Infrastructure;
using E_CommerceAPI.Infrastructure.Filters;
using E_CommerceAPI.Infrastructure.Services.Storage.GCP;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. -> kendi injectionlarimizi metodu extension ederek dahil ettik
builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
//builder.Services.AddStorage<LocalStorage>();  // -> ozel dosya kayd� i�in hangi storage�n kullan�lacag�n� at�yoruz -> alternatifi de var anlars�n :)
builder.Services.AddStorage<GCPStorage>();  // -> ozel dosya kayd� i�in hangi storage�n kullan�lacag�n� at�yoruz -> alternatifi de var anlars�n :)
builder.Services.AddAplicationServices();


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


//Authentication� bildirecegiz
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", option =>
    {
        // gelen token�n jwt oldgunu soyledik ve validate yaparken nelere dikkat etmesini belirticez
        option.TokenValidationParameters = new()
        {
            // olusturulacak token degerini kimlerin hangi originlerin/sitelerin kullanacag�n� belirledigimiz yerdir. -> www.bilmemne.com
            ValidateAudience = true,
            // olusturulacak token degerinin kimin dag�tt�n�n ifade ettigimiz yerdir -> www.api.com       
            ValidateIssuer = true,
            // olusturulan token degerinin suresini kontrol eder
            ValidateLifetime= true,
            // uretilecek token degerinin uygulamam�za ait oldugunu ifade eden securtity key verisinin dogrulanmas�d�r
            ValidateIssuerSigningKey = true,



            // -> true verek kontrol edilmesini sagladik simdi bunlar�n degelerini atayal�m
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            
            //expire zaman� belirleme
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires!=null ? expires > DateTime.UtcNow : false,

        };
    });



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

app.UseAuthentication(); // authentication ekledik
app.UseAuthorization();

app.MapControllers();

app.Run();
