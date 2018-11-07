
$('.arama-tur-secilmedi+.input-group-btn').click(function (e) {
    var error = document.getElementsByClassName("arama-tur-secilmedi");
    for (var i = 0; i < error.length; i++) {
        error[i].style.display = "none";
    }
});
$('#fototabi').click(function (e) {
    e.preventDefault();
    var first = document.getElementById("first");
    var second = document.getElementById("second");
    first.classList.remove("active");
    second.classList.add("active");
});
$('#yorumtabi').click(function (e) {
    e.preventDefault();
    var first = document.getElementById("first");
    var second = document.getElementById("second");
    second.classList.remove("active");
    first.classList.add("active");
});


/*mvc de yazıldı*/
var sehirler, ulke_id, eyalet_id;
function UlkeDegisti(e, sehir_control_id) {
    ulke_id = e.target.value;
    var select = document.getElementById(sehir_control_id), drpeyaletler = document.getElementById("drpeyaletler"), eyalet_ust = document.getElementById("eyalet_ust"),
    drpeyaletler1 = document.getElementById("drpeyaletler1"), eyalet_ust1 = document.getElementById("eyalet_ust1");
    if (select.select2 != null)
        select.select2('destroy');
    select.innerHTML = '<option value="-1">...</option>';
    if (ulke_id != 221) {
        if (e.target.id != 'on_kayit_yasadigi_ulke')
            eyalet_ust.style.display = "none";
        else
            eyalet_ust1.style.display = "none";
        SehirleriGetir(select);
    }
    else {
        if (e.target.id != 'on_kayit_yasadigi_ulke') {
            eyalet_ust.style.display = "block";
            EyaletGetir(drpeyaletler);
        }
        else {
            eyalet_ust1.style.display = "block";
            EyaletGetir(drpeyaletler1);
        }
    }
}
function EyaletGetir(yuklenecek) {
    if (yuklenecek.select2 != null)
        yuklenecek.select2('destroy');
    var seciniz = yuklenecek.options[0].text;
    yuklenecek.innerHTML = '<option>...</option>';
    $.ajax({
        type: "Post",
        url: "/EyaletGetir",
        data: { ulke_id: ulke_id },
        success: function (result) {
            yuklenecek.innerHTML = result;
            $('#' + yuklenecek.id).select2();
            $("#" + yuklenecek.id).trigger('change');
        }
    });
}
function EyaletDegisti(e, sehir_control_id) {
    eyalet_id = e.target.value;
    var select = document.getElementById(sehir_control_id);
    select.innerHTML = '<option>...</option>';
    EyaletSehirleriGetir(select);
}
function EyaletSehirleriGetir(eleman) {
    $.ajax({
        type: "Post",
        url: "/EyaletSehirleriGetir",
        data: { e_id: eyalet_id },
        success: function (result) {
            eleman.innerHTML = result;
            if (eleman.id == 'on_kayit_dogum_sehir' || eleman.id == 'on_kayit_yasadigi_sehir')
                eleman.remove(0);
        }
    });
}
function SehirleriGetir(eleman) {
    if (eleman.id == 'kurumsal_kayit_sehir') {
        $.ajax({
            type: "Post",
            url: "/SehirleriGetirTextValue",
            data: { u_id: ulke_id },
            success: function (result) {
                var newSource = JSON.parse(result), okultype = $('#kurumsal_kayit_sehir').typeahead({ hint: true, highlight: true, minLength: 1, limit: 15, displayText: function (item) { return item.text; }, });
                okultype.data('typeahead').source = newSource;
            }
        });
    }
    else {
        $.ajax({
            type: "Post",
            url: "/SehirleriGetir",
            data: { u_id: ulke_id },
            success: function (result) {
                eleman.innerHTML = result;
                $('#' + eleman.id).select2();
                if (eleman.id == 'on_kayit_dogum_sehir' || eleman.id == 'on_kayit_yasadigi_sehir')
                    eleman.remove(0);
            }
        });
    }
}
function KurumsalKayitEgitimTuruDegisti(e) {
    var p_var = e.target.options[e.target.selectedIndex].getAttribute('data-program');
    $('#fakulte_secimi')[0].style.display = e.target.options[e.target.selectedIndex].getAttribute('data-v') == 1 ? "block" : "none";
    if ($('#kurumsal_kayit_program_ve_tur').length > 0)
        $('#kurumsal_kayit_program_ve_tur')[0].style.display = p_var == 0 ? "none" : "block";
    if (p_var == 1) {
        if ($('#kurumsal_kayit_programlar').length > 0) {
            KurumsalKayitProgramlarGetir();
        }
    }
    if ($('#kurumsal_kayit_okullar').length > 0) {
        KurumsalKayitOkullarGetir();
    }
}
function KurumsalKayitProgramlarGetir() {
    var kurumsal_kayit_egitimturu = document.getElementById("kurumsal_kayit_egitimturu");
    $('#kurumsal_kayit_programlar').html('<option>...</option>');
    $('#kurumsal_kayit_programtur').html('<option>...</option>');
    $.ajax({
        type: "Post",
        url: "/ProgramlariGetir",
        data: { egitim_id: kurumsal_kayit_egitimturu.value },
        success: function (result) {
            $('#kurumsal_kayit_program_ve_tur').html(result);
        }
    });

}
function KurumsalKayitOkullarGetir() {
    var kurumsal_kayit_egitimturu = document.getElementById("kurumsal_kayit_egitimturu");
    $('#kurumsal_kayit_okullar')[0].value = "";
    $.ajax({
        type: "Post",
        url: "/KurumsalKayitOkullar",
        data: { egitim_id: kurumsal_kayit_egitimturu.value },
        success: function (result) {
            var newSource = JSON.parse(result), okultype = $('#kurumsal_kayit_okullar').typeahead({ hint: true, highlight: true, minLength: 1, items: 15, displayText: function (item) { return item.text; }, });
            okultype.data('typeahead').source = newSource;
        }
    });
}
function KurumsalKayitYetkiliGonder(e) {
    e.preventDefault();
    var kurumsal_kayit_okullar = document.getElementById("kurumsal_kayit_okullar"),
        kurumsal_kayit_egitimturu = document.getElementById("kurumsal_kayit_egitimturu"),
        txtsoyadi = document.getElementById("txtsoyadi"),
        txtkurumsalemail = document.getElementById("txtkurumsalemail"),
        txtkurumsaltelefon = document.getElementById("txtkurumsaltelefon"),
        kurumsal_kayit_ulkeler = document.getElementById("kurumsal_kayit_ulkeler"),
        kurumsal_kayit_sehir = document.getElementById("kurumsal_kayit_sehir"),
        Kurumsalbasarilipanel = document.getElementById("Kurumsalbasarilipanel"),
        Kurumsalhatalipanel = document.getElementById("Kurumsalhatalipanel");
    Kurumsalbasarilipanel.style.display = Kurumsalhatalipanel.style.display = "none";
    txtkurumsaltelefon.value = txtkurumsaltelefon.value.replace(/_/g, "");
    BosKontrol(txtadi);
    BosKontrol(txtsoyadi);
    BosKontrol(txtkurumsalemail);
    BosKontrol(kurumsal_kayit_okullar);
    BosKontrol(kurumsal_kayit_ulkeler);
    BosKontrol(kurumsal_kayit_sehir);
    if (kurumsal_kayit_okullar.style.borderColor == "red") {
        kurumsal_kayit_okullar.focus();
        return;
    }
    if (kurumsal_kayit_ulkeler.style.borderColor == "red") {
        kurumsal_kayit_ulkeler.focus();
        return;
    }

    if (kurumsal_kayit_sehir.style.borderColor == "red") {
        kurumsal_kayit_sehir.focus();
        return;
    }

    if (txtadi.style.borderColor == "red") {
        txtadi.focus();
        return;
    }

    if (txtsoyadi.style.borderColor == "red") {
        txtsoyadi.focus();
        return;
    }

    if (txtkurumsalemail.style.borderColor == "red") {
        txtkurumsalemail.focus();
        return;
    }
    else {
        if (!validateEmail(txtkurumsalemail.value)) {
            txtkurumsalemail.style.borderColor = "red";
            txtkurumsalemail.focus();
            return;
        }
    }

    var frm = document.getElementById("kurumsal_kayit_ekle_form"), kurumsal_kayit_load = document.getElementById("kurumsal_kayit_load");
    var dat = objectifyForm(frm.getElementsByClassName("krmsl-js"));
    kurumsal_kayit_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/KurumsalKayitOkulEkle",
        data: dat,
        success: function (result) {
            kurumsal_kayit_load.style.display = "none";
            if (result == '1') {
                frm.classList.add("kurumsal_kayit_gonderildi")
                frm.reset();
                $('#yenigrup')[0].style.display = "block";
            }
            else {
                Kurumsalhatalipanel.style.display = "block";
            }
        }
    });
}
function KurumsalKayitOgrenciGonder(e) {
    e.preventDefault();
    var kurumsal_kayit_okullar = document.getElementById("kurumsal_kayit_okullar"),
        kurumsal_kayit_egitimturu = document.getElementById("kurumsal_kayit_egitimturu"),
        kurumsal_kayit_ulke = document.getElementById("kurumsal_kayit_ulkeler"),
        kurumsal_kayit_sehir = document.getElementById("kurumsal_kayit_sehir"),
        kurumsal_kayit_programlar = document.getElementById("kurumsal_kayit_programlar"),
        kurumsal_kayit_programtur = document.getElementById("kurumsal_kayit_programtur"),
        txtYildiz = document.getElementById("txtYildiz"),
        txtYorumBasligi = document.getElementById("txtYorumBasligi"),
        txtYorumicerik = document.getElementById("txtYorumicerik"),
        txtOkulBas = document.getElementById("txtOkulBas"),
        txtOkulBit = document.getElementById("txtOkulBit"),
        HalaDevamEdiyorChck = document.getElementById("HalaDevamEdiyorChck"),
        kurumsalfileupload1 = document.getElementById("kurumsalfileupload1"),
        kurumsalfileupload2 = document.getElementById("kurumsalfileupload2"),
        kurumsalfileupload3 = document.getElementById("kurumsalfileupload3"),
        kurumsal_data1 = document.getElementById("kurumsal_data1"),
        kurumsal_data2 = document.getElementById("kurumsal_data2"),
        kurumsal_data3 = document.getElementById("kurumsal_data3"),
        error = document.getElementById("rq"),
    Kurumsalbasarilipanel = document.getElementById("Kurumsalbasarilipanel"),
    tahhut = document.getElementById("tahhut"),
    Kurumsalhatalipanel = document.getElementById("Kurumsalhatalipanel"),
        panel4 = document.getElementById("panel4");
    Kurumsalbasarilipanel.style.display = Kurumsalhatalipanel.style.display = "none";

    BosKontrol(txtYorumBasligi);
    BosKontrol(txtYorumicerik);
    BosKontrol(txtOkulBas);
    BosKontrol(kurumsal_kayit_okullar);
    BosKontrol(kurumsal_kayit_ulkeler);
    BosKontrol(kurumsal_kayit_sehir);
    panel4.style.display = "none";
    if (kurumsal_kayit_okullar.style.borderColor == "red") {
        kurumsal_kayit_okullar.focus();
        return;
    }
    if ($('#kurumsal_kayit_program_ve_tur')[0].style.display != "none") {
        if (kurumsal_kayit_programlar.value == "-1") {
            kurumsal_kayit_programlar.style.borderColor = "red";
            kurumsal_kayit_programlar.focus();
            return;
        }
        else
            kurumsal_kayit_programlar.style.borderColor = "#ccc";
        if (kurumsal_kayit_programtur.value == "-1") {
            kurumsal_kayit_programtur.style.borderColor = "red";
            kurumsal_kayit_programtur.focus();
            return;
        }
        else
            kurumsal_kayit_programtur.style.borderColor = "#ccc";
    }
    if (kurumsal_kayit_ulkeler.style.borderColor == "red") {
        kurumsal_kayit_ulkeler.focus();
        return;
    }

    if (kurumsal_kayit_sehir.style.borderColor == "red") {
        kurumsal_kayit_sehir.focus();
        return;
    }

    if (txtYildiz.value == '0') {
        error.style.visibility = "inherit";
        txtYorumBasligi.focus();
        return;
    }
    error.style.visibility = "hidden";

    if (txtYorumBasligi.style.borderColor == "red") {
        txtYorumBasligi.focus();
        return;
    }

    if (txtYorumicerik.style.borderColor == "red") {
        txtYorumicerik.focus();
        return;
    }

    if (txtOkulBas.style.borderColor == "red") {
        txtOkulBas.focus();
        return;
    }

    if (kurumsalfileupload1.files.length > 0)
        kurumsal_data1.value = $('#kurumsal_1').cropit('export');
    else
        kurumsal_data1.value = "-1";
    if (kurumsalfileupload2.files.length > 0)
        kurumsal_data2.value = $('#kurumsal_2').cropit('export');
    else
        kurumsal_data2.value = "-1";
    if (kurumsalfileupload3.files.length > 0)
        kurumsal_data3.value = $('#kurumsal_3').cropit('export');
    else
        kurumsal_data3.value = "-1";
    if (tahhut.checked) {
        var frm = document.getElementById("kurumsal_kayit_ekle_form"), kurumsal_kayit_load = document.getElementById("kurumsal_kayit_load");
        var dat = objectifyForm(frm.getElementsByClassName("krmsl-js"));
        kurumsal_kayit_load.style.display = "block";
        dat["yorum_bitis_chck"] = HalaDevamEdiyorChck.checked == true ? "on" : "off";
        if ($('#kurumsal_kayit_program_ve_tur')[0].style.display == "none") {
            dat["program_id"] = "-1";
            dat["program_tur_id"] = "-1";
        }
        $.ajax({
            type: "Post",
            url: "/KurumsalKayitOkulEkle",
            data: dat,
            success: function (result) {
                kurumsal_kayit_load.style.display = "none";
                if (result == '1') {
                    frm.classList.add("kurumsal_kayit_gonderildi")
                    frm.reset();
                    $('#kurumsal_sil1').click();
                    $('#kurumsal_sil2').click();
                    $('#kurumsal_sil3').click();
                }
                else {
                    Kurumsalhatalipanel.style.display = "block";
                }
            }
        });
    }
    else
        panel4.style.display = "block";
}
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function objectifyForm(formArray) {
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}
function BizeUlasinGonder(e) {
    e.preventDefault();
    var txt_ad = document.getElementById("txt_ad"),
        txt_email = document.getElementById("txt_email"),
        txt_tel = document.getElementById("BizeUlasinTelefon"),
        txt_konu = document.getElementById("txt_konu"),
        txt_message = document.getElementById("txt_message"),
        bizeulasinbasarili = document.getElementById("bizeulasinbasarili"),
        bizeulasinhata = document.getElementById("bizeulasinhata");
    bizeulasinbasarili.style.display = bizeulasinhata.style.display = "none";
    txt_tel.value = txt_tel.value.replace(/_/g, "");
    BosKontrol(txt_ad);
    BosKontrol(txt_email);
    BosKontrol(txt_tel);
    BosKontrol(txt_konu);
    BosKontrol(txt_message);
    if (txt_ad.style.borderColor == "red") {
        txt_ad.focus();
        return;
    }
    if (txt_email.style.borderColor == "red") {
        txt_email.focus();
        return;
    }
    else {
        if (!validateEmail(txt_email.value)) {
            txt_email.style.borderColor = "red";
            txt_email.focus();
            return;
        }
    }
    if (txt_tel.style.borderColor == "red") {
        txt_tel.focus();
        return;
    }
    if (txt_konu.style.borderColor == "red") {
        txt_konu.focus();
        return;
    }
    if (txt_message.style.borderColor == "red") {
        txt_message.focus();
        return;
    }
    var frm = document.getElementById("bize_ulasin_form"), bize_ulasin_load = document.getElementById("bize_ulasin_load");
    var dat = objectifyForm(frm.getElementsByClassName("msj-js"));
    bize_ulasin_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/BizeUlasinMesajGonder",
        data: dat,
        success: function (result) {
            bize_ulasin_load.style.display = "none";
            if (result == '1') {
                bizeulasinbasarili.style.display = "block";
                frm.reset();
            }
            else {
                bizeulasinhata.style.display = "block";
            }
        }
    });
}
function AboneOl(e) {
    e.preventDefault();
    var txtabone_ol = document.getElementById("txtabone_ol"),
        abone = document.getElementsByClassName("abone-ol")[0],
        abonebasarili = document.getElementsByClassName("abone-ol-basarili")[0],
        abonebasarisiz = document.getElementsByClassName("abone-ol-basarisiz")[0],
        kayitli = document.getElementsByClassName("abone-ol-kayitli")[0],
        abone_ol_load = document.getElementById("abone_ol_load");
    abone.style.display = abonebasarili.style.display = abonebasarisiz.style.display = kayitli.style.display = "none";
    BosKontrol(txtabone_ol);
    if (txtabone_ol.style.borderColor == 'red') {
        txtabone_ol.focus();
    }
    else if (!validateEmail(txtabone_ol.value)) {
        txtabone_ol.style.borderColor = "red";
        txtabone_ol.focus();
    }
    else {
        abone_ol_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/AboneOl",
            data: { mail: txtabone_ol.value },
            success: function (result) {
                abone_ol_load.style.display = "none";
                if (result == "1")
                    abonebasarili.style.display = "block";
                else if (result == "-1")
                    kayitli.style.display = "block";
                else
                    abonebasarisiz.style.display = "block";
            }
        });
    }
}
function AnasayfaTabAra(e) {
    e.preventDefault();
    var seo = document.getElementById($('#AramaTabs .tabs ul.tab-nav li.ui-tabs-active a')[0].getAttribute("href").substring(1)).getAttribute("data-v");
    if (seo == '-1') {
        var anasayfa_arama_diger_egitimler = document.getElementById("anasayfa_arama_diger_egitimler");
        seo = anasayfa_arama_diger_egitimler.value;
    }
    var anasayfa_arama_ulke = document.getElementById("anasayfa_arama_ulke"), anasayfa_arama_sehir = document.getElementById("anasayfa_arama_sehir"), anasayfa_arama_kelime = document.getElementById("anasayfa_arama_kelime");
    window.location.href = "/Okul/" + seo + "?country=" + anasayfa_arama_ulke.value + "&city=" + anasayfa_arama_sehir.value + "&search=" + anasayfa_arama_kelime.value;
}
function SoruGonder(e) {
    e.preventDefault();
    var sikca_sorular_email = document.getElementById("sikca_sorular_email"),
        sikca_sorular_soru = document.getElementById("sikca_sorular_soru"),
        sikcasorularbasarili = document.getElementById("sikcasorularbasarili"),
        sikcasorularhata = document.getElementById("sikcasorularhata");
    sikcasorularbasarili.style.display = sikcasorularhata.style.display = "none";
    BosKontrol(sikca_sorular_email);
    BosKontrol(sikca_sorular_soru);

    if (sikca_sorular_email.style.borderColor == "red") {
        sikca_sorular_email.focus();
        return;
    }
    else {
        if (!validateEmail(sikca_sorular_email.value)) {
            sikca_sorular_email.style.borderColor = "red";
            sikca_sorular_email.focus();
            return;
        }
    }
    if (sikca_sorular_soru.style.borderColor == "red") {
        sikca_sorular_soru.focus();
        return;
    }
    var frm = document.getElementById("sikca_sorular_form"), sikca_sorular_load = document.getElementById("sikca_sorular_load");
    sikca_sorular_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/SoruSor",
        data: { mail: sikca_sorular_email.value, soru: sikca_sorular_soru.value },
        success: function (result) {
            sikca_sorular_load.style.display = "none";
            if (result == '1') {
                sikcasorularbasarili.style.display = "block";
                frm.reset();
            }
            else {
                sikcasorularhata.style.display = "block";
            }
        }
    });
}
function BosKontrol(eleman) {
    if (eleman.value == '' || eleman.value == null) {
        eleman.style.borderColor = "red";
    }
    else {
        eleman.style.borderColor = "#DDD";
    }
}
function BireyselGirisYap(e) {
    e.preventDefault();
    var txt_mailgiris = document.getElementById("txt_mailgiris"), txt_sifregiris = document.getElementById("txt_sifregiris");
    BosKontrol(txt_mailgiris);
    BosKontrol(txt_sifregiris);
    if (txt_mailgiris.style.borderColor == "red") {
        txt_mailgiris.focus();
        return;
    }
    else {
        if (!validateEmail(txt_mailgiris.value)) {
            txt_mailgiris.style.borderColor = "red"
            txt_mailgiris.focus();
            return;

        }
    }
    if (txt_sifregiris.style.borderColor == "red") {
        txt_sifregiris.focus();
        return;
    }
    var loading3 = document.getElementById("loading3"), lblGirisHata = document.getElementById("lblGirisHata"), lblGirisBasarili = document.getElementById("lblGirisBasarili");
    lblGirisHata.style.display = lblGirisBasarili.style.display = "none";
    loading3.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/GirisYap",
        data: { mail: txt_mailgiris.value, sifre: txt_sifregiris.value },
        success: function (result) {
            loading3.style.display = "none";
            lblGirisBasarili.style.display = "block";
            if ($('#ReturnUrl').length > 0)
                window.location.href = $('#ReturnUrl').val();
            else
                location.reload();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            loading3.style.display = "none";
            lblGirisHata.style.display = "block";
        }
    });
}
function CikisYap() {
    $.ajax({
        type: "Get",
        url: "/Cikis",
        success: function () {
            location.reload();
        }
    });
}
function DilDegistir(e, dil_kod) {
    e.preventDefault();
    $.ajax({
        type: "Post",
        url: "/DilDegistir",
        data: { kod: dil_kod },
        success: function () {
            window.location.href = document.URL
        }
    });
}
function YorumYap(e) {
    e.preventDefault();
    var txtYorumBasligi = document.getElementById("txtYorumBasligi"), drpprogramturu = document.getElementById("drpprogramturu"), drpprogramadi = document.getElementById("drpprogramadi"),
        txtYorumicerik = document.getElementById("txtYorumicerik"), txtOkulBas = document.getElementById("txtOkulBas"),
        txtOkulBit = document.getElementById("txtOkulBit"), btnveri = document.getElementById("btnveri"),
        yildiz = document.getElementById("txtYildiz"), error = document.getElementById("rq");
    if (yildiz.value == '0') {
        error.style.visibility = "inherit";
        txtYorumBasligi.focus();
        return;
    }
    error.style.visibility = "hidden";
    BosKontrol(txtYorumBasligi);
    BosKontrol(txtYorumicerik);
    BosKontrol(txtOkulBas);

    if (txtYorumBasligi.style.borderColor == "red") {
        txtYorumBasligi.focus();
        return;
    }
    if (txtYorumicerik.style.borderColor == "red") {
        txtYorumicerik.focus();
        return;
    }
    if (txtOkulBas.style.borderColor == "red") {
        txtOkulBas.focus();
        return;
    }
    if (drpprogramadi.value == "-1") {
        drpprogramadi.style.borderColor = "red";
        drpprogramadi.focus();
        return;
    }
    else
        drpprogramadi.style.borderColor = "#ccc";
    if (drpprogramturu.value == "-1") {
        drpprogramturu.style.borderColor = "red";
        drpprogramturu.focus();
        return;
    }
    else
        drpprogramturu.style.borderColor = "#ccc";
    var yorumfileupload1 = document.getElementById("yorumfileupload1"), yorumfileupload2 = document.getElementById("yorumfileupload2"), yorumfileupload3 = document.getElementById("yorumfileupload3"),
        yorum_data1 = document.getElementById("yorum_data1"), yorum_data2 = document.getElementById("yorum_data2"), yorum_data3 = document.getElementById("yorum_data3");

    if (yorumfileupload1.files.length > 0)
        yorum_data1.value = $('#1_yorum_resmi').cropit('export');
    else
        yorum_data1.value = "-1";
    if (yorumfileupload2.files.length > 0)
        yorum_data2.value = $('#2_yorum_resmi').cropit('export');
    else
        yorum_data2.value = "-1";
    if (yorumfileupload3.files.length > 0)
        yorum_data3.value = $('#3_yorum_resmi').cropit('export');
    else
        yorum_data3.value = "-1";
    var panel1 = document.getElementById("panel1");
    panel2 = document.getElementById("panel2");
    panel3 = document.getElementById("panel3");
    panel4 = document.getElementById("panel4");
    drm = document.getElementById("drm"), tahhut = document.getElementById("tahhut");
    panel1.style.display = panel2.style.display = panel3.style.display = panel4.style.display = "none";

    if (tahhut.checked) {
        var frm = document.getElementById("yorum_formu"), yorum_load = document.getElementById("yorum_load");
        var dat = objectifyForm(frm.getElementsByClassName("yrm-js"));
        yorum_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/YorumYap",
            data: dat,
            success: function (result) {
                yorum_load.style.display = "none";
                if (result == '-1') {
                    panel1.style.display = "block";
                }
                else if (result == '0') {
                    panel3.style.display = "block";
                }
                else {
                    panel2.style.display = "block";
                    $("#v").val(result);
                    frm.reset();
                    $('#yorum_sil1').click();
                    $('#yorum_sil2').click();
                    $('#yorum_sil3').click();
                    $('.clear-rating').click();
                    YorumSonrasihazirla();
                }
            }
        });
    }
    else
        panel4.style.display = "block";
}
function YorumSonrasihazirla() {
    $('.yorumyapildiaktifanketgetir').remove();
    $.ajax({
        type: "Post",
        url: "/AnketGetir",
        data: { y_id: $("#v").val() },
        success: function (result) {
            if (result != '') {
                var YorumSonrasiAnketModalBody = document.getElementById("YorumSonrasiAnketModalBody");
                YorumSonrasiAnketModalBody.innerHTML = result;
                var secilidil = document.getElementById("secilidil");
                if (secilidil.value == "tr-TR") {
                    $(".yorumsonrasianketpuan").rating({
                        starCaptions: { 0: "Seçim Yapılmadı", 1: "Çok Kötü", 2: "Kötü", 3: "İyi", 4: "Çok İyi", 5: "Mükemmel" },
                        starCaptionClasses: { 0: "text-danger", 1: "text-danger", 2: "text-warning", 3: "text-info", 4: "text-primary", 5: "text-success" },
                    });
                }
                else {
                    $(".yorumsonrasianketpuan").rating({
                        starCaptions: { 0: "No Selection", 1: "Very Bad", 2: "Bad", 3: "Good", 4: "Very Good", 5: "Excellent" },
                        starCaptionClasses: { 0: "text-danger", 1: "text-danger", 2: "text-warning", 3: "text-info", 4: "text-primary", 5: "text-success" },
                    });
                }
                var lnk = document.createElement("a");
                lnk.className = "yorumyapildiaktifanketgetir";
                lnk.style.display = "none";
                lnk.setAttribute("href", "#");
                lnk.setAttribute("data-target", "#YorumSonrasiAnket");
                lnk.setAttribute("data-toggle", "modal");
                document.getElementsByTagName("form")[0].appendChild(lnk);
                lnk.click();
            }
        }
    });
}
function SifreHatirlat(e) {
    e.preventDefault();
    var txtmail = document.getElementById("txtmail"), basarilisifrehatirlatma = document.getElementById("basarilisifrehatirlatma")
        , sifremi_unuttum_load = document.getElementById("sifremi_unuttum_load"), basarisizsifrehatirlatma = document.getElementById("basarisizsifrehatirlatma");
    BosKontrol(txtmail);
    if (txtmail.style.borderColor == 'red') {
        txtmail.focus();
    }
    else if (!validateEmail(txtmail.value)) {
        txtmail.style.borderColor = "red";
        txtmail.focus();
    }
    else {
        basarilisifrehatirlatma.style.display = basarisizsifrehatirlatma.style.display = "none";
        sifremi_unuttum_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/Sifremi-Unuttum",
            data: { mail: txtmail.value },
            success: function (result) {
                sifremi_unuttum_load.style.display = "none";
                if (result == "1")
                    basarilisifrehatirlatma.style.display = "block";
                else
                    basarisizsifrehatirlatma.style.display = "block";
            }
        });
    }
}
function SosyalMedyaGeri(e) {
    e.preventDefault();
    var epostabilgisi = document.getElementById("epostabilgisi");
    var sosyalmedya = document.getElementById("sosyalmedya");
    sosyalmedya.classList.remove("hide");
    sosyalmedya.classList.add("show");
    epostabilgisi.classList.remove("show");
    epostabilgisi.classList.add("hide");
}
function ManuelkayitOl(e) {
    e.preventDefault();
    var txtBireyselKayitEposta = document.getElementById("txtBireyselKayitEposta"), txtBireyselKayitIlkSifre = document.getElementById("txtBireyselKayitIlkSifre"),
    chk_haber = document.getElementById("chk_haber"), kayitli = document.getElementsByClassName("kullanici-kayitli")[0],
    kayiterroru = document.getElementsByClassName("kayiterroru")[0], gecerlimailerror = document.getElementsByClassName("gecerlimailerror")[0],
    mailikontrolet = document.getElementsByClassName("mailikontrolet")[0], loading = document.getElementById("loading1"), lblGirisBasarili = document.getElementById("lblGirisBasarili");
    lblGirisBasarili.style.display = "none"
    BosKontrol(txtBireyselKayitEposta);
    BosKontrol(txtBireyselKayitIlkSifre);
    if (txtBireyselKayitEposta.style.borderColor == 'red') {
        txtBireyselKayitEposta.focus();
        return;
    }
    else if (!validateEmail(txtBireyselKayitEposta.value)) {
        txtBireyselKayitEposta.style.borderColor = "red";
        gecerlimailerror.style.display = "block";
        txtBireyselKayitEposta.focus();
        return;
    }
    gecerlimailerror.style.display = "none";
    if (txtBireyselKayitIlkSifre.style.borderColor == 'red') {
        txtBireyselKayitIlkSifre.focus();
        return;
    }
    loading.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/BireyselKayitOl",
        data: { mail: txtBireyselKayitEposta.value, sifre: txtBireyselKayitIlkSifre.value, haber: chk_haber.checked == true ? "on" : "off" },
        success: function (result) {
            loading.style.display = "none";
            if (result == "-9") {
                kayitli.style.display = "block";
                return false;
            }
            else if (result == "0") {
                kayiterroru.style.display = "block";
                return false;
            }
            else if (result == "1") {
                lblGirisBasarili.style.display = "block";
                location.reload();
            }
        }
    });
}
function ProfiliAc() {
    window.location.href = "/Uye-Profil"
}
function OnKayitSayfasiGetir(GrupSeo, OkulSeo, d) {
    if (d == "1")
        window.location.href = "/On-Kayit/" + GrupSeo + "/" + OkulSeo;
    else {
        var onkayituyari = document.getElementById("onkayituyari");
        onkayituyari.getElementsByTagName("div")[0].style.display = onkayituyari.style.display = "block";
    }
}
function OnKayitIlkAdimIleri(e) {
    e.preventDefault();
    var txt_kisiadi = document.getElementById("txt_kisiadi"), txt_kisisoyadi = document.getElementById("txt_kisisoyadi"), kisidogumtarihi = document.getElementById("kisidogumtarihi");
    BosKontrol(txt_kisiadi);
    BosKontrol(txt_kisisoyadi);
    BosKontrol(kisidogumtarihi);
    if (txt_kisiadi.style.borderColor == 'red') {
        txt_kisiadi.focus();
        return;
    }
    if (txt_kisisoyadi.style.borderColor == 'red') {
        txt_kisisoyadi.focus();
        return;
    }
    if (kisidogumtarihi.style.borderColor == 'red') {
        kisidogumtarihi.focus();
        return;
    }
    $('.asililkileri').click();
}
function OnKayitIkinciAdimIleri(e) {
    e.preventDefault();
    var on_kayit_dogum_ulke = document.getElementById("on_kayit_dogum_ulke"), on_kayit_dogum_sehir = document.getElementById("on_kayit_dogum_sehir"),
        on_kayit_yasadigi_ulke = document.getElementById("on_kayit_yasadigi_ulke"), on_kayit_yasadigi_sehir = document.getElementById("on_kayit_yasadigi_sehir");
    BosKontrol(on_kayit_dogum_ulke);
    BosKontrol(on_kayit_dogum_sehir);
    BosKontrol(on_kayit_yasadigi_ulke);
    BosKontrol(on_kayit_yasadigi_sehir);
    if (on_kayit_dogum_ulke.style.borderColor == 'red' || on_kayit_dogum_ulke.value < 0) {
        on_kayit_dogum_ulke.style.borderColor = 'red';
        on_kayit_dogum_ulke.focus();
        return;
    }
    if (on_kayit_dogum_sehir.style.borderColor == 'red' || on_kayit_dogum_sehir.value < 0) {
        on_kayit_dogum_sehir.style.borderColor = 'red';
        on_kayit_dogum_sehir.focus();
        return;
    }
    if (on_kayit_yasadigi_ulke.style.borderColor == 'red' || on_kayit_yasadigi_ulke.value < 0) {
        on_kayit_yasadigi_ulke.style.borderColor = 'red';
        on_kayit_yasadigi_ulke.focus();
        return;
    }
    if (on_kayit_yasadigi_sehir.style.borderColor == 'red' || on_kayit_yasadigi_sehir.value < 0) {
        on_kayit_yasadigi_sehir.style.borderColor = 'red';
        on_kayit_yasadigi_sehir.focus();
        return;
    }
    $('.asilikinciileri').click();
}
function OnKayitUcuncuAdimIleri(e) {
    e.preventDefault();
    var txt_cepno = document.getElementById("txt_cepno"), txt_kisiemail = document.getElementById("txt_kisiemail");
    BosKontrol(txt_cepno);
    BosKontrol(txt_kisiemail);
    if (txt_cepno.style.borderColor == 'red') {
        txt_cepno.focus();
        return;
    }
    if (txt_kisiemail.style.borderColor == 'red') {
        txt_kisiemail.focus();
        return;
    }
    else if (!validateEmail(txt_kisiemail.value)) {
        txt_kisiemail.style.borderColor = 'red';
        txt_kisiemail.focus();
        return;
    }
    $('.asilucuncuileri').click();
}
function OnKayitGonder(e) {
    e.preventDefault();
    var txt_kisiadi = document.getElementById("txt_kisiadi"), txt_kisisoyadi = document.getElementById("txt_kisisoyadi"), kisidogumtarihi = document.getElementById("kisidogumtarihi");
    BosKontrol(txt_kisiadi);
    BosKontrol(txt_kisisoyadi);
    BosKontrol(kisidogumtarihi);
    if (txt_kisiadi.style.borderColor == 'red') {
        txt_kisiadi.focus();
        $('#tabsonkayit a[href="#ptab1"]').click();
        return;
    }
    if (txt_kisisoyadi.style.borderColor == 'red') {
        txt_kisisoyadi.focus();
        $('#tabsonkayit a[href="#ptab1"]').click();
        return;
    }
    if (kisidogumtarihi.style.borderColor == 'red') {
        kisidogumtarihi.focus();
        $('#tabsonkayit a[href="#ptab1"]').click();
        return;
    }
    var txt_cepno = document.getElementById("txt_cepno"), txt_kisiemail = document.getElementById("txt_kisiemail");
    BosKontrol(txt_cepno);
    BosKontrol(txt_kisiemail);
    if (txt_cepno.style.borderColor == 'red') {
        txt_cepno.focus();
        $('#tabsonkayit a[href="#ptab3"]').click();
        return;
    }
    if (txt_kisiemail.style.borderColor == 'red') {
        txt_kisiemail.focus();
        $('#tabsonkayit a[href="#ptab3"]').click();
        return;
    }
    else if (!validateEmail(txt_kisiemail.value)) {
        txt_kisiemail.style.borderColor = 'red';
        txt_kisiemail.focus();
        $('#tabsonkayit a[href="#ptab3"]').click();
        return;
    }
    var txt_egitimtarihi = document.getElementById("txt_egitimtarihi");
    BosKontrol(txt_egitimtarihi);
    if (txt_egitimtarihi.style.borderColor == 'red') {
        txt_egitimtarihi.focus();
        return;
    }
    if ($('#dil').length > 0) {
        var txt_kurshafta = document.getElementById("txt_kurshafta");
        var drp_dilseviyesi = document.getElementById("drp_dilseviyesi");
        if (txt_kurshafta != null) {
            BosKontrol(txt_kurshafta);
            if (txt_kurshafta.style.borderColor == 'red') {
                txt_kurshafta.focus();
                return;
            }
        }
    }
    var frm = document.getElementById("on_kayit_form"), on_kayit_load = document.getElementById("on_kayit_load"), onkayitvar = document.getElementById("onkayitvar"), onkayithata = document.getElementById("onkayithata"),
        kayitgonderildi = document.getElementById("kayitgonderildi"), gonderildikapat = document.getElementById("gonderildikapat");
    var dat = objectifyForm(frm.getElementsByClassName("onkayit-js"));
    on_kayit_load.style.display = "block";
    onkayitvar.style.display = onkayithata.style.display = kayitgonderildi.style.display = "none";
    $.ajax({
        type: "Post",
        url: window.location.href,
        data: dat,
        success: function (result) {
            on_kayit_load.style.display = "none";
            if (result == "-9") {
                onkayitvar.style.display = "block";
                gonderildikapat.remove();
                return false;
            }
            else if (result == "1") {
                kayitgonderildi.style.display = "block";
                gonderildikapat.remove();
            }
            else
                onkayithata.style.display = "block";
        }
    });
}
function FacebookGirisYap(e) {
    e.preventDefault();
    var sosyal_loading = document.getElementById("sosyal_loading");
    sosyal_loading.style.display = "block";
    $.ajax({
        type: "Post",
        data: { url: location.href },
        url: "/FacebookGirisYap",
    });
}
function GoogleGirisYap(e) {
    e.preventDefault();
    var sosyal_loading = document.getElementById("sosyal_loading");
    sosyal_loading.style.display = "block";
    $.ajax({
        type: "Post",
        data: { url: location.href },
        url: "/GoogleGirisYap",
    });
}
function LinkedinGirisYap(e) {
    e.preventDefault();
    var sosyal_loading = document.getElementById("sosyal_loading");
    sosyal_loading.style.display = "block";
    $.ajax({
        type: "Post",
        data: { url: location.href },
        url: "/LinkedinGirisYap",
    });
}
function UstAramaBarOkulAra(e) {
    e.preventDefault();
    var anasayfa_ust_aramaegitim = document.getElementById("anasayfa_ust_aramaegitim"), anasayfa_ust_arama_ulkesehir = document.getElementById("anasayfa_ust_arama_ulkesehir"), error = document.getElementsByClassName("anasayfa_aramaerror")[0];
    if (anasayfa_ust_aramaegitim.value == -1) {
        error.style.display = "block";
        return false;
    }
    else
        error.style.display = "none";
    if (anasayfa_ust_arama_ulkesehir.value == null || anasayfa_ust_arama_ulkesehir.value == '')
        $('#anasayfa_ust_arama_ulkesehir').select2('open');
    else {
        if (anasayfa_ust_arama_ulkesehir.value.indexOf('0-') >= 0) {
            window.location.href = "/Okul/" + anasayfa_ust_aramaegitim.value + "?search=" + $('#anasayfaust_arama_aramaokuladi').val() + "&country=" + anasayfa_ust_arama_ulkesehir.value.split('-')[1];
        }
        else {
            window.location.href = "/Okul/" + anasayfa_ust_aramaegitim.value + "?search=" + $('#anasayfaust_arama_aramaokuladi').val() + "&country=" + anasayfa_ust_arama_ulkesehir.value.split('-')[0] + "&city=" + anasayfa_ust_arama_ulkesehir.value.split('-')[1];
        }
    }
}
function UyeProfilKullaniciGuncelle(e) {
    e.preventDefault();
    var ilgihata = document.getElementById("ilgihata"), basarili = document.getElementById("basarili"), uye_adi = document.getElementById("uye_adi"), uye_soyadi = document.getElementById("uye_soyadi"),
        uye_mail = document.getElementById("uye_mail"), uye_yas = document.getElementById("uye_yas"), uye_tel = document.getElementById("uye_tel"), chk_ilgi_alani = document.getElementById("chk_ilgi_alani"),
        ilgihata = document.getElementById("ilgihata"), ilgianachck_altlari = document.getElementsByClassName("ilgianachck_altlari"), chk_haber = document.getElementById("chk_haber"),
        uye_ulke_kodu = document.getElementById("uye_ulke_kodu"), uye_bolge_kodu = document.getElementById("uye_bolge_kodu");
    ilgihata.style.display = "none";
    uye_tel.value = uye_tel.value.replace(/_/g, "");
    BosKontrol(uye_adi);
    BosKontrol(uye_soyadi);
    BosKontrol(uye_mail);
    BosKontrol(uye_yas);
    BosKontrol(uye_tel);
    BosKontrol(uye_ulke_kodu);
    BosKontrol(uye_bolge_kodu);
    if (uye_adi.style.borderColor == 'red') {
        uye_adi.focus();
        return;
    }
    if (uye_soyadi.style.borderColor == 'red') {
        uye_soyadi.focus();
        return;
    }
    if (uye_mail.style.borderColor == 'red') {
        uye_mail.focus();
        return;
    }
    else if (!validateEmail(uye_mail.value)) {
        uye_mail.style.borderColor = 'red';
        uye_mail.focus();
        return;
    }
    if (uye_yas.style.borderColor == 'red') {
        uye_yas.focus();
        return;
    }
    if (uye_ulke_kodu.style.borderColor == 'red') {
        uye_ulke_kodu.focus();
        return;
    }
    if (uye_bolge_kodu.style.borderColor == 'red') {
        uye_bolge_kodu.focus();
        return;
    }
    if (uye_tel.style.borderColor == 'red') {
        uye_tel.focus();
        return;
    }
    var ilgiler = [];
    if (chk_ilgi_alani.checked) {

        var tutarli = 0;
        for (var i = 0; i < ilgianachck_altlari.length; i++) {
            if (ilgianachck_altlari[i].checked) {
                tutarli = 1;
                ilgiler.push(ilgianachck_altlari[i].getAttribute("name"));
            }
        }
        if (tutarli == 0) {
            ilgihata.style.display = "block";
            chk_ilgi_alani.focus();
            return;
        }
    }
    else {
        for (var i = 0; i < ilgianachck_altlari.length; i++) {
            ilgianachck_altlari[i].checked = false;
        }
    }
    var frm = document.getElementById("profil_kullanici_form"), profil_kullanici_load = document.getElementById("profil_kullanici_load"), kullaniciguncellemebasarili = document.getElementById("kullaniciguncellemebasarili"),
        kullaniciguncellemehata = document.getElementById("kullaniciguncellemehata");
    var dat = objectifyForm(frm.getElementsByClassName("kullanici-js"));
    dat["haber"] = chk_haber.checked == true ? "on" : "off";
    dat["ilgiler"] = ilgiler;
    kullaniciguncellemebasarili.style.display = kullaniciguncellemehata.style.display = "none";
    profil_kullanici_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/UyeProfilKullanici",
        data: dat,
        success: function (result) {
            profil_kullanici_load.style.display = "none";
            if (result == "1") {
                kullaniciguncellemebasarili.style.display = "block";
                $('#profil_adisoyadi')[0].innerText = uye_adi.value + " " + uye_soyadi.value;
            }
            else
                kullaniciguncellemehata.style.display = "block";
        }
    });
}
function uploadac(e) {
    e.preventDefault();
    $('#yeni_profil_resmi').click();
}
function UyeProfilResimGuncelle(e) {
    e.preventDefault();
    var yeni_profil_resmi = document.getElementById("yeni_profil_resmi"), logo_data = document.getElementById("logo_data"),
        imageData = $('.image-editor').cropit('export'), profil_resmi_load = document.getElementById("profil_resmi_load"), profilresmiguncellehata = document.getElementById("profilresmiguncellehata");
    if (yeni_profil_resmi.files.length > 0) {
        logo_data.value = imageData;
        profil_resmi_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/UyeProfilResmi",
            data: { resim_data: logo_data.value },
            success: function (result) {
                profil_resmi_load.style.display = "none";
                if (result != "0") {
                    var imgProfilResmi = document.getElementById("imgProfilResmi");
                    imgProfilResmi.src = "/Content/img/kul_profil/" + result;
                    $("#yeni_profil_resmi").val('');
                    $('.cropit-preview-image')[0].src = "";
                    logo_data.value = "-1";
                }
                else
                    profilresmiguncellehata.style.display = "block";
            }
        });
    }
    else {
        var profil_resim_sec_uyari = document.getElementById("profil_resim_sec_uyari");
        profil_resim_sec_uyari.style.display = "block";
        logo_data.value = "-1";
    }

}
function ProfilEgitimTuruDegisti(e) {
    $('#uye_profil_egitim_programlar').html('<option value="-1">...</option>');
    $('#uye_profil_egitim_program_parent')[0].style.display = e.target.options[e.target.selectedIndex].getAttribute("data-program") == 1 ? "block" : "none";
    $('#uye_profil_egitim_program_tur_parent')[0].style.display = $('#uye_profil_egitim_program_parent')[0].style.display;
    if (e.target.options[e.target.selectedIndex].getAttribute("data-program") == 1) {
        ProfilProgramTurleriGetir();
    }
    ProfilOkullarGetir();
}
function ProfilProgramTurleriGetir() {
    var uye_profil_egitim = document.getElementById("uye_profil_egitim");
    $('#uye_profil_egitim_programtur').html('<option>...</option>');
    $.ajax({
        type: "Post",
        url: "/ProfilProgramTurleriGetir",
        data: { egitim_id: uye_profil_egitim.value },
        success: function (result) {
            $('#uye_profil_egitim_programtur').html(result);
        }
    });
}
function ProfilOkullarGetir() {
    var uye_profil_egitim = document.getElementById("uye_profil_egitim");
    $('#uye_profil_egitim_okullar').html("<option>...</option>");
    $.ajax({
        type: "Post",
        url: "/UyeProfilOkullarGetir",
        data: { egitim_id: uye_profil_egitim.value },
        success: function (result) {
            $('#uye_profil_egitim_okullar').html(result);
            ProfilKampusleriGetir();
        }
    });
}
function ProfilKampusleriGetir() {
    $('#uye_profil_egitim_kampusler').html("<option>...</option>");
    $.ajax({
        type: "Post",
        url: "/UyeProfilEgitimKampusler",
        data: { okul_id: $('#uye_profil_egitim_okullar').val() },
        success: function (result) {
            $('#uye_profil_egitim_kampusler').html(result);
            if ($('#uye_profil_egitim_program_tur_parent')[0].style.display == "block")
                ProfilProgramlarGetir();
        }
    });
}
function ProfilProgramlarGetir() {
    $('#uye_profil_egitim_programlar').html('<option>...</option>');
    $.ajax({
        type: "Post",
        url: "/ProfilProgramlariGetir",
        data: { okul_seo: $('#uye_profil_egitim_kampusler').val() },
        success: function (result) {
            $('#uye_profil_egitim_programlar').html(result);
        }
    });

}
function UyeProfilEgitimEkle(e) {
    e.preventDefault();
    var baslangic_tarih = document.getElementById("baslangic_tarih"), uye_profil_egitim_okullar = document.getElementById("uye_profil_egitim_okullar"),
        uye_profil_egitim_kampusler = document.getElementById("uye_profil_egitim_kampusler"), uye_profil_egitim_programlar = document.getElementById("uye_profil_egitim_programlar"),
        uye_profil_egitim_programtur = document.getElementById("uye_profil_egitim_programtur"), bitis_tarih = document.getElementById("bitis_tarih"), chk_haladevam = document.getElementById("chk_haladevam");
    if (uye_profil_egitim_okullar.value == "" || uye_profil_egitim_okullar.value == null || uye_profil_egitim_okullar.value == "-1") {
        uye_profil_egitim_okullar.style.borderColor = "red";
        uye_profil_egitim_okullar.focus();
    }
    else
        uye_profil_egitim_okullar.style.borderColor = "#DDD";

    if (uye_profil_egitim_kampusler.value == "" || uye_profil_egitim_kampusler.value == null || uye_profil_egitim_kampusler.value == "-1") {
        uye_profil_egitim_kampusler.style.borderColor = "red";
        uye_profil_egitim_kampusler.focus();
    }
    else
        uye_profil_egitim_kampusler.style.borderColor = "#DDD";

    if ($('#uye_profil_egitim_program_tur_parent')[0].style.display == "block") {
        if (uye_profil_egitim_programlar.value == "" || uye_profil_egitim_programlar.value == null || uye_profil_egitim_programlar.value == "-1") {
            uye_profil_egitim_programlar.style.borderColor = "red";
            uye_profil_egitim_programlar.focus();
        }
        else
            uye_profil_egitim_programlar.style.borderColor = "#DDD";

        if (uye_profil_egitim_programtur.value == "" || uye_profil_egitim_programtur.value == null || uye_profil_egitim_programtur.value == "-1") {
            uye_profil_egitim_programtur.style.borderColor = "red";
            uye_profil_egitim_programtur.focus();
        }
        else
            uye_profil_egitim_programtur.style.borderColor = "#DDD";
    }
    BosKontrol(baslangic_tarih);
    if (baslangic_tarih.style.borderColor == 'red') {
        baslangic_tarih.focus();
        return;
    }
    if (!chk_haladevam.checked) {
        BosKontrol(bitis_tarih);
        if (bitis_tarih.style.borderColor == 'red') {
            bitis_tarih.focus();
            return;
        }
    }
    else
        bitis_tarih.style.borderColor = "#DDD";

    var frm = document.getElementById("uye_profil_egitim_form"), profil_egitim_load = document.getElementById("profil_egitim_load"),
        profilegitimbasarili = document.getElementById("profilegitimbasarili"), profilegitimbasarisiz = document.getElementById("profilegitimbasarisiz");
    var dat = objectifyForm(frm.getElementsByClassName("profil-egitim-js"));
    if ($('#uye_profil_egitim_program_tur_parent')[0].style.display == "none") {
        dat["program_id"] = "-1";
        dat["program_tur_id"] = "-1";
    }
    profilegitimbasarili.style.display = profilegitimbasarisiz.style.display = "none";
    profil_egitim_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilEgitimEkle",
        data: dat,
        success: function (result) {
            profil_egitim_load.style.display = "none";
            if (result == "1") {
                profilegitimbasarili.style.display = "block";
                frm.reset();
                ProfilEgitimlerGetir();
            }
            else
                profilegitimbasarisiz.style.display = "block";
        }
    });
}
function ProfilEgitimlerGetir() {
    var profil_egitim = document.getElementById("profil_egitim");
    profil_egitim.style.display = "block";
    $.ajax({
        type: "Get",
        url: "/ProfilEgitimlerGetir",
        success: function (result) {
            $('#kayitli_egitimler').html(result);
            profil_egitim.style.display = "none";
        }
    });
}
function ProfilEgitimSil(e, id) {
    e.preventDefault();
    if (confirm(e.target.getAttribute("data-s"))) {
        var profil_egitim = document.getElementById("profil_egitim");
        profil_egitim.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/ProfilEgitimSil",
            data: { id: id },
            success: function (result) {
                if (result == "1")
                    ProfilEgitimlerGetir();
                else {
                    var profilegitimbasarisiz = document.getElementById("profilegitimbasarisiz");
                    profilegitimbasarisiz.style.display = "block";
                    profil_egitim.style.display = "none";
                }
            }
        });
    }
}
function UyeSifreGuncelle(e) {
    e.preventDefault();
    var txt_gecerlisifre = document.getElementById("txt_gecerlisifre"), uye_sifre = document.getElementById("uye_sifre"), uye_sifre_tekrar = document.getElementById("uye_sifre_tekrar"), sifretekrarhata = document.getElementById("sifretekrarhata"),
        gecerlisifrekontrol = document.getElementById("gecerlisifrekontrol"), sifrebasarili = document.getElementById("sifrebasarili"), sifrebasarisiz = document.getElementById("sifrebasarisiz");
    sifretekrarhata.style.display = gecerlisifrekontrol.style.display = sifrebasarili.style.display = sifrebasarisiz.style.display = "none";
    BosKontrol(txt_gecerlisifre);
    BosKontrol(uye_sifre);
    BosKontrol(uye_sifre_tekrar);
    if (txt_gecerlisifre.style.borderColor == 'red') {
        txt_gecerlisifre.focus();
        return;
    }
    if (uye_sifre.style.borderColor == 'red') {
        uye_sifre.focus();
        return;
    }
    if (uye_sifre_tekrar.style.borderColor == 'red') {
        uye_sifre_tekrar.focus();
        return;
    }
    else if (uye_sifre_tekrar.value.localeCompare(uye_sifre.value) != 0) {
        uye_sifre_tekrar.style.borderColor = 'red';
        uye_sifre_tekrar.focus();
        sifretekrarhata.style.display = "block";
        return;
    }
    var profil_ayarlar_load = document.getElementById("profil_ayarlar_load");
    profil_ayarlar_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilSifreGuncelle",
        data: { mevcut: txt_gecerlisifre.value, yeni: uye_sifre_tekrar.value },
        success: function (result) {
            profil_ayarlar_load.style.display = "none";
            if (result == "-9")
                gecerlisifrekontrol.style.display = "block";
            else if (result == "1") {
                sifrebasarili.style.display = "block";
                txt_gecerlisifre.value = uye_sifre.value = uye_sifre_tekrar.value = "";
            }
            else
                sifrebasarisiz.style.display = "block";

        }
    });
}
function UyeHesapDondurDiger() {
    var hesap_dondur_diger = document.getElementById("hesap_dondur_diger");
    hesap_dondur_diger.style.display = $('#rdb_uye_dondur_diger')[0].checked == true ? "block" : "none";
}
function UyeHesapDondur() {
    var txt_dondurSfr = document.getElementById("txt_dondurSfr"), rdbDondur = document.getElementById("rdbDondur"), hesap_dondur_aciklama = document.getElementById("hesap_dondur_aciklama");
    var y_sebep;
    if (rdbDondur.rows.item(rdbDondur.rows.length - 1).getElementsByTagName("input")[0].checked) {
        BosKontrol(hesap_dondur_aciklama);
        if (hesap_dondur_aciklama.style.borderColor == 'red') {
            hesap_dondur_aciklama.focus();
            return;
        }
        y_sebep = hesap_dondur_aciklama.value;
    }
    else {
        for (var i = 0; i < rdbDondur.rows.length - 1; i++) {
            if (rdbDondur.rows.item(i).getElementsByTagName("input")[0].checked) {
                y_sebep = rdbDondur.rows.item(i).getElementsByTagName("input")[0].value;
            }
        }
    }
    BosKontrol(txt_dondurSfr);
    if (txt_dondurSfr.style.borderColor == 'red') {
        txt_dondurSfr.focus();
        return;
    }
    var hesap_dondur_load = document.getElementById("hesap_dondur_load"), uye_dondur_sifre_kontrol = document.getElementById("uye_dondur_sifre_kontrol"), uye_dondur_basarisiz = document.getElementById("uye_dondur_basarisiz"),
        uye_donduruldu = document.getElementById("uye_donduruldu");
    uye_dondur_sifre_kontrol.style.display = uye_dondur_basarisiz.style.display = uye_donduruldu.style.display = "none";
    hesap_dondur_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilHesapDondur",
        data: { mevcut: txt_dondurSfr.value, sebep: y_sebep },
        success: function (result) {
            hesap_dondur_load.style.display = "none";
            if (result == "-9")
                uye_dondur_sifre_kontrol.style.display = "block";
            else if (result == "1") {
                uye_donduruldu.style.display = "block";
                txt_dondurSfr.value = "";
                var bekle = setInterval(function () {
                    clearInterval(bekle);
                    window.location.href = "/";
                }, 2000);
            }
            else
                uye_dondur_basarisiz.style.display = "block";

        }
    });
}
function DevamEdiyorChange(chck, id) {
    var eleman = document.getElementById(id);
    eleman.value = "";
    eleman.disabled = chck.checked;
}
function UyeProfilYorumSil(e, soru, y_id) {
    e.preventDefault();
    if (confirm(soru)) {
        var profil_yorumlar_load = document.getElementById("profil_yorumlar_load"), yorumsilmebasarili = document.getElementById("yorumsilmebasarili"),
            yorumsilmebasarisiz = document.getElementById("yorumsilmebasarisiz");
        profil_yorumlar_load.style.display = "block";
        yorumsilmebasarili.style.display = "none";
        $.ajax({
            type: "Post",
            url: "/ProfilYorumSil",
            data: { id: y_id },
            success: function (result) {
                if (result == "1") {
                    yorumsilmebasarili.style.display = "block";
                    ProfilYorumlariGetir();
                }
                else {
                    yorumsilmebasarisiz.style.display = "block";
                    profil_yorumlar_load.style.display = "none";
                }

            }
        });
    }
}
function ProfilYorumlariGetir() {
    var profil_yorumlar_load = document.getElementById("profil_yorumlar_load");
    $.ajax({
        type: "Get",
        url: "/ProfilYorumlariGetir",
        success: function (result) {
            $('#profil_yorumlar').html(result);
            profil_yorumlar_load.style.display = "none";
        }
    });
}
function ProfilYorumDetayGoster(y_id, e) {
    e.preventDefault();
    var profil_yorumlar_load = document.getElementById("profil_yorumlar_load"), yorum_detay = document.getElementById("yorum_detay");
    profil_yorumlar_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilYorumDetayGetir",
        data: { id: y_id },
        success: function (result) {
            yorum_detay.innerHTML = result;
            YorumDetayJsCalistir();
            profil_yorumlar_load.style.display = "none";
            yorum_detay.classList.add("active");
            tab_yorumlar.classList.remove("active");
        }
    });
}
function ProfilimYorumDetayKapat(e) {
    e.preventDefault();
    var yorum_detay = document.getElementById("yorum_detay"), tab_yorumlar = document.getElementById("tab_yorumlar");
    tab_yorumlar.classList.add("active");
    yorum_detay.classList.remove("active");
    ProfilYorumlariGetir();
}
function ProfilYorumResimSil(e, soru, y_id, r_id) {
    e.preventDefault();
    if (confirm(soru)) {
        var yorumresim_load = document.getElementById("yorumresim_load"), yorumguncelhata = document.getElementById("yorumguncelhata");
        yorumresim_load.style.display = "block";
        yorumguncelhata.style.display = "none";
        $.ajax({
            type: "Post",
            url: "/ProfilYorumResimSil",
            data: { id: r_id },
            success: function (result) {
                if (result == "1") {
                    $.ajax({
                        type: "Post",
                        url: "/ProfilYorumDetayGetir",
                        data: { id: y_id },
                        success: function (result) {
                            yorum_detay.innerHTML = result;
                            YorumDetayJsCalistir();
                        }
                    });
                }
                else {
                    yorumguncelhata.style.display = "block";
                    yorumresim_load.style.display = "none";
                }

            }
        });
    }
}
function YorumDetayJsCalistir() {
    $('.select-image-btn').click(function () { $('#yorumfileupload1').click(); });
    $('.select-image-btn1').click(function () { $('#yorumfileupload2').click(); });
    $('.select-image-btn2').click(function () { $('#yorumfileupload3').click(); });

    $('#yorum_resmi1').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#yorum_resmi1 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#uye_yorum_sag1').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi1').cropit('rotateCW');
    });
    $('#uye_yorum_sol1').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi1').cropit('rotateCCW');
    });
    $('#uye_yorum_sil1').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload1").val('');
        $('#yorum_resmi1 .cropit-preview-image')[0].src = "";
        $('#yorum_data1').val("-1");
    });

    $('#yorum_resmi2').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#yorum_resmi2 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#uye_yorum_sag2').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi2').cropit('rotateCW');
    });
    $('#uye_yorum_sol2').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi2').cropit('rotateCCW');
    });
    $('#uye_yorum_sil2').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload2").val('');
        $('#yorum_resmi2 .cropit-preview-image')[0].src = "";
        $('#yorum_data2').val("-1");
    });

    $('#yorum_resmi3').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#yorum_resmi3 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#uye_yorum_sag3').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi3').cropit('rotateCW');
    });
    $('#uye_yorum_sol3').click(function (e) {
        e.preventDefault();
        $('#yorum_resmi3').cropit('rotateCCW');
    });
    $('#uye_yorum_sil3').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload3").val('');
        $('#yorum_resmi3 .cropit-preview-image')[0].src = "";
        $('#yorum_data3').val("-1");
    });
}
function ProfilYorumGuncelle(y_id, e) {
    e.preventDefault();
    var yorumfileupload1 = document.getElementById("yorumfileupload1"), yorumfileupload2 = document.getElementById("yorumfileupload2"), yorumfileupload3 = document.getElementById("yorumfileupload3");

    var yorum_resim1 = "-1", yorum_resim2 = "-1", yorum_resim3 = "-1";

    if (yorumfileupload1 != null)
        if (yorumfileupload1.files.length > 0)
            yorum_resim1 = $('#yorum_resmi1').cropit('export');

    if (yorumfileupload2 != null)
        if (yorumfileupload2.files.length > 0)
            yorum_resim2 = $('#yorum_resmi2').cropit('export');

    if (yorumfileupload3 != null)
        if (yorumfileupload3.files.length > 0)
            yorum_resim3 = $('#yorum_resmi3').cropit('export');


    if (yorum_resim1 != "-1" || yorum_resim2 != "-1" || yorum_resim3 != "-1") {
        var frm = document.getElementById("yorum_guncel_form"), yorumresim_load = document.getElementById("yorumresim_load"),
        yorumguncelhata = document.getElementById("yorumguncelhata");
        var dat = objectifyForm(frm.getElementsByClassName("yorum_guncel_js"));
        yorumresim_load.style.display = "block";
        yorumguncelhata.style.display = "none";
        dat["yorum_resim1"] = yorum_resim1;
        dat["yorum_resim2"] = yorum_resim2;
        dat["yorum_resim3"] = yorum_resim3;
        $.ajax({
            type: "Post",
            url: "/ProfilYorumGuncelle",
            data: dat,
            success: function (result) {
                if (result == "1") {
                    $.ajax({
                        type: "Post",
                        url: "/ProfilYorumDetayGetir",
                        data: { id: y_id },
                        success: function (result) {
                            yorumresim_load.style.display = "none";
                            yorum_detay.innerHTML = result;
                            YorumDetayJsCalistir();
                        }
                    });
                }
                else {
                    yorumguncelhata.style.display = "block";
                    yorumresim_load.style.display = "none";
                }
            }
        });
    }
}
function UyeDogrulamaYap(e) {
    e.preventDefault();
    var dogrulama_kodu = document.getElementById("dogrulama_kodu"), profil_dogrulama_load = document.getElementById("profil_dogrulama_load"),
        uyeprofildogrulamakodugonderildi = document.getElementById("uyeprofildogrulamakodugonderildi"), uyeprofildogrulamakodugonderilemedi = document.getElementById("uyeprofildogrulamakodugonderilemedi"),
        uyedogrulamabasarili = document.getElementById("uyeprofildogrulamabasarili"), uyedogrulamabasarisiz = document.getElementById("uyeprofildogrulamabasarisiz");
    uyedogrulamabasarili.style.display = uyedogrulamabasarisiz.style.display = uyeprofildogrulamakodugonderildi.style.display = uyeprofildogrulamakodugonderilemedi.style.display = "none";
    BosKontrol(dogrulama_kodu);
    if (dogrulama_kodu.style.borderColor == "red") {
        dogrulama_kodu.focus();
        return;
    }
    profil_dogrulama_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilUyeDogrula",
        data: { dogrulama_kodu: dogrulama_kodu.value },
        success: function (result) {
            profil_dogrulama_load.style.display = "none";
            if (result == "1") {
                uyedogrulamabasarili.style.display = "block";
                var bekle = setInterval(function () {
                    clearInterval(bekle);
                    location.reload();
                }, 2000);
            }
            else {
                uyedogrulamabasarisiz.style.display = "block";
            }
        }
    });
}
function TekrarKodGonder(e) {
    e.preventDefault();
    var dogrulama_kodu = document.getElementById("dogrulama_kodu"), profil_dogrulama_load = document.getElementById("profil_dogrulama_load"),
        uyeprofildogrulamakodugonderildi = document.getElementById("uyeprofildogrulamakodugonderildi"), uyeprofildogrulamakodugonderilemedi = document.getElementById("uyeprofildogrulamakodugonderilemedi"),
        uyedogrulamabasarili = document.getElementById("uyeprofildogrulamabasarili"), uyedogrulamabasarisiz = document.getElementById("uyeprofildogrulamabasarisiz");
    uyedogrulamabasarili.style.display = uyedogrulamabasarisiz.style.display = uyeprofildogrulamakodugonderildi.style.display = uyeprofildogrulamakodugonderilemedi.style.display = "none";
    profil_dogrulama_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/ProfilTekrarKodGonder",
        success: function (result) {
            profil_dogrulama_load.style.display = "none";
            if (result == "1") {
                uyeprofildogrulamakodugonderildi.style.display = "block";
            }
            else {
                uyeprofildogrulamakodugonderilemedi.style.display = "block";
            }
        }
    });
}
function UyeProfilMesajGonder(e) {
    e.preventDefault();
    var mesajicerik = document.getElementById("mesajicerik"), mesajhata = document.getElementById("mesajhata"),
        mesajicerikerror = document.getElementsByClassName("mesajicerikerror")[0], mesaj_load = document.getElementById("mesaj_load");
    mesajhata.style.display = "none";
    BosKontrol(mesajicerik);
    if (mesajicerik.style.borderColor == "red") {
        mesajicerikerror.style.display = "block";
        mesajicerik.focus();
        return;
    }
    mesaj_load.style.display = "block";
    $.ajax({
        type: "Post",
        url: "/UyeProfilMesajGonder",
        data: { mesaj: mesajicerik.value },
        success: function (result) {
            if (result == "1") {
                mesajicerik.value = "";
                UyeProfilMesajlariGetir();
            }
            else {
                mesaj_load.style.display = "none";
                mesajhata.style.display = "block";
            }
        }
    });
}
function UyeProfilMesajlariGetir() {
    $.ajax({
        type: "Get",
        url: "/UyeProfilMesajlariGetir",
        success: function (result) {
            $('#mesaj_panel').html(result);
            var mesaj_panel = document.getElementById('mesaj_panel'), mesaj_load = document.getElementById("mesaj_load");
            mesaj_panel.scrollTop = mesaj_panel.scrollHeight;
            mesaj_load.style.display = "none";
        }
    });
}
function UyeProfilMesajlariOkundu() {
    $.ajax({
        type: "Get",
        url: "/UyeProfilMesajlariOkundu",
    });
}
function EgitimTurleriKontrol(e) {
    var ilgilendigimokullar = document.getElementById("ilgilendigimokullar");
    if (e.target.checked)
        ilgilendigimokullar.style.display = "block";
    else
        ilgilendigimokullar.style.display = "none";
}
function MesajlitabClick(e) {
    var silmehata = document.getElementById("silmehata"), silmebasarili = document.getElementById("silmebasarili"), onaylahata = document.getElementById("onaylahata");
    silmehata.style.display = silmebasarili.style.display = onaylahata.style.display = "none";
    UyeMesajTabName.value = e.target.parentNode.id;
    if (UyeMesajTabName.value == "mesajlitab") {
        okunmayan_mesaj_sayisi.innerText = "";
    }
    if ($('#tabsayarla li.active').length > 0)
        $('#tabsayarla li.active')[0].classList.remove("active");
    $('#tabsayarla li#' + UyeMesajTabName.value)[0].classList.add("active");
    $('#tabsicerikayarla div.active')[0].classList.remove("active");
    if ($('#tabsicerikayarla div#' + UyeMesajTabName.value + "_icerik").length > 0)
        $('#tabsicerikayarla div#' + UyeMesajTabName.value + "_icerik")[0].classList.add("active");
}
function BildirimSil(e, b_id, soru, silinecek) {
    e.preventDefault();
    var silmehata = document.getElementById("silmehata"), silmebasarili = document.getElementById("silmebasarili"), onaylahata = document.getElementById("onaylahata"),
        mesaj_load = document.getElementById("mesaj_load"), okunmayan_bildiri = document.getElementById("okunmayan_bildiri");
    silmehata.style.display = silmebasarili.style.display = onaylahata.style.display = mesaj_load.style.display = "none";
    if (confirm(soru)) {
        mesaj_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/UyeProfilBildirimSil",
            data: { id: b_id },
            success: function (result) {
                mesaj_load.style.display = "none";
                if (result == "1") {
                    silmebasarili.style.display = "block";
                    silinecek.remove();
                    $("#top_bildirim_" + b_id).remove();
                    if (parseInt(okunmayan_bildiri.innerText) - 1 > 0)
                        okunmayan_bildiri.innerText = parseInt(okunmayan_bildiri.innerText) - 1;
                    else {
                        okunmayan_bildiri.style.display = "none";
                    }
                    if ($('#edubildirimlist li').length == 0) {
                        $("#edubildirimlist").remove();
                        $("#edubildi").remove();
                    }
                    if ($('#kampanyabilidirimlist li').length == 0) {
                        $("#kampanyabilidirimlist").remove();
                        $("#edukampanyabaslik").remove();
                    }
                    $('#mesajlitab a').click();
                    $('#' + silinecek.id + '_icerik').remove();
                }
                else if (result == "-1")
                    location.reload();
                else
                    silmehata.style.display = "block";
            }
        });
    }
}
function BildirimOnayla(e, b_id, soru) {
    e.preventDefault();
    var silmehata = document.getElementById("silmehata"), silmebasarili = document.getElementById("silmebasarili"), onaylahata = document.getElementById("onaylahata"),
        mesaj_load = document.getElementById("mesaj_load"), okunmayan_bildiri = document.getElementById("okunmayan_bildiri");
    silmehata.style.display = silmebasarili.style.display = onaylahata.style.display = mesaj_load.style.display = "none";
    if (confirm(soru)) {
        mesaj_load.style.display = "block";
        $.ajax({
            type: "Post",
            url: "/UyeProfilBildirimOnayla",
            data: { id: b_id },
            success: function (result) {
                mesaj_load.style.display = "none";
                if (result == "1") {
                    $("#top_bildirim_" + b_id).remove();
                    if (parseInt(okunmayan_bildiri.innerText) - 1 > 0)
                        okunmayan_bildiri.innerText = parseInt(okunmayan_bildiri.innerText) - 1;
                    else {
                        okunmayan_bildiri.style.display = "none";
                    }
                    $('#tabs_' + b_id + '_icerik_islemler').remove();
                    $('#tabs_' + b_id + '_icerik_tesekkur_alani')[0].style.display = "block";
                }
                else if (result == "-1")
                    location.reload();
                else
                    onaylahata.style.display = "block";
            }
        });
    }
}
function gorunmezyap(e, v) {
    e.preventDefault();
    v.style.display = "none";
}
function btnbegen(e, y_id) {
    e.preventDefault();
    var drm = document.getElementById("drm");
    $('.yorumhataoturum').remove();
    if (drm.value == '0') {
        var oturum = document.createElement("div");
        oturum.className = "alert alert-danger col-md-12 yorumhataoturum margintop10 marginbottom0";
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            oturum.innerHTML = " <a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>Oturum Açınız! </strong>Yorumu beğenmek için önce oturum açmanız gerekmektedir..";
        }
        else {
            oturum.innerHTML = "<a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>Sign in! </strong>In order to comment, you must first log in..";
        }
        e.target.parentNode.appendChild(oturum);
        OturumPopupAc();
    }
    else {
        $.ajax({
            type: "Post",
            url: "/UyeYorumBegen",
            data: { id: y_id, durum: e.target.getAttribute("data-s") },
            success: function (result) {
                if (result == '-1')
                    location.reload();
                else if (result == "1") {
                    if (e.target.getAttribute("data-s") == '0')
                        $('#yorum_begeni_sayisi_' + y_id)[0].innerText = parseInt($('#yorum_begeni_sayisi_' + y_id)[0].innerText) + 1
                    else
                        $('#yorum_begeni_sayisi_' + y_id)[0].innerText = parseInt($('#yorum_begeni_sayisi_' + y_id)[0].innerText) - 1
                    var secilidil = document.getElementById("secilidil");
                    if (secilidil.value == "tr-TR") {
                        e.target.setAttribute("data-s", e.target.getAttribute("data-s") == '0' ? "1" : "0");
                        e.target.innerText = e.target.getAttribute("data-s") == '0' ? "Beğen" : "Beğenmekten Vazgeç";
                    }
                    else {
                        e.target.setAttribute("data-s", e.target.getAttribute("data-s") == '0' ? "1" : "0");
                        e.target.innerText = e.target.getAttribute("data-s") == '0' ? "Like" : "Dislike";
                        e.target.parentNode.getElementsByClassName("yorumbegeni_sayisi")[0].innerText = " | Liking : " + result;
                    }
                }
            }
        });
    }
}
function YorumSikayetNedeniChange(e) {
    var diger_yorum_sikayet_nedeni = document.getElementById("diger_yorum_sikayet_nedeni");
    diger_yorum_sikayet_nedeni.style.display = $('#sikayet_nedeni_diger')[0].checked == true ? "block" : "none";
}
function YorumSikayetGonder(e) {
    e.preventDefault();
    var txt_sikayetSfr = document.getElementById("txt_sikayetSfr"), rdbYorumSikayetNedenleri = document.getElementById("rdbYorumSikayetNedenleri"),
      txtsikayetnedeni_aciklama = document.getElementById("txtsikayetnedeni_aciklama"), neden, yorumsikayet_load = document.getElementById("yorumsikayet_load"),
       sonuc = document.getElementById("result");
    BosKontrol(txtsikayetnedeni_aciklama);
    BosKontrol(txt_sikayetSfr);
    if (rdbYorumSikayetNedenleri.rows.item(rdbYorumSikayetNedenleri.rows.length - 1).getElementsByTagName("input")[0].checked && txtsikayetnedeni_aciklama.style.borderColor == "red") {
        txtsikayetnedeni_aciklama.focus();
        return;
    }
    if (txt_sikayetSfr.style.borderColor == "red") {
        txt_sikayetSfr.focus();
        return;
    }
    yorumsikayet_load.style.display = "block";
    if (rdbYorumSikayetNedenleri.rows.item(rdbYorumSikayetNedenleri.rows.length - 1).getElementsByTagName("input")[0].checked) {
        neden = txtsikayetnedeni_aciklama.value;
    }
    else {
        for (var i = 0; i < rdbYorumSikayetNedenleri.rows.length - 1; i++) {
            if (rdbYorumSikayetNedenleri.rows.item(i).getElementsByTagName("input")[0].checked) {
                neden = rdbYorumSikayetNedenleri.rows.item(i).getElementsByTagName("input")[0].value;
                break;
            }
        }
    }
    rdbYorumSikayetNedenleri.rows.item(0).getElementsByTagName("input")[0].checked = true;
    $.ajax({
        type: "Post",
        url: "/UyeYorumSikayetEt",
        data: { id: e.target.getAttribute("data-v"), neden: neden, sifre: txt_sikayetSfr.value },
        success: function (result) {
            yorumsikayet_load.style.display = "none";
            var secilidil = document.getElementById("secilidil");
            if (secilidil.value == "tr-TR") {
                if (result == '1') {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-success col-md-12\"><strong>Şikayetiniz Gönderildi! </strong>Yorum Hakkındaki Şikayetiniz Değerlendirilecektir..</div>";
                    txtsikayetnedeni_aciklama.value = txt_sikayetSfr.value = "";
                }
                else if (result == '-8') {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-danger col-md-12\"><strong>Şifre Yanlış! </strong>Geçerli şifreniz uyuşmamaktadır.Kontrol ederek tekrar deneyiniz..</div>";
                }
                else if (result == '-9') {
                    sonuc.innerHTML = "<div class=\"alert alert-danger col-md-12\"><strong>Oturum Açınız! </strong>Şikayet bildirimi göndermek için önce oturum açmanız gerekmektedir..</div>";
                }
                else {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-danger col-md-12\"><strong>Şikayetiniz Gönderilemedi! </strong>Şikayet bildiriminiz gönderilirken bir sorun oluştu.Tekrar deneyiniz..</div>";
                }
            }
            else {
                if (result == '1') {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-success col-md-12\"><strong> Your complaint has been sent! </strong>Your complaint about the comment will be evaluated ..</div>";
                    txtsikayetnedeni_aciklama.value = txt_sikayetSfr.value = "";
                }
                else if (result == '-8') {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-danger col-md-12\"><strong>Wrong password!</strong>Your current password is incompatible. Check and try again ..</div>";
                }
                else if (result == '-9') {
                    sonuc.innerHTML = "<div class=\"alert alert-danger col-md-12\"><strong>Sign in! </strong>You need to sign in first to submit a complaint.</div>";
                }
                else {
                    sonuc.innerHTML = "<div id=\"panel2\" class=\"alert alert-danger col-md-12\"><strong>Your complaint could not be sent! </strong>There was a problem sending your complaint notice. Try again..</div>";
                }
            }
        }
    });
}
function hatalarikapat() {
    var errorlar = $('.arama-tur-secilmedi');
    for (var i = 0; i < errorlar.length; i++) {
        errorlar[i].style.display = "none";
    }
    var girisuyarisi = $('.giris-uyarisi');
    if (girisuyarisi.length > 0) {
        for (var i = 0; i < girisuyarisi.length; i++) {
            girisuyarisi[i].style.display = "none";
        }
    }
}
function AnketGonder(e) {
    e.preventDefault
    var yanitlar = [], sayac = 0;
    var sorular = document.getElementsByClassName("yorumsonrasianketpuan");
    for (var i = 0; i < sorular.length; i++) {
        if (sorular[i].value != '0') {
            yanitlar.push({ value: sorular[i].getAttribute("data-v"), text: sorular[i].value });
            sayac++;
        }
    }
    if (sayac > 0) {
        $.ajax({
            type: "Post",
            url: "/AnketGonder",
            data: { y_id: $("#v").val(), degerler: yanitlar }
        });
    }
    $('#btnYorumSonraAnketKapat').click();
}
function OkulDetayYorumlariFiltrele(e) {
    e.preventDefault();
    var filter_id = e.target.value;
    $(".filtre_hepsi").css("display", "none");
    if (filter_id != -1) {
        $(".filtre_" + filter_id).css("display", "block");
    }
    else {
        $(".filtre_hepsi").css("display", "block");
    }
}
function OturumPopupAc() {
    $('#girisyap_link').click();
}
function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(null, obj.Page, obj.Url);
    } else {
        console.log("Browser does not support HTML5.");
    }
}
function OkulSonucSayfalaAyarla(url, event) {
    event.preventDefault();
    if (document.URL.indexOf('?') != -1) {
        if (document.URL.indexOf("page=") != -1) {
            var page_yer = document.URL.indexOf("page="), son_and = document.URL.lastIndexOf("&");
            if (son_and < page_yer) {
                url = document.URL.substring(0, page_yer) + url;
            }
            else {
                var sonraki_and = document.URL.indexOf("&", page_yer + 1);
                url = document.URL.substring(0, page_yer) + url + document.URL.substring(sonraki_and);
            }
        }
        else
            url = document.URL + "&" + url;
    }
    else
        url = document.URL + "?" + url;
    $.ajax({
        type: "Get",
        url: url,
        success: function (result) {
            ChangeUrl('index', url);
            $('#sonuclar').html(result);
            var alar = $('#okul_sonuc_sayfalar a');
            for (var i = 0; i < alar.length; i++) {
                if (alar[i].getAttribute("href") != null)
                    alar[i].setAttribute("onclick", "OkulSonucSayfalaAyarla('" + alar[i].getAttribute("href").toString().split('?')[1] + "',event)")
            }
        }
    });
}
function OkulSonuclariGetir(e) {
    e.preventDefault();
    var okul_filtre_aramakelime = document.getElementById("okul_filtre_aramakelime"), okul_filtre_ulke = document.getElementById("okul_filtre_ulke"), chck_yildizbes = document.getElementsByClassName("chck_yildizbes"), drp_okulsira = document.getElementById("drp_okulsira"),
        okul_filtre_sehir = document.getElementById("okul_filtre_sehir"), okul_filtre_egitim_turu = document.getElementById("okul_filtre_egitim_turu"), okul_loading = document.getElementsByClassName("okul_loading")[0];
    var kelime = okul_filtre_aramakelime.value, ulke_id = okul_filtre_ulke.value, sehir_id = okul_filtre_sehir.value, kat = okul_filtre_egitim_turu.value;
    var pt = '';
    for (var i = 0; i < chck_yildizbes.length; i++) {
        if (chck_yildizbes[i].checked)
            pt += i + ',';
    }
    pt = pt.substring(0, pt.length - 1);
    var url = "/Okul/" + kat + "?search=" + kelime + "&country=" + ulke_id + "&city=" + sehir_id;
    ChangeUrl('index', url);
    okul_loading.style.display = "block";
    $.ajax({
        type: "Get",
        url: url,
        success: function (result) {
            okul_loading.style.display = "none";
            $('#sonuclar').html(result);
            var alar = $('#okul_sonuc_sayfalar a');
            for (var i = 0; i < alar.length; i++) {
                if (alar[i].getAttribute("href") != null)
                    alar[i].setAttribute("onclick", "OkulSonucSayfalaAyarla('" + alar[i].getAttribute("href").toString().split('?')[1] + "',event)")
            }
        }
    });
}
function MapAnasayfaInfoButton(controlDiv, mapAnasayfa) {
    // Set CSS for the control interior.
    var controlIcon = document.createElement('i');
    controlIcon.className = "fa fa-info";
    controlIcon.style.fontSize = '18px';
    controlIcon.style.marginTop = '5px';
    controlDiv.appendChild(controlIcon);

    controlDiv.addEventListener('click', function () {
        $('#HaritaPinModal').modal('show');
    });

}

