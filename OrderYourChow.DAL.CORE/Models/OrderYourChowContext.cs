using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OrderYourChow.DAL.CORE.Models
{
    public partial class OrderYourChowContext : DbContext
    {
        public OrderYourChowContext()
        {
        }

        public OrderYourChowContext(DbContextOptions<OrderYourChowContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DPlan> DPlans { get; set; }
        public virtual DbSet<DPlanRecipe> DPlanRecipes { get; set; }
        public virtual DbSet<DRecipe> DRecipes { get; set; }
        public virtual DbSet<DRecipeImage> DRecipeImages { get; set; }
        public virtual DbSet<DRecipeProduct> DRecipeProducts { get; set; }
        public virtual DbSet<DShopping> DShoppings { get; set; }
        public virtual DbSet<DShoppingList> DShoppingLists { get; set; }
        public virtual DbSet<SDay> SDays { get; set; }
        public virtual DbSet<SProduct> SProducts { get; set; }
        public virtual DbSet<SProductCategory> SProductCategories { get; set; }
        public virtual DbSet<SProductMeasure> SProductMeasures { get; set; }
        public virtual DbSet<SRecipeCategory> SRecipeCategories { get; set; }
        public virtual DbSet<SWeek> SWeeks { get; set; }
        public virtual DbSet<SDateDay> SDateDays { get; set; }
        public virtual DbSet<DDietDay> DDietDays { get; set; }
        public virtual DbSet<DDietDayRecipe> DDietDayRecipes { get; set; }
        public virtual DbSet<DUser> DUsers { get; set; }    
        public virtual DbSet<DRecipeFavourite> DRecipeFavourites { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__D_PLAN__755C22B70D68E5B2");

                entity.ToTable("D_PLAN");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.DPlans)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_PLAN__Sysdate__44FF419A");
            });

            modelBuilder.Entity<DPlanRecipe>(entity =>
            {
                entity.HasKey(e => e.PlanRecipeId)
                    .HasName("PK__D_PLAN_R__531B21B7FFF811D6");

                entity.ToTable("D_PLAN_RECIPE");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.DPlanRecipes)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_PLAN_RE__DayId__00200768");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DPlanRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_PLAN_RE__Sysda__7F2BE32F");
            });

            modelBuilder.Entity<DRecipe>(entity =>
            {
                entity.HasKey(e => e.RecipeId)
                    .HasName("PK__D_RECIPE__FDD988B019C05448");

                entity.ToTable("D_RECIPE");

                entity.Property(e => e.Description);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DRecipes)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_RECIPE__Sysdat__6B24EA82");
            });

            modelBuilder.Entity<DRecipeImage>(entity =>
            {
                entity.HasKey(e => e.RecipeImageId)
                    .HasName("PK__D_RECIPE__23E65C238874E5A7");

                entity.ToTable("D_RECIPE_IMAGES");

                entity.Property(e => e.Image).HasMaxLength(200);

              

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DRecipeImages)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_RECIPE___Sysda__18EBB532");
            });

            modelBuilder.Entity<DRecipeProduct>(entity =>
            {
                entity.HasKey(e => e.RecipeProductId)
                    .HasName("PK__D_RECIPE__3879DB74C27AE08D");

                entity.Property(e => e.Count).HasColumnType("decimal(10,2)");

                entity.ToTable("D_RECIPE_PRODUCT");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DRecipeProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_RECIPE___Produ__05D8E0BE");

                entity.HasOne(d => d.ProductMeasure)
                    .WithMany(p => p.DRecipeProducts)
                    .HasForeignKey(d => d.ProductMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_RECIPE___Produ__06CD04F7");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DRecipeProducts)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_RECIPE___Sysda__04E4BC85");
            });

            modelBuilder.Entity<DShopping>(entity =>
            {
                entity.HasKey(e => e.ShoppingId)
                    .HasName("PK__D_SHOPPI__8E3AF518CAB77B3A");

                entity.ToTable("D_SHOPPING");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Shop).HasMaxLength(50);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<DShoppingList>(entity =>
            {
                entity.HasKey(e => e.ShoppingListId)
                    .HasName("PK__D_SHOPPI__6CBBDD145EB0D3C8");

                entity.ToTable("D_SHOPPING_LIST");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.DShoppingLists)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_SHOPPIN__Sysda__797309D9");

                entity.HasOne(d => d.Shopping)
                    .WithMany(p => p.DShoppingLists)
                    .HasForeignKey(d => d.ShoppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__D_SHOPPIN__Shopp__7A672E12");
            });

            modelBuilder.Entity<SDay>(entity =>
            {
                entity.HasKey(e => e.DayId)
                    .HasName("PK__S_DAY__BF3DD8C568465FE9");

                entity.ToTable("S_DAY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<SProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__S_PRODUC__B40CC6CD550497AE");

                entity.ToTable("S_PRODUCT");

                entity.Property(e => e.Image).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__S_PRODUCT__Sysda__619B8048");
            });

            modelBuilder.Entity<SProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryId)
                    .HasName("PK__S_PRODUC__3224ECCEAB43FBAA");

                entity.ToTable("S_PRODUCT_CATEGORY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<SProductMeasure>(entity =>
            {
                entity.HasKey(e => e.ProductMeasureId)
                    .HasName("PK__S_PRODUC__4A00517B8885EE62");

                entity.ToTable("S_PRODUCT_MEASURE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<SRecipeCategory>(entity =>
            {
                entity.HasKey(e => e.RecipeCategoryId)
                    .HasName("PK__S_RECIPE__747A031B1E2535C2");

                entity.ToTable("S_RECIPE_CATEGORY");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<SWeek>(entity =>
            {
                entity.HasKey(e => e.WeekId)
                    .HasName("PK__S_WEEK__C814A5C1B8C5888A");

                entity.ToTable("S_WEEK");

                entity.Property(e => e.End).HasColumnType("date");

                entity.Property(e => e.Start).HasColumnType("date");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");
            });

            modelBuilder.Entity<SDateDay>(entity =>
            {
                entity.HasKey(e => e.DateDayId)
                    .HasName("PK__S_DATE_DAY__DateDayId");

                entity.ToTable("S_DATE_DAY");

                entity.Property(e => e.DayId).HasColumnType("int");

                entity.Property(e => e.DateDay).HasColumnType("date");

                entity.HasOne(d => d.SDay)
                           .WithMany(p => p.SDateDays)
                           .HasForeignKey(d => d.DayId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK__S_DATE_DAY__S_DAY");
            });

            modelBuilder.Entity<DDietDay>(entity =>
            {
                entity.HasKey(e => e.DietDayId)
                    .HasName("PK__D_DIET_DAY__DietDayId");

                entity.ToTable("D_DIET_DAY");

                entity.Property(e => e.DateDayId).HasColumnType("int");

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.SDateDay)
                           .WithMany(p => p.DDietDays)
                           .HasForeignKey(d => d.DateDayId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK__D_DIET_DAY__S_DATE_DAY");

                entity.HasOne(d => d.DUser)
                           .WithMany(p => p.DDietDays)
                           .HasForeignKey(d => d.UserId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK__D_DIET_DAY__D_USER");
            });

            modelBuilder.Entity<DDietDayRecipe>(entity =>
            {
                entity.HasKey(e => e.DietDayRecipeId)
                    .HasName("PK__D_DIET_DAY_RECIPE_DietDayRecipeId");

                entity.ToTable("D_DIET_DAY_RECIPE");

                entity.Property(e => e.DietDayId).HasColumnType("int");
                entity.Property(e => e.RecipeId).HasColumnType("int");
                entity.Property(e => e.Eaten).HasColumnType("bit").HasDefaultValue(false);
                entity.Property(e => e.Multiplier).HasColumnType("decimal").HasDefaultValue(1);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

                entity.HasOne(d => d.DDietDay)
                           .WithMany(p => p.DDietDayRecipes)
                           .HasForeignKey(d => d.DietDayId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK__D_DIET_DAY_RECIPE_D_DIET_DAY");

                entity.HasOne(d => d.DRecipe)
                           .WithMany(p => p.DDietDayRecipes)
                           .HasForeignKey(d => d.RecipeId)
                           .OnDelete(DeleteBehavior.ClientSetNull)
                           .HasConstraintName("FK__D_DIET_DAY_RECIPE_D_RECIPE");
            });

            modelBuilder.Entity<DUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__D_USER_UserId");

                entity.ToTable("D_USER");

                entity.Property(e => e.Login).HasColumnType("nvarchar(30)");
                entity.Property(e => e.Email).HasColumnType("nvarchar(320)");
                entity.Property(e => e.MultiplierDiet).HasColumnType("int").HasDefaultValue(1);

                entity.Property(e => e.Sysdate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Syslog)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasDefaultValueSql("(suser_name())");

            });

            modelBuilder.Entity<DRecipeFavourite>(entity =>
            {
                entity.HasKey(e => e.RecipeFavouriteId)
                    .HasName("PK__D_RECIPE_FAVOURITE_RecipeFavouriteId");

                entity.ToTable("D_RECIPE_FAVOURITE");

                entity.Property(e => e.UserId).HasColumnType("int");
                entity.Property(e => e.RecipeId).HasColumnType("int");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
