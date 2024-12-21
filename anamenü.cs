using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kitap_Satış_Sistemi
{
    public partial class anamenü : Form
    {
        public anamenü()
        {
            InitializeComponent();
            this.MaximizeBox = false;  // Pencereyi büyütme butonunu devre dışı bırak
            this.MinimizeBox = false;  // Pencereyi küçültme butonunu devre dışı bırak
        }

        //1-FORM TÜRETME
        kitap kitap = new kitap();
        müşteri müşteri=new müşteri();
        yönetici yönetici=new yönetici();
        sepet sepet=new sepet();
        sipariş sipariş=new sipariş();
        log log=new log();
        istatistik istatistik=new istatistik();

        private void anamenü_Load(object sender, EventArgs e)
        {
            label12.Text = "Kullanıcı Türü:"+bilgi.kullanıcıtürü;
            label11.Text = "Kullanıcı Adı: " + bilgi.kullanıcıadı; // Kullanıcı adını Label'a yazdır
        }

        public void göster(Form x)
        {
       
        }

        private void kitapİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(kitap);//kitap işlemleri formunu aç
          
        }

        private void müşteriİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(müşteri);//müşteri işlemleri formunu aç
           
        }

        private void YöneticiİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(yönetici);//yönetici işlemleri formunu aç
           
        }

        private void sepetİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(sepet);//sepet işlemleri formunu aç
         
        }

        private void siparişİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(sipariş);//sipariş işlemleri formunu aç
          
        }

        private void lOGİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(log);//log işlemleri formunu aç
          
        }

        private void istatistikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //3-FORM GÖSTER
            göster(istatistik);//istatistik  formunu aç
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
           kitap kitapfrm = new kitap();
            kitapfrm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            müşteri frmmüşteri = new müşteri();
            frmmüşteri.Show();
            this.Hide();
        }

        private void yöneticiİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kullanıcı türü kontrolü
            if (bilgi.kullanıcıtürü == "yönetici")
            {
                // Yönetici formunu göster
                göster(yönetici);
            }
            else
            {
                // Yetkisiz kullanıcıya mesaj göster
                MessageBox.Show("Yetkili değilsiniz. Bu işlemi yalnızca yöneticiler gerçekleştirebilir.", "Erişim Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
        private void button3_Click(object sender, EventArgs e)
        {
            // Kullanıcı türü kontrolü
            if (bilgi.kullanıcıtürü == "yönetici")
            {
                // Yönetici formunu göster
                yönetici frmyönetici = new yönetici();
                frmyönetici.Show();
                this.Hide();
            }
            else
            {
                // Yetkisiz kullanıcıya mesaj göster
                MessageBox.Show("Yetkili değilsiniz. Bu işlemi yalnızca yöneticiler gerçekleştirebilir.", "Erişim Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {

            sepet frmsepet = new sepet();
            frmsepet.Show();
            this.Hide();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            log frmlog = new log(); 
            frmlog.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sipariş frmsipariş = new sipariş();
            frmsipariş.Show();
           this.Hide(); 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            istatistik frmistatistik = new istatistik();    
            frmistatistik.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            yardım frmyardım = new yardım();
            frmyardım.Show();
            this.Hide();    
        }

        private void label12_Click(object sender, EventArgs e)
        {
           
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
