// kitapTakip.js

// Yeni Ekleme butonunun durumunu kontrol eden fonksiyon
function kontrolEtEkleButonDurumu() {
    // Örneğin, KitapAdi, KitapTuruId gibi gerekli alanların değerlerini kontrol edebilirsiniz
    var kitapAdi = $('#KitapAdi').val();
    var kitapTuruId = $('#KitapTuruId').val();

    // KitapAdi ve KitapTuruId boşsa veya undefined ise, ekle butonunu pasif yap
    if (!kitapAdi || !kitapTuruId) {
        $('#ekleButton').prop('disabled', true);
    } else {
        $('#ekleButton').prop('disabled', false);
    }
}

// KitapAdi ve KitapTuruId alanlarında herhangi bir değişiklik olduğunda çalışacak fonksiyon
$('#KitapAdi, #KitapTuruId').on('input', function () {
    // KitapAdi ve KitapTuruId alanlarında herhangi bir değişiklik olduğunda ekle butonunun durumunu kontrol et
    kontrolEtEkleButonDurumu();
});
