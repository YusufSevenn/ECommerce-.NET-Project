namespace ECommerce.Core.RequestParameters
{
    public class PaginationParams
    {
        //İstek başına çekilecek maksimum kayıt sayısı
        private const int MaxPageSize = 50;

        //Varsayılan sayfa boyutu 
        private int _pageSize = 10;

        //Varsayılan olarak 1.sayfayı getir
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;

            //İstenen boyut maksimumdan büyükse, maksimmum değeri kullan
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}