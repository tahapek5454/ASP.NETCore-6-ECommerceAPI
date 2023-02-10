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
builder.Services.AddStorage<LocalStorage>();  // -> ozel dosya kaydý için hangi storageýn kullanýlacagýný atýyoruz -> alternatifi de var anlarsýn :)

//Cors politikalarinin duzenleyecegimiz kisim cors politikasi browserdan gelen isteklerin hangi turlerinin kabul edilecegini soyleyecegiz
//Bu tanýmlama tum isteklere izin verir -> ornek olmasi acisindan yazdým biz projede sadece frontendimizin backendden yararlanmasini istiyoruz
//builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//                            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));    

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
                        policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
                        ));




builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>()) // -> son olarak kendi filterýmýz tanýmlattýk
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()) // -> aslýnda validation için bu yeterli
    //contolleýmýza gelen verileri olustudgumuz validatorlar kontrol et diyoruz
    // kendisi reflection ile calisma aninda kontrol islemleri yapýyor .NetCore Gucu ->
    // VERÝLEN sinifin bulundugu assempleddki tum validatorlarý kendisi ekinlestiricek bizde application'ý tarif eden bir sýnýf koyduk
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    // Controller normalde ustte tanýttýmýz validatorlarý kullnarak kendi filterýndan gecirir ve gerekli davranýsý sergiler
    // biz yapmýs oldugumuz hamleyle kendi filterýmýzý olusturuyoruz senin filterýný eziyoruz dedik egitim amacli
                                                                                                            
                                                                                                                    
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
