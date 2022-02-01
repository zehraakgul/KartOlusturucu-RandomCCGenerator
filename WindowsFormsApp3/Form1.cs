using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KartOlusuturucu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Borc Ödeme kısmında sadece kullanıcıdan nümerik değer alınmasını saglıyor. Başka değer girilirse Textboxa yazmıyor.
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //Ana kod
        private void button1_Click(object sender, EventArgs e)
        {
            int islem_;
            int limit_ = Convert.ToInt32(comboBox2.SelectedItem);
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }

            islem_ = Convert.ToInt32(textBox3.Text);
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Adınızı ve Soyadınızı eksiksiz giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBox2.Text == "Seçiniz..." || comboBox2.Text == "")
            {
                MessageBox.Show("Lütfen kart limitinizi seçiniz...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBox1.Text == "Seçiniz..." || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen kart seçimi yapınız...", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                label23.Text = textBox1.Text + " " + textBox2.Text;
                if (comboBox1.Text == "MasterCard")
                {

                    IKart masterkart = KartFactory.KartOlustur(Kart.Master);
                    MessageBox.Show("Kart başarıyla oluşturuldu. Kart bilgilerini görüntülemek için Tamam'a basın.", "Kart Oluşturma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label5.Text = masterkart.no();
                    label6.Text = Convert.ToString(masterkart.ccv());
                    label7.Text = masterkart.sonKullanma();
                    label9.Text = Convert.ToString(masterkart.puan(limit_));
                    label12.Text = Convert.ToString(masterkart.bakiye(limit_, islem_));
                    label8.Text = Convert.ToString(masterkart.limit(limit_));
                    textBox3.Text = "";
                    label24.Text = comboBox1.Text;
                }
                if (comboBox1.Text == "Visa")
                {
                    IKart visakart = KartFactory.KartOlustur(Kart.Visa);
                    MessageBox.Show("Kart başarıyla oluşturuldu. Kart bilgilerini görüntülemek için Tamam'a basın.", "Kart Oluşturma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label5.Text = visakart.no();
                    label6.Text = Convert.ToString(visakart.ccv());
                    label7.Text = visakart.sonKullanma();
                    label9.Text = Convert.ToString(visakart.puan(limit_));
                    label12.Text = Convert.ToString(visakart.bakiye(limit_, islem_));

                    int limit1_ = Convert.ToInt32(comboBox2.SelectedItem);
                    label8.Text = Convert.ToString(visakart.limit(limit1_));
                    textBox3.Text = "";
                    label24.Text = comboBox1.Text;
                }
                if (comboBox1.Text == "Amex")
                {
                    IKart amexkart = KartFactory.KartOlustur(Kart.Amex);
                    MessageBox.Show("Kart başarıyla oluşturuldu. Kart bilgilerini görüntülemek için Tamam'a basın.", "Kart Oluşturma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label5.Text = amexkart.no();
                    label6.Text = Convert.ToString(amexkart.ccv());
                    label7.Text = amexkart.sonKullanma();
                    label9.Text = Convert.ToString(amexkart.puan(limit_));
                    label12.Text = Convert.ToString(amexkart.bakiye(limit_, islem_));

                    int limit1_ = Convert.ToInt32(comboBox2.SelectedItem);
                    label8.Text = Convert.ToString(amexkart.limit(limit1_));
                    textBox3.Text = "";
                    label24.Text = comboBox1.Text;
                }
            }
        }





        //arayüze bütün kartların sahip olduğu fonksiyonlar yazıldı
        public interface IKart
        {
            int ccv(); //kart ccvsi
            string no(); // kart nosu
            int bakiye(int limit, int islem); //kart bakiyesi
            string sonKullanma(); //Son kullanma tarihi
            string harca(); //harcama fonksiyonu
            string borcode(); //borc ödeme fonksiyonu
            int limit(int limit); //kart limiti
            double puan(int limit); //ek puan
            int puanileode(int puan, int islem);//puan ile ödeme fonksiyonu
        }
        //Fonksiyonları kullanabilmek için arayüz sınıfından implemente edilir.
        public class Master : IKart
        {
            private Random r = null;
            public Master() { r = new Random(); }
            //Kart numarası almak için dışarıda tanımladığım statik KartNoOlusturucu sınıfından
            //çağırılmış methodun ürettiği kart numarası
            
            public string no()
            {
                var master_no = r.KartNoAl_(KartNoOlusturucu.Master_KartNoListesiOlustur());
                return String.Join(System.Environment.NewLine, master_no);
            }
            
            //ccv için bir kural belirtilmediği için rastgele 3 basamaklı sayı döndürüldü.
            public int ccv() { return r.Next(0, 1000); }
            
            public string harca() { return "Harcama yapıldı"; }
            public string borcode() { return "Borç Ödendi"; }

            public string sonKullanma()
            {
                string ay = Convert.ToString(r.Next(1, 13));//son kullanma tarihlerini yakın tarihten itibaren max. 10 yıllık ömrü olan kartlar üretmek için
                string yil = Convert.ToString(r.Next(22, 33)); //rastgele ay ve yıl seçimi yapıp string olarak döndürülmüştür.                                                  
                return ay + "/" + yil;
            }
            
            public int limit(int limit) { return limit; } //kullanıcının belirleyeceği kart limiti
                                                          //puanlar kart ilk oluşturulduğunda bakiyenin %5'i olacak şekilde ayarlandı.
            public double puan(int limit)
            {
                double puan = limit * 0.05;
                return puan;
            }
            
            //kart bakiyesi kullanıcıdan alınan değere tanımlandı
            //harcama ve ödeme işlemleri de bakiyeden düşüldü.
            public int bakiye(int limit, int islem)
            {
                int yenibakiye = limit - islem;
                return yenibakiye;
            }

            //puan ile ödeme
            public int puanileode(int puan, int islem)
            {
                int puanbakiye = puan - islem;
                return puanbakiye;
            }
        }
        public class Visa : IKart
        {
            private Random r = null;
            public Visa()
            {
                r = new Random();
            }
            public int ccv() { return r.Next(0, 1000); }
           
            public string harca() { return "Harcama yapıldı"; }
            public string borcode() { return "Borç Ödendi"; }
            
            public string no()
            {
                var visa_no = r.KartNoAl_(KartNoOlusturucu.Visa_KartNoListesiOlustur());
                return String.Join(System.Environment.NewLine, visa_no);
            }
            
            public string sonKullanma()
            {
                string ay = Convert.ToString(r.Next(1, 13));
                string yil = Convert.ToString(r.Next(22, 33));
                return ay + "/" + yil;
            }
            
            public int limit(int limit)
            {
                return limit;
            }
            
            public double puan(int limit)
            {
                double puan = limit * 0.05;
                return puan;
            }
            
            public int bakiye(int limit, int islem)
            {
                int yenibakiye = limit - islem;
                return yenibakiye;
            }

            public int puanileode(int puan, int islem)
            {
                int puanbakiye = puan - islem;
                return puanbakiye;
            }

        }
        public class Amex : IKart
        {
            private Random r = null;
            public Amex()
            {
                r = new Random();
            }
            public int ccv() { return r.Next(0, 10000); }
            public string harca() { return "Harcama yapıldı"; }
            public string borcode() { return "Borç Ödendi"; }

            public string no()
            {
                var amex_no = r.KartNoAl_(KartNoOlusturucu.Amex_KartNoListesiOlustur());
                return String.Join(System.Environment.NewLine, amex_no);
            }

            public string sonKullanma()
            {
                string ay = Convert.ToString(r.Next(1, 13));
                string yil = Convert.ToString(r.Next(22, 33));


                return ay + "/" + yil;
            }

            public int limit(int limit)
            {

                return limit;
            }

            public double puan(int limit)
            {
                double puan = limit * 0.05;
                return puan;
            }

            public int bakiye(int limit, int islem)
            {
                int yenibakiye = limit - islem;
                return yenibakiye;
            }

            public int puanileode(int puan, int islem)
            {
                int puanbakiye = puan - islem;
                return puanbakiye;
            }

        }

        public enum Kart
        {
            Master,
            Visa,
            Amex
        }

        //Factory Pattern uygulandığı için kart üretebilmek adına bir oluşturucu sınıf tanımlandı.
        public class KartFactory
        {
            public static IKart KartOlustur(Kart d)
            {
                switch (d)
                {
                    case Kart.Master: return new Master();
                    case Kart.Visa: return new Visa();
                    case Kart.Amex: return new Amex();
                    default: return new Master();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int islem_;
            int limit_ = Convert.ToInt32(comboBox2.SelectedItem);
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }

            islem_ = Convert.ToInt32(textBox3.Text);
            IKart masterkart = KartFactory.KartOlustur(Kart.Master);
            label12.Text = Convert.ToString(masterkart.bakiye(limit_, islem_));
            label13.Text = masterkart.harca();
            textBox3.Text = "";

            IKart visakart = KartFactory.KartOlustur(Kart.Visa);
            label12.Text = Convert.ToString(visakart.bakiye(limit_, islem_));
            label13.Text = visakart.harca();
            textBox3.Text = "";

            IKart amexkart = KartFactory.KartOlustur(Kart.Amex);
            label12.Text = Convert.ToString(amexkart.bakiye(limit_, islem_));
            label13.Text = amexkart.harca();
            textBox3.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            int islem_ = Convert.ToInt32(textBox4.Text);
            int limit_ = Convert.ToInt32(label12.Text);
            IKart masterkart = KartFactory.KartOlustur(Kart.Master);
            label12.Text = Convert.ToString(masterkart.bakiye(limit_, islem_));
            textBox4.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            int islem_ = Convert.ToInt32(textBox4.Text);
            int puan_ = Convert.ToInt32(label9.Text);
            IKart masterkart = KartFactory.KartOlustur(Kart.Master);
            label9.Text = Convert.ToString(masterkart.puanileode(puan_, islem_));
            textBox4.Text = "";
            label14.Text = "Puan İle Ödendi.";
        }


    }

    public static class KartNoOlusturucu
    {
        //Verilen kart numaralarının standartlara göre oluşması için liste içine tanımlanmıs özellikleri
        //Amex için ilk eleman 3 olmalı. 2. bsamagin elemanı 3 veya 7 
        public static string[] AMEX = new[] { "34", "37" };
        //Master için ilk eleman 5'te başlar. ikinci eleman 1-5 (dahil) arasında olmalı.
        public static string[] MASTERCARD = new[] { "51", "52", "53", "54", "55" };
        //Visa için ilk eleman mutlaka 4 ile başlamalı.
        public static string[] VISA = new[] { "4" };

        public struct KartNo
        {
            public KartNo(string ilkElemanlar, int digerElemanlar)
            {
                ilk = ilkElemanlar;
                devam = digerElemanlar;
            }
            public string ilk { get; set; }
            public int devam { get; set; }
        }

        public static IEnumerable<KartNo> KartNoListesiOlustur(string[] elemanlistesi, int length)
        {
            var list = from p in elemanlistesi select new KartNo(p, length);
            return list;
        }

        public static KartNo[] Master_KartNoListesiOlustur()
        {
            var list = KartNoListesiOlustur(MASTERCARD, 16);
            return list.ToArray();
        }

        public static KartNo[] Visa_KartNoListesiOlustur()
        {
            var list = KartNoListesiOlustur(VISA, 16);
            return list.ToArray();
        }

        public static KartNo[] Amex_KartNoListesiOlustur()
        {
            var list = KartNoListesiOlustur(AMEX, 15);
            return list.ToArray();
        }

        public static IEnumerable<string> KartNoAl(KartNo[] prefixAndLengthList)
        {
            Random rndGen = new Random();
            return KartNoAl_(rndGen, prefixAndLengthList);
        }



        public static IEnumerable<string> KartNoAl_(this Random random, KartNo[] NoListesi)
        {
            var result = new Stack<string>();
            for (int i = 0; i < 1; i++)
            {
                int randomPrefix = random.Next(0, NoListesi.Length - 1);

                var kartNoları = NoListesi[randomPrefix];

                result.Push(KartOlustur(random, kartNoları.ilk, kartNoları.devam));
            }

            return result;
        }

        /*
      ilkElemanlar,liste içinde ilk basta tanımlanan sayılar. 
        uzunluk ise ilkElemanlar seçildikten sonraki geriye kalan kart numarasını olusturacak rakamlar dizisi.
        */
        private static string KartOlustur(this Random random, string ilkElmenalar, int uzunluk)
        {
            string kartno = ilkElmenalar;
            while (kartno.Length < (uzunluk - 1))
            {
                double rnd = (random.NextDouble() * 1.0f - 0f);
                kartno += Math.Floor(rnd * 10);
            }

            // numarayı al ve int'e dönüstür
            var DondurKartNo = kartno.ToCharArray().Reverse();
            var DondurulmusKartNo = DondurKartNo.Select(c => Convert.ToInt32(c.ToString()));

            //Luhn Algoritması (mod10) uygula
            int toplam = 0;
            int konum = 0;
            int[] DondurulmusKartNo_ = DondurulmusKartNo.ToArray();

            while (konum < uzunluk - 1)
            {
                int teksayi = DondurulmusKartNo_[konum] * 2;
                if (teksayi > 9)
                    teksayi -= 9;
                toplam += teksayi;

                if (konum != (uzunluk - 2))
                    toplam += DondurulmusKartNo_[konum + 1];
                konum += 2;
            }

            // basamakların kontrolü
            int basamakKontrol =
                Convert.ToInt32((Math.Floor((decimal)toplam / 10) + 1) * 10 - toplam) % 10;
            kartno += basamakKontrol;
            return kartno;
        }


        public static IEnumerable<string> KKNoAl(string[] elemanListesi, int uzunluk)
        {
            var sonuc = new Stack<string>();
            var random = new Random();
            for (int i = 0; i < 1; i++)
            {
                int rastgeleEleman = random.Next(0, elemanListesi.Length - 1);
                if (rastgeleEleman > 1) //son elemanı secmesi icin
                {
                    rastgeleEleman--;
                }

                string kkno = elemanListesi[rastgeleEleman];
                sonuc.Push(KartOlustur(random, kkno, uzunluk));
            }
            return sonuc;
        }

        //olusturulan kart no'ya Luhn algoritması Uygulanıyor
        public static bool KKNoDogruMu(string KartNo)
        {
            try
            {
                var DondurulmusNo = KartNo.ToCharArray().Reverse();
                int mod10Count = 0;

                for (int i = 0; i < DondurulmusNo.Count(); i++)
                {
                    int augend = Convert.ToInt32(DondurulmusNo.ElementAt(i).ToString());
                    if (((i + 1) % 2) == 0)
                    {
                        string sonuc = (augend * 2).ToString();
                        augend = 0;
                        for (int j = 0; j < sonuc.Length; j++) { augend += Convert.ToInt32(sonuc.ElementAt(j).ToString()); }
                    }

                    mod10Count += augend;
                }

                if ((mod10Count % 10) == 0)
                {
                    return true;
                }
            }

            catch
            {
                return false;
            }

            return false;
        }
    }
}
