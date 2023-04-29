using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection.Emit;

namespace DershaneOtomasyonTd
{
    public partial class giris : Form
    {
        ArrayList listcaptcha = new ArrayList();    
        public  giris()
        {
            InitializeComponent();
            string captcha = captchaMetod();
            listcaptcha.Add(captcha);
        }
        SqlConnection bag = new SqlConnection("Data Source=DESKTOP-CBUGCJB\\SQLEXPRESS;Initial Catalog=dershane2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void girisyap_Click(object sender, EventArgs e)
        {
            string no = kulpersno.Text;
            string sifre2 = sifretext.Text;
            string captcha = listcaptcha[0].ToString();

            string sorgu = "SELECT Sifre, KullanıcıAdı , Yetki FROM tblAdminPanel WHERE KulPersNo = @No";

            SqlCommand komut = new SqlCommand(sorgu, bag);

            komut.Parameters.AddWithValue("@No", no);

            // Bağlantı açma
            bag.Open();

            // Okuyucu oluşturma
            using (SqlDataReader reader = komut.ExecuteReader())
            {
                // Verileri okuma
                while (reader.Read())
                {
                    string sifre = reader.GetString(0);
                    string kullaniciAdi = reader.GetString(1);
                    string yetki = reader.GetString(2);

                    if(sifre2 == sifre)
                    {
                        switch (yetki)
                        {
                            case "Yönetici":
                                yoneticipanel yoneticipanel = new yoneticipanel();
                                if (captcha == guvenliktextb.Text)
                                {
                                    yoneticipanel.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Güvenlik Kodunu Yanlış Girdiniz.");
                                }
                                break;
                            case "Öğrenci":
                                ogrencipanel ogrencipanel = new ogrencipanel();
                                if (captcha == guvenliktextb.Text)
                                {
                                    ogrencipanel.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Güvenlik Kodunu Yanlış Girdiniz.");
                                }
                                break;
                            case "Öğretmen":
                                ogretmenpanel ogretmenpanel= new ogretmenpanel();
                                if (captcha == guvenliktextb.Text)
                                {
                                    ogretmenpanel.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Güvenlik Kodunu Yanlış Girdiniz.");
                                }
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Şifreyi Yanlış");
                    }
                }
                if(bag.State.ToString() == "Open")
                {
                    bag.Close();    
                }
                else
                {
                    bag.Close();
                    bag.Open();
                }
                bag.Close();
            }
        }

        private void bunifuLabel1_Click_1(object sender, EventArgs e)
        {

        }
        public string captchaMetod()
        {
            Random random = new Random();
            string captcha = "";
            for (int i = 0; i < 6; i++) // 6 karakterlik rastgele bir dize oluşturulur.
            {
                int num = random.Next(0, 10); // 0'dan 9'a kadar rastgele bir sayı üretir.
                captcha += num.ToString();
            }

            for (int i = 0; i < 2; i++) // 2 karakterlik rastgele bir dize oluşturulur.
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))); // A'dan Z'ye kadar rastgele bir harf üretir.
                captcha += ch.ToString();
                captchatextb.Text = captcha;
            }
            return captcha;
        }

        private void captcha_Click(object sender, EventArgs e)
        {

        }
    }
}

