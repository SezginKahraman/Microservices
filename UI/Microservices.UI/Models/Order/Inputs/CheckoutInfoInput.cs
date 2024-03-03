using System.ComponentModel.DataAnnotations;

namespace Microservices.UI.Models.Order.Inputs
{
    public class CheckoutInfoInput
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string PhoneNumber { get; set; }

        public string PostalCode { get; set; }

        public string PostOffice { get; set; }

        public string PostOfficeNumber { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;

        public string PhoneCode { get; set; } = string.Empty;

        public string PhoneNumberCode { get; set; } = string.Empty;

        public string Province { get; set; }

        [Display(Name = "İlçe")]
        public string District { get; set; }

        [Display(Name = "Sokak")]
        public string Street { get; set; }

        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "Adres")]
        public string Line { get; set; } = string.Empty;

        #region Card

        [Display(Name = "Kart üzerinde yazan isim")]
        public string CardName { get; set; }

        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "Son Kullanma Tarihi")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2 Numarası")]
        public string CVV { get; set; }

        public decimal TotalPrice { get; set; }

        #endregion


    }
}
