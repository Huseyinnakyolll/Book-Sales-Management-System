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
    public partial class sepet : Form
    {
        public sepet()
        {
            InitializeComponent();
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
        string sepetid = "";


        public void doldur(string sql, DataGridView dt)
        {
            //3-DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu = new DataTable();

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dt.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label8.Text = "Sisteme kayıtlı " + dt.RowCount + " adet sepet kaydı vardır.";
        }

        public void göster(int satırno)
        {
            //4-DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno > dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//sepetid
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//müşteriid
            textBox3.Text = dataGridView1.Rows[satırno].Cells[2].Value.ToString();//kitapid
            comboBox1.Text = dataGridView1.Rows[satırno].Cells[3].Value.ToString();//adet
            dateTimePicker1.Text = dataGridView1.Rows[satırno].Cells[4].Value.ToString();//tarih
            dateTimePicker2.Text = dataGridView1.Rows[satırno].Cells[5].Value.ToString();//saat
        }


        private void sepet_Load(object sender, EventArgs e)
        {
            //FORM TAM EKRAN YAPMA
        }

        private void button6_Click(object sender, EventArgs e)
        {
            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > dataGridView1.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            göster(e.RowIndex);//tıklanan satır göster


        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > dataGridView1.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > dataGridView1.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            göster(e.RowIndex);//tıklanan satır göster
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //sepetid oluşturma
            sepetid = DateTime.Now.ToString("ddMMyyyyHHmmss");
            label7.Text = sepetid;
            textBox1.Text = sepetid;
            listBox1.Items.Clear();
            listBox1.Enabled = true;
            listBox1.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sql;
            //SEPETTEKİ ÜRÜNLERİ KAYDETME İŞLEMİ
            bağlantı.Open();//1-bağlantı ça
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                sql = "insert into sepet values(";
                sql += "'" + sepetid + "',";//sepetid
                sql += bilgi.kullanıcıid + ",";//müşteriid
                sql += listBox1.Items[i].ToString() + ",";//kitapid
                sql += comboBox1.Text + ",";//adet
                sql += "'" + dateTimePicker1.Text + "',";//tarih
                sql += "'" + dateTimePicker2.Text + "')";//saat
                komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
                komut.ExecuteNonQuery();//3-komut çalıştırma
            }
            bağlantı.Close();//4-bağlantı close

            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor


        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

            //sepetden ürün silme
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }



        private void button7_Click(object sender, EventArgs e)
        {
            doldur("select müşteriid,ad,soyad,eposta,telefon from müşteri", dataGridView2);//müşteri tablosundaki tüm kayıtlar dataGridView2'de gösteriliyor

        }

        private void button8_Click(object sender, EventArgs e)
        {
            doldur("select * from kitap", dataGridView3);//kitap tablosundaki tüm kayıtlar dataGridView3'de gösteriliyor
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > dataGridView1.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > dataGridView2.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > dataGridView2.RowCount - 2 || e.RowIndex < 0)//satır numaraları sınırlar içinde mi?
                return;
            textBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex > dataGridView3.RowCount - 2 || e.RowIndex < 0)//satır numaralıarı sınırlar içinde mi?
                return;
            textBox3.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > dataGridView3.RowCount - 2 || e.RowIndex < 0)//satır numaralıarı sınırlar içinde mi?
                return;
            textBox3.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //sepete ürün ekleme
            if (e.RowIndex > dataGridView3.RowCount - 2 || e.RowIndex < 0)//satır numaralıarı sınırlar içinde mi?
                return;
            if (e.Button == MouseButtons.Right)//sağ tuşa basınca tıklana ürünü silme
                listBox1.Items.Add(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VERİ GÖSTERME İŞLEMİ
            string sql = "select * from sepet where sepetid='" + textBox1.Text + "'";

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            veriokuyucu = komut.ExecuteReader();//3-komut çalıştırma/tablodan gelen bilgiler veriokuyucu'da
            if (veriokuyucu.Read())//böyle bir kayıt var mı?
            {
                textBox1.Text = veriokuyucu.GetValue(0).ToString();//sepetid
                textBox2.Text = veriokuyucu.GetValue(1).ToString();//müşteriid
                textBox3.Text = veriokuyucu.GetValue(2).ToString();//kitapaid
                comboBox1.Text = veriokuyucu.GetValue(3).ToString();//adet
                dateTimePicker1.Text = veriokuyucu.GetValue(4).ToString(); //tarih
                dateTimePicker2.Text = veriokuyucu.GetValue(5).ToString();//saat
            }
            bağlantı.Close();//4-bağlantı kapat
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //VERİ EKLEME İŞLEMİ
            string sql = "insert into sepet values(";
            sql += "'" + textBox1.Text + "',";//sepetid
            sql += textBox2.Text + ",";//müşeteriid
            sql += textBox3.Text + ",";//kitapid
            sql += comboBox1.Text + ",";//adet
            sql += "'" + dateTimePicker1.Text + "',";//tarih
            sql += "'" + dateTimePicker2.Text + "')";//saat

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Ekleme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + "IDli müşteri " +
            textBox2.Text.Replace("'", "''") + " " +
            textBox3.Text.Replace("'", "''") + " IDli kitabı  Sepetetine  ekledi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma



            bağlantı.Close();//4-bağlantı close

            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //VERİ GÜNCELLE İŞLEMİ
            string sql = "update sepet set";
            sql += "kitapid=" + textBox3.Text + ",";//kitapid
            sql += "adet=" + comboBox1.Text + ",";//adet
            sql += "tarih='" + dateTimePicker1.Text + "',";//tarih
            sql += "saat='" + dateTimePicker2.Text + "' ";//saat
            sql += "where sepetid='" + textBox1.Text + "' and müşteriid=" + textBox2.Text;//sepetid ve müşeteriid kullanarak güncelleme yap

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Güncelleme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + " IDsi " +
            textBox1.Text + " olan Sepet bilgilerini güncelledi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma



            bağlantı.Close();//4-bağlantı close

            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //VERİ SİLME İŞLEMİ
            string sql = "delete from  sepet ";
            sql += "where sepetid='" + textBox1.Text + "' and müşteriid=" + textBox2.Text + " and kitapid=" + textBox3.Text;//sepetid,müşeteriid,kitapid kullanarak silme yap

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma


            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Silme',";//işlemtürü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid +
            " IDli " + bilgi.kullanıcıtürü + " IDsi " +
            textBox1.Text + " olan Sepet sildi.')";//açıklama
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma


            bağlantı.Close();//4-bağlantı close

            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button10_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            doldur("select * from sepet", dataGridView1);//sepet tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button14_Click(object sender, EventArgs e)
        {
            doldur("select müşteriid,ad,soyad,eposta,telefon from müşteri", dataGridView2);//müşteri tablosundaki tüm kayıtlar dataGridView2'de gösteriliyor
        }

        private void button15_Click(object sender, EventArgs e)
        {

            doldur("select * from kitap", dataGridView3);//kitap tablosundaki tüm kayıtlar dataGridView3'de gösteriliyor
        }


    }


}
