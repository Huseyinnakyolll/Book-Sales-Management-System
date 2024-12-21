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
    public partial class yönetici : Form
    {
        public yönetici()
        {
            InitializeComponent();
            this.MaximizeBox = false;  // Pencereyi büyütme butonunu devre dışı bırak
            this.MinimizeBox = false;  // Pencereyi küçültme butonunu devre dışı bırak
        }

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

        public void doldur(string sql)
        {
            //3-DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu.Clear();//veritablosu temizle

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dataGridView1.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView1'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label6.Text = "Sisteme kayıtlı " + dataGridView1.RowCount + " adet yönetici vardır.";

        }

        public void göster(int satırno)
        {
            //4-DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno > dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//yöneticiid
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//ad
            textBox3.Text = dataGridView1.Rows[satırno].Cells[2].Value.ToString();//soyad
            comboBox1.Text = dataGridView1.Rows[satırno].Cells[3].Value.ToString();//yetki
            textBox4.Text = dataGridView1.Rows[satırno].Cells[4].Value.ToString();//eposta
            textBox5.Text = dataGridView1.Rows[satırno].Cells[5].Value.ToString();//şifre
        }



        private void yönetici_Load(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doldur("select * from yönetici");//yönetici tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //5-VERİ GÖSTERME İŞLEMİ
            string sql = "select * from yönetici where yöneticiid=" + textBox1.Text;

            bağlantı.Open();//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı);//2-komut tanımlama
            veriokuyucu=komut.ExecuteReader();//3-komut çalıştırma/tablodan gelen bilgiler veriokuyucu'da
            if(veriokuyucu.Read())//böyle bir kayıt var mı?
            {
                textBox1.Text=veriokuyucu.GetValue(0).ToString();//yöneticiid
                textBox2.Text=veriokuyucu.GetValue(1).ToString();//ad
                textBox3.Text=veriokuyucu.GetValue(2).ToString();//soyad
                comboBox1.Text = veriokuyucu.GetValue(3).ToString();//yetki
                textBox4.Text = veriokuyucu.GetValue(4).ToString();//eposta
                textBox5.Text = veriokuyucu.GetValue(5).ToString();//şifre
            }
            bağlantı.Close();//4-bağlantı kapat

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //6-VERİ EKLEME İŞLEMİ
            string sql = "insert into yönetici(ad,soyad,yetki,eposta,şifre) values(";
            sql += "'" + textBox2.Text.Replace("'", "''") + "',";//ad
            sql += "'" + textBox3.Text.Replace("'","''")+"',";//soyad
            sql += comboBox1.Text + ",";//yetki
            sql += "'" + textBox4.Text.Replace("'", "''") + "',";//eposta
            sql += "'" + textBox5.Text.Replace("'", "''") + "')";//şifre
        
            bağlantı.Open();//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Ekleme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + " " +
            textBox2.Text.Replace("'", "''") + " " +
            textBox3.Text.Replace("'", "''") + " adlı yönetici sisteme ekledi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma



            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from yönetici");//yönetici tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //7-VERİ GÜNCELLEME İŞLEMİ
            string sql = "update yönetici set ";
            sql += "ad='" + textBox2.Text.Replace("'", "''") + "',";//ad
            sql += "soyad='" + textBox3.Text.Replace("'", "''") + "',";//soyad
            sql += "yetki=" + comboBox1.Text + ",";//yetki
            sql += "eposta='" + textBox4.Text.Replace("'", "''") + "',";//eposta
            sql += "şifre='" + textBox5.Text.Replace("'", "''") + "'";//şifre
            sql += " where yöneticiid=" + textBox1.Text;//güncelleme kriteri yöneticiid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut .ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Güncelleme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + " IDsi " +
            textBox1.Text + " olan Yönetici bilgilerini güncelledi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma



            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from yönetici");//yönetici tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //8-VERİ SİLME İŞLEMİ

            string sql = "delete  from yönetici where yöneticiid=" + textBox1.Text;

            bağlantı.Open() ;//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı) ; //2-komut tanımlama
            komut .ExecuteNonQuery();//3-komut çalıştırma


            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Silme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + " IDsi " +
            textBox1.Text + " olan Yönetici sildi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma


            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from yönetici");//yönetici tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button6_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Yönetici arama işlemi

            string sql = "select * from yönetici where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            // Yönetici arama işlemi

            string sql = "select * from yönetici where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            // yönetici arama işlemi

            string sql = "select * from yönetici where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            // Yönetici arama işlemi

            string sql = "select * from yönetici where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }
    }
}
