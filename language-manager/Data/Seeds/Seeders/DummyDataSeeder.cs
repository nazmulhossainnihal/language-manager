using language_manager.Data.Context;
using language_manager.Model.Domain;
using MongoDB.Driver;

namespace language_manager.Data.Seeds.Seeders;

/// <summary>
/// Seeds the database with dummy data for development purposes.
/// Creates sample apps, modules, translations, app-user and app-language relationships.
/// Only registered in development environments.
/// </summary>
public class DummyDataSeeder : ISeeder
{
    private readonly MongoDbContext _context;

    public DummyDataSeeder(MongoDbContext context)
    {
        _context = context;
    }

    public string Name => "003_DummyDataSeeder";
    public int Order => 100;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        // Fetch existing languages and user seeded by earlier seeders
        var languages = await _context.Languages.Find(_ => true).ToListAsync(cancellationToken);
        var users = await _context.Users.Find(_ => true).ToListAsync(cancellationToken);

        var langEn = languages.First(l => l.LanguageKey == "en");
        var langEs = languages.First(l => l.LanguageKey == "es");
        var langFr = languages.First(l => l.LanguageKey == "fr");
        var langDe = languages.First(l => l.LanguageKey == "de");
        var langJa = languages.First(l => l.LanguageKey == "ja");

        var adminUser = users.First(u => u.Email == "admin@language-manager");

        // --- Apps ---
        var ecommerceApp = new App
        {
            AppId = Guid.NewGuid().ToString(),
            Name = "E-Commerce Platform",
            Domain = "ecommerce.example.com",
            Environment = "development",
            DefaultLanguageId = langEn.LanguageId
        };

        var blogApp = new App
        {
            AppId = Guid.NewGuid().ToString(),
            Name = "Blog Application",
            Domain = "blog.example.com",
            Environment = "development",
            DefaultLanguageId = langEn.LanguageId
        };

        var mobileApp = new App
        {
            AppId = Guid.NewGuid().ToString(),
            Name = "Mobile Banking App",
            Domain = "banking.example.com",
            Environment = "development",
            DefaultLanguageId = langEn.LanguageId
        };

        await _context.Apps.InsertManyAsync(new[] { ecommerceApp, blogApp, mobileApp }, cancellationToken: cancellationToken);

