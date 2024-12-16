using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //MySql bağlantısı için 
using MySql.Data;             //MySql bağlantısı için 
using MySql.Data.MySqlClient; //MySql bağlantısı için 
using System.Data;//veritablosu için


namespace Kitap_Satış_Sistemi
{
    public partial class müşteri : Form
    {
        public müşteri()
        {
            InitializeComponent();
        }

        //VERİTABANI NESNELERİ
        MySqlCommand komut; //sql komutunu çalıştırmak için
        MySqlDataAdapter veritutucu; //tablo ile veri bağı kurar
        DataTable veritablosu = new DataTable();  //seçme sorgusu(select) ile gelen verilerin tutulacağı yer 
        MySqlDataReader veriokuyucu;//bir tablodan gelen değerleri satır satır okumak için kullanılır(select)
        MySqlConnection bağlantı = new MySqlConnection("Server=localhost;" +
            "Database=veri;" +//veritabanı adı
            "Uid=root;" +//kullanıcı
            "Pwd='';" +//şifre
            "AllowUserVariables=True;" +
            //"Convert Zero Datetime = true;"+ 
            //"Allow Zero Datetime=true;"+
            "UseCompression=True");

        string resimadı = "";//resim adı için

        public void doldur(string sql)
        {
            //DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu.Clear();//veritablosu temizle

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dataGridView1.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView1'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label16.Text = "Sisteme kayıtlı " + dataGridView1.RowCount + " adet müşteri vardır.";
        }


        public void göster(int satırno)
        {
            // DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno > dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//müşteriid
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//ad
            textBox3.Text = dataGridView1.Rows[satırno].Cells[2].Value.ToString();//soyad
            textBox4.Text = dataGridView1.Rows[satırno].Cells[3].Value.ToString();//tckimlikno
            dateTimePicker1.Text = dataGridView1.Rows[satırno].Cells[4].Value.ToString();//doğumtarihi
            if (dataGridView1.Rows[satırno].Cells[5].Value.ToString() == "Erkek")//cinsiyet
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            comboBox1.Text = dataGridView1.Rows[satırno].Cells[6].Value.ToString();//doğumil
            textBox5.Text = dataGridView1.Rows[satırno].Cells[7].Value.ToString();//doğumilçe
            textBox6.Text = dataGridView1.Rows[satırno].Cells[8].Value.ToString();//adres
            textBox7.Text = dataGridView1.Rows[satırno].Cells[9].Value.ToString();//telefon
            textBox8.Text = dataGridView1.Rows[satırno].Cells[10].Value.ToString();//eposta
            textBox9.Text = dataGridView1.Rows[satırno].Cells[11].Value.ToString();//kullanıcıadı
            textBox10.Text = dataGridView1.Rows[satırno].Cells[12].Value.ToString();//şifre
            textBox11.Text = dataGridView1.Rows[satırno].Cells[13].Value.ToString();//resim
            pictureBox1.Image = Image.FromFile(dataGridView1.Rows[satırno].Cells[13].Value.ToString());//resim gösterme
        }




        private void müşteri_Load(object sender, EventArgs e)
        {
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim gösterme işlemi
            resimadı = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";//20241017110705.png her açılan resim farklı isimde
            label15.Text = "profil\\" + resimadı;
            openFileDialog1.ShowDialog();//openFileDialog1 göster
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);//resim pictureBox1'de gösteriliyor
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim kaydetme
            if (resimadı == "")//boş ise kaydetme
                return;
            pictureBox1.Image.Save("profil\\" + resimadı);//resim kaydetme -->resim\20241017110705.png
            label15.Text = "profil\\" + resimadı;
        }

        private void resimGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim güncelleme
            if (resimadı == "" || textBox1.Text == "")//boş ise kaydetme
                return;

