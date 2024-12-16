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
    public partial class kitap : Form
    {
        public kitap()
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
            "UseCompression=True");

        string resimadı="";//resim adı için

        public void doldur(string sql)
        {
            //DİNAMİK DATAGRIDVIEW DOLDURMA
            veritablosu.Clear();//veritablosu temizle

            bağlantı.Open();//1-bağlantı aç
            veritutucu = new MySqlDataAdapter(sql, bağlantı);//2-sql komutu çalıştır,tablodan gelen bilgiler veritutucu'da
            veritutucu.Fill(veritablosu);//3-veritutucudaki bilgiler veritablosu'na aktarıldı
            dataGridView1.DataSource = veritablosu;//4-tablodan gelen bilgiler dataGridView1'de gösteriliyor
            bağlantı.Close();//5-bağlantı kapat

            label15.Text = "Sisteme kayıtlı " + dataGridView1.RowCount + " adet kitap vardır.";
        }

        public void göster(int satırno )
        {// DATAGRIDVIEW TIKLANAN SATIRI GÖSTERME
            if (satırno >dataGridView1.RowCount - 2 || satırno < 0)//satır numaralıarı sınırlar içinde mi?
                return;

            textBox1.Text = dataGridView1.Rows[satırno].Cells[0].Value.ToString();//kitapid
            textBox2.Text = dataGridView1.Rows[satırno].Cells[1].Value.ToString();//kitapadı
            /*if (dataGridView1.Rows[satırno].Cells[2].Value.ToString() == "Var")
                radioButton1.Checked=true;
            else
                radioButton2.Checked = true;*/
            textBox3.Text = dataGridView1.Rows[satırno].Cells[2].Value.ToString();//yazar
            comboBox1.Text= dataGridView1.Rows[satırno].Cells[3].Value.ToString();//tür
            textBox4.Text = dataGridView1.Rows[satırno].Cells[4].Value.ToString();//özet
            textBox5.Text = dataGridView1.Rows[satırno].Cells[5].Value.ToString();//yayınevi
            comboBox2.Text = dataGridView1.Rows[satırno].Cells[6].Value.ToString();//basımyılı 
            textBox6.Text = dataGridView1.Rows[satırno].Cells[7].Value.ToString();//fiyat
            textBox7.Text = dataGridView1.Rows[satırno].Cells[8].Value.ToString();//sayfasayısı
            textBox8.Text = dataGridView1.Rows[satırno].Cells[9].Value.ToString();//ebat
            textBox9.Text = dataGridView1.Rows[satırno].Cells[10].Value.ToString();//isbn
            comboBox3.Text = dataGridView1.Rows[satırno].Cells[11].Value.ToString();//dil 
            textBox10.Text = dataGridView1.Rows[satırno].Cells[12].Value.ToString();//resim
            pictureBox1.Image = Image.FromFile(dataGridView1.Rows[satırno].Cells[12].Value.ToString());//resim gösterme

        }

        private void kitap_Load(object sender, EventArgs e)
        {
            //FORM TAM EKRAN YAPMA
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
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

        private void button1_Click(object sender, EventArgs e)
        {
            //VERİ GÖSTERME İŞLEMİ
            string sql = "select * from kitap where kitapid=" + textBox1.Text;

            bağlantı.Open();//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı);//2-komut tanımlama
            veriokuyucu=komut.ExecuteReader();//3-komut çalıştırıldı,tabloda gelen bilgiler veriokuyucuda
            if(veriokuyucu.Read())//tabloda böyle bir kayıt var mı?
            {
                textBox1.Text = veriokuyucu.GetValue(0).ToString() ;//kitapid
                textBox2.Text = veriokuyucu.GetValue(1).ToString();//kitapadı
                textBox3.Text = veriokuyucu.GetValue(2).ToString();//yazar
                comboBox1.Text = veriokuyucu.GetValue(3).ToString();//tür
                textBox4.Text=veriokuyucu.GetValue(4).ToString();//özet
                /*
                if(veriokuyucu.GetValue(5).ToString()=="Evet")
                    radioButton1.Checked=true;
                else
                    radioButton2.Checked=true;*/
                textBox5.Text = veriokuyucu.GetValue(5).ToString();//yayınevi
                comboBox2.Text = veriokuyucu.GetValue(6).ToString();//basımyılı
                textBox6.Text = veriokuyucu.GetValue(7).ToString();//fiyat
                textBox7.Text = veriokuyucu.GetValue(8).ToString();//sayfasayısı
                textBox8.Text = veriokuyucu.GetValue(9).ToString();//ebat
                textBox9.Text = veriokuyucu.GetValue(10).ToString();//isbn
                comboBox3.Text = veriokuyucu.GetValue(11).ToString();//dil
                textBox10.Text = veriokuyucu.GetValue(12).ToString();//resim
                pictureBox1.Image = Image.FromFile(veriokuyucu.GetValue(12).ToString());//resim gösterme
            }
            bağlantı.Close();//4-bağlantı kapat


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //VERİ EKLEME İŞLEMİ
            string sql = "insert into kitap(kitapadı,yazar,tür,özet,yayınevi,basımyılı,fiyat,sayfasayısı,ebat,isbn,dil,resim) values(";
            sql += "'" + textBox2.Text.Replace("'", "''") + "',";//kitapadı
            sql += "'" + textBox3.Text.Replace("'", "''") + "',";//yazar
            sql += "'" + comboBox1.Text.Replace("'", "''") + "',";//tür
            sql += "'" + textBox4.Text.Replace("'", "''") + "',";//özet
            sql += "'" + textBox5.Text.Replace("'", "''") + "',";//yayınevi
            sql +=  comboBox2.Text+ ",";//basımyılı
            sql += textBox6.Text.Replace(",",".")+",";//fiyat
            sql +=  textBox7.Text+",";//sayfasayısı
            sql += "'" + textBox8.Text.Replace("'", "''") + "',";//ebat
            sql += "'" + textBox9.Text.Replace("'", "''") + "',";//isbn
            sql +="'"+ comboBox3.Text.Replace("'", "''") + "',";//dil
            sql += "'" + textBox10.Text.Replace("'", "''") + "')";//resim

            bağlantı.Open();//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Ekleme',";//İşlem Türü
            sql +="'" +DateTime.Now.ToString("yyyy-MM-dd")+"',";//tarih
            sql += "'"+DateTime.Now.ToString("HH:mm:ss")+"',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + " " +
                textBox2.Text.Replace("'","''")+ "adlı kitabı sisteme eklendi.')";//Açıklama

           komut=new MySqlCommand(sql,bağlantı);//Komut tanımlama
           komut.ExecuteNonQuery();//komut Çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //VERİ GÜNCELLEME İŞLEMİ
            string sql = "update kitap set ";
            sql += "kitapadı='" + textBox2.Text.Replace("'", "''") + "',";//kitapadı
            sql += "yazar='" + textBox3.Text.Replace("'", "''") + "',";//yazar
            sql += "tür='" + comboBox1.Text.Replace("'", "''") + "',";//tür
            sql += "özet='" + textBox4.Text.Replace("'", "''") + "',";//özet
            sql += "yayınevi='" + textBox5.Text.Replace("'", "''") + "',";//yayınevi
            sql += "basımyılı="+comboBox2.Text + ",";//basımyılı
            sql += "fiyat="+textBox6.Text.Replace(",", ".") + ",";//fiyat
            sql += "sayfasayısı="+textBox7.Text + ",";//sayfasayısı
            sql += "ebat='" + textBox8.Text.Replace("'", "''") + "',";//ebat
            sql += "isbn='" + textBox9.Text.Replace("'", "''") + "',";//isbn
            sql += "dil='" + comboBox3.Text.Replace("'", "''") + "',";//dil
            sql += "resim='" + textBox10.Text.Replace("'", "''") + "'";//resim
            sql += " where kitapid=" + textBox1.Text;//güncelleme kriteri kitapid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma

            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü +"',";//kullanıcıtürü
            sql += "'Kayıt Güncelleme',";//İşlem Türü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + " IDsi " +
                textBox1.Text+ " olan kitap bilgisini güncelledi.')";//Açıklama


            komut = new MySqlCommand(sql, bağlantı);//komut tanımlama
            komut.ExecuteNonQuery();

            bağlantı.Close();//4-bağlantı kapat
            doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //VERİ SİLME İŞLEMİ
            string sql = "delete from kitap where kitapid=" + textBox1.Text;//silme kriteri kitapid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut .ExecuteNonQuery();//3-komut çalıştırma
            bağlantı .Close();//4-bağlantı kapat


            //LOG TABLOSUNA İŞLEM KAYDETME
            sql = "insert into log(kullanıcıid,kullanıcıtürü,işlemtürü,tarih,saat,açıklama)values(";
            sql += bilgi.kullanıcıid + ",";//kullanıcıid
            sql += "'" + bilgi.kullanıcıtürü + "',";//kullanıcıtürü
            sql += "'Kayıt Silme',";//İşlem Türü
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";//tarih
            sql += "'" + DateTime.Now.ToString("HH:mm:ss") + "',";//saat
            sql += "'" + bilgi.kullanıcıid + "IDli" + bilgi.kullanıcıtürü + "IDsi" +
                textBox1.Text + " olan kitap Silindi.')";//Açıklama


            komut = new MySqlCommand(sql, bağlantı);//komut tanımlama
            komut.ExecuteNonQuery();




            doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim gösterme işlemi
            resimadı = DateTime.Now.ToString("yyyyMMddHHmss") + ".png";//20241017110705.png her açılan resim farklı isimde
            label13.Text = resimadı;
            openFileDialog1.ShowDialog();//openFileDialog1 göster
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);//resim pictureBox1'de gösteriliyor
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim kaydetme
            if (resimadı == "")//boş ise kaydetme
                return;
                pictureBox1.Image.Save("resim\\" + resimadı);//resim kaydetme -->resim\20241017110705.png
        }

        private void resimGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resim güncelleme
            if (resimadı == "" || textBox1.Text=="")//boş ise kaydetme
                return;

            //VERİ GÜNCELLEME İŞLEMİ
            string sql = "update kitap set resim='resim/" + resimadı + "'";//resim
            sql += " where kitapid=" + textBox1.Text;//güncelleme kriteri kitapid

            bağlantı.Open();//1-bağlantı aç
            komut = new MySqlCommand(sql, bağlantı);//2-komut tanımlama
            komut.ExecuteNonQuery();//3-komut çalıştırma
            bağlantı.Close();//4-bağlantı kapat

            doldur("select * from kitap");//kitap tablosundaki tüm kayıtlar dataGridView1'de gösteriliyor

        }

        private void button6_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Kitap arama işlemi

            string sql = "select * from kitap where kitapadı like '%" + textBox14.Text.Replace("'", "''") + "%'";
            sql+= "  and yazar like '%"+textBox13.Text.Replace("'","''") + "%'";
            sql +="  and yayınevi like '%" + textBox15.Text.Replace("'", "''") + "%'";
            sql += "  and özet like '%" + textBox12.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            //Kitap arama işlemi

            string sql = "select * from kitap where kitapadı like '%" + textBox14.Text.Replace("'", "''") + "%'";
            sql += "  and yazar like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and yayınevi like '%" + textBox15.Text.Replace("'", "''") + "%'";
            sql += "  and özet like '%" + textBox12.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //Kitap arama işlemi

            string sql = "select * from kitap where kitapadı like '%" + textBox14.Text.Replace("'", "''") + "%'";
            sql += "  and yazar like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and yayınevi like '%" + textBox15.Text.Replace("'", "''") + "%'";
            sql += "  and özet like '%" + textBox12.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            //Kitap arama işlemi

            string sql = "select * from kitap where kitapadı like '%" + textBox14.Text.Replace("'", "''") + "%'";
            sql += "  and yazar like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and yayınevi like '%" + textBox15.Text.Replace("'", "''") + "%'";
            sql += "  and özet like '%" + textBox12.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            //Kitap arama işlemi

            string sql = "select * from kitap where kitapadı like '%" + textBox14.Text.Replace("'", "''") + "%'";
            sql += "  and yazar like '%" + textBox13.Text.Replace("'", "''") + "%'";
            sql += "  and yayınevi like '%" + textBox15.Text.Replace("'", "''") + "%'";
            sql += "  and özet like '%" + textBox12.Text.Replace("'", "''") + "%'";
            doldur(sql);
        }
    }
}
