using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemFırstOdev
{
    class Program
    {
        static void Main(string[] args)
        {
            string dosyaKayit = "ogrenciler.dat";

            while (true)
            {
                Console.WriteLine("1 - Kişi Kayıt Ekle");
                Console.WriteLine("2 - Listele");
                Console.WriteLine("3 - Kayıt Ara");
                Console.WriteLine("4 - Düzenle");
                Console.WriteLine("5 - Çıkış");
                Console.Write("Seçenek Girin: ");
                string secim = Console.ReadLine();

                if (secim == "1")
                {
                    Console.Write("Ad: ");
                    string ad = Console.ReadLine();

                    Console.Write("Soyad: ");
                    string soyad = Console.ReadLine();

                    Console.Write("Numara: ");
                    string numara = Console.ReadLine();

                    Console.Write("Bölüm: ");
                    string bolum = Console.ReadLine();

                    Console.Write("Cinsiyet: ");
                    string cinsiyet = Console.ReadLine();

                    Console.Write("Doğum Yeri: ");
                    string dogumYeri = Console.ReadLine();

                    Console.Write("Yaş: ");
                    string yas = Console.ReadLine();

                    Console.Write("Telefon Numarası: ");
                    string telefon = Console.ReadLine();

                    string[] yeniOgrenci = { ad, soyad, numara, bolum, cinsiyet, dogumYeri, yas, telefon };
                    string yeniOgrenciSatir = string.Join(",", yeniOgrenci);

                    using (StreamWriter yazici = File.AppendText(dosyaKayit))
                    {
                        yazici.WriteLine(yeniOgrenciSatir);
                    }

                    Console.WriteLine("Öğrenci Kaydedildi.");
                }
                else if (secim == "2")
                {
                    List<string[]> ogrenciler = ReadStudentsFromFile(dosyaKayit);
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("Ad   Soyad   Numara   Bölüm   Cinsiyet   DoğumYeri   Yaş   Telefon");
                    Console.WriteLine("-------------------------------------------------------------");
                    foreach (string[] ogrenci in ogrenciler)
                    {
                        Console.WriteLine(string.Join("\t", ogrenci));
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("");
                }
                else if (secim == "3")
                {
                    Console.Write("Aranacak Kriteri Girin: ");
                    string kriterAra = Console.ReadLine();

                    List<string[]> ogrenciler = ReadStudentsFromFile(dosyaKayit);
                    List<string[]> eslesenOgrenci = new List<string[]>();

                    foreach (string[] ogrenci in ogrenciler)
                    {
                        if (Array.IndexOf(ogrenci, kriterAra) != -1)
                        {
                            eslesenOgrenci.Add(ogrenci);
                        }
                    }

                    if (eslesenOgrenci.Count == 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Bunu İçeren Öğrenci Bulunamadı");
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine("Ad   Soyad   Numara   Bölüm   Cinsiyet   DoğumYeri   Yaş   Telefon");
                        Console.WriteLine("-------------------------------------------------------------");

                        foreach (string[] ogrenci in eslesenOgrenci)
                        {
                            Console.WriteLine(string.Join("\t", ogrenci));
                        }
                        Console.WriteLine("-------------------------------------------------------------");
                    }
                }
                else if (secim == "4")
                {
                    Console.Write("Düzenlenecek Öğrencinin Numarasını Girin: ");
                    string numara = Console.ReadLine();

                    List<string[]> ogrenciler = ReadStudentsFromFile(dosyaKayit);
                    int ogrenciIndis = -1;

                    for (int i = 0; i < ogrenciler.Count; i++)
                    {
                        if (ogrenciler[i][2] == numara)
                        {
                            ogrenciIndis = i;
                            break;
                        }
                    }

                    if (ogrenciIndis == -1)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Bu Numaraya Sahip Öğrenci Bulunamadı");
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.Write("Ad: ");
                        ogrenciler[ogrenciIndis][0] = Console.ReadLine();
                        Console.Write("Soyad: ");
                        ogrenciler[ogrenciIndis][1] = Console.ReadLine();

                        Console.Write("Numara: ");
                        ogrenciler[ogrenciIndis][2] = Console.ReadLine();

                        Console.Write("Bölüm: ");
                        ogrenciler[ogrenciIndis][3] = Console.ReadLine();

                        Console.Write("Cinsiyet: ");
                        ogrenciler[ogrenciIndis][4] = Console.ReadLine();

                        Console.Write("Doğum Yeri: ");
                        ogrenciler[ogrenciIndis][5] = Console.ReadLine();

                        Console.Write("Yaş: ");
                        ogrenciler[ogrenciIndis][6] = Console.ReadLine();

                        Console.Write("Telefon Numarası: ");
                        ogrenciler[ogrenciIndis][7] = Console.ReadLine();

                        WriteStudentsToFile(dosyaKayit, ogrenciler);

                        Console.WriteLine("Öğrenci Bilgileri Güncellendi.");
                    }
                }
                else if (secim == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz Seçenek.");
                }
            }
        }


        #region Okuma
        static List<string[]> ReadStudentsFromFile(string dosyKayit)
        {
            List<string[]> ogrenciler = new List<string[]>();

            try
            {
                using (StreamReader okuyucu = new StreamReader(dosyKayit))
                {
                    while (!okuyucu.EndOfStream)
                    {
                        string[] ogrenciSatiri = okuyucu.ReadLine().Split(',');
                        ogrenciler.Add(ogrenciSatiri);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Listelenecek Kayıtlı Kullanıcı Yok");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("");
            }
            return ogrenciler;
        }
        #endregion


        #region Yazma
        static void WriteStudentsToFile(string dosyaKayit, List<string[]> ogrenciler)
        {
            using (StreamWriter yazici = new StreamWriter(dosyaKayit))
            {
                foreach (string[] ogrenci in ogrenciler)
                {
                    string ogrenciSatiri = string.Join(",", ogrenci);
                    yazici.WriteLine(ogrenciSatiri);
                }
            }
        }
        #endregion

    }
}