            //VERİ GÜNCELLEME İŞLEMİ
            string sql = "update müşteri set resim='profil/" + resimadı + "'";//resim
            sql += " where müşteriid=" + textBox1.Text;//güncelleme kriteri kitapid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            //doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doldur("select * from müşteri");//müşteri tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VERİ GÖSTERME İŞLEMİ
            string sql = "select * from müşteri where müşteriid=" + textBox1.Text;

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            veriokuyucu = komut.ExecuteReader();//3-komut çalıştırıldı,tabloda gelen bilgiler veriokuyucuda
            if (veriokuyucu.Read())//tabloda böyle bir kayıt var mı?
            {
                textBox1.Text = veriokuyucu.GetValue(0).ToString();//müşteriid
                textBox2.Text = veriokuyucu.GetValue(1).ToString();//ad
                textBox3.Text = veriokuyucu.GetValue(2).ToString();//soyad
                textBox4.Text = veriokuyucu.GetValue(3).ToString();//tckimlikno
                dateTimePicker1.Text = veriokuyucu.GetValue(4).ToString();//doğumtarihi
                if (veriokuyucu.GetValue(5).ToString() == "Erkek")//cinsiyet
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;
                comboBox1.Text = veriokuyucu.GetValue(6).ToString();//doğumil
                textBox5.Text = veriokuyucu.GetValue(7).ToString();//müşteriid
                textBox6.Text = veriokuyucu.GetValue(8).ToString();//ad
                textBox7.Text = veriokuyucu.GetValue(9).ToString();//soyad
                textBox8.Text = veriokuyucu.GetValue(10).ToString();//tckimlikno
                textBox9.Text = veriokuyucu.GetValue(11).ToString();//ad
                textBox10.Text = veriokuyucu.GetValue(12).ToString();//soyad
                textBox11.Text = veriokuyucu.GetValue(13).ToString();//tckimlikno
                pictureBox1.Image = Image.FromFile(veriokuyucu.GetValue(13).ToString());//resim gösterme
            }
            bağlantı.Close();//4-bağlantı kapat
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //VERİ EKLEME İŞLEMİ
            string sql = "insert into müşteri(ad,soyad,tckimlikno,doğumtarihi,cinsiyet,doğumil,doğumilçe,adres,telefon,eposta,kullanıcıadı,şifre,resim) values(";
            sql += "'" + textBox2.Text.Replace("'", "''") + "',";//ad
            sql += "'" + textBox3.Text.Replace("'", "''") + "',";//soyad
            sql += "'" + textBox4.Text.Replace("'", "''") + "',";//tckimlikno
            sql += "'" + dateTimePicker1.Text + "',";//doğumtarihi
            if (radioButton1.Checked)//cinsiyet erkek?
                sql += "'Erkek',";
            else
                sql += "'Kadın',";
            sql += "'" + comboBox1.Text.Replace("'", "''") + "',";//doğumil
            sql += "'" + textBox5.Text.Replace("'", "''") + "',";//doğumilçe
            sql += "'" + textBox6.Text.Replace("'", "''") + "',";//adres
            sql += "'" + textBox7.Text.Replace("'", "''") + "',";//telefon
            sql += "'" + textBox8.Text.Replace("'", "''") + "',";//eposta
            sql += "'" + textBox9.Text.Replace("'", "''") + "',";//kullanıcıadı
            sql += "'" + textBox10.Text.Replace("'", "''") + "',";//şifre
            sql += "'" + textBox11.Text.Replace("'", "''") + "')";//resim


            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma


            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Ekleme',";//İşlem Türü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + " " +
                textBox2.Text.Replace("'", "''") + textBox3.Text.Replace("'", "''") + "adlı müşteri sisteme eklendi.')";//Açıklama
            komut = new MySqlCommand(sql, bağlantı);//Komut tanımlama
            komut.ExecuteNonQuery();//komut Çalıştırma


            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from müşteri");//müşteri tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //VERİ GÜNCELLEME İŞLEMİ
            string sql = "update müşteri set ";
            sql += "ad='" + textBox2.Text.Replace("'", "''") + "',";//ad
            sql += "soyad='" + textBox3.Text.Replace("'", "''") + "',";//soyad
            sql += "tckimlikno='" + textBox4.Text.Replace("'", "''") + "',";//tckimlikno
            sql += "doğumtarihi='" + dateTimePicker1.Text + "',";//doğumtarihi
            if (radioButton1.Checked)//cinsiyet erkek?
                sql += "cinsiyet='Erkek',";
            else
                sql += "cinsiyet='Kadın',";
            sql += "doğumil='" + comboBox1.Text.Replace("'", "''") + "',";//doğumil
            sql += "doğumilçe='" + textBox5.Text.Replace("'", "''") + "',";//doğumilçe
            sql += "adres='" + textBox6.Text.Replace("'", "''") + "',";//adres
            sql += "telefon='" + textBox7.Text.Replace("'", "''") + "',";//telefon
            sql += "eposta='" + textBox8.Text.Replace("'", "''") + "',";//eposta
            sql += "kullanıcıadı='" + textBox9.Text.Replace("'", "''") + "',";//kullanıcıadı
            sql += "şifre='" + textBox10.Text.Replace("'", "''") + "',";//şifre
            sql += "resim='" + textBox11.Text.Replace("'", "''") + "'";//resim
            sql += " where müşteriid=" + textBox1.Text;//güncelleme kriteri müşteriid


            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma


            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Güncelleme',";//İşlem Türü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + " IDsi " +
                textBox1.Text + " olan müşteri bilgilerini  güncelledi.')";//Açıklama


            komut = new MySqlCommand(sql, bağlantı);//komut tanımlama
            komut.ExecuteNonQuery();



            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from müşteri");//müşteri tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //VERİ SİLME İŞLEMİ
            string sql = "delete from müşteri where müşteriid=" + textBox1.Text;//silme kriteri müşteriid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Silme',";//İşlem Türü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + "IDsi" +
                textBox1.Text + " olan MÜşteriyi Silindi.')";//Açıklama


            komut = new MySqlCommand(sql, bağlantı);//komut tanımlama
            komut.ExecuteNonQuery();

            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from müşteri");//müşteri tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button6_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }

       
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //Müşteri arama işlemi

            string sql = "select * from müşteri where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox12_TextChanged_1(object sender, EventArgs e)
        {
                 //Müşteri arama işlemi

            string sql = "select * from müşteri where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Müşteri arama işlemi

            string sql = "select * from müşteri where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            // Müşteri arama işlemi

            string sql = "select * from müşteri where ad like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and soyad like '%" + textBox12.Text.Replace("'", "''") + "%'";
            sql += "  and eposta like '%" + textBox14.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    
}
