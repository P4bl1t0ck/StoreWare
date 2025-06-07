builder.Services.AddDbContext<StoreWare>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));