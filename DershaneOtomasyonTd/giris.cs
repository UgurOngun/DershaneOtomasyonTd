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

namespace DershaneOtomasyonTd
{
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
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
                                yoneticipanel.Show();
                                break;
                            case "Öğrenci":
                                ogrencipanel ogrencipanel = new ogrencipanel();
                                ogrencipanel.Show();
                                break;
                            case "Öğretmen":
                                ogretmenpanel ogretmenpanel= new ogretmenpanel();
                                ogretmenpanel.Show();
                                break;
                        }
                        bag.Close();
                    }
                    else
                    {
                        MessageBox.Show("Şifreyi Yanlış");
                    }
                }
            }
        }
    }
}

