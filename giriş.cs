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
            //E-POSTA ŞİFRE KONTROLÜ
            string sql = "select * from yönetici where eposta='" + textBox1.Text+"' and şifre='"+textBox2.Text+"'";
            bağlantı.Open();//1-bağlantı aç
            komut=new MySqlCommand(sql,bağlantı);//2-bağlnatı tanımla
            veriokuyucu=komut.ExecuteReader();//3-komut çalıştırma
            if (veriokuyucu.Read())//böyle bir kayıt var mı? //YÖNETİCİ TESTİ
            {
                bilgi.kullanıcıtürü = "yönetici";//kullanıcı türü
                bilgi.kullanıcıid=veriokuyucu.GetValue(0).ToString();//yöneticiid
                bağlantı.Close();//4-bağlantı kapat
                //2-FORMLAR ARASI GEÇİŞ
                anamenü.Show();//form göster
                this.Hide();//(bu form-->giriş)form gizle  
            }
            else //MÜŞTERİ TESTİ
            {

                sql = "select * from müşteri where eposta='" + textBox1.Text + "' and şifre='" + textBox2.Text + "'";
                bağlantı.Open();//1-bağlantı aç
                komut = new MySqlCommand(sql, bağlantı);//2-bağlnatı tanımla
                veriokuyucu = komut.ExecuteReader();//3-komut çalıştırma

                if (veriokuyucu.Read())//böyle bir kayıt var mı? //MÜŞTERİ TESTİ
                {
                    bilgi.kullanıcıtürü = "müşteri";//kullanıcı türü
                    bilgi.kullanıcıid = veriokuyucu.GetValue(0).ToString();//müşteriid
                    bağlantı.Close();//4-bağlantı kapat
                    //2-FORMLAR ARASI GEÇİŞ
                    anamenü.Show();//form göster
                    this.Hide();//(bu form-->giriş)form gizle  
                }
                else
                {
                    
                    bağlantı.Close();//4-bağlantı kapat  
                }

                bağlantı.Close();//4-bağlantı kapat  
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
