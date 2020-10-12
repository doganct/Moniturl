using System;
using System.Collections.Generic;
using System.Text;

namespace Moniturl.Core
{
    public class Messages
    {
        public const string Forbidden = "Bu işlem için yetkiniz bulunmamaktadır.";
        public const string ThisEmailAddressIsNotAvailable = "Bu email adresi kullanılmaktadır.";
        public const string CheckYourEmailAddressOrPassword = "Email adresinizi veya şifrenizi kontrol ediniz.";
        public const string PleaseEnterEmailAddressCorrectFormat = "Lütfen uygun formatta bir email giriniz.";
        public const string CheckYourUrl = "Lütfen uygun formatta bir url giriniz.";
        public const string PleaseFillEmail = "Lütfen email alanını doldurunuz.";
        public const string PleaseFillPassword = "Lütfen şifre alanını doldurunuz.";
        public const string ThisFieldIsRequired = "Bu alan zorunludur.";
        public const string EmailField = "E-mail Adresi";
        public const string PasswordField = "Şifre";
        public const string Name = "Ad";
        public const string Url = "Url";
        public const string Interval = "Kontrol Süresi";
        public const string IntervalMustBeMinimumOne = "Kontrol Süresi en az 1 olabilir.";
        public const string Edit = "Düzenle";
        public const string Delete = "Sil";
        public const string TargetMailSubject = "Konu";
        public static string TargetMailBody(string name, string url, int interval, DateTime dateTime)
        {
            return $"Takip ettiğiniz {name} isimli {url} adresine {dateTime.ToString("dd.MM.yyyy HH:mm")} tarihinde erişim sağlanamamıştır. {interval} dakika sonra tekrar denenecektir.";
        }
    }
}
