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
    public partial class log : Form
    {
        public log()
        {
            InitializeComponent();
        }

        //2-VERİTABANI NESNELERİ
        MySqlCommand komut; //sql komutunu çalıştırmak için
        MySqlDataAdapter veritutucu; //tablo ile veri bağı kurar
        DataTable veritablosu = new DataTable(); //seçme sorgusu(select) ile gelen verilerin tutulacağı yer
        MySqlDataReader veriokuyucu;//bir tablodan gelen değerleri satır satır okumak için kullanılır(select)
        MySqlConnection bağlantı = new MySqlConnection("Server=localhost;" +
        "Database=veri;" +//veritabanı adı veri
        "Uid=root;" +//kullanıcı
        "Pwd='';" +//şifre
        "AllowUserVariables=True;" +
        "UseCompression=True");

        string kullanıcıtürü = "yönetici";

        public void doldur(string sql)
        {
            //3-DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu = new DataTable();

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dataGridView1.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView1'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label8.Text = "Sisteme kayıtlı " + dataGridView1.RowCount + " adet " + kullanıcıtürü + " kaydı vardır.";
        }

        public void göster(int satırno)
        {
            //4-DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno > dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//logid
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//kullanıcıid
            if (dataGridView1.Rows[satırno].Cells[2].Value.ToString() == "Yönetici")//kullanıcıtürü Yönetici mi?
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;

            comboBox1.Text = dataGridView1.Rows[satırno].Cells[3].Value.ToString();//İşlem Türü
            dateTimePicker1.Text = dataGridView1.Rows[satırno].Cells[4].Value.ToString();//tarih
            dateTimePicker2.Text = dataGridView1.Rows[satırno].Cells[5].Value.ToString();//saat
            textBox3.Text = dataGridView1.Rows[satırno].Cells[6].Value.ToString();//açıklama
        }



        private void log_Load(object sender, EventArgs e)
        {
            doldur("select * from log");//log tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor


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
            //5-VERİ GÖSTERME İŞLEMİ
            string sql = "select * from log where logid=" + textBox1.Text;
            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            veriokuyucu = komut.ExecuteReader();//3-komut çalıştırma/tablodan gelen bilgiler veriokuyucu
            if (veriokuyucu.Read())//böyle bir kayıt var mı?
            {

                textBox1.Text = veriokuyucu.GetValue(0).ToString();//logid
                textBox2.Text = veriokuyucu.GetValue(1).ToString();//kullanıcıid
                if (veriokuyucu.GetValue(2).ToString() == "Yönetici")//kullanıcıtürü Yönetici mi?
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;

                comboBox1.Text = veriokuyucu.GetValue(3).ToString();//İşlem Türü
                dateTimePicker1.Text = veriokuyucu.GetValue(4).ToString();//tarih
                dateTimePicker2.Text = veriokuyucu.GetValue(5).ToString();//saat
                textBox3.Text = veriokuyucu.GetValue(6).ToString();//açıklama
            }
            bağlantı.Close();//4-bağlantı kapat



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //6-VERİ EKLEME İŞLEMİ
            string sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama) values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            if (radioButton1.Checked)
                sql += "'Yönetici',";//kullanıcıtürü
            else
                sql += "'Müşteri',";//kullanıcıtürü
            sql += "'" + comboBox1.Text + "',";//işlemtürü
            sql += "'" + dateTimePicker1.Text + "',";//tarih
            sql += "'" + dateTimePicker2.Text + "',";//saat
            sql += "'" + textBox3.Text + "')";//açıklama

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from log");//log tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //7-VERİ GÜNCELLEME İŞLEMİ
            string sql = "update log set ";
            sql += "kullanıcıid=" + bilgi.kullanıcıid + ",";//kullanıcıid
            if (radioButton1.Checked)
                sql += "kullanıcıtürü='Yönetici',";//kullanıcıtürü
            else
                sql += "kullanıcıtürü='Müşteri',";//kullanıcıtürü
            sql += "işlemtürü='" + comboBox1.Text + "',";//işlemtürü
            sql += "tarih='" + dateTimePicker1.Text + "',";//tarih
            sql += "saat='" + dateTimePicker2.Text + "',";//saat
            sql += "açıklama='" + textBox3.Text + "' ";//açıklama
            sql += " where logid=" + textBox1.Text;//güncelleme kriteri logid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from log");//log tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "delete from log where logid=" + textBox1.Text;

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımla
            komut.ExecuteNonQuery();//3-komut çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from log");//log tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor

        }

        private void button5_Click(object sender, EventArgs e)
        {
            doldur("select * from log where kullanıcıtürü='Yönetici'");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            doldur("select * from log where kullanıcıtürü='Müşteri'");
        }

        private void button7_Click(object sender, EventArgs e)
        {

            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }
    }
    
}
