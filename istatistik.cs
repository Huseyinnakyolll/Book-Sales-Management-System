using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Kitap_Satış_Sistemi
{
    public partial class istatistik : Form
    {
        public istatistik()
        {
            InitializeComponent();
            this.MaximizeBox = false;  // Pencereyi büyütme butonunu devre dışı bırak
            this.MinimizeBox = false;  // Pencereyi küçültme butonunu devre dışı bırak
        }


        // Bağlantı dizesi
        private string connectionString = "Server=localhost;Database=veri;Uid=root;Pwd='';AllowUserVariables=True;UseCompression=True;";

        private void istatistik_Load(object sender, EventArgs e)
        {
            // Toplam kitap sayısını panelde göster
            lbkitapCount.Text = GetScalarValue("SELECT COUNT(*) FROM kitap").ToString();

            // Son eklenen kitap adı
            lblLastkitapadıName.Text = GetScalarValue("SELECT kitapadı FROM kitap ORDER BY kitapid DESC LIMIT 1").ToString();

            // En pahalı kitap fiyatını panelde göster
            lbMaxPricekitap.Text = Convert.ToDecimal(GetScalarValue("SELECT MAX(Fiyat) FROM kitap")).ToString("N0") + " ₺";

            // Müşteri sayısını panelde göster
            lblCountmüşteri.Text = GetScalarValue("SELECT COUNT(*) FROM müşteri").ToString();

            // Yönetici sayısını panelde göster
            lblyöneticiCount.Text = GetScalarValue("SELECT COUNT(*) FROM yönetici").ToString();

            //yazar sayısı
            lblSumyazar.Text = GetScalarValue(" SELECT COUNT(DISTINCT yazar) FROM kitap;").ToString();

            //ortalam kitap fiyatı
            lblAvgkitapPrice.Text =Convert.ToDecimal(GetScalarValue("SELECT AVG(fiyat) FROM kitap")).ToString("N0") + " ₺";
            //talep sayısı
            lbltalepCount.Text = GetScalarValue("Select COUNT(*) From talep;").ToString();

            //Fiyatı en yüksek kitap
            lblMaxPriceNamekitap.Text = GetScalarValue("SELECT kitapadı FROM kitap ORDER BY fiyat DESC LIMIT 1 ").ToString();


        }

        private object GetScalarValue(string query)
        {
            object result = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını loglamak için veya başka bir işlem yapmak için
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            anamenü frmanenü = new anamenü();
            frmanenü.Show();
            this.Hide();
        }
    }
}
