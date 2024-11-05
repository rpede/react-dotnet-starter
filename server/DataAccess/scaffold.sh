connectionString="HOST=localhost;DB=postgres;UID=postgres;PWD=mysecret;PORT=5432;"
context="AppDbContext"

dotnet ef dbcontext scaffold \
  $connectionString \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --context-dir . \
  --context $context \
  --no-onconfiguring \
  --data-annotations \
  --force

pre="
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
"

dbsets=$(cat $context.cs | grep DbSet | grep -v AspNet)

post="}"

echo -e $pre $dbsets $post >$context.cs

# rm Models/AspNet*.cs
#
# models=$(/usr/bin/ls Models)
# while IFS= read -r line || [[ -n $line ]]; do
#   sed -i -e 's/AspNetUser/Microsoft.AspNetCore.Identity.IdentityUser/g' Models/$line
# done < <(printf '%s' "$models")

dotnet format
