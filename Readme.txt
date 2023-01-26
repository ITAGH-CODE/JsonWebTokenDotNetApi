Important points in order to use JWT in .NET 6 Web API

=> Install package : Microsoft.AspNetCore.Authentication.JwtBearer

=> Add the following in the Program.cs : 
    // Begin JWT Section
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });
    builder.Services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();
    // End JWT Section

=> Add the JWT Key in the appsetting.json which we use to Generate the token (Also the same to validate it).
    For Demo, I used this web site to generate key https://mkjwk.org/
