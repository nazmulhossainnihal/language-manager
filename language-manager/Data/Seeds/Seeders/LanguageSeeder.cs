using language_manager.Data.Context;
using language_manager.Model.Domain;

namespace language_manager.Data.Seeds.Seeders;

/// <summary>
/// Seeds the database with a comprehensive list of languages.
/// Based on ISO 639-1 language codes with native names.
/// </summary>
public class LanguageSeeder : ISeeder
{
    private readonly MongoDbContext _context;

    public LanguageSeeder(MongoDbContext context)
    {
        _context = context;
    }

    public string Name => "001_LanguageSeeder";
    public int Order => 1;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var languages = GetLanguages();
        await _context.Languages.InsertManyAsync(languages, cancellationToken: cancellationToken);
    }

    private static List<Language> GetLanguages()
    {
        return new List<Language>
        {
            // Major World Languages
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en", Name = "English" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-US", Name = "English (United States)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-GB", Name = "English (United Kingdom)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-AU", Name = "English (Australia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-CA", Name = "English (Canada)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-IN", Name = "English (India)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-NZ", Name = "English (New Zealand)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-ZA", Name = "English (South Africa)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "en-IE", Name = "English (Ireland)" },

            // Chinese variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zh", Name = "Chinese (中文)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zh-CN", Name = "Chinese Simplified (简体中文)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zh-TW", Name = "Chinese Traditional (繁體中文)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zh-HK", Name = "Chinese (Hong Kong)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zh-SG", Name = "Chinese (Singapore)" },

            // Spanish variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es", Name = "Spanish (Español)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-ES", Name = "Spanish (Spain)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-MX", Name = "Spanish (Mexico)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-AR", Name = "Spanish (Argentina)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-CO", Name = "Spanish (Colombia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-CL", Name = "Spanish (Chile)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-PE", Name = "Spanish (Peru)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-VE", Name = "Spanish (Venezuela)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "es-419", Name = "Spanish (Latin America)" },

            // Portuguese variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pt", Name = "Portuguese (Português)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pt-BR", Name = "Portuguese (Brazil)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pt-PT", Name = "Portuguese (Portugal)" },

            // French variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fr", Name = "French (Français)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fr-FR", Name = "French (France)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fr-CA", Name = "French (Canada)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fr-BE", Name = "French (Belgium)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fr-CH", Name = "French (Switzerland)" },

            // German variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "de", Name = "German (Deutsch)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "de-DE", Name = "German (Germany)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "de-AT", Name = "German (Austria)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "de-CH", Name = "German (Switzerland)" },

            // Arabic variants
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar", Name = "Arabic (العربية)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-SA", Name = "Arabic (Saudi Arabia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-EG", Name = "Arabic (Egypt)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-AE", Name = "Arabic (UAE)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-MA", Name = "Arabic (Morocco)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-DZ", Name = "Arabic (Algeria)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-TN", Name = "Arabic (Tunisia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-IQ", Name = "Arabic (Iraq)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-JO", Name = "Arabic (Jordan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-LB", Name = "Arabic (Lebanon)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ar-SY", Name = "Arabic (Syria)" },

            // Hindi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hi", Name = "Hindi (हिन्दी)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hi-IN", Name = "Hindi (India)" },

            // Bengali
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bn", Name = "Bengali (বাংলা)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bn-BD", Name = "Bengali (Bangladesh)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bn-IN", Name = "Bengali (India)" },

            // Russian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ru", Name = "Russian (Русский)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ru-RU", Name = "Russian (Russia)" },

            // Japanese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ja", Name = "Japanese (日本語)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ja-JP", Name = "Japanese (Japan)" },

            // Korean
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ko", Name = "Korean (한국어)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ko-KR", Name = "Korean (South Korea)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ko-KP", Name = "Korean (North Korea)" },

            // Italian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "it", Name = "Italian (Italiano)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "it-IT", Name = "Italian (Italy)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "it-CH", Name = "Italian (Switzerland)" },

            // Dutch
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nl", Name = "Dutch (Nederlands)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nl-NL", Name = "Dutch (Netherlands)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nl-BE", Name = "Dutch (Belgium)" },

            // Turkish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tr", Name = "Turkish (Türkçe)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tr-TR", Name = "Turkish (Turkey)" },

            // Polish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pl", Name = "Polish (Polski)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pl-PL", Name = "Polish (Poland)" },

            // Ukrainian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uk", Name = "Ukrainian (Українська)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uk-UA", Name = "Ukrainian (Ukraine)" },

            // Vietnamese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "vi", Name = "Vietnamese (Tiếng Việt)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "vi-VN", Name = "Vietnamese (Vietnam)" },

            // Thai
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "th", Name = "Thai (ไทย)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "th-TH", Name = "Thai (Thailand)" },

            // Greek
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "el", Name = "Greek (Ελληνικά)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "el-GR", Name = "Greek (Greece)" },

            // Czech
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "cs", Name = "Czech (Čeština)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "cs-CZ", Name = "Czech (Czech Republic)" },

            // Romanian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ro", Name = "Romanian (Română)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ro-RO", Name = "Romanian (Romania)" },

            // Hungarian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hu", Name = "Hungarian (Magyar)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hu-HU", Name = "Hungarian (Hungary)" },

            // Swedish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sv", Name = "Swedish (Svenska)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sv-SE", Name = "Swedish (Sweden)" },

            // Norwegian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "no", Name = "Norwegian (Norsk)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nb", Name = "Norwegian Bokmål (Norsk Bokmål)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nb-NO", Name = "Norwegian Bokmål (Norway)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nn", Name = "Norwegian Nynorsk (Norsk Nynorsk)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nn-NO", Name = "Norwegian Nynorsk (Norway)" },

            // Danish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "da", Name = "Danish (Dansk)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "da-DK", Name = "Danish (Denmark)" },

            // Finnish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fi", Name = "Finnish (Suomi)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fi-FI", Name = "Finnish (Finland)" },

            // Slovak
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sk", Name = "Slovak (Slovenčina)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sk-SK", Name = "Slovak (Slovakia)" },

            // Bulgarian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bg", Name = "Bulgarian (Български)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bg-BG", Name = "Bulgarian (Bulgaria)" },

            // Croatian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hr", Name = "Croatian (Hrvatski)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hr-HR", Name = "Croatian (Croatia)" },

            // Serbian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sr", Name = "Serbian (Српски)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sr-RS", Name = "Serbian (Serbia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sr-Latn", Name = "Serbian (Latin)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sr-Cyrl", Name = "Serbian (Cyrillic)" },

            // Slovenian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sl", Name = "Slovenian (Slovenščina)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sl-SI", Name = "Slovenian (Slovenia)" },

            // Estonian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "et", Name = "Estonian (Eesti)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "et-EE", Name = "Estonian (Estonia)" },

            // Latvian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lv", Name = "Latvian (Latviešu)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lv-LV", Name = "Latvian (Latvia)" },

            // Lithuanian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lt", Name = "Lithuanian (Lietuvių)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lt-LT", Name = "Lithuanian (Lithuania)" },

            // Hebrew
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "he", Name = "Hebrew (עברית)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "he-IL", Name = "Hebrew (Israel)" },

            // Persian/Farsi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fa", Name = "Persian (فارسی)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fa-IR", Name = "Persian (Iran)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fa-AF", Name = "Dari (Afghanistan)" },

            // Urdu
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ur", Name = "Urdu (اردو)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ur-PK", Name = "Urdu (Pakistan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ur-IN", Name = "Urdu (India)" },

            // Indonesian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "id", Name = "Indonesian (Bahasa Indonesia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "id-ID", Name = "Indonesian (Indonesia)" },

            // Malay
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ms", Name = "Malay (Bahasa Melayu)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ms-MY", Name = "Malay (Malaysia)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ms-SG", Name = "Malay (Singapore)" },

            // Filipino/Tagalog
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fil", Name = "Filipino (Filipino)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tl", Name = "Tagalog (Tagalog)" },

            // Swahili
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sw", Name = "Swahili (Kiswahili)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sw-KE", Name = "Swahili (Kenya)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sw-TZ", Name = "Swahili (Tanzania)" },

            // Afrikaans
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "af", Name = "Afrikaans (Afrikaans)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "af-ZA", Name = "Afrikaans (South Africa)" },

            // Zulu
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zu", Name = "Zulu (isiZulu)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "zu-ZA", Name = "Zulu (South Africa)" },

            // Xhosa
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "xh", Name = "Xhosa (isiXhosa)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "xh-ZA", Name = "Xhosa (South Africa)" },

            // Amharic
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "am", Name = "Amharic (አማርኛ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "am-ET", Name = "Amharic (Ethiopia)" },

            // Hausa
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ha", Name = "Hausa (Hausa)" },

            // Yoruba
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "yo", Name = "Yoruba (Yorùbá)" },

            // Igbo
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ig", Name = "Igbo (Igbo)" },

            // Tamil
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ta", Name = "Tamil (தமிழ்)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ta-IN", Name = "Tamil (India)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ta-LK", Name = "Tamil (Sri Lanka)" },

            // Telugu
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "te", Name = "Telugu (తెలుగు)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "te-IN", Name = "Telugu (India)" },

            // Kannada
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kn", Name = "Kannada (ಕನ್ನಡ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kn-IN", Name = "Kannada (India)" },

            // Malayalam
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ml", Name = "Malayalam (മലയാളം)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ml-IN", Name = "Malayalam (India)" },

            // Marathi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mr", Name = "Marathi (मराठी)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mr-IN", Name = "Marathi (India)" },

            // Gujarati
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gu", Name = "Gujarati (ગુજરાતી)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gu-IN", Name = "Gujarati (India)" },

            // Punjabi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pa", Name = "Punjabi (ਪੰਜਾਬੀ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pa-IN", Name = "Punjabi (India)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pa-PK", Name = "Punjabi (Pakistan)" },

            // Odia/Oriya
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "or", Name = "Odia (ଓଡ଼ିଆ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "or-IN", Name = "Odia (India)" },

            // Assamese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "as", Name = "Assamese (অসমীয়া)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "as-IN", Name = "Assamese (India)" },

            // Nepali
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ne", Name = "Nepali (नेपाली)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ne-NP", Name = "Nepali (Nepal)" },

            // Sinhala
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "si", Name = "Sinhala (සිංහල)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "si-LK", Name = "Sinhala (Sri Lanka)" },

            // Burmese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "my", Name = "Burmese (မြန်မာ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "my-MM", Name = "Burmese (Myanmar)" },

            // Khmer
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "km", Name = "Khmer (ខ្មែរ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "km-KH", Name = "Khmer (Cambodia)" },

            // Lao
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lo", Name = "Lao (ລາວ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lo-LA", Name = "Lao (Laos)" },

            // Georgian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ka", Name = "Georgian (ქართული)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ka-GE", Name = "Georgian (Georgia)" },

            // Armenian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hy", Name = "Armenian (Հայերdelays)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hy-AM", Name = "Armenian (Armenia)" },

            // Azerbaijani
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "az", Name = "Azerbaijani (Azərbaycan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "az-AZ", Name = "Azerbaijani (Azerbaijan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "az-Latn", Name = "Azerbaijani (Latin)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "az-Cyrl", Name = "Azerbaijani (Cyrillic)" },

            // Kazakh
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kk", Name = "Kazakh (Қазақ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kk-KZ", Name = "Kazakh (Kazakhstan)" },

            // Uzbek
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uz", Name = "Uzbek (O'zbek)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uz-UZ", Name = "Uzbek (Uzbekistan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uz-Latn", Name = "Uzbek (Latin)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "uz-Cyrl", Name = "Uzbek (Cyrillic)" },

            // Tajik
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tg", Name = "Tajik (Тоҷикӣ)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tg-TJ", Name = "Tajik (Tajikistan)" },

            // Kyrgyz
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ky", Name = "Kyrgyz (Кыргызча)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ky-KG", Name = "Kyrgyz (Kyrgyzstan)" },

            // Turkmen
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tk", Name = "Turkmen (Türkmen)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tk-TM", Name = "Turkmen (Turkmenistan)" },

            // Mongolian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mn", Name = "Mongolian (Монгол)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mn-MN", Name = "Mongolian (Mongolia)" },

            // Tibetan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bo", Name = "Tibetan (བོད་སྐད)" },

            // Uyghur
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ug", Name = "Uyghur (ئۇيغۇرچە)" },

            // Pashto
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ps", Name = "Pashto (پښتو)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ps-AF", Name = "Pashto (Afghanistan)" },

            // Kurdish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ku", Name = "Kurdish (Kurdî)" },

            // Basque
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "eu", Name = "Basque (Euskara)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "eu-ES", Name = "Basque (Spain)" },

            // Catalan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ca", Name = "Catalan (Català)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ca-ES", Name = "Catalan (Spain)" },

            // Galician
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gl", Name = "Galician (Galego)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gl-ES", Name = "Galician (Spain)" },

            // Welsh
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "cy", Name = "Welsh (Cymraeg)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "cy-GB", Name = "Welsh (United Kingdom)" },

            // Irish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ga", Name = "Irish (Gaeilge)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ga-IE", Name = "Irish (Ireland)" },

            // Scottish Gaelic
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gd", Name = "Scottish Gaelic (Gàidhlig)" },

            // Breton
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "br", Name = "Breton (Brezhoneg)" },

            // Maltese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mt", Name = "Maltese (Malti)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mt-MT", Name = "Maltese (Malta)" },

            // Icelandic
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "is", Name = "Icelandic (Íslenska)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "is-IS", Name = "Icelandic (Iceland)" },

            // Albanian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sq", Name = "Albanian (Shqip)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sq-AL", Name = "Albanian (Albania)" },

            // Macedonian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mk", Name = "Macedonian (Македонски)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mk-MK", Name = "Macedonian (North Macedonia)" },

            // Bosnian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bs", Name = "Bosnian (Bosanski)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bs-BA", Name = "Bosnian (Bosnia and Herzegovina)" },

            // Belarusian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "be", Name = "Belarusian (Беларуская)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "be-BY", Name = "Belarusian (Belarus)" },

            // Luxembourgish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lb", Name = "Luxembourgish (Lëtzebuergesch)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lb-LU", Name = "Luxembourgish (Luxembourg)" },

            // Esperanto (constructed language)
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "eo", Name = "Esperanto (Esperanto)" },

            // Latin
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "la", Name = "Latin (Latina)" },

            // Sanskrit
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sa", Name = "Sanskrit (संस्कृतम्)" },

            // Yiddish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "yi", Name = "Yiddish (ייִדיש)" },

            // Maori
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mi", Name = "Maori (Te Reo Māori)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mi-NZ", Name = "Maori (New Zealand)" },

            // Hawaiian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "haw", Name = "Hawaiian (ʻŌlelo Hawaiʻi)" },

            // Samoan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sm", Name = "Samoan (Gagana Samoa)" },

            // Tongan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "to", Name = "Tongan (Lea Faka-Tonga)" },

            // Fijian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fj", Name = "Fijian (Vosa Vakaviti)" },

            // Javanese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "jv", Name = "Javanese (Basa Jawa)" },

            // Sundanese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "su", Name = "Sundanese (Basa Sunda)" },

            // Cebuano
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ceb", Name = "Cebuano (Binisaya)" },

            // Hmong
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hmn", Name = "Hmong (Hmoob)" },

            // Corsican
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "co", Name = "Corsican (Corsu)" },

            // Frisian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fy", Name = "Frisian (Frysk)" },

            // Faroese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fo", Name = "Faroese (Føroyskt)" },

            // Romansh
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "rm", Name = "Romansh (Rumantsch)" },

            // Occitan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "oc", Name = "Occitan (Occitan)" },

            // Aragonese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "an", Name = "Aragonese (Aragonés)" },

            // Asturian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ast", Name = "Asturian (Asturianu)" },

            // Sardinian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sc", Name = "Sardinian (Sardu)" },

            // Sicilian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "scn", Name = "Sicilian (Sicilianu)" },

            // Friulian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "fur", Name = "Friulian (Furlan)" },

            // Haitian Creole
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ht", Name = "Haitian Creole (Kreyòl ayisyen)" },

            // Somali
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "so", Name = "Somali (Soomaali)" },

            // Tigrinya
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ti", Name = "Tigrinya (ትግርኛ)" },

            // Oromo
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "om", Name = "Oromo (Oromoo)" },

            // Shona
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sn", Name = "Shona (chiShona)" },

            // Chichewa
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ny", Name = "Chichewa (Chichewa)" },

            // Malagasy
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mg", Name = "Malagasy (Malagasy)" },

            // Kinyarwanda
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "rw", Name = "Kinyarwanda (Ikinyarwanda)" },

            // Sesotho
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "st", Name = "Sesotho (Sesotho)" },

            // Setswana
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tn", Name = "Setswana (Setswana)" },

            // Lingala
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ln", Name = "Lingala (Lingála)" },

            // Wolof
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "wo", Name = "Wolof (Wolof)" },

            // Fulah
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ff", Name = "Fulah (Fulfulde)" },

            // Bambara
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bm", Name = "Bambara (Bamanankan)" },

            // Ewe
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ee", Name = "Ewe (Eʋegbe)" },

            // Akan/Twi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ak", Name = "Akan (Akan)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tw", Name = "Twi (Twi)" },

            // Luganda
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lg", Name = "Luganda (Luganda)" },

            // Guarani
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gn", Name = "Guarani (Avañe'ẽ)" },

            // Quechua
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "qu", Name = "Quechua (Runasimi)" },

            // Aymara
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ay", Name = "Aymara (Aymar aru)" },

            // Nahuatl
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nah", Name = "Nahuatl (Nāhuatl)" },

            // Mayan languages
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "yua", Name = "Yucatec Maya (Màaya t'àan)" },

            // Cherokee
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "chr", Name = "Cherokee (ᏣᎳᎩ)" },

            // Navajo
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "nv", Name = "Navajo (Diné bizaad)" },

            // Inuktitut
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "iu", Name = "Inuktitut (ᐃᓄᒃᑎᑐᑦ)" },

            // Greenlandic
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kl", Name = "Greenlandic (Kalaallisut)" },

            // Sami
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "se", Name = "Northern Sami (Davvisámegiella)" },

            // Sindhi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sd", Name = "Sindhi (سنڌي)" },

            // Konkani
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kok", Name = "Konkani (कोंकणी)" },

            // Kashmiri
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ks", Name = "Kashmiri (کٲشُر)" },

            // Dogri
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "doi", Name = "Dogri (डोगरी)" },

            // Manipuri
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mni", Name = "Manipuri (মৈতৈলোন্)" },

            // Bodo
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "brx", Name = "Bodo (बड़ो)" },

            // Santali
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sat", Name = "Santali (ᱥᱟᱱᱛᱟᱲᱤ)" },

            // Maithili
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mai", Name = "Maithili (मैथिली)" },

            // Bhojpuri
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bho", Name = "Bhojpuri (भोजपुरी)" },

            // Dhivehi/Maldivian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "dv", Name = "Dhivehi (ދިވެހި)" },

            // Dzongkha
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "dz", Name = "Dzongkha (རྫོང་ཁ)" },

            // Tongan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ton", Name = "Tongan (Lea fakatonga)" },

            // Tahitian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ty", Name = "Tahitian (Reo Tahiti)" },

            // Chamorro
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ch", Name = "Chamorro (Chamoru)" },

            // Marshallese
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mh", Name = "Marshallese (Ebon)" },

            // Tetum
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tet", Name = "Tetum (Tetun)" },

            // Tok Pisin
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tpi", Name = "Tok Pisin (Tok Pisin)" },

            // Bislama
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bi", Name = "Bislama (Bislama)" },

            // Papiamento
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "pap", Name = "Papiamento (Papiamentu)" },

            // Tatar
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tt", Name = "Tatar (Татарча)" },

            // Bashkir
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ba", Name = "Bashkir (Башҡортса)" },

            // Chuvash
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "cv", Name = "Chuvash (Чӑвашла)" },

            // Chechen
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ce", Name = "Chechen (Нохчийн)" },

            // Ossetian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "os", Name = "Ossetian (Ирон)" },

            // Abkhaz
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "ab", Name = "Abkhaz (Аԥсуа)" },

            // Avar
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "av", Name = "Avar (Авар)" },

            // Lezgian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "lez", Name = "Lezgian (Лезги)" },

            // Komi
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kv", Name = "Komi (Коми)" },

            // Udmurt
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "udm", Name = "Udmurt (Удмурт)" },

            // Mari
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mhr", Name = "Mari (Марий)" },

            // Moksha
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "mdf", Name = "Moksha (Мокшень)" },

            // Erzya
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "myv", Name = "Erzya (Эрзянь)" },

            // Sakha/Yakut
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "sah", Name = "Sakha (Саха тыла)" },

            // Buryat
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "bua", Name = "Buryat (Буряад)" },

            // Tuvan
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "tyv", Name = "Tuvan (Тыва)" },

            // Altai
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "alt", Name = "Altai (Алтай)" },

            // Khakas
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kjh", Name = "Khakas (Хакас)" },

            // Sorbian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "hsb", Name = "Upper Sorbian (Hornjoserbsce)" },
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "dsb", Name = "Lower Sorbian (Dolnoserbski)" },

            // Kashubian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "csb", Name = "Kashubian (Kaszëbsczi)" },

            // Silesian
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "szl", Name = "Silesian (Ślůnski)" },

            // Rusyn
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "rue", Name = "Rusyn (Русиньскый)" },

            // Manx
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "gv", Name = "Manx (Gaelg)" },

            // Cornish
            new() { LanguageId = Guid.NewGuid().ToString(), LanguageKey = "kw", Name = "Cornish (Kernewek)" }
        };
    }
}
