using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;

namespace AESform
{
   public class AESşifrele
    {
        private const string AES_IV = @"!&+QWSDF!123126+";
        private string Key = @"QQsaw!257()%%ert";
        AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
        public void SifreleVeCoz()
        {
            try
            {
                Stopwatch st = new Stopwatch();
                st.Restart();
                st.Start();
                provider.BlockSize = 128;//bit
                provider.KeySize = 128; //bit

                provider.IV = Encoding.UTF8.GetBytes(AES_IV);//hangi türden karakterler kullanarak şifreleyecek
                provider.Key = Encoding.UTF8.GetBytes(Key);// anahtarı hangi türden karakterler kullanarak oluşturcak
                Console.WriteLine("Şifreleme modu tuşlayın 1=ECB 2=CBC şeçin");
                string mod = Console.ReadLine();
                if (mod == "1")
                {
                    provider.Mode = CipherMode.ECB;
                }
                else if (mod == "2")
                {
                    provider.Mode = CipherMode.CBC;
                }
                else
                {
                    Console.WriteLine("Mod bulunamadı");
                }
                //ecb modu
                if(mod=="1" || mod == "2")
                {
                    provider.Padding = PaddingMode.PKCS7;//doldurma modu blokları 

                    st.Stop();

                    MessageBox.Show(st.Elapsed.ToString(), "Performan Testi ECB", MessageBoxButtons.OK);

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message, "Uyarı-  SifreleVeCoz()", MessageBoxButtons.OK);
            }
           
        }
        public string Sifreleme(string metin)
        {

            SifreleVeCoz();
            byte[] kaynak = Encoding.UTF8.GetBytes(metin);//metinleri gönderecek 

            using (ICryptoTransform crypto = provider.CreateEncryptor()) //şifreleme yapacağımız alan
            {
                byte[] hedef = crypto.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Convert.ToBase64String(hedef);
            }
        }

        public string SifreCoz(string SifreliMetin)
        {
            SifreleVeCoz();
            byte[] kaynak = System.Convert.FromBase64String(SifreliMetin);
            using (ICryptoTransform decrypt = provider.CreateDecryptor()) //şifreli metini çöz
            {
                byte[] hedef = decrypt.TransformFinalBlock(kaynak, 0, kaynak.Length);
                return Encoding.UTF8.GetString(hedef);
            }
        }

    }


}
