namespace AlisRestaurant.Enum;

public enum OrderStatus
{
    Open = 1,        // masa açılıb, sifariş davam edir
    SentToKitchen = 2, // mətbəxə göndərilib
    Preparing = 3,   // hazırlanır
    Ready = 4,       // hazırdır
    Served = 5,      // masaya gətirildi
    Paid = 6,        // ödəndi
    Closed = 7       // sifariş bağlandı
}
