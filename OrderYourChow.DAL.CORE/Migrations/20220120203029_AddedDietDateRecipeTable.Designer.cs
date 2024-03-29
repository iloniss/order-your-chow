﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.DAL.CORE.Migrations
{
    [DbContext(typeof(OrderYourChowContext))]
    [Migration("20220120203029_AddedDietDateRecipeTable")]
    partial class AddedDietDateRecipeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderYourChow.DbModels.DDietDay", b =>
                {
                    b.Property<int>("DietDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DateDayId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("DietDayId")
                        .HasName("PK__D_DIET_DAY__DietDayId");

                    b.HasIndex("DateDayId");

                    b.ToTable("D_DIET_DAY");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DDietDayRecipe", b =>
                {
                    b.Property<int>("DietDayRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DietDayId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("DietDayRecipeId")
                        .HasName("PK__D_DIET_DAY_RECIPE_DietDayRecipeId");

                    b.HasIndex("DietDayId");

                    b.HasIndex("RecipeId");

                    b.ToTable("D_DIET_DAY_RECIPE");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DPlan", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.Property<int>("WeekId")
                        .HasColumnType("int");

                    b.HasKey("PlanId")
                        .HasName("PK__D_PLAN__755C22B70D68E5B2");

                    b.HasIndex("WeekId");

                    b.ToTable("D_PLAN");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DPlanRecipe", b =>
                {
                    b.Property<int>("PlanRecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("PlanRecipeId")
                        .HasName("PK__D_PLAN_R__531B21B7FFF811D6");

                    b.HasIndex("DayId");

                    b.HasIndex("RecipeId");

                    b.ToTable("D_PLAN_RECIPE");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("Favourite")
                        .HasColumnType("bit");

                    b.Property<string>("MainImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Meat")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(2,1)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("RecipeId")
                        .HasName("PK__D_RECIPE__FDD988B019C05448");

                    b.HasIndex("CategoryId");

                    b.ToTable("D_RECIPE");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipeImage", b =>
                {
                    b.Property<int>("RecipeImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("RecipeImageId")
                        .HasName("PK__D_RECIPE__23E65C238874E5A7");

                    b.HasIndex("RecipeId");

                    b.ToTable("D_RECIPE_IMAGES");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipeProduct", b =>
                {
                    b.Property<int>("RecipeProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductMeasureId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("RecipeProductId")
                        .HasName("PK__D_RECIPE__3879DB74C27AE08D");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductMeasureId");

                    b.HasIndex("RecipeId");

                    b.ToTable("D_RECIPE_PRODUCT");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DShopping", b =>
                {
                    b.Property<int>("ShoppingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Cost")
                        .HasColumnType("money");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Shop")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("ShoppingId")
                        .HasName("PK__D_SHOPPI__8E3AF518CAB77B3A");

                    b.ToTable("D_SHOPPING");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DShoppingList", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("ShoppingListId")
                        .HasName("PK__D_SHOPPI__6CBBDD145EB0D3C8");

                    b.HasIndex("RecipeId");

                    b.HasIndex("ShoppingId");

                    b.ToTable("D_SHOPPING_LIST");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SDateDay", b =>
                {
                    b.Property<int>("DateDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateDay")
                        .HasColumnType("date");

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.HasKey("DateDayId")
                        .HasName("PK__S_DATE_DAY__DateDayId");

                    b.HasIndex("DayId");

                    b.ToTable("S_DATE_DAY");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SDay", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("DayId")
                        .HasName("PK__S_DAY__BF3DD8C568465FE9");

                    b.ToTable("S_DAY");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("ProductId")
                        .HasName("PK__S_PRODUC__B40CC6CD550497AE");

                    b.HasIndex("CategoryId");

                    b.ToTable("S_PRODUCT");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("ProductCategoryId")
                        .HasName("PK__S_PRODUC__3224ECCEAB43FBAA");

                    b.ToTable("S_PRODUCT_CATEGORY");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProductMeasure", b =>
                {
                    b.Property<int>("ProductMeasureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("ProductMeasureId")
                        .HasName("PK__S_PRODUC__4A00517B8885EE62");

                    b.ToTable("S_PRODUCT_MEASURE");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SRecipeCategory", b =>
                {
                    b.Property<int>("RecipeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.HasKey("RecipeCategoryId")
                        .HasName("PK__S_RECIPE__747A031B1E2535C2");

                    b.ToTable("S_RECIPE_CATEGORY");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SWeek", b =>
                {
                    b.Property<int>("WeekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End")
                        .HasColumnType("date");

                    b.Property<DateTime>("Start")
                        .HasColumnType("date");

                    b.Property<DateTime>("Sysdate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Syslog")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasDefaultValueSql("(suser_name())");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("WeekId")
                        .HasName("PK__S_WEEK__C814A5C1B8C5888A");

                    b.ToTable("S_WEEK");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DDietDay", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SDateDay", "SDateDay")
                        .WithMany("DDietDays")
                        .HasForeignKey("DateDayId")
                        .HasConstraintName("FK__D_DIET_DAY__S_DATE_DAY")
                        .IsRequired();

                    b.Navigation("SDateDay");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DDietDayRecipe", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.DDietDay", "DDietDay")
                        .WithMany("DDietDayRecipes")
                        .HasForeignKey("DietDayId")
                        .HasConstraintName("FK__D_DIET_DAY_RECIPE_D_DIET_DAY")
                        .IsRequired();

                    b.HasOne("OrderYourChow.DbModels.DRecipe", "DRecipe")
                        .WithMany("DDietDayRecipes")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__D_DIET_DAY_RECIPE_D_RECIPE")
                        .IsRequired();

                    b.Navigation("DDietDay");

                    b.Navigation("DRecipe");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DPlan", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SWeek", "Week")
                        .WithMany("DPlans")
                        .HasForeignKey("WeekId")
                        .HasConstraintName("FK__D_PLAN__Sysdate__44FF419A")
                        .IsRequired();

                    b.Navigation("Week");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DPlanRecipe", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SDay", "Day")
                        .WithMany("DPlanRecipes")
                        .HasForeignKey("DayId")
                        .HasConstraintName("FK__D_PLAN_RE__DayId__00200768")
                        .IsRequired();

                    b.HasOne("OrderYourChow.DbModels.DRecipe", "Recipe")
                        .WithMany("DPlanRecipes")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__D_PLAN_RE__Sysda__7F2BE32F")
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipe", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SRecipeCategory", "Category")
                        .WithMany("DRecipes")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__D_RECIPE__Sysdat__6B24EA82")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipeImage", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.DRecipe", "Recipe")
                        .WithMany("DRecipeImages")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__D_RECIPE___Sysda__18EBB532")
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipeProduct", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SProduct", "Product")
                        .WithMany("DRecipeProducts")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__D_RECIPE___Produ__05D8E0BE")
                        .IsRequired();

                    b.HasOne("OrderYourChow.DbModels.SProductMeasure", "ProductMeasure")
                        .WithMany("DRecipeProducts")
                        .HasForeignKey("ProductMeasureId")
                        .HasConstraintName("FK__D_RECIPE___Produ__06CD04F7")
                        .IsRequired();

                    b.HasOne("OrderYourChow.DbModels.DRecipe", "Recipe")
                        .WithMany("DRecipeProducts")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__D_RECIPE___Sysda__04E4BC85")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductMeasure");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DShoppingList", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.DRecipe", "Recipe")
                        .WithMany("DShoppingLists")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK__D_SHOPPIN__Sysda__797309D9")
                        .IsRequired();

                    b.HasOne("OrderYourChow.DbModels.DShopping", "Shopping")
                        .WithMany("DShoppingLists")
                        .HasForeignKey("ShoppingId")
                        .HasConstraintName("FK__D_SHOPPIN__Shopp__7A672E12")
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Shopping");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SDateDay", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SDay", "SDay")
                        .WithMany("SDateDays")
                        .HasForeignKey("DayId")
                        .HasConstraintName("FK__S_DATE_DAY__S_DAY")
                        .IsRequired();

                    b.Navigation("SDay");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProduct", b =>
                {
                    b.HasOne("OrderYourChow.DbModels.SProductCategory", "Category")
                        .WithMany("SProducts")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__S_PRODUCT__Sysda__619B8048")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DDietDay", b =>
                {
                    b.Navigation("DDietDayRecipes");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DRecipe", b =>
                {
                    b.Navigation("DDietDayRecipes");

                    b.Navigation("DPlanRecipes");

                    b.Navigation("DRecipeImages");

                    b.Navigation("DRecipeProducts");

                    b.Navigation("DShoppingLists");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.DShopping", b =>
                {
                    b.Navigation("DShoppingLists");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SDateDay", b =>
                {
                    b.Navigation("DDietDays");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SDay", b =>
                {
                    b.Navigation("DPlanRecipes");

                    b.Navigation("SDateDays");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProduct", b =>
                {
                    b.Navigation("DRecipeProducts");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProductCategory", b =>
                {
                    b.Navigation("SProducts");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SProductMeasure", b =>
                {
                    b.Navigation("DRecipeProducts");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SRecipeCategory", b =>
                {
                    b.Navigation("DRecipes");
                });

            modelBuilder.Entity("OrderYourChow.DbModels.SWeek", b =>
                {
                    b.Navigation("DPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
