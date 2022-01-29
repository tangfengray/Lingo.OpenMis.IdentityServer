using Lingo.OpenMis.IdentityServer;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()//配置id4（跟物业说我现在需要给顾客分配钥匙）
                .AddInMemoryApiScopes(Config.GetApiResources())//配置允许请求的api（告诉物业我家里的哪些东西可以让顾客使用）
                .AddInMemoryClients(Config.GetClients())//配置允许哪些client请求（告诉物业满足什么条件的顾客才可以拿到我家的钥匙）
                .AddDeveloperSigningCredential();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//启用id4，将id4中间件添加到管道中（跟物业说一下，身份符合要求的才可以发送钥匙）
app.UseIdentityServer();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
