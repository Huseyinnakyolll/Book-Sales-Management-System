using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kitap_Satış_Sistemi
{
    public partial class yardım : Form
    {
        public yardım()
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

        private void yardım_Load(object sender, EventArgs e)
        {

        }

        public void doldur(string sql)
        {
            //DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu.Clear();//veritablosu temizle

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dataGridView1.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView1'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label5.Text = "Sisteme kayıtlı " + dataGridView1.RowCount + " adet Talep vardır.";
        }

        public void göster(int satırno)
        {
            //4-DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno > dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//ad
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//Soad
            textBox3.Text = dataGridView1.Rows[satırno].Cells[2].Value.ToString();//email
            textBox4.Text = dataGridView1.Rows[satırno].Cells[3].Value.ToString();//istek
        }





        private void button6_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // TextBox'ların hiçbirinin boş olmaması gerektiğini kontrol et
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }


            //6-VERİ EKLEME İŞLEMİ
            string sql = "insert into talep(ad,soyad,email,istek) values(";
            sql += "'" + textBox1.Text.Replace("'", "''") + "',";//ad
            sql += "'" + textBox2.Text.Replace("'", "''") + "',";//soyad
            sql += "'" + textBox3.Text.Replace("'", "''") + "',";//email
            sql += "'" + textBox4.Text.Replace("'", "''") + "')";//istek
          
            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            bağlantı.Close();//4-bağlantı kapat
            doldur("select * from talep");

            MessageBox.Show("Talebiniz Alınmıştır");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            doldur("select * from talep");
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

    }
}
