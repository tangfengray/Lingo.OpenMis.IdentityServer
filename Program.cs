using Lingo.OpenMis.IdentityServer;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()//����id4������ҵ˵��������Ҫ���˿ͷ���Կ�ף�
                .AddInMemoryApiScopes(Config.GetApiResources())//�������������api��������ҵ�Ҽ������Щ���������ù˿�ʹ�ã�
                .AddInMemoryClients(Config.GetClients())//����������Щclient���󣨸�����ҵ����ʲô�����Ĺ˿Ͳſ����õ��Ҽҵ�Կ�ף�
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

//����id4����id4�м����ӵ��ܵ��У�����ҵ˵һ�£���ݷ���Ҫ��Ĳſ��Է���Կ�ף�
app.UseIdentityServer();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