$('.tiklamaengelle').click(function (e) {
    e.preventDefault();
});
$('.fakultesatir').click(function (e) {
    e.preventDefault();
    var parent = e.target.parentNode;
    while (parent.tagName != "TR") {
        parent = parent.parentNode;
    }
    console.log(parent.tagName);
    if (parent.nextElementSibling.style.display == "none")
        parent.nextElementSibling.style.display = "block";
    else
        parent.nextElementSibling.style.display = "none";
});
$('.yorum_sikayet_et').click(function (e) {
    var btnYorumuSikayetEt = document.getElementById("btnYorumuSikayetEt"), sonuc = document.getElementById("result"), drm = document.getElementById("drm");
    btnYorumuSikayetEt.setAttribute("data-v", e.target.getAttribute("data-v"));
    sonuc.innerHTML = "";
    $('.yorumhataoturum').remove();
    if (drm.value == '0') {
        var oturum = document.createElement("div");
        oturum.className = "alert alert-danger col-md-12 yorumhataoturum margintop10 marginbottom0";
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            oturum.innerHTML = " <a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>Oturum Açınız! </strong>Şikayet bildirimi göndermek için önce oturum açmanız gerekmektedir..";
        }
        else {
            oturum.innerHTML = " <a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><strong>Sign in! </strong>You need to sign in first to submit a complaint..";
        }
        e.target.parentNode.appendChild(oturum);
        OturumPopupAc();
        return false;
    }
    txtsikayetnedeni_aciklama.value = txt_sikayetSfr.value = "";
});
$('.select-image-btn4').click(function () {
    $('#kurumsalfileupload1').click();
});
$('.select-image-btn5').click(function () {
    $('#kurumsalfileupload2').click();
});
$('.select-image-btn6').click(function () {
    $('#kurumsalfileupload3').click();
});
$('.select-image-btn7').click(function () {
    $('#yorumfileupload1').click();
});
$('.select-image-btn8').click(function () {
    $('#yorumfileupload2').click();
});
$('.select-image-btn9').click(function () {
    $('#yorumfileupload3').click();
});
$('.yorumyazbtn').click(function (e) {
    $('body').scrollTop(0);
    document.getElementById("detaytabs4").getElementsByTagName("a")[0].click();
    $('#txtYorumBasligi').focus();
    return false;
});
$(".tab-linker").click(function () {
    $("#processTabs").tabs("option", "active", $(this).attr('rel') - 1);
    return false;
});
$('.yrm-js').click(function (e) {
    var drm = document.getElementById("drm"), geneloturumkontrol = document.getElementById("geneloturumkontrol");
    geneloturumkontrol.style.display = "none";
    geneloturumkontrol.classList.remove("hide");
    if (drm.value == '0') {
        geneloturumkontrol.style.display = "block";
        OturumPopupAc();
    }
});
$("#girisyap_link").click(function (e) {
    var errorlar = document.getElementsByClassName("arama-tur-secilmedi"), sosyalmedya = document.getElementById("sosyalmedya"), giriseposta = document.getElementById("giriseposta"),
        epostabilgisi = document.getElementById("epostabilgisi"), girisdogrulama = document.getElementById("girisdogrulama"), dogrulamakodu = document.getElementById("dogrulamakodu"),
        girisuyarisi = document.getElementsByClassName("giris-uyarisi"), BireyselPopupForm = document.getElementById("BireyselPopupForm"), txtBireyselKayitEposta = document.getElementById("txtBireyselKayitEposta"),
            txtBireyselKayitIlkSifre = document.getElementById("txtBireyselKayitIlkSifre"), txt_mailgiris = document.getElementById("txt_mailgiris"), txt_sifregiris = document.getElementById("txt_sifregiris");
    for (var i = 0; i < errorlar.length; i++) {
        errorlar[i].style.display = "none";
    }
    if (girisuyarisi.length > 0) {
        for (var i = 0; i < girisuyarisi.length; i++) {
            girisuyarisi[i].style.display = "none";
        }
    }
    if (sosyalmedya.className.indexOf("hide") != -1) {
        sosyalmedya.classList.remove("hide");
        sosyalmedya.classList.add("show");
    }
    if (giriseposta.className.indexOf("hide") != -1) {
        giriseposta.classList.remove("hide");
        giriseposta.classList.add("show");
    }
    if (epostabilgisi.className.indexOf("show") != -1) {
        epostabilgisi.classList.remove("show");
        epostabilgisi.classList.add("hide");
    }
    if (girisdogrulama.className.indexOf("show") != -1) {
        girisdogrulama.classList.remove("show");
        girisdogrulama.classList.add("hide");
    }
    if (dogrulamakodu.className.indexOf("show") != -1) {
        dogrulamakodu.classList.remove("show");
        dogrulamakodu.classList.add("hide");
    }
    BireyselPopupForm.reset();
    txtBireyselKayitEposta.style.borderColor = txtBireyselKayitIlkSifre.style.borderColor = txt_mailgiris.style.borderColor = txt_sifregiris.style.borderColor = "#DDD";

});
$('#anabtnkayit').click(function (e) {
    e.preventDefault();
    var epostabilgisi = document.getElementById("epostabilgisi");
    var sosyalmedya = document.getElementById("sosyalmedya");
    if (sosyalmedya.className.indexOf("show") != -1)
        sosyalmedya.classList.remove("show");
    else
        sosyalmedya.classList.remove("hide");
    sosyalmedya.classList.add("hide");
    epostabilgisi.classList.remove("hide");
    epostabilgisi.classList.add("show");
    $('#txtBireyselKayitEposta').val('');
    $('#txtBireyselKayitIlkSifre').val('');
    if ($('#chk_haber').checked)
        $('#chk_haber').click();
    var errorlar = $('#BireyselKayitOlModal .arama-tur-secilmedi');
    for (var i = 0; i < errorlar.length; i++) {
        errorlar[i].style.display = "none";
    }

});
$('.YorumSonraAnketKapat').click(function (e) {
    var YorumSonrasiAnketModalBody = document.getElementById("YorumSonrasiAnketModalBody");
    YorumSonrasiAnketModalBody.innerHTML = "";
    $('.yorumyapildiaktifanketgetir').remove();
})
$("#txt_sifregiris").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#girisyap").click();
    }
});
$("#txt_mailgiris").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#girisyap").click();
    }
});
$("#mesajicerik").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#mesajGonder").click();
    }
});
$(".sadece-sayi-girilecek").keydown(function (e) {
    if ($.inArray(e.keyCode, [8, 9]) !== -1 || (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)) {
        return;
    }
    else
        e.preventDefault();
});
$(".virgullu-sayi-girilecek").keydown(function (e) {
    if ($.inArray(e.keyCode, [46, 8, 9]) !== -1 || (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || (e.target.value.indexOf(',') < 0 && (e.keyCode == 110 || e.keyCode == 188))) {
        return;
    }
    else
        e.preventDefault();
});
$(".deger-girilmicek").keydown(function (e) {
    e.preventDefault();
});
$(".telefon-girilecek").keydown(function (e) {
    if ($.inArray(e.keyCode, [8, 9]) !== -1 || (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || (e.target.value.indexOf('+') < 0 && e.keyCode == 107)) {
        return;
    }
    else
        e.preventDefault();
});
$(".reveal").on('click', function () {
    var $pwd = $(".pwd");
    if ($pwd.attr('type') === 'password') {
        $pwd.attr('type', 'text');
    } else {
        $pwd.attr('type', 'password');
    }
});
$(".revealkayit").on('click', function () {
    var $pwd = $(".pwdh");
    if ($pwd.attr('type') === 'password') {
        $pwd.attr('type', 'text');
    } else {
        $pwd.attr('type', 'password');
    }
});
$(".gecerli_sifre_real").on('click', function () {
    var $pwd = $(".pwdt");
    if ($pwd.attr('type') === 'password') {
        $pwd.attr('type', 'text');
    } else {
        $pwd.attr('type', 'password');
    }
});
$(".yeni_sifre_real").on('click', function () {
    var $pwd = $(".pwdp");
    if ($pwd.attr('type') === 'password') {
        $pwd.attr('type', 'text');
    } else {
        $pwd.attr('type', 'password');
    }
});
$(".yeni_sifre_tekrar_real").on('click', function () {
    var $pwd = $(".pwdk");
    if ($pwd.attr('type') === 'password') {
        $pwd.attr('type', 'text');
    } else {
        $pwd.attr('type', 'password');
    }
});

window.addEventListener("popstate", function (e) {
    if ($('#okul_sonuc_sayfalar').length > 0) {
        var okul_loading = document.getElementsByClassName("okul_loading")[0];
        okul_loading.style.display = "block";
        $.ajax({
            type: "Get",
            url: location.href,
            success: function (result) {
                $('#sonuclar').html(result);
                var alar = $('#okul_sonuc_sayfalar a');
                for (var i = 0; i < alar.length; i++) {
                    if (alar[i].getAttribute("href") != null)
                        alar[i].setAttribute("onclick", "OkulSonucSayfalaAyarla('" + alar[i].getAttribute("href").toString().split('?')[1] + "',event)")
                }
                okul_loading.style.display = "none";
            }
        });
    }
});

var fix_arama_bar = document.getElementById("fix_arama_bar");
if (fix_arama_bar != null) {
    var aramaAcik = 0;
    var doc = document.documentElement;
    $("#fix_arama_bar").animate({ top: '-=40' });
    if ($('#fix_arama_bar #ilk_div.collapse.in').length > 0)
        $('#fix_arama_bar a.accordion-toggle').click();
    $(window).scroll(function () {
        var ScrollTop = doc.scrollTop;
        if (ScrollTop > 130 && aramaAcik == 0) {
            aramaAcik = 1;
            $("#fix_arama_bar").animate({ top: '+=40' }, 400);
        }
        else if (ScrollTop < 130 && aramaAcik == 1) {
            if ($('#fix_arama_bar #ilk_div.collapse.in').length > 0)
                $('#fix_arama_bar a.accordion-toggle').click();
            aramaAcik = 0;
            $("#fix_arama_bar").animate({ top: '-=40' });
        }
    });
}
var sidebardetay = document.getElementById("sidebar");
if (sidebardetay != null) {
    jQuery('#sidebar').theiaStickySidebar({
        additionalMarginTop: 215
    });
}
var krmslyr = document.getElementById("kurumsal_resimleri_cropit");
if (krmslyr != null) {
    $('#kurumsal_1').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#kurumsal_1 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#kurumsal_sag1').click(function (e) {
        e.preventDefault();
        $('#kurumsal_1').cropit('rotateCW');
    });
    $('#kurumsal_sol1').click(function (e) {
        e.preventDefault();
        $('#kurumsal_1').cropit('rotateCCW');
    });
    $('#kurumsal_sil1').click(function (e) {
        e.preventDefault();
        $("#kurumsalfileupload1").val('');
        $('#kurumsal_1 .cropit-preview-image')[0].src = "";
        $('#kurumsal_data1').val("-1");
    });

    $('#kurumsal_2').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#kurumsal_2 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#kurumsal_sag2').click(function (e) {
        e.preventDefault();
        $('#kurumsal_2').cropit('rotateCW');
    });
    $('#kurumsal_sol2').click(function (e) {
        e.preventDefault();
        $('#kurumsal_2').cropit('rotateCCW');
    });
    $('#kurumsal_sil2').click(function (e) {
        e.preventDefault();
        $("#kurumsalfileupload2").val('');
        $('#kurumsal_2 .cropit-preview-image')[0].src = "";
        $('#kurumsal_data2').val("-1");
    });

    $('#kurumsal_3').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#kurumsal_3 .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#kurumsal_sag3').click(function (e) {
        e.preventDefault();
        $('#kurumsal_3').cropit('rotateCW');
    });
    $('#kurumsal_sol3').click(function (e) {
        e.preventDefault();
        $('#kurumsal_3').cropit('rotateCCW');
    });
    $('#kurumsal_sil3').click(function (e) {
        e.preventDefault();
        $("#kurumsalfileupload3").val('');
        $('#kurumsal_3 .cropit-preview-image')[0].src = "";
        $('#kurumsal_data3').val("-1");
    });

}
var jssor1 = document.getElementById("jssor_1");
if (jssor1 != null) {
    var jssor_1_SlideshowTransitions = [
           { $Duration: 1200, x: 0.3, $During: { $Left: [0.3, 0.7] }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: -0.3, $SlideOut: true, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: -0.3, $During: { $Left: [0.3, 0.7] }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, $SlideOut: true, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: 0.3, $During: { $Top: [0.3, 0.7] }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: -0.3, $SlideOut: true, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: -0.3, $During: { $Top: [0.3, 0.7] }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: 0.3, $SlideOut: true, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, $Cols: 2, $During: { $Left: [0.3, 0.7] }, $ChessMode: { $Column: 3 }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, $Cols: 2, $SlideOut: true, $ChessMode: { $Column: 3 }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: 0.3, $Rows: 2, $During: { $Top: [0.3, 0.7] }, $ChessMode: { $Row: 12 }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: 0.3, $Rows: 2, $SlideOut: true, $ChessMode: { $Row: 12 }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: 0.3, $Cols: 2, $During: { $Top: [0.3, 0.7] }, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, y: -0.3, $Cols: 2, $SlideOut: true, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, $Rows: 2, $During: { $Left: [0.3, 0.7] }, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: -0.3, $Rows: 2, $SlideOut: true, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, y: 0.3, $Cols: 2, $Rows: 2, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $ChessMode: { $Column: 3, $Row: 12 }, $Easing: { $Left: $Jease$.$InCubic, $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, x: 0.3, y: 0.3, $Cols: 2, $Rows: 2, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $SlideOut: true, $ChessMode: { $Column: 3, $Row: 12 }, $Easing: { $Left: $Jease$.$InCubic, $Top: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, $Delay: 20, $Clip: 3, $Assembly: 260, $Easing: { $Clip: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, $Delay: 20, $Clip: 3, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $Jease$.$OutCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, $Delay: 20, $Clip: 12, $Assembly: 260, $Easing: { $Clip: $Jease$.$InCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 },
           { $Duration: 1200, $Delay: 20, $Clip: 12, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $Jease$.$OutCubic, $Opacity: $Jease$.$Linear }, $Opacity: 2 }
    ];

    var jssor_1_options = {
        $AutoPlay: 1,
        $SlideshowOptions: {
            $Class: $JssorSlideshowRunner$,
            $Transitions: jssor_1_SlideshowTransitions,
            $TransitionsOrder: 1
        },
        $ArrowNavigatorOptions: {
            $Class: $JssorArrowNavigator$
        },
        $ThumbnailNavigatorOptions: {
            $Class: $JssorThumbnailNavigator$,
            $Cols: 10,
            $SpacingX: 8,
            $SpacingY: 8,
            $Align: 360
        }
    };

    var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

    /*responsive code begin*/
    /*remove responsive code if you don't want the slider scales while window resizing*/
    function ScaleSlider() {
        var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
        if (refSize) {
            refSize = Math.min(refSize, 800);
            jssor_1_slider.$ScaleWidth(refSize);
        }
        else {
            window.setTimeout(ScaleSlider, 30);
        }
    }
    ScaleSlider();
    $(window).bind("load", ScaleSlider);
    $(window).bind("resize", ScaleSlider);
    $(window).bind("orientationchange", ScaleSlider);
}
var cropitkontrol = document.getElementById("tab_profilresmi");
if (cropitkontrol != null) {
    $('#tab_profilresmi .image-editor').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#tab_profilresmi .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#tab_profilresmi .rotate-cw').click(function (e) {
        e.preventDefault();
        $('.image-editor').cropit('rotateCW');
    });
    $('#tab_profilresmi .rotate-ccw').click(function (e) {
        e.preventDefault();
        $('.image-editor').cropit('rotateCCW');
    });
}
cropitkontrol = document.getElementById("yorum_resimleri_cropit");
if (cropitkontrol != null) {
    $('#1_yorum_resmi').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#1_yorum_resmi .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#yorum_sag1').click(function (e) {
        e.preventDefault();
        $('#1_yorum_resmi').cropit('rotateCW');
    });
    $('#yorum_sol1').click(function (e) {
        e.preventDefault();
        $('#1_yorum_resmi').cropit('rotateCCW');
    });
    $('#yorum_sil1').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload1").val('');
        $('#1_yorum_resmi .cropit-preview-image')[0].src = "";
        $('#yorum_data1').val("-1");
    });
    $('#2_yorum_resmi').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#2_yorum_resmi .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#yorum_sag2').click(function (e) {
        e.preventDefault();
        $('#2_yorum_resmi').cropit('rotateCW');
    });
    $('#yorum_sol2').click(function (e) {
        e.preventDefault();
        $('#2_yorum_resmi').cropit('rotateCCW');
    });
    $('#yorum_sil2').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload2").val('');
        $('#2_yorum_resmi .cropit-preview-image')[0].src = "";
        $('#yorum_data2').val("-1");
    });
    $('#3_yorum_resmi').cropit({
        smallImage: 'allow',
        minZoom: 'fit',
        maxZoom: 1.5,
        onImageLoaded: function () {
            $('#3_yorum_resmi .cropit-image-zoom-input').val("0.5").change();
        }
    });
    $('#yorum_sag3').click(function (e) {
        e.preventDefault();
        $('#3_yorum_resmi').cropit('rotateCW');
    });
    $('#yorum_sol3').click(function (e) {
        e.preventDefault();
        $('#3_yorum_resmi').cropit('rotateCCW');
    });
    $('#yorum_sil3').click(function (e) {
        e.preventDefault();
        $("#yorumfileupload3").val('');
        $('#3_yorum_resmi .cropit-preview-image')[0].src = "";
        $('#yorum_data3').val("-1");
    });
}
var datedogum = document.getElementById("dogumtarihi");
if (datedogum != null) {
    var secilidil = document.getElementById("secilidil");
    if (secilidil.value == "tr-TR") {
        $('#dogumtarihi .input-daterange').datepicker({
            format: "dd/mm/yyyy",
            language: "tr",
            orientation: "bottom left",
            changeMonth: true,
            todayHighlight: true,
            endDate: "today",
            toggleActive: true,
            autoUpdateInput: false,
            keyboardNavigation: false

        });
        $('#dogumtarihi .input-daterange').datepicker('clear');
        $('#passtarihi .input-daterange').datepicker({
            format: "dd/mm/yyyy",
            language: "tr",
            orientation: "bottom left",
            autoclose: true,
            startDate: '-0d',
            changeMonth: true,
            todayHighlight: true,
            toggleActive: true,
            keyboardNavigation: false,
            autoUpdateInput: false
        });
    }
    else {
        $('#dogumtarihi .input-daterange').datepicker({
            format: "dd/mm/yyyy",
            language: "en",
            orientation: "bottom left",
            changeMonth: true,
            todayHighlight: true,
            endDate: "today",
            toggleActive: true,
            autoUpdateInput: false,
            keyboardNavigation: false

        });
        $('#dogumtarihi .input-daterange').datepicker('clear');
        $('#passtarihi .input-daterange').datepicker({
            format: "dd/mm/yyyy",
            language: "en",
            orientation: "bottom left",
            autoclose: true,
            startDate: '-0d',
            changeMonth: true,
            todayHighlight: true,
            toggleActive: true,
            keyboardNavigation: false,
            autoUpdateInput: false
        });
    }
    $('#passtarihi .input-daterange').datepicker('clear');
}
var onkayitTabsName = document.getElementById("onkayitTabsName");
if (onkayitTabsName != null) {
    var onkayittab = $("#onkayitTabsName").val() != "" ? $("#onkayitTabsName").val() : "ptab1";
    $('#tabsonkayit a[href="#' + onkayittab + '"]').click();
    $("#tabsonkayit a").click(function () {
        $("#onkayitTabsName").val($(this).attr("href").replace("#", ""));
    });
}

