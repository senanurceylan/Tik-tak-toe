using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TikTakTok
{
    class Program
    {
        // 3x3'lük oyun tahtasını temsil eden iki boyutlu bir dizi oluşturduk.
        static char[,] board = { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
        static char currentPlayer = 'X'; // Oyun sırasındaki oyuncuyu tutmak için 'currentPlayer' değişkeni oluşturduk. Başlangıçta 'X' başlar.

        static void Main(string[] args)
        {
            int turns = 0;  // Toplam yapılan hamle sayısını takip etmek için turns değişkeni tanımladık.
            bool gameWon = false;  // Oyunun kazanılıp kazanılmadığını takip etmek için gameWon değişkeni tanımladık.

            // Oyun bitene veya 9 tur tamamlanana kadar devam eder.
            while (!gameWon && turns < 9)
            {
                Console.Clear();  // Her turdan önce tahtanın güncel halini göstermek için ekranı temizliyoruz.
                DrawBoard();  // Mevcut oyun tahtasını çiziyoruz.
                PlayerMove();  // Oyuncunun hamlesini alıyoruz.
                gameWon = CheckWin();  // Oyunun kazanılıp kazanılmadığını kontrol ediyoruz.
                turns++;  // Hamle sayısını bir arttırıyoruz.

                // Sadece kazanan olmadığı durumda oyuncuyu değiştirmek için kontrol ekliyoruz.
                if (!gameWon)
                {
                    SwitchPlayer();  // Oyuncu değiştiriyoruz, sıradaki oyuncuya geçiyoruz.
                }
            }

            Console.Clear();  // Oyunun sonunda tahtanın son halini göstermek için ekranı temizliyoruz.
            DrawBoard();  // Son hali ekrana çiziyoruz.
            if (gameWon)
            {
                Console.WriteLine($"Oyuncu {currentPlayer} kazandı!");  // Oyunu kazanan oyuncuyu ekrana yazdırıyoruz.
            }
            else
            {
                Console.WriteLine("Oyun berabere!");  // 9 tur sonunda kimse kazanamamışsa oyun berabere bitmiştir.
            }

            // Programın kapanmasını engellemek için son mesajı bekletiyoruz.
            Console.WriteLine("Çıkmak için bir tuşa basın...");
            Console.ReadKey(); // Bu satır, kullanıcı bir tuşa basana kadar programı açık tutar.
        }

        static void DrawBoard()
        {
            // Oyun tahtasını çizmek için bu metodu oluşturduk.
            Console.WriteLine(" {0} | {1} | {2} ", board[0, 0], board[0, 1], board[0, 2]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2} ", board[1, 0], board[1, 1], board[1, 2]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2} ", board[2, 0], board[2, 1], board[2, 2]);
        }

        static void PlayerMove()
        {
            int choice;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine($"Oyuncu {currentPlayer}, 1-9 arasında bir hücre seçin:");
                // Kullanıcıdan bir girdi alıyoruz ve bu girdinin geçerli olup olmadığını kontrol ediyoruz.
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 9)
                {
                    int row = (choice - 1) / 3;  // Seçilen hücrenin hangi satırda olduğunu buluyoruz.
                    int col = (choice - 1) % 3;  // Seçilen hücrenin hangi sütunda olduğunu buluyoruz.

                    // Seçilen hücrenin dolu olup olmadığını kontrol ediyoruz.
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        board[row, col] = currentPlayer;  // Hücre boşsa, mevcut oyuncunun simgesiyle dolduruyoruz.
                        validInput = true;  // Geçerli bir girdi alındı.
                    }
                    else
                    {
                        Console.WriteLine("Bu hücre zaten dolu. Lütfen başka bir hücre seçin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş. Lütfen 1-9 arasında bir sayı girin.");
                }
            }
        }

        static void SwitchPlayer()
        {
            // Oyuncu değişimini yapıyoruz. Eğer şu anki oyuncu 'X' ise 'O' yapıyoruz, eğer 'O' ise 'X' yapıyoruz.
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }

        static bool CheckWin()
        {
            // Kazananı kontrol etmek için satırları, sütunları ve çaprazları kontrol ediyoruz.
            // Satırları kontrol et
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                {
                    return true;  // Eğer bir satırdaki tüm hücreler aynı oyuncuya aitse, bu oyuncu kazanır.
                }
            }

            // Sütunları kontrol et
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                {
                    return true;  // Eğer bir sütundaki tüm hücreler aynı oyuncuya aitse, bu oyuncu kazanır.
                }
            }

            // Çaprazları kontrol et
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            {
                return true;  // Sol üstten sağ alta olan çaprazdaki tüm hücreler aynı oyuncuya aitse, bu oyuncu kazanır.
            }
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
            {
                return true;  // Sağ üstten sol alta olan çaprazdaki tüm hücreler aynı oyuncuya aitse, bu oyuncu kazanır.
            }

            return false;  // Hiçbir kazanan bulunmadıysa false dönüyoruz.
        }
    }
}