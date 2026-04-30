using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IrohBooks.Models;

namespace IrohBooks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ProductGenre> ProductGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key and relationship for ProductIngredient
            modelBuilder.Entity < ProductGenre > ()
              .HasKey(pi => new { pi.ProductId, pi.GenreId });

            modelBuilder.Entity <ProductGenre> ()
              .HasOne(pi => pi.Product)
              .WithMany(p => p.ProductGenres)
              .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity <ProductGenre> ()
              .HasOne(pi => pi.Genre)
              .WithMany(i => i.ProductGenres)
              .HasForeignKey(pi => pi.GenreId);

            // Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Fiction" },
                new Category { CategoryId = 2, Name = "Non-Fiction" },
                new Category { CategoryId = 3, Name = "Technology" },
                new Category { CategoryId = 4, Name = "Education" },
                new Category { CategoryId = 5, Name = "Biography" },
                new Category { CategoryId = 6, Name = "Science" }
            );

         modelBuilder.Entity<Genre>().HasData(
         // add book store genres here

           new Genre { GenreId = 1, Name = "Fantasy"},
           new Genre { GenreId = 2, Name = "Romance"},
           new Genre { GenreId = 3, Name = "Mystery"},
           new Genre { GenreId = 4, Name = "Self-Help"},
           new Genre { GenreId = 5, Name = "Motivation"},
           new Genre { GenreId = 6, Name = "Programming"},
           new Genre { GenreId = 7, Name = "Web Development"},
           new Genre { GenreId = 8, Name = "Data Science"},
           new Genre { GenreId = 9, Name = "Textbook"},
           new Genre { GenreId = 10, Name = "Exam Preparation"},
           new Genre { GenreId = 11, Name = "Autobiography"},
           new Genre { GenreId = 12, Name = "Memoir"},
           new Genre { GenreId = 13, Name = "Physics"},
           new Genre { GenreId = 14, Name = "Biology"},
           new Genre { GenreId = 15, Name = "Epic Fantasy" },
           new Genre { GenreId = 16, Name = "Urban Fantasy" },
           new Genre { GenreId = 17, Name = "Detective" },
           new Genre { GenreId = 18, Name = "Thriller" },
           new Genre { GenreId = 19, Name = "Habit Building" },
           new Genre { GenreId = 20, Name = "Productivity" },
           new Genre { GenreId = 21, Name = "Coding Practices" },
           new Genre { GenreId = 22, Name = "System Design" },
           new Genre { GenreId = 23, Name = "Machine Learning" },
           new Genre { GenreId = 24, Name = "Algorithm Design" },
           new Genre { GenreId = 25, Name = "Life Story" },
           new Genre { GenreId = 26, Name = "Entrepreneur Journey" },
           new Genre { GenreId = 27, Name = "Cosmology" },
           new Genre { GenreId = 28, Name = "Astrophysics" },
           new Genre { GenreId = 29, Name = "Adventure" },
           new Genre { GenreId = 30, Name = "Philosophical" },
           new Genre { GenreId = 31, Name = "Career Growth" },
           new Genre { GenreId = 32, Name = "Best Practices" },
           new Genre { GenreId = 33, Name = "Problem Solving" },
           new Genre { GenreId = 34, Name = "Space Science" }
          );

            // Add book store product entries here
            modelBuilder.Entity<Product>().HasData(

            new Product
            {
                ProductId = 1,
                Name = "The Alchemist",
                Description = "A philosophical novel about following your dreams by Paulo Coelho",
                Price = 500m,
                Stock = 50,
                CategoryId = 1
            },

            new Product
            {
                ProductId = 2,
                Name = "Atomic Habits",
                Description = "A self-improvement book about building good habits by James Clear",
                Price = 650m,
                Stock = 40,
                CategoryId = 2
            },

            new Product
            {
                ProductId = 3,
                Name = "Clean Code",
                Description = "A guide to writing clean and maintainable code by Robert C. Martin",
                Price = 800m,
                Stock = 30,
                CategoryId = 3
            },

            new Product
            {
                ProductId = 4,
                Name = "Introduction to Algorithms",
                Description = "A comprehensive textbook on algorithms by Thomas H. Cormen",
                Price = 1200m,
                Stock = 20,
                CategoryId = 4
            },

            new Product
            {
                ProductId = 5,
                Name = "Steve Jobs",
                Description = "A biography of Steve Jobs by Walter Isaacson",
                Price = 700m,
                Stock = 25,
                CategoryId = 5
            },

            new Product
            {
                ProductId = 6,
                Name = "A Brief History of Time",
                Description = "A popular science book on cosmology by Stephen Hawking",
                Price = 900m,
                Stock = 15,
                CategoryId = 6
            },

            new Product
            {
                ProductId = 7,
                Name = "The Hobbit",
                Description = "A fantasy adventure novel by J.R.R. Tolkien",
                Price = 550m,
                Stock = 35,
                CategoryId = 1
            }

            );

         modelBuilder.Entity<ProductGenre>().HasData(

            // Product 1: The Alchemist
            new ProductGenre { ProductId = 1, GenreId = 1 },  // Fantasy
            new ProductGenre { ProductId = 1, GenreId = 2 },  // Romance
            new ProductGenre { ProductId = 1, GenreId = 29 }, // Adventure
            new ProductGenre { ProductId = 1, GenreId = 30 }, // Philosophical
            new ProductGenre { ProductId = 1, GenreId = 16 }, // Urban Fantasy

            // Product 2: Atomic Habits
            new ProductGenre { ProductId = 2, GenreId = 4 },  // Self-Help
            new ProductGenre { ProductId = 2, GenreId = 5 },  // Motivation
            new ProductGenre { ProductId = 2, GenreId = 19 }, // Habit Building
            new ProductGenre { ProductId = 2, GenreId = 20 }, // Productivity
            new ProductGenre { ProductId = 2, GenreId = 31 }, // Career Growth

            // Product 3: Clean Code
            new ProductGenre { ProductId = 3, GenreId = 6 },  // Programming
            new ProductGenre { ProductId = 3, GenreId = 7 },  // Web Development
            new ProductGenre { ProductId = 3, GenreId = 21 }, // Coding Practices
            new ProductGenre { ProductId = 3, GenreId = 18 }, // Best Practices
            new ProductGenre { ProductId = 3, GenreId = 33 }, // Problem Solving

            // Product 4: Introduction to Algorithms
            new ProductGenre { ProductId = 4, GenreId = 9 },  // Textbook
            new ProductGenre { ProductId = 4, GenreId = 10 }, // Exam Preparation
            new ProductGenre { ProductId = 4, GenreId = 24 }, // Algorithm Design
            new ProductGenre { ProductId = 4, GenreId = 33 }, // Problem Solving
            new ProductGenre { ProductId = 4, GenreId = 22 }, // System Design

            // Product 5: Steve Jobs
            new ProductGenre { ProductId = 5, GenreId = 11 }, // Autobiography
            new ProductGenre { ProductId = 5, GenreId = 12 }, // Memoir
            new ProductGenre { ProductId = 5, GenreId = 25 }, // Life Story
            new ProductGenre { ProductId = 5, GenreId = 26 }, // Entrepreneur Journey
            new ProductGenre { ProductId = 5, GenreId = 31 }, // Career Growth

            // Product 6: A Brief History of Time
            new ProductGenre { ProductId = 6, GenreId = 13 }, // Physics
            new ProductGenre { ProductId = 6, GenreId = 27 }, // Cosmology
            new ProductGenre { ProductId = 6, GenreId = 28 }, // Astrophysics
            new ProductGenre { ProductId = 6, GenreId = 34 }, // Space Science
            new ProductGenre { ProductId = 6, GenreId = 30 }, // Philosophical

            // Product 7: The Hobbit
            new ProductGenre { ProductId = 7, GenreId = 1 },  // Fantasy
            new ProductGenre { ProductId = 7, GenreId = 15 }, // Epic Fantasy
            new ProductGenre { ProductId = 7, GenreId = 29 }, // Adventure
            new ProductGenre { ProductId = 7, GenreId = 3 },  // Mystery
            new ProductGenre { ProductId = 7, GenreId = 17 }  // Detective

);

        }
    }
}