$(document).ready(function () {
    var mp = document.getElementById("google-map-custom");
    if (mp != null) {
        var info;
        var mapOptions = {
            zoom: 17,
            mapTypeId: 'hybrid',
            streetViewControl: true,
            mapTypeControl: false,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            },
            panControl: false,
            panControlOptions: {
                position: google.maps.ControlPosition.TOP_RIGHT
            },
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.LARGE,
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            },
            //gestureHandling: 'greedy',
            gestureHandling: 'cooperative',
            scaleControl: false,
            scaleControlOptions: {
                position: google.maps.ControlPosition.LEFT_CENTER
            },
            streetViewControlOptions: {
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            }
        };
        var mapAnasayfa = new google.maps.Map(mp, mapOptions);
        mapAnasayfa.setCenter(new google.maps.LatLng(41.043491, 28.894869));
        jQuery(window).load(function () {
            var t = setTimeout(function () {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        $.ajax({
                            type: "Post",
                            url: "/Yakin-Okullar",
                            data: { lat: position.coords.latitude, lng: position.coords.longitude },
                            success: function (data) {
                                var json = $.parseJSON(data);
                                var en_yakinokul = document.getElementById("en_yakinokul");

                                var enyakinokul_List = document.getElementById("enyakinokul_List"), icerik = "";
                                for (var i = 0; i < json.maps.length; i++) {
                                    if (i == 0) {
                                        mapAnasayfa.setCenter(new google.maps.LatLng(json.maps[i].latitude, json.maps[i].longitude));
                                    }
                                    var marker = new google.maps.Marker({
                                        position: new google.maps.LatLng(json.maps[i].latitude, json.maps[i].longitude),
                                        map: mapAnasayfa,
                                        icon: 'Content/images/pins/' + json.maps[i].Key + '.png',
                                        deger: json.maps[i]
                                    });
                                    google.maps.event.addListener(marker, 'click', (function () {
                                        if (info) {
                                            info.close();
                                        }
                                        if (this.deger.Key == 'Konum') {
                                            info = new google.maps.InfoWindow({
                                                content:
                                                   '<div class="marker_info konum_marker_info" id="marker_info" style="width:165px;height:65px;">' +
                                                   '<h4 style="margin-bottom:5px;margin-top:0;">' + this.deger.name + '</h4>' +
                                                   '<div class="marker_tools">' +
                                                   '<form action="http://maps.google.com/maps" method="get" target="_blank" style="display:inline-block;margin-bottom:0;width:100%;""><input type="hidden" name="daddr" value="' + this.deger.latitude + ',' + this.deger.longitude + '"><button type="submit" class="btn-maps-google-btn btn btn-xs btn-google-maps-direction"><i class="icon-map-marker" style="margin-right:2px;"></i>Google Maps</button></form>' +
                                                       '</div>' +
                                                   '</div>',
                                                maxWidth: 300
                                            });
                                        }
                                        else {
                                            info = new google.maps.InfoWindow({
                                                content:
                                                     '<div class="marker_info" id="marker_info">' +
                                                     '<img src="' + this.deger.image_url + '" alt="Image" style="height:140px;"/>' +
                                                     '<h4 style="margin-bottom:5px;margin-top:0;">' + this.deger.name + '</h4>' +

                                                     '<div class="marker_tools">' +
                                                     '<form action="http://maps.google.com/maps" method="get" target="_blank" style="display:inline-block;margin-bottom:5px;"><input type="hidden" name="daddr" value="' + this.deger.latitude + ',' + this.deger.longitude + '"><button type="submit" class="btn-maps-google-btn btn btn-xs btn-google-maps-direction"><i class="icon-map-marker" style="margin-right:2px;"></i>Google Maps</button></form>' +
                                                         '<a href="tel://' + this.deger.phone + '" class="btn btn-xs btn-google-maps-telefon"><i class="glyphicon glyphicon-earphone" style="margin-right:2px;"></i>' + this.deger.phone + '</a>' +
                                                         '</div>' +
                                                          (this.deger.indirimliokulimg == '' ? '' : '<a href="' + this.deger.Url + '"><img src=' + this.deger.indirimliokulimg + ' style="margin-bottom: 5px;" alt="' + this.deger.name + '"/></a>') +
                                                          '<a href="' + this.deger.Url + '" class="btn btn-xs btn-info btn-block">' + this.deger.detayliincele + '</a>' +
                                                     '</div>',
                                                maxWidth: 300
                                            });
                                        }

                                        info.open(mapAnasayfa, this);
                                        mapAnasayfa.setCenter(new google.maps.LatLng(this.deger.latitude, this.deger.longitude));
                                    }));
                                }
                                for (var i = 0; i < json.list.length; i++) {
                                    icerik += '<div class="col-md-3 other_tours"><ul class="anasayfaliste" ><li><a href="' + json.list[i].Url + '" class="basliklink" style="font-size:12px !important;font-family:edu !important" title="' + json.list[i].name + '">' + json.list[i].name + '</a></li></ul></div>';
                                }
                                enyakinokul_List.innerHTML = icerik;
                                if (json.list.length > 0) {
                                    en_yakinokul.classList.remove("hide");
                                }
                                else {
                                    en_yakinokul.remove();
                                }
                            }
                        });
                    }, function () {
                    });
                }
            }, 200);
        });
        var centerControlDiv = document.createElement('div');
        centerControlDiv.className = "text-center";
        centerControlDiv.style.backgroundColor = '#fff';
        centerControlDiv.style.cursor = 'pointer';
        centerControlDiv.style.margin = "-2px 14px";
        centerControlDiv.style.width = "25px";
        centerControlDiv.style.height = "25px";
        var centerControl = new MapAnasayfaInfoButton(centerControlDiv, mapAnasayfa);

        centerControlDiv.index = 1;
        mapAnasayfa.controls[google.maps.ControlPosition.RIGHT_TOP].push(centerControlDiv);
    }
    if ($('#kurumsal_kayit_programlar').length > 0) {
        KurumsalKayitProgramlarGetir();
    }
    if ($('#kurumsal_kayit_okullar').length > 0) {
        KurumsalKayitOkullarGetir();
        $.ajax({
            type: "Post",
            url: "/KurumsalKayitFakulteler",
            data: { egitim_id: kurumsal_kayit_egitimturu.value },
            success: function (result) {
                var newSource = JSON.parse(result), okultype = $('#kurumsal_kayit_fakulteler').typeahead({ hint: true, highlight: true, minLength: 1, limit: 15, displayText: function (item) { return item.text; }, });
                okultype.data('typeahead').source = newSource;
            }
        });
        $('#kurumsal_kayit_ulkeler').typeahead({
            source: js_ulke_json, hint: true, highlight: true,
            minLength: 1, limit: 15,
            displayText: function (item) { return item.text; },
            updater: function (item) {
                ulke_id = item.value;
                $('#kurumsal_kayit_sehir').val('');
                SehirleriGetir($('#kurumsal_kayit_sehir')[0])
                return item;
            }
        }).keyup(function (event) {
            if ($('#kurumsal_kayit_sehir').data('typeahead') != null) {
                $('#kurumsal_kayit_sehir').val('');
                $("#kurumsal_kayit_sehir").typeahead("destroy")
            }
        });
    }
    if ($('#uye_profil_egitim_programlar').length > 0) {
        $('#uye_profil_egitim').change();
    }
    if ($('#detaytabs1').length > 0) {
        $('#detaytabs1').click();
    }
    if ($('.more').length > 0) {
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {

            var showChar = 200;  // How many characters are shown by default
            var ellipsestext = "...";
            var moretext = "daha fazla ..";
            var lesstext = "kapat";
            $('.more').each(function () {
                var content = $(this).html();
                if (content.length > showChar) {

                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar, content.length - showChar);

                    var html = c + '<span class="moreellipses">' + ellipsestext + '&nbsp;</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
                    $(this).html(html);
                }
            });
            $(".morelink").click(function () {
                if ($(this).hasClass("less")) {
                    $(this).removeClass("less");
                    $(this).html(moretext);
                } else {
                    $(this).addClass("less");
                    $(this).html(lesstext);
                }
                $(this).parent().prev().toggle();
                $(this).prev().toggle();
                return false;
            });
        } else {
            var showChar = 200;  // How many characters are shown by default
            var ellipsestext = "...";
            var moretext = "more ..";
            var lesstext = "close";
            $('.more').each(function () {
                var content = $(this).html();

                if (content.length > showChar) {
                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar, content.length - showChar);
                    var html = c + '<span class="moreellipses">' + ellipsestext + '&nbsp;</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
                    $(this).html(html);
                }

            });
            $(".morelink").click(function () {
                if ($(this).hasClass("less")) {
                    $(this).removeClass("less");
                    $(this).html(moretext);
                } else {
                    $(this).addClass("less");
                    $(this).html(lesstext);
                }
                $(this).parent().prev().toggle();
                $(this).prev().toggle();
                return false;
            });
        }
    }
    if ($('#ReturnUrl').length > 0) {
        $("#girisyap_link")[0].click();
    }
    if ($('#on_kayit_yasadigi_sehir').length > 0) {
        $('#on_kayit_yasadigi_ulke').change();
        $('#on_kayit_dogum_ulke').change();
    }
    if ($('#uyeFormuTabName').length > 0) {
        var uyeFormuTabName = document.getElementById("uyeFormuTabName");
        var uyeFormuTabName = $("[id*=uyeFormuTabName]").val() != "" ? $("[id*=uyeFormuTabName]").val() : "tab_kullanici";
        $('#Tabs a[href="#' + uyeFormuTabName + '"]').tab('show');

        $("#Tabs a").click(function () {
            $("[id*=uyeFormuTabName]").val($(this).attr("href").replace("#", ""));
        });
    }
    if ($('#uye_yasadigi_yer').length > 0) {
        var uye_yasadigi_yer = document.getElementById("uye_yasadigi_yer");
        var autocomplete = new google.maps.places.Autocomplete(uye_yasadigi_yer, {
            types: ['(regions)']
        });
    }
    if ($('#onkayitsayfala').length > 0) {
        $('#onkayitsayfala').easyPaginate({
            paginateElement: 'section',
            elementsPerPage: 1,
            effect: 'climb'
        });
        var easyPaginateNav = document.getElementsByClassName("easyPaginateNav")[0];
        easyPaginateNav.classList.add("pager");
        easyPaginateNav.classList.add("pagination");
        easyPaginateNav.style.width = "100%";
        easyPaginateNav.classList.add("paddingsagsol0");
        var alar = easyPaginateNav.getElementsByTagName("a");
        for (var i = 0; i < alar.length; i++) {
            alar[i].classList.add("btn");
            alar[i].classList.add("btn-default");
        }
    }
    if ($('#anasayfa_ust_arama_ulkesehir').length > 0) {
        $.ajax({
            type: "Post",
            url: "/UlkeSehirEyaletGetir",
            success: function (result) {
                var data = [];
                var jsonresult = JSON.parse(result);
                for (var i = 0; i < jsonresult.length; i++) {
                    data.push({ id: jsonresult[i].value, text: jsonresult[i].text });
                };
                $("#anasayfa_ust_arama_ulkesehir").select2({
                    initSelection: function (element, callback) {
                        var selection = _.find(data, function (metric) {
                            return metric.id === element.val();
                        })
                        callback(selection);
                    },
                    query: function (options) {
                        var pageSize = 100;
                        var startIndex = (options.page - 1) * pageSize;
                        var filteredData = data;
                        var stripDiacritics = window.Select2.util.stripDiacritics;

                        if (options.term && options.term.length > 0) {
                            if (!options.context) {
                                var term = stripDiacritics(options.term.toLowerCase());
                                options.context = data.filter(function (metric) {
                                    //since data is very big... save the stripDiacritics result for later
                                    //to speed up next search.
                                    //this does modify the original array!
                                    if (!metric.stripped_text) {
                                        metric.stripped_text = stripDiacritics(metric.text.toLowerCase());
                                    }
                                    return (metric.stripped_text.indexOf(term) !== -1);
                                });
                            }
                            filteredData = options.context;
                        }

                        options.callback({
                            context: filteredData,
                            results: filteredData.slice(startIndex, startIndex + pageSize),
                            more: (startIndex + pageSize) < filteredData.length
                        });
                    },
                });
                $('#anasayfa_ust_arama_ulkesehir').val(null).trigger('change')
            }
        });
        $.ajax({
            type: "Post",
            url: "/OkulOnerisiGetir",
            success: function (result) {
                var newSource = JSON.parse(result), okultype = $('.typeahead#anasayfaust_arama_aramaokuladi').typeahead({ hint: true, highlight: true, minLength: 1, items: 15, displayText: function (item) { return item.text; }, });

                okultype.data('typeahead').source = newSource;
            }
        });
        $("#anasayfa_arama_ulke").select2();
        $('#anasayfa_arama_ulke').trigger('change');
    }
    var mpharita = document.getElementById("okuldetay-map");
    if (mpharita != null) {
        var mapOptions = {
            zoom: 18,
            mapTypeId: 'hybrid',
            mapTypeControl: true,
            mapTypeControlOptions: {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                position: google.maps.ControlPosition.TOP_LEFT
            },
            panControl: false,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.LARGE,
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            },
            scrollwheel: true,
            scaleControl: false,
            //gestureHandling: 'greedy',
            gestureHandling: 'cooperative',
            streetViewControl: false,
        };
        var mapOkul = new google.maps.Map(mpharita, mapOptions);
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            var infowindow = new google.maps.InfoWindow({
                content: 'Okulun Yeri'
            });
        }
        else {
            var infowindow = new google.maps.InfoWindow({
                content: 'School Place'
            });
        }
        var lat = document.getElementById("lat"), lng = document.getElementById("lng");
        var konumlatlng = new google.maps.LatLng(lat.value.replace(',', '.'), lng.value.replace(',', '.'));
        mapOkul.setCenter(konumlatlng);
        if (secilidil.value == "tr-TR") {
            var konum = new google.maps.Marker({
                map: mapOkul,
                position: konumlatlng,
                title: 'Okulun Yeri'
            });
        }
        else {
            var konum = new google.maps.Marker({
                map: mapOkul,
                position: konumlatlng,
                title: 'School Place'
            });
        }
        konum.addListener('click', function () {
            infowindow.open(mapOkul, konum);
        });
        $('#detaytabs3').click(function () {
            google.maps.event.trigger(mapOkul, 'resize');
            mapOkul.setCenter(konumlatlng);
            mapOkul.setZoom(18);
        });
    }
    if ($('#uyedogrulamabasarili').length > 0) {
        var bekle = setInterval(function () {
            clearInterval(bekle);
            window.location.href = "/";
        }, 2000);
    }
    var UyeMesajTabName = document.getElementById("UyeMesajTabName"), okunmayan_mesaj_sayisi = document.getElementById("okunmayan_mesaj_sayisi"), mesaj_panel = document.getElementById('mesaj_panel');
    var mesajtabs = document.getElementById("mesajtabs");
    if (mesajtabs != null) {
        mesaj_panel.scrollTop = mesaj_panel.scrollHeight;
    }
    if (UyeMesajTabName != null) {
        $('#tabsayarla li.active')[0].classList.remove("active");
        $('#tabsayarla li#' + UyeMesajTabName.value)[0].classList.add("active");
        $('#tabsicerikayarla div.active')[0].classList.remove("active");
        $('#tabsicerikayarla div#' + UyeMesajTabName.value + "_icerik")[0].classList.add("active");
        if (UyeMesajTabName.value == "mesajlitab" && okunmayan_mesaj_sayisi.innerText != "") {
            UyeProfilMesajlariOkundu();
        }
    }
    if ($('#okul_filtre_ulke').length > 0) {
        $('#okul_filtre_ulke').select2();
        $('#okul_filtre_ulke').trigger('change');
    }
    if ($('#okul_sonuc_sayfalar').length > 0) {
        var alar = $('#okul_sonuc_sayfalar a');
        for (var i = 0; i < alar.length; i++) {
            if (alar[i].getAttribute("href") != null)
                alar[i].setAttribute("onclick", "OkulSonucSayfalaAyarla('" + alar[i].getAttribute("href").toString().split('?')[1] + "',event)")
        }
    }
    if ($("#txtYildiz").length > 0) {
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            $("#txtYildiz").rating({
                starCaptions: { 0: "Seçim Yapılmadı", 1: "Çok Kötü", 2: "Kötü", 3: "İyi", 4: "Çok İyi", 5: "Mükemmel" },
                starCaptionClasses: { 0: "text-danger", 1: "text-danger", 2: "text-warning", 3: "text-info", 4: "text-primary", 5: "text-success" },
            });
        }
        else {
            $("#txtYildiz").rating({
                starCaptions: { 0: "No Selection", 1: "Very bad", 2: "Bad", 3: "Good", 4: "Very good", 5: "Excellent" },
                starCaptionClasses: { 0: "text-danger", 1: "text-danger", 2: "text-warning", 3: "text-info", 4: "text-primary", 5: "text-success" },
            });
        }
    }
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        $('.selectpicker').selectpicker('mobile');
    }
    if ($('#datatable1').length > 0) {
        $('#datatable1').dataTable();
    }
    if ($('.okul_detay_fiyat').length > 0) {
        $('.okul_detay_fiyat').dataTable({
            paging: false,
            info: false,
            searching: false
        });
    }
    $('[data-toggle="tooltip"]').tooltip();
    $("#processTabs").tabs({ show: { effect: "fade", duration: 400 } });
    var date = document.getElementById("okuldetaytarih");
    if ($(".travel-date-group .input-daterange").length > 0) {
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            $('.travel-date-group .input-daterange').datepicker({
                format: "mm/yyyy",
                clearBtn: true,
                language: "tr",
                orientation: "top auto",
                calendarWeeks: true,
                autoclose: true,
                minViewMode: 1,
                maxViewMode: 3
            })
        }
        else {
            $('.travel-date-group .input-daterange').datepicker({
                format: "mm/yyyy",
                clearBtn: true,
                language: "en",
                orientation: "top auto",
                calendarWeeks: true,
                autoclose: true,
                minViewMode: 1,
                maxViewMode: 3
            })
        }
    }
    if ($('.onkayitformujs .input-daterange').length > 0) {
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            $('.onkayitformujs .input-daterange').datepicker({
                format: "dd/mm/yyyy",
                maxViewMode: 2,
                todayBtn: "linked",
                language: "tr",
                startDate: '-0d',
                orientation: "bottom left",
                changeMonth: true,
                todayHighlight: true,
                toggleActive: true,
                autoUpdateInput: false,
                keyboardNavigation: false

            });
            $('.onkayitformujs .input-daterange').datepicker('clear');
        }
        else {
            $('.onkayitformujs .input-daterange').datepicker({
                format: "dd/mm/yyyy",
                maxViewMode: 2,
                todayBtn: "linked",
                language: "en",
                startDate: '-0d',
                orientation: "bottom left",
                changeMonth: true,
                todayHighlight: true,
                toggleActive: true,
                autoUpdateInput: false,
                keyboardNavigation: false

            });
            $('.onkayitformujs .input-daterange').datepicker('clear');
        }
    }
    if ($('.input-daterange .input-group').length > 0) {
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            $('.input-daterange .input-group').datepicker({
                format: "yyyy",
                clearBtn: true,
                language: "tr",
                orientation: "top auto",
                calendarWeeks: true,
                autoclose: true,
                minViewMode: 2
            })
        }
        else {
            $('.input-daterange .input-group').datepicker({
                format: "yyyy",
                clearBtn: true,
                language: "en",
                orientation: "top auto",
                calendarWeeks: true,
                autoclose: true,
                minViewMode: 2
            })
        }
    }
    $('#okul_detay_ust #txtYildiz').change(function (e) {
        var drm = document.getElementById("drm"), geneloturumkontrol = document.getElementById("geneloturumkontrol");
        geneloturumkontrol.style.display = "none";
        geneloturumkontrol.classList.remove("hide");
        if (drm.value == '0') {
            geneloturumkontrol.style.display = "block";
            OturumPopupAc();
        }
    });
    var textarea_feedback = document.getElementById("textarea_feedback");
    if (textarea_feedback != null) {
        var text_max = 200;
        var secilidil = document.getElementById("secilidil");
        if (secilidil.value == "tr-TR") {
            $('#textarea_feedback').html(text_max + ' Maksimum karakter');
            $('#textarea').keyup(function () {
                var text_length = $('#textarea').val().length;
                var text_remaining = text_max - text_length;
                $('#textarea_feedback').html(text_remaining + ' Maksimum karakter');
            });
        }
        else {
            $('#textarea_feedback').html(text_max + 'Maximum characters');
            $('#textarea').keyup(function () {
                var text_length = $('#textarea').val().length;
                var text_remaining = text_max - text_length;
                $('#textarea_feedback').html(text_remaining + ' Maximum characters');
            });
        }
    }
    if ($('#BizeUlasinTelefon').length > 0) {
        $("#BizeUlasinTelefon").mask("999 999 999999999999", { placeholder: "_", autoclear: false });
    }
    if ($('#txtkurumsaltelefon').length > 0) {
        $('#txtkurumsaltelefon').mask("999 999 999999999999", { placeholder: "_", autoclear: false });
    }
    if ($('#uye_tel').length > 0) {
        $('#uye_tel').mask("999999999999", { placeholder: "_", autoclear: false });
    }

});