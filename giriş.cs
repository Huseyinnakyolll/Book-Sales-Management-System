using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //1-MySql bağlantısı için
using MySql.Data; //1-MySql bağlantısı için
using MySql.Data.MySqlClient; //1-MySql bağlantısı için
using System.Data;//1-veritablosu için

namespace Kitap_Satış_Sistemi
{
    public partial class giriş : Form
    {
        public giriş()
        {
            InitializeComponent();
            this.MaximizeBox = false;  // Pencereyi büyütme butonunu devre dışı bırak
            this.MinimizeBox = false;  // Pencereyi küçültme butonunu devre dışı bırak
        }
        //1-FORM TÜRETME
        anamenü anamenü=new anamenü();

        //2-VERİTABANI NESNELERİ
        MySqlCommand komut; //sql komutunu çalıştırmak için
        MySqlDataAdapter veritutucu; //tablo ile veri bağı kurar
        DataTable veritablosu = new DataTable(); //seçme sorgusu(select) ile gelen verilerin tutulacağı yer
        MySqlDataReader veriokuyucu;//bir tablodan gelen değerleri satır satır okumak için kullanılır(select)
        MySqlConnection bağlantı = new MySqlConnection("Server=localhost;" +
        "Database=veri;" +//veritabanı adı
        "Uid=root;" +//kullanıcı
        "Pwd='';" +//şifre
        "AllowUserVariables=True;" +
        "UseCompression=True");


        private void button1_Click(object sender, EventArgs e)
        {
          
                try
                {
                    // E-POSTA VE ŞİFRE KONTROLÜ
                    string sql = "SELECT * FROM yönetici WHERE eposta=@eposta AND şifre=@şifre";

                    // Bağlantı durumu kontrolü
                    if (bağlantı.State == ConnectionState.Closed)
                        bağlantı.Open();

                    using (MySqlCommand komut = new MySqlCommand(sql, bağlantı))
                    {
                        komut.Parameters.AddWithValue("@eposta", textBox1.Text);
                        komut.Parameters.AddWithValue("@şifre", textBox2.Text);

                        using (MySqlDataReader veriokuyucu = komut.ExecuteReader())
                        {
                            if (veriokuyucu.Read()) // YÖNETİCİ TESTİ
                            {
                                bilgi.kullanıcıtürü = "yönetici";
                                bilgi.kullanıcıid = veriokuyucu.GetValue(0).ToString(); // yöneticiid
                            bilgi.kullanıcıadı = veriokuyucu["ad"].ToString(); // Kullanıcı adını kaydet
                            bağlantı.Close();
                            bağlantı.Close(); // Bağlantıyı kapat

                                // Formlar arası geçiş
                                anamenü.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }

                    // MÜŞTERİ TESTİ
                    sql = "SELECT * FROM müşteri WHERE eposta=@eposta AND şifre=@şifre";

                    if (bağlantı.State == ConnectionState.Closed)
                        bağlantı.Open();

                    using (MySqlCommand komut = new MySqlCommand(sql, bağlantı))
                    {
                        komut.Parameters.AddWithValue("@eposta", textBox1.Text);
                        komut.Parameters.AddWithValue("@şifre", textBox2.Text);

                        using (MySqlDataReader veriokuyucu = komut.ExecuteReader())
                        {
                            if (veriokuyucu.Read())
                            {
                                bilgi.kullanıcıtürü = "müşteri";
                                bilgi.kullanıcıid = veriokuyucu.GetValue(0).ToString(); // müşteriid
                            bilgi.kullanıcıadı = veriokuyucu["ad"].ToString(); // Kullanıcı adını kaydet
                            bağlantı.Close();

                                // Formlar arası geçiş
                                anamenü.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }

                    // Kullanıcı bulunamadı
                    MessageBox.Show("E-posta veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bağlantı.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Bağlantıyı garanti kapatma
                    if (bağlantı.State == ConnectionState.Open)
                        bağlantı.Close();
                }
            


        }

        private void giriş_Load(object sender, EventArgs e)
        {

        }
    }
}