        // --- Modules ---
        var ecomAuth = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleKey = "auth", Name = "Authentication" };
        var ecomCart = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleKey = "cart", Name = "Shopping Cart" };
        var ecomCheckout = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleKey = "checkout", Name = "Checkout" };
        var ecomProducts = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleKey = "products", Name = "Products" };

        var blogPosts = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleKey = "posts", Name = "Blog Posts" };
        var blogComments = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleKey = "comments", Name = "Comments" };
        var blogAuth = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleKey = "auth", Name = "Authentication" };

        var bankDashboard = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleKey = "dashboard", Name = "Dashboard" };
        var bankTransfers = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleKey = "transfers", Name = "Transfers" };
        var bankAuth = new Module { ModuleId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleKey = "auth", Name = "Authentication" };

        var allModules = new[] { ecomAuth, ecomCart, ecomCheckout, ecomProducts, blogPosts, blogComments, blogAuth, bankDashboard, bankTransfers, bankAuth };
        await _context.Modules.InsertManyAsync(allModules, cancellationToken: cancellationToken);

        // --- AppUsers ---
        var appUsers = new[]
        {
            new AppUser { UserId = adminUser.UserId, AppId = ecommerceApp.AppId, Role = "admin" },
            new AppUser { UserId = adminUser.UserId, AppId = blogApp.AppId, Role = "admin" },
            new AppUser { UserId = adminUser.UserId, AppId = mobileApp.AppId, Role = "admin" }
        };
        await _context.AppUsers.InsertManyAsync(appUsers, cancellationToken: cancellationToken);

        // --- AppLanguages ---
        var appLanguages = new[]
        {
            // E-Commerce: English, Spanish, French, German
            new AppLanguage { AppId = ecommerceApp.AppId, LanguageId = langEn.LanguageId },
            new AppLanguage { AppId = ecommerceApp.AppId, LanguageId = langEs.LanguageId },
            new AppLanguage { AppId = ecommerceApp.AppId, LanguageId = langFr.LanguageId },
            new AppLanguage { AppId = ecommerceApp.AppId, LanguageId = langDe.LanguageId },
            // Blog: English, Spanish
            new AppLanguage { AppId = blogApp.AppId, LanguageId = langEn.LanguageId },
            new AppLanguage { AppId = blogApp.AppId, LanguageId = langEs.LanguageId },
            // Banking: English, Japanese, French
            new AppLanguage { AppId = mobileApp.AppId, LanguageId = langEn.LanguageId },
            new AppLanguage { AppId = mobileApp.AppId, LanguageId = langJa.LanguageId },
            new AppLanguage { AppId = mobileApp.AppId, LanguageId = langFr.LanguageId }
        };
        await _context.AppLanguages.InsertManyAsync(appLanguages, cancellationToken: cancellationToken);

        // --- Translations ---
        var translations = new List<Translation>();

        // E-Commerce Auth translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "login.title", Text = "Sign In", Context = "Login page title" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "login.email", Text = "Email Address" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "login.password", Text = "Password" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "login.submit", Text = "Sign In" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "login.forgot_password", Text = "Forgot your password?" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "register.title", Text = "Create Account" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "register.submit", Text = "Sign Up" }
        });

        // E-Commerce Auth translations (Spanish)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "login.title", Text = "Iniciar Sesion" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "login.email", Text = "Correo Electronico" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "login.password", Text = "Contrasena" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "login.submit", Text = "Iniciar Sesion" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "login.forgot_password", Text = "Olvidaste tu contrasena?" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "register.title", Text = "Crear Cuenta" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomAuth.ModuleId, LanguageId = langEs.LanguageId, Key = "register.submit", Text = "Registrarse" }
        });

        // E-Commerce Cart translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEn.LanguageId, Key = "cart.title", Text = "Shopping Cart" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEn.LanguageId, Key = "cart.empty", Text = "Your cart is empty" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEn.LanguageId, Key = "cart.total", Text = "Total" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEn.LanguageId, Key = "cart.remove_item", Text = "Remove" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEn.LanguageId, Key = "cart.checkout", Text = "Proceed to Checkout" }
        });

        // E-Commerce Cart translations (Spanish)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEs.LanguageId, Key = "cart.title", Text = "Carrito de Compras" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEs.LanguageId, Key = "cart.empty", Text = "Tu carrito esta vacio" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEs.LanguageId, Key = "cart.total", Text = "Total" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEs.LanguageId, Key = "cart.remove_item", Text = "Eliminar" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCart.ModuleId, LanguageId = langEs.LanguageId, Key = "cart.checkout", Text = "Proceder al Pago" }
        });

        // E-Commerce Products translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.title", Text = "Products" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.search", Text = "Search products..." },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.add_to_cart", Text = "Add to Cart" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.out_of_stock", Text = "Out of Stock" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.price", Text = "Price" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomProducts.ModuleId, LanguageId = langEn.LanguageId, Key = "products.description", Text = "Description" }
        });

        // E-Commerce Checkout translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCheckout.ModuleId, LanguageId = langEn.LanguageId, Key = "checkout.title", Text = "Checkout" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCheckout.ModuleId, LanguageId = langEn.LanguageId, Key = "checkout.shipping", Text = "Shipping Address" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCheckout.ModuleId, LanguageId = langEn.LanguageId, Key = "checkout.payment", Text = "Payment Method" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCheckout.ModuleId, LanguageId = langEn.LanguageId, Key = "checkout.place_order", Text = "Place Order" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = ecommerceApp.AppId, ModuleId = ecomCheckout.ModuleId, LanguageId = langEn.LanguageId, Key = "checkout.order_summary", Text = "Order Summary" }
        });

        // Blog Posts translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEn.LanguageId, Key = "posts.title", Text = "Latest Posts" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEn.LanguageId, Key = "posts.read_more", Text = "Read More" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEn.LanguageId, Key = "posts.published_on", Text = "Published on" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEn.LanguageId, Key = "posts.author", Text = "Author" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEn.LanguageId, Key = "posts.no_posts", Text = "No posts found" }
        });

        // Blog Posts translations (Spanish)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEs.LanguageId, Key = "posts.title", Text = "Ultimas Publicaciones" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEs.LanguageId, Key = "posts.read_more", Text = "Leer Mas" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEs.LanguageId, Key = "posts.published_on", Text = "Publicado el" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEs.LanguageId, Key = "posts.author", Text = "Autor" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogPosts.ModuleId, LanguageId = langEs.LanguageId, Key = "posts.no_posts", Text = "No se encontraron publicaciones" }
        });

        // Blog Comments translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogComments.ModuleId, LanguageId = langEn.LanguageId, Key = "comments.title", Text = "Comments" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogComments.ModuleId, LanguageId = langEn.LanguageId, Key = "comments.add", Text = "Add a Comment" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogComments.ModuleId, LanguageId = langEn.LanguageId, Key = "comments.submit", Text = "Post Comment" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = blogApp.AppId, ModuleId = blogComments.ModuleId, LanguageId = langEn.LanguageId, Key = "comments.no_comments", Text = "No comments yet" }
        });

        // Banking Dashboard translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langEn.LanguageId, Key = "dashboard.title", Text = "Dashboard" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langEn.LanguageId, Key = "dashboard.balance", Text = "Current Balance" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langEn.LanguageId, Key = "dashboard.recent_transactions", Text = "Recent Transactions" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langEn.LanguageId, Key = "dashboard.accounts", Text = "My Accounts" }
        });

        // Banking Dashboard translations (Japanese)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langJa.LanguageId, Key = "dashboard.title", Text = "ダッシュボード" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langJa.LanguageId, Key = "dashboard.balance", Text = "現在の残高" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langJa.LanguageId, Key = "dashboard.recent_transactions", Text = "最近の取引" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankDashboard.ModuleId, LanguageId = langJa.LanguageId, Key = "dashboard.accounts", Text = "マイアカウント" }
        });

        // Banking Transfers translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.title", Text = "Transfer Money" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.from", Text = "From Account" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.to", Text = "To Account" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.amount", Text = "Amount" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.submit", Text = "Send Transfer" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankTransfers.ModuleId, LanguageId = langEn.LanguageId, Key = "transfers.success", Text = "Transfer completed successfully" }
        });

        // Banking Auth translations (English)
        translations.AddRange(new[]
        {
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "auth.login", Text = "Log In" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "auth.pin", Text = "Enter PIN" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "auth.biometric", Text = "Use Fingerprint" },
            new Translation { TranslationId = Guid.NewGuid().ToString(), AppId = mobileApp.AppId, ModuleId = bankAuth.ModuleId, LanguageId = langEn.LanguageId, Key = "auth.logout", Text = "Log Out" }
        });

        await _context.Translations.InsertManyAsync(translations, cancellationToken: cancellationToken);
    }
}
