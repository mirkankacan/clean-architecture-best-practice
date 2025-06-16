namespace CleanArchitecture.WebApi.Constants
{
    public static class ErrorMessages
    {
        // Validation Errors
        public const string ValidationFailed = "Doğrulama başarısız";

        public const string RequiredField = "Bu alan zorunludur";
        public const string InvalidEmail = "Geçerli bir e-posta adresi giriniz";
        public const string InvalidPhoneNumber = "Geçerli bir telefon numarası giriniz";
        public const string PasswordTooShort = "Şifre en az 8 karakter olmalıdır";
        public const string InvalidDateFormat = "Geçerli bir tarih formatı giriniz";

        // Authentication & Authorization
        public const string UnauthorizedAccess = "Yetkisiz erişim";

        public const string InvalidCredentials = "Kullanıcı adı veya şifre hatalı";
        public const string AccountLocked = "Hesabınız kilitlenmiştir";
        public const string TokenExpired = "Oturum süresi dolmuş";
        public const string InsufficientPermissions = "Bu işlem için yetkiniz bulunmamaktadır";

        // Not Found Errors
        public const string ResourceNotFound = "Kaynak bulunamadı";

        public const string UserNotFound = "Kullanıcı bulunamadı";
        public const string RecordNotFound = "Kayıt bulunamadı";

        // Business Logic Errors
        public const string InvalidArgument = "Geçersiz parametre sağlandı";

        public const string OperationNotAllowed = "Bu işleme izin verilmemektedir";
        public const string DuplicateRecord = "Bu kayıt zaten mevcut";
        public const string InsufficientBalance = "Yetersiz bakiye";

        // Server Errors
        public const string InternalServerError = "Bir iç sunucu hatası oluştu";

        public const string DatabaseConnectionError = "Veritabanı bağlantı hatası";
        public const string ExternalServiceError = "Harici servis hatası";
        public const string ServiceUnavailable = "Servis şu anda kullanılamıyor";

        // File Operations
        public const string FileNotFound = "Dosya bulunamadı";

        public const string InvalidFileFormat = "Geçersiz dosya formatı";
        public const string FileSizeExceeded = "Dosya boyutu limiti aşıldı";
        public const string UploadFailed = "Dosya yükleme başarısız";

        // Network Errors
        public const string ConnectionTimeout = "Bağlantı zaman aşımı";

        public const string NetworkError = "Ağ hatası";
        public const string RequestTimeout = "İstek zaman aşımı";
    }